Imports MySql.Data.MySqlClient
Imports System.Management
Public Class activationform
    
    Private Sub activationform_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TextBox1.Text = getuuid()
        'TextBox2.Text = criptagee()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If criptagee() = TextBox2.Text Then
            My.Settings.uuidnew = TextBox2.Text
            My.Settings.Save()
            My.Settings.Reload()
            Me.Close()
        Else

            MessageBox.Show("code d'activation Incorrect !!!", "ActivationError E007", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class