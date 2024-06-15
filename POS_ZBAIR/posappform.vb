Imports MySql.Data.MySqlClient
Imports System.IO.Compression
Public Class posappform


    
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

 
    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath.ToString & "\images")
            System.IO.File.Copy(OpenFileDialog1.FileName.ToString, My.Application.Info.DirectoryPath.ToString & "\images\logo.png", True)
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName.ToString)
        End If
    End Sub

    Private Sub posappform_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.nom
        TextBox2.Text = My.Settings.tele
        TextBox3.Text = My.Settings.wifi
        TextBox4.Text = My.Settings.message
        TextBox5.Text = My.Settings.adresse
        If System.IO.File.Exists("images\logo.png") Then
            Using fs As New IO.FileStream("images\logo.png", IO.FileMode.Open, IO.FileAccess.Read)
                PictureBox1.Image = Image.FromStream(fs)
            End Using
        End If
        If My.Settings.mobileapp = True Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        My.Settings.nom = TextBox1.Text
        My.Settings.tele = TextBox2.Text
        My.Settings.wifi = TextBox3.Text
        My.Settings.message = TextBox4.Text
        My.Settings.adresse = TextBox5.Text
        If RadioButton1.Checked = True Then
            My.Settings.mobileapp = True
        Else
            My.Settings.mobileapp = False
        End If
        My.Settings.Save()
        My.Settings.Reload()
        MsgBox("les infos est bien enregistrer")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            My.Computer.FileSystem.CopyDirectory("posapk", "C:\xampp\htdocs\pos", True)
            reloadimages()
            MsgBox("L'application mobile est bien ajouter a serveur locale")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class