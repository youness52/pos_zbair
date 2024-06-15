Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Public Class Form1
    Private WithEvents price As Label
    Private WithEvents pan As Panel
    Private WithEvents pic As PictureBox
    Private WithEvents opt As CheckBox
     Dim total As Single = 0
    Dim tablechoix As String
    Dim lastidorder As String
    Public cmdline2 As String
    Public stp As String()
    Sub lister_table()

        pan = New Panel
        With pan
            .Width = 100
            .Height = 100
            .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        End With
        price = New Label
        With price

            .TextAlign = ContentAlignment.MiddleLeft
            .Dock = System.Windows.Forms.DockStyle.Fill
            .TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        End With
        pic = New PictureBox
        With pic
            .Width = 100
            .Height = 75
            .SizeMode = PictureBoxSizeMode.StretchImage
            .Dock = System.Windows.Forms.DockStyle.Top
            .Image = My.Resources.delivery
        End With
        price.Text = "Emporter"
        pic.Tag = "Emporter"
        pan.Controls.Add(price)

        'pic.Image = Image.FromFile("D:\Logo3.png")
        pan.Controls.Add(pic)
        FlowLayoutPanel2.Controls.Add(pan)
        AddHandler price.Click, AddressOf choixtable
        AddHandler pic.Click, AddressOf choixtable


        st = "SELECT * from tables"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        While dr.Read
            pan = New Panel
            With pan
                .Width = 100
                .Height = 100
                .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            End With
            price = New Label
            With price

                .TextAlign = ContentAlignment.MiddleLeft
                .Dock = System.Windows.Forms.DockStyle.Fill
                .TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            End With
            pic = New PictureBox
            With pic
                .Width = 100
                .Height = 75
                .SizeMode = PictureBoxSizeMode.StretchImage
                .Dock = System.Windows.Forms.DockStyle.Top
                .Image = My.Resources.tablepng
            End With
            price.Text = dr(0).ToString
            pic.Tag = dr(0).ToString
            pan.Controls.Add(price)

            'pic.Image = Image.FromFile("D:\Logo3.png")
            pan.Controls.Add(pic)
            FlowLayoutPanel2.Controls.Add(pan)
            AddHandler price.Click, AddressOf choixtable
            AddHandler pic.Click, AddressOf choixtable
        End While
        cn.Close()
    End Sub
    Sub lister_category()
        st = "SELECT * from category"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        While dr.Read
            pan = New Panel
            With pan
                .Width = 100
                .Height = 130

                .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            End With
            price = New Label
            With price

                .TextAlign = ContentAlignment.MiddleLeft
                .Dock = System.Windows.Forms.DockStyle.Fill
                .TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            End With
            pic = New PictureBox
            With pic
                .Width = 100
                .Height = 90
                .SizeMode = PictureBoxSizeMode.StretchImage
                .Dock = System.Windows.Forms.DockStyle.Top
                If dr(1).ToString <> "" And System.IO.File.Exists("images\" & dr(1).ToString) Then
                    Using fs As New IO.FileStream("images\" & dr(1).ToString, IO.FileMode.Open, IO.FileAccess.Read)
                        .Image = Image.FromStream(fs)
                    End Using

                End If


            End With

            price.Text = dr(0).ToString
            pic.Tag = dr(0).ToString
            pan.Controls.Add(price)

            'pic.Image = Image.FromFile("D:\Logo3.png")
            pan.Controls.Add(pic)
            FlowLayoutPanel3.Controls.Add(pan)
            AddHandler price.Click, AddressOf remplir
            AddHandler pic.Click, AddressOf remplir
        End While
        cn.Close()
    End Sub
    Sub enregistrer_orders()
        st = "INSERT INTO `orders` (`num`, `user_id`, `table_id`, `datecmd`, `total`) VALUES (NULL, '" & usernamelogin & "', '" & tablechoix & "', now(), '" & total & "');"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        cn.Close()
        'MessageBox.Show("Le materiel est bien enregistré ")
    End Sub
    Sub enregistrer_orders_items()
        st = "SELECT max(num) from orders"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        lastidorder = cmd.ExecuteScalar.ToString
        cn.Close()
        Dim ordd As String
        ouvrir()
        For i As Integer = 0 To DataGridView1.Rows.Count - 2
            ordd = Nothing
            If Split(DataGridView1.Rows(i).Cells(0).Value.ToString()).Length > 2 Then
                ordd = Split(DataGridView1.Rows(i).Cells(0).Value.ToString(), ":")(1).ToString
            End If
            st = "INSERT INTO `orders_items` (`id`, `order_num`, `item`, `qty`, `price`, `opt`) VALUES (NULL, '" & lastidorder & "', '" & Trim(Split(DataGridView1.Rows(i).Cells(0).Value.ToString(), ":")(0).ToString) & "', '" & DataGridView1.Rows(i).Cells(1).Value.ToString & "', '" & DataGridView1.Rows(i).Cells(2).Value.ToString & "', '" & Trim(ordd) & "');"
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
        Next
        cn.Close()
        'MessageBox.Show("Le materiel est bien enregistré ")
    End Sub
    Private Sub prclick(ByVal sender As Object, ByVal e As EventArgs)
        cmdline2 = ""
        Button4.Enabled = False
        For Each c As Control In FlowLayoutPanel1.Controls
            c.BackColor = Color.Transparent
        Next
        sender.Parent.BackColor = System.Drawing.Color.LightGreen
        Dim bl As Boolean = False
        For i As Integer = 0 To DataGridView1.Rows.Count - 2
            If DataGridView1.Rows(i).Cells(0).Value.ToString & "#" & DataGridView1.Rows(i).Cells(2).Value.ToString = sender.text.ToString Then
                DataGridView1.Rows(i).Cells(1).Value = DataGridView1.Rows(i).Cells(1).Value + 1
                bl = True
            End If
        Next
        If bl = False Then
            stp = Split(sender.text, "#")
            If sender.Parent.tag.ToString = "" Then
                DataGridView1.Rows.Add(stp(0), 1, stp(1))
            End If
        End If

        totalcalcul()
        FlowLayoutPanel4.Controls.Clear()
        If sender.Parent.tag.ToString <> "" Then
            Panel7.Visible = True
            Dim stp2 As String()
            stp2 = Split(sender.Parent.tag.ToString, "#")
            For Each optt As String In stp2
                If optt <> Nothing Then
                    opt = New CheckBox
                    With opt
                        .Font = New Font("Arial", 13)
                        .Text = optt
                    End With
                    FlowLayoutPanel4.Controls.Add(opt)
                    AddHandler opt.CheckedChanged, AddressOf changeopt
                End If
            Next

        Else
            Panel7.Visible = False

        End If
    End Sub

    Private Sub picclick(ByVal sender As Object, ByVal e As EventArgs)
        cmdline2 = ""
        Button4.Enabled = False
        For Each c As Control In FlowLayoutPanel1.Controls
            c.BackColor = Color.Transparent
        Next
        sender.Parent.BackColor = System.Drawing.Color.LightGreen



        Dim bl As Boolean = False
        For i As Integer = 0 To DataGridView1.Rows.Count - 2
            If DataGridView1.Rows(i).Cells(0).Value.ToString & "#" & DataGridView1.Rows(i).Cells(2).Value.ToString = sender.tag.ToString Then
                DataGridView1.Rows(i).Cells(1).Value = DataGridView1.Rows(i).Cells(1).Value + 1
                bl = True
            End If
        Next
        If bl = False Then
            stp = Split(sender.tag, "#")
            If sender.Parent.tag.ToString = "" Then
                DataGridView1.Rows.Add(stp(0), 1, stp(1))
            End If
        End If
        totalcalcul()

        FlowLayoutPanel4.Controls.Clear()
        If sender.Parent.tag.ToString <> "" Then
            Panel7.Visible = True
            Dim stp2 As String()
            stp2 = Split(sender.Parent.tag.ToString, "#")
            For Each optt As String In stp2
                If optt <> Nothing Then
                    opt = New CheckBox
                    With opt
                        .Font = New Font("Arial", 13)
                        .Text = optt
                    End With
                    FlowLayoutPanel4.Controls.Add(opt)
                    AddHandler opt.CheckedChanged, AddressOf changeopt
                End If

            Next

        Else
            Panel7.Visible = False

        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Enabled = False
        Button4.Enabled = False
        '--------------------------------enable when end dev ---------------
        'checkserver()
        'If My.Settings.uuidnew <> criptagee() Then
        'activationform.ShowDialog()
        ' End If
        'ouvrir()
        '-----------------------------------------------------------

        login.ShowDialog()
        Label2.Text = usernamelogin
        lister_table()
        lister_category()

        If My.Settings.mobileapp = True Then
            posAPK.Show()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub remplir(ByVal sender As Object, ByVal e As EventArgs)
        Panel7.Visible = False
        Button4.Enabled = False
        For Each c As Control In FlowLayoutPanel3.Controls
            c.BackColor = Color.Transparent
        Next
        sender.Parent.BackColor = System.Drawing.Color.LightGreen
        FlowLayoutPanel1.Controls.Clear()
        Dim categoryName As String

        If sender.text = "" Then
            categoryName = sender.tag
        Else
            categoryName = sender.text
        End If
        st = "SELECT * from items where category='" & categoryName & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        While dr.Read
            
            pan = New Panel
            With pan
                .Width = 100
                .Height = 130
                .BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                .Tag = dr(5).ToString
            End With
            price = New Label
            With price
                .TextAlign = ContentAlignment.MiddleLeft
                .Dock = System.Windows.Forms.DockStyle.Fill
                .TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            End With
            pic = New PictureBox
            With pic
                .Width = 100
                .Height = 80
                .SizeMode = PictureBoxSizeMode.StretchImage
                .Dock = System.Windows.Forms.DockStyle.Top
                If dr(1).ToString <> "" And System.IO.File.Exists("images\" & dr(2).ToString) Then
                    Using fs As New IO.FileStream("images\" & dr(2).ToString, IO.FileMode.Open, IO.FileAccess.Read)
                        .Image = Image.FromStream(fs)
                    End Using
                End If
            End With
            price.Text = dr(0).ToString & vbCrLf & "#" & dr(1)
            pic.Tag = dr(0).ToString & vbCrLf & "#" & dr(1)
            pan.Controls.Add(price)

            'pic.Image = Image.FromFile("D:\Logo3.png")
            pan.Controls.Add(pic)
            FlowLayoutPanel1.Controls.Add(pan)

            AddHandler price.Click, AddressOf prclick
            AddHandler pic.Click, AddressOf picclick
        End While
        cn.Close()
    End Sub
    Sub totalcalcul()
        total = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 2
            total += DataGridView1.Rows(i).Cells(1).Value * DataGridView1.Rows(i).Cells(2).Value
        Next
        Label1.Text = "TOTAL :  " & total & " DH"
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            DataGridView1.Rows.RemoveAt(e.RowIndex)
            totalcalcul()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For Each c As Control In FlowLayoutPanel2.Controls
            c.BackColor = Color.Transparent
        Next
        tablechoix = ""
        Me.Enabled = False
        Label2.Text = Nothing
        login.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        enregistrer_orders()
        enregistrer_orders_items()
        changelongpaper()
        PPD.Document = PD
        PPD.ShowDialog()
        'PD.PrinterSettings.Copies = 2
        'PD.Print()  'Direct Print
        DataGridView1.Rows.Clear()
        Label1.Text = Nothing
        For Each c As Control In FlowLayoutPanel2.Controls
            c.BackColor = Color.Transparent
        Next
        tablechoix = ""
    End Sub

    Private Sub choixtable(ByVal sender As Object, ByVal e As EventArgs)
        For Each c As Control In FlowLayoutPanel2.Controls
            c.BackColor = Color.Transparent
        Next
        sender.Parent.BackColor = System.Drawing.Color.LightGreen
        If sender.tag = "" Then
            tablechoix = sender.text
        Else
            tablechoix = sender.tag
        End If
        'MsgBox(tablechoix.ToString)
    End Sub


    '----------------------------------------------++++++++++Print-------------------------------------------------------------------------
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer
    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
        rowcount = DataGridView1.Rows.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 240
    End Sub


    Private Sub PD_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 500) 'fixed size
        'pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)
        pagesetup.PrinterSettings.Copies = 2
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat

        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "***********************************************************"

        'range from top
        'logo

        If System.IO.File.Exists("images\logo.png") Then
            Using fs As New IO.FileStream("images\logo.png", IO.FileMode.Open, IO.FileAccess.Read)
                Dim logoImage As Image = Image.FromStream(fs)
                e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 35)
            End Using

        End If


        'e.Graphics.DrawImage(logoImage, 0, 250, 150, 50)
        'e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - logoImage.Width) / 2), CInt((e.PageBounds.Height - logoImage.Height) / 2), logoImage.Width, logoImage.Height)

        'e.Graphics.DrawString("Store :", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString(My.Settings.adresse.ToString, f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel : " & My.Settings.tele.ToString, f10, Brushes.Black, centermargin, 55, center)

        e.Graphics.DrawString("Nº : ", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 75)
        e.Graphics.DrawString(lastidorder, f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Serveur", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString(usernamelogin, f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString(Date.Now().ToString & "  - Table : " & tablechoix, f8, Brushes.Black, 0, 95)
        'DetailHeader
        e.Graphics.DrawString("QT", f8, Brushes.Black, 0, 110)
        e.Graphics.DrawString("Article", f8, Brushes.Black, 25, 110)
        e.Graphics.DrawString("Prix", f8, Brushes.Black, 180, 110, right)
        e.Graphics.DrawString("Total", f8, Brushes.Black, rightmargin, 110, right)
        '
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 120)

        Dim height As Integer 'DGV Position
        'Dim i As Long
        ' DataGridView1.AllowUserToAddRows = False
        'If DataGridView1.CurrentCell.Value Is Nothing Then
        '    Exit Sub
        'Else
        For row As Integer = 0 To DataGridView1.RowCount - 2
            height += 15
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(1).Value.ToString, f8, Brushes.Black, 0, 115 + height)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(0).Value.ToString, f8, Brushes.Black, 25, 115 + height)

            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(2).Value.ToString, f8, Brushes.Black, 180, 115 + height, right)

            'totalprice
            Dim totalprice As Single
            totalprice = DataGridView1.Rows(row).Cells(1).Value * DataGridView1.Rows(row).Cells(2).Value
            e.Graphics.DrawString(totalprice.ToString, f8, Brushes.Black, rightmargin, 115 + height, right)
            If Split(DataGridView1.Rows(row).Cells(0).Value.ToString(), ":").Length > 1 Then
                height += 15
            End If

        Next
        'End If

        Dim height2 As Integer
        height2 = 145 + height
        'sumprice() 'call sub
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total: " & total, f10b, Brushes.Black, rightmargin, 10 + height2, right)
        'Barcode
        Dim gbarcode As New MessagingToolkit.Barcode.BarcodeEncoder
        Try
            Dim barcodeimage As Image
            barcodeimage = New Bitmap(gbarcode.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, lastidorder))
            e.Graphics.DrawImage(barcodeimage, CInt((e.PageBounds.Width - 150) / 2), 35 + height2, 150, 35)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        e.Graphics.DrawString("~ " & My.Settings.message.ToString & " ~", f10, Brushes.Black, centermargin, 70 + height2, center)
        e.Graphics.DrawString("~ " & My.Settings.nom.ToString & " ~", f10, Brushes.Black, centermargin, 85 + height2, center)
        e.Graphics.DrawString("Wifi : " & My.Settings.wifi.ToString, f8, Brushes.Black, centermargin, 100 + height2, center)

    End Sub
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles btnsettings.Click
        settingsform.ShowDialog()
    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click
        serveurdayform.ShowDialog()
    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click
        serveurdayform.ShowDialog()
    End Sub


    Private Sub DataGridView1_RowStateChanged(sender As Object, e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles DataGridView1.RowStateChanged
        If DataGridView1.RowCount > 1 Then
            Button3.Enabled = True
        Else
            Button3.Enabled = False
        End If
    End Sub

    Private Sub changeopt(sender As Object, e As EventArgs)
        cmdline2 = "  "
        Button4.Enabled = False
        For Each c As CheckBox In FlowLayoutPanel4.Controls
            If c.Checked = True Then
                Button4.Enabled = True
                cmdline2 = cmdline2 & " - " & c.Text
            End If
        Next
    End Sub

    Private Sub Button4_Click_1(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        DataGridView1.Rows.Add(stp(0) & ": " & cmdline2.ToString, 1, stp(1))
        totalcalcul()
        cmdline2 = ""
        For Each c As Control In FlowLayoutPanel1.Controls
            c.BackColor = Color.Transparent
        Next
        Button4.Enabled = False
        Panel7.Visible = False

    End Sub

End Class
