Imports MySql.Data.MySqlClient
Public Class itemsform

    Sub lister_items()
        st = "SELECT * from items"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()
        While dr.Read
            DataGridView1.Rows.Add(dr(0), dr(1), dr(3), dr(5))
        End While
        cn.Close()
    End Sub
    Sub remplir_items()
        st = "SELECT * from items"
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
    Sub remplir_category()
        st = "SELECT * from category"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        ComboBox2.Items.Clear()
        While dr.Read
            ComboBox2.Items.Add(dr(0))
        End While
        cn.Close()
    End Sub
    Sub rechercher_items()
        st = "SELECT * from items where name = '" & ComboBox1.Text & "'"
        ouvrir()
        cmd = New MySqlCommand(st, cn)
        cmd.CommandType = CommandType.Text
        dr = cmd.ExecuteReader
        If dr.Read Then
            PictureBox1.Image = Nothing
            If dr(2).ToString <> "" And System.IO.File.Exists("images\" & dr(2).ToString) Then
                Using fs As New IO.FileStream("images\" & dr(2).ToString, IO.FileMode.Open, IO.FileAccess.Read)
                    PictureBox1.Image = Image.FromStream(fs)
                End Using

            End If
            NumericUpDown1.Value = dr(1).ToString
            ComboBox2.Text = dr(3)
            RichTextBox1.Lines = Split(dr(5).ToString, "#")
            
        End If
        cn.Close()
    End Sub
    Sub enregistrer_items()
        Dim ln As String = ""
        For Each iten As String In RichTextBox1.Lines
            ln = ln & iten & "#"
        Next
        Try
            st = "INSERT INTO `items` (`name`, `price`, `image`, `category`,`opt`) VALUES ('" & ComboBox1.Text & "', '" & NumericUpDown1.Value & "',  '" & ComboBox1.Text & ".png" & "','" & ComboBox2.Text & "','" & ln & "');"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("Le produit est bien enregistré ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub supprimer_items()
        Try
            st = "DELETE from items where name = '" & ComboBox1.Text & "'"
            ouvrir()
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()
            MessageBox.Show("Le produit est bien supprimer ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
    Sub modifier_items()
        Try
            Dim ln As String = ""
            For Each iten As String In RichTextBox1.Lines
                ln = ln & iten & "#"
            Next
            st = " update items set name='" & ComboBox1.Text & "', image='" & ComboBox1.Text & ".png',price='" & NumericUpDown1.Value.ToString & "',category='" & ComboBox2.Text & "',`opt`='" & ln & "'  where name = '" & ComboBox1.Text & "'"
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
        lister_items()
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As System.EventArgs) Handles ComboBox1.Click
        remplir_items()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        rechercher_items()
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Nothing

            PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName.ToString)
            System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath.ToString & "\images")
            System.IO.File.Copy(OpenFileDialog1.FileName.ToString, My.Application.Info.DirectoryPath.ToString & "\images\" & ComboBox1.Text & ".png", True)
        End If
    End Sub


    Private Sub ToolStripButton2_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton2.Click
        If ComboBox1.Text <> Nothing Then
            enregistrer_items()
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        ComboBox1.Text = Nothing
        PictureBox1.Image = Nothing
        ComboBox2.Text = Nothing
        NumericUpDown1.Value = Nothing
        ComboBox1.Focus()
    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton3.Click
        lister_items()
    End Sub

    Private Sub ToolStripButton4_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton4.Click
        modifier_items()
    End Sub

    Private Sub ToolStripButton6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton6.Click
        If MessageBox.Show("Voulez vous supprimer ce produit ? ", "supprission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            supprimer_items()
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        System.IO.File.Delete(My.Application.Info.DirectoryPath.ToString & "\images\" & ComboBox1.Text & ".png")
    End Sub

    Private Sub ComboBox2_Click(sender As Object, e As System.EventArgs) Handles ComboBox2.Click
        remplir_category()
    End Sub

   
End Class