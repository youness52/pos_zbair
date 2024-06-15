Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Public Class serveurdayform
    Dim totalcmd As Single
    Sub lister_orders()
        totalcmd = 0
        st = "SELECT * from orders where user_id ='" & usernamelogin & "' and datecmd > '" & DateTimePicker1.Text & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()
        While dr.Read
            DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4))
            totalcmd += Val(dr(4))
        End While
        lbtotal.Text = totalcmd
        cn.Close()
    End Sub
    Sub remplir_orders()
        totalcmd = 0
        st = "SELECT * from orders where user_id ='" & usernamelogin & "' and datecmd > '" & DateTimePicker1.Text & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        TextBox1.Items.Clear()
        While dr.Read
            TextBox1.Items.Add(dr(0))
        End While

        cn.Close()
    End Sub
    Sub rechercher_orders()
        totalcmd = 0
        st = "SELECT * from orders where num like '%" & TextBox1.Text & "%' and user_id ='" & usernamelogin & "' and datecmd > '" & lastdate & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()
        While dr.Read
            DataGridView1.Rows.Add(dr(0), dr(1), dr(2), dr(3), dr(4))
            totalcmd += Val(dr(4))
        End While
        lbtotal.Text = totalcmd
        cn.Close()
    End Sub
    
    Sub lister_orderitems(num As String)
        st = "SELECT * from orders_items where order_num = '" & num & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView2.Rows.Clear()
        While dr.Read
            DataGridView2.Rows.Add(dr(2), dr(3), dr(4))
        End While
        cn.Close()
    End Sub
    Sub modifier_lastdate()
        Try
            st = " update users set datefin=now() where name = '" & usernamelogin & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            'MessageBox.Show("Les modifications sont bien éffectuées ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
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
        Dim logoImage As Image = My.Resources.ResourceManager.GetObject("logo")
        e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 35)

        'e.Graphics.DrawImage(logoImage, 0, 250, 150, 50)
        'e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - logoImage.Width) / 2), CInt((e.PageBounds.Height - logoImage.Height) / 2), logoImage.Width, logoImage.Height)

        'e.Graphics.DrawString("Store :", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("18 GR G R11 Ksar El Kebir", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel : 0622668517", f10, Brushes.Black, centermargin, 55, center)


        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 75)
        e.Graphics.DrawString("", f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Serveur", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString(usernamelogin, f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString(Date.Now().ToString, f8, Brushes.Black, 0, 95)
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
        st = "SELECT orders_items.item, SUM(orders_items.qty), orders_items.price FROM orders_items WHERE orders_items.order_num IN( SELECT num FROM orders WHERE orders.user_id = '" & usernamelogin & "' and  datecmd > '" & DateTimePicker1.Text & "') GROUP BY orders_items.item,orders_items.price;"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        Dim totalpriceall As Single = 0

        While dr.Read
            height += 15
            e.Graphics.DrawString(dr(1).ToString, f8, Brushes.Black, 0, 115 + height)
            e.Graphics.DrawString(dr(0).ToString, f8, Brushes.Black, 25, 115 + height)

            e.Graphics.DrawString(dr(2).ToString, f8, Brushes.Black, 180, 115 + height, right)

            'totalprice
            Dim totalprice As Single = 0

            totalprice = dr(2) * dr(1)
            totalpriceall += totalprice
            e.Graphics.DrawString(totalprice.ToString, f8, Brushes.Black, rightmargin, 115 + height, right)
        End While

        cn.Close()

        Dim height2 As Integer
        height2 = 145 + height
        'sumprice() 'call sub
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total: " & totalpriceall, f10b, Brushes.Black, rightmargin, 10 + height2, right)
        'Barcode
        'Dim gbarcode As New MessagingToolkit.Barcode.BarcodeEncoder
        'Try
        'Dim barcodeimage As Image
        'barcodeimage = New Bitmap(gbarcode.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, "0000"))
        ' e.Graphics.DrawImage(barcodeimage, CInt((e.PageBounds.Width - 150) / 2), 35 + height2, 150, 35)
        ' Catch ex As Exception
        'MsgBox(ex.Message)
        '  End Try

        e.Graphics.DrawString("~ cafe name ~", f10, Brushes.Black, centermargin, 85 + height2, center)


    End Sub
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    Private Sub orderssform_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Text = lastdate
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        ' lister_orders()
    End Sub



    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            lister_orderitems(DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs)
        lister_orders()
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As System.EventArgs) Handles TextBox1.Click
        remplir_orders()
    End Sub

 

    Private Sub TextBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.SelectedIndexChanged
        rechercher_orders()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox1.TextChanged
        rechercher_orders()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If MessageBox.Show("Voulez vous fermer jour ? ", "confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            changelongpaper()
            PPD.Document = PD
            PPD.ShowDialog()
            modifier_lastdate()
            Form1.Enabled = False
            Form1.Label2.Text = Nothing
            Me.Close()
            login.ShowDialog()

        End If
    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub


    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        lister_orders()
    End Sub
End Class