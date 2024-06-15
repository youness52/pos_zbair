Imports MySql.Data.MySqlClient
Public Class dbsettings
    Sub importdb()
        Try
            ouvrir()
            st = My.Resources.db.ToString
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Sub createdb()
        Try
            cn = New MySqlConnection(My.Settings.dbcn.ToString)
            cn.Open()
            st = "CREATE DATABASE IF NOT EXISTS `pos`;"
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If MessageBox.Show("Voulez vous impoter la base de donnee db : pos ? ", "db", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            createdb()
            importdb()
            MsgBox("DB est bien importer")
        End If

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        My.Settings.dbcn = TextBox1.Text
        My.Settings.Save()
        MsgBox("les infos est bien enregistrer")
    End Sub


    Private Sub dbsettings_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TextBox1.Text = My.Settings.dbcn.ToString
    End Sub
End Class