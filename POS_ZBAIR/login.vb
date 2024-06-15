Imports MySql.Data.MySqlClient
Imports System.Management
Public Class login
    Sub userlogin()
        Try
            usernamelogin = ""
            lastdate = ""
            st = "SELECT * from users where pin = '" & TextBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            If dr.Read Then
                usernamelogin = dr(1)
                lastdate = dr(3).ToString
                Form1.Enabled = True
                TextBox1.Text = Nothing
                If dr(2).ToString = "admin" Then
                    Form1.btnsettings.Enabled = True
                Else
                    Form1.btnsettings.Enabled = False
                End If
                Me.Close()
            Else
                MessageBox.Show("PIN Incorrect !!!", "LoginError E001", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            cn.Close()
        Catch ex As Exception
            MsgBox("E002 login f E008" & ex.ToString)
        End Try

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub login_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Focus()
        TextBox1.Text &= "1"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Focus()
        TextBox1.Text &= "2"
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox1.Focus()
        TextBox1.Text &= "3"
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TextBox1.Focus()
        TextBox1.Text &= "4"
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        TextBox1.Focus()
        TextBox1.Text &= "5"
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        TextBox1.Focus()
        TextBox1.Text &= "6"
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        TextBox1.Focus()
        TextBox1.Text &= "7"
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        TextBox1.Focus()
        TextBox1.Text &= "8"
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        TextBox1.Focus()
        TextBox1.Text &= "9"
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click

        userlogin()
        Form1.Label2.Text = usernamelogin
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        TextBox1.Focus()
        TextBox1.Text &= "0"
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        TextBox1.Focus()
        If TextBox1.Text <> "" Then
            TextBox1.Text = Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 1)
        End If

    End Sub

    
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            userlogin()
            Form1.Label2.Text = usernamelogin
        End If
    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click
        MsgBox(getuuid)
    End Sub
End Class