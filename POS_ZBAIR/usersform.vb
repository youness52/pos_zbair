Imports MySql.Data.MySqlClient
Public Class usersform

    Sub lister_users()
        st = "SELECT * from users"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()
        While dr.Read
            DataGridView1.Rows.Add(dr(0), dr(1), dr(2))
        End While
        cn.Close()
    End Sub
    Sub remplir_users()
        st = "SELECT * from users"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        While dr.Read
            ComboBox1.Items.Add(dr(1))
        End While
        cn.Close()
    End Sub
    Sub rechercher_users()
        st = "SELECT * from users where name= '" & ComboBox1.Text & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        If dr.Read Then
            ComboBox2.Text = dr(2).ToString
            TextBox1.Text = dr(0).ToString
        End If
        cn.Close()
    End Sub

    Sub enregistrer_users()
        Try
            st = "INSERT INTO `users` (`name`,`pin`,`role`,`datefin`) VALUES ('" & UCase(ComboBox1.Text) & "','" & TextBox1.Text & "','" & ComboBox2.Text & "',now());"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("L'utilisateur est bien enregistré ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub supprimer_users()
        Try
            st = "DELETE from users where name = '" & ComboBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("L'utilisateur est bien supprimer ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub modifier_users()
        Try

            st = " update users set name='" & ComboBox1.Text & "',pin='" & TextBox1.Text & "',role='" & ComboBox2.Text & "' where name = '" & ComboBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("Les modifications sont bien éffectuées ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub categoryform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lister_users()
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As System.EventArgs) Handles ComboBox1.Click
        remplir_users()
    End Sub



    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        If ComboBox1.Text <> Nothing Then
            enregistrer_users()
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        ComboBox1.Text = Nothing
        ComboBox1.Focus()
    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton3.Click
        lister_users()
    End Sub

    Private Sub ToolStripButton4_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton4.Click
        modifier_users()
    End Sub

    Private Sub ToolStripButton6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton6.Click
        If MessageBox.Show("Voulez vous supprimer ce l'utilisateur ? ", "supprission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            supprimer_users()
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        rechercher_users()
    End Sub
End Class