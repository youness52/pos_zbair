Imports MySql.Data.MySqlClient
Public Class tableform

    Sub lister_table()
        st = "SELECT * from tables"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()
        While dr.Read
            DataGridView1.Rows.Add(dr(0))
        End While
        cn.Close()
    End Sub
    Sub remplir_table()
        st = "SELECT * from tables"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        While dr.Read
            ComboBox1.Items.Add(dr(0))
        End While
        cn.Close()
    End Sub

    Sub enregistrer_table()
        Try
            st = "INSERT INTO `tables` (`name`) VALUES ('" & ComboBox1.Text & "');"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("La table est bien enregistré ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub supprimer_table()
        Try
            st = "DELETE from tables where name = '" & ComboBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("La table est bien supprimer ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub modifier_table()
        Try

            st = " update tables set name='" & ComboBox1.Text & "' where name = '" & ComboBox1.Text & "'"
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
        lister_table()
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As System.EventArgs) Handles ComboBox1.Click
        remplir_table()
    End Sub



    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        If ComboBox1.Text <> Nothing Then
            enregistrer_table()
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        ComboBox1.Text = Nothing
        ComboBox1.Focus()
    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton3.Click
        lister_table()
    End Sub

    Private Sub ToolStripButton4_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton4.Click
        modifier_table()
    End Sub

    Private Sub ToolStripButton6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton6.Click
        If MessageBox.Show("Voulez vous supprimer ce table ? ", "supprission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            supprimer_table()
        End If

    End Sub

End Class