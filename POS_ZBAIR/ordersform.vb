Imports MySql.Data.MySqlClient
Public Class orderssform
    Dim totalcmd As Single
    Sub lister_orders()
        totalcmd = 0
        st = "SELECT * from orders"
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
    Sub rechercher_orders()
        totalcmd = 0
        st = "SELECT * from orders where num like '" & TextBox1.Text & "'"
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
    Sub rechercher_orders_by_date()
        totalcmd = 0
        If ComboBox1.Text <> Nothing Then
            st = "SELECT * from orders where user_id='" & ComboBox1.Text & "' and datecmd between '" & DateTimePicker1.Text & "' and '" & DateTimePicker2.Text & "'"
        Else
            st = "SELECT * from orders where  datecmd between '" & DateTimePicker1.Text & "' and '" & DateTimePicker2.Text & "'"
        End If

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
    Sub supprimer_orders()
        Try
            ouvrir()
            st = "DELETE from orders where num = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()

            st = "DELETE from orders_items where order_num = '" & TextBox1.Text & "'"
            cmd = New MySqlCommand(st, cn)
            cmd.ExecuteNonQuery()

            cn.Close()
            MessageBox.Show("La commande est bien supprimer ")
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

 
    Private Sub orderssform_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'lister_orders()
    End Sub



    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Try
            lister_orderitems(DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripButton3_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton3.Click
        'lister_orders()
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        rechercher_orders
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        rechercher_orders_by_date()
    End Sub

    Private Sub ToolStripButton6_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton6.Click
        If MessageBox.Show("Voulez vous supprimer commande ? ", "supprission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            supprimer_orders()
        End If
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As System.EventArgs) Handles ComboBox1.Click
        remplir_users()
    End Sub
End Class