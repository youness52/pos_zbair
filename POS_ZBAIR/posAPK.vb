Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient
Public Class posAPK
    Public newnum As String
    Public totalcmdd As Single
    '----------------------------------------------++++++++++Print-------------------------------------------------------------------------
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer
    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
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

        st = "SELECT * from orders where num = '" & newnum & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        If dr.Read Then
            totalcmdd = dr(4)


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
            e.Graphics.DrawString(newnum, f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Serveur", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
            e.Graphics.DrawString(dr(1).ToString, f8, Brushes.Black, 70, 85)

            e.Graphics.DrawString(Date.Now().ToString & "  - Table : " & dr(2).ToString, f8, Brushes.Black, 0, 95)

        End If
        cn.Close()
        'DetailHeader
        e.Graphics.DrawString("QT", f8, Brushes.Black, 0, 110)
        e.Graphics.DrawString("Article", f8, Brushes.Black, 25, 110)
        e.Graphics.DrawString("Prix", f8, Brushes.Black, 180, 110, right)
        e.Graphics.DrawString("Total", f8, Brushes.Black, rightmargin, 110, right)
        '
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 120)

        Dim height As Integer 'DGV Position




        st = "SELECT * from orders_items where order_num = '" & newnum & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader

        While dr.Read

            height += 15
            e.Graphics.DrawString(dr(3).ToString, f8, Brushes.Black, 0, 115 + height)

            If dr(5).ToString <> "" Then
                e.Graphics.DrawString(dr(5).ToString, f8, Brushes.Black, 30, 115 + height + 15)
            End If


            e.Graphics.DrawString(dr(2).ToString, f8, Brushes.Black, 25, 115 + height)

            e.Graphics.DrawString(dr(4).ToString, f8, Brushes.Black, 180, 115 + height, right)

            'totalprice
            Dim totalprice As Single = 0
            totalprice = Val(dr(3)) * Val(dr(4))
            e.Graphics.DrawString(totalprice.ToString, f8, Brushes.Black, rightmargin, 115 + height, right)
            If dr(5).ToString <> "" Then
                height += 15
            End If
        End While
        cn.Close()

        Dim height2 As Integer
        height2 = 145 + height
        'sumprice() 'call sub
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total: " & totalcmdd, f10b, Brushes.Black, rightmargin, 10 + height2, right)
        'Barcode
        Dim gbarcode As New MessagingToolkit.Barcode.BarcodeEncoder
        Try
            Dim barcodeimage As Image
            barcodeimage = New Bitmap(gbarcode.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, newnum))
            e.Graphics.DrawImage(barcodeimage, CInt((e.PageBounds.Width - 150) / 2), 35 + height2, 150, 35)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        e.Graphics.DrawString("~ " & My.Settings.message.ToString & " ~", f10, Brushes.Black, centermargin, 70 + height2, center)
        e.Graphics.DrawString("~ " & My.Settings.nom.ToString & " ~", f10, Brushes.Black, centermargin, 85 + height2, center)
        e.Graphics.DrawString("Wifi : " & My.Settings.wifi.ToString, f8, Brushes.Black, centermargin, 100 + height2, center)

    End Sub
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Sub supprimerMobile()
        ouvrir()
        st = "DELETE from orders_mobile where id = '" & newnum & "'"
        cmd = New MySqlCommand(st, cn)
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub
    Private Sub posAPK_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        st = "SELECT * from orders_mobile"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        If dr.Read Then
            newnum = dr(0).ToString
            changelongpaper()
            PPD.Document = PD
            PPD.ShowDialog()
            supprimerMobile()
            'PD.Print()

        End If
        cn.Close()
    End Sub
End Class