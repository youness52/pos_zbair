Imports MySql.Data.MySqlClient
Public Class categoryform

    Sub lister_category()
        st = "SELECT * from category"
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
    Sub remplir_category()
        st = "SELECT * from category"
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
    Sub rechercher_category()
        st = "SELECT * from category where name = '" & ComboBox1.Text & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        If dr.Read Then
            PictureBox1.Image = Nothing
            If dr(1).ToString <> "" And System.IO.File.Exists("images\" & dr(1).ToString) Then
                Using fs As New IO.FileStream("images\" & dr(1).ToString, IO.FileMode.Open, IO.FileAccess.Read)
                    PictureBox1.Image = Image.FromStream(fs)
                End Using
            End If
        End If
        cn.Close()
    End Sub
    Sub enregistrer_category()
        Try
            st = "INSERT INTO `category` (`name`, `avatar`) VALUES ('" & ComboBox1.Text & "', '" & ComboBox1.Text & ".png" & "');"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("Le categorie est bien enregistré ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub supprimer_category()
        Try
            st = "DELETE from category where name = '" & ComboBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("Le categorie est bien supprimer ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub modifier_category()
        Try

            st = " update category set name='" & ComboBox1.Text & "', avatar='" & ComboBox1.Text & ".png' where name = '" & ComboBox1.Text & "'"
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
        lister_category()
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As System.EventArgs) Handles ComboBox1.Click
        remplir_category()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        rechercher_category()
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = My.Resources.logo
            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName.ToString)
            System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath.ToString & "\images")
            System.IO.File.Copy(OpenFileDialog1.FileName.ToString, My.Application.Info.DirectoryPath.ToString & "\images\" & ComboBox1.Text & ".png", True)
        End If
    End Sub


    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        If ComboBox1.Text <> Nothing Then
            enregistrer_category()
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        ComboBox1.Text = Nothing
        PictureBox1.Image = Nothing
        ComboBox1.Focus()
    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton3.Click
        lister_category()
    End Sub

    Private Sub ToolStripButton4_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton4.Click
        modifier_category()
    End Sub

    Private Sub ToolStripButton6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton6.Click
        If MessageBox.Show("Voulez vous supprimer ce categorie ? ", "supprission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            supprimer_category()
        End If

    End Sub

   
End Class