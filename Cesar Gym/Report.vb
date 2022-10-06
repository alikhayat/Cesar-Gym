Public Class Report
    Dim clicked As Boolean = False
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
            con.Open()
            Dim myadapter As New OleDb.OleDbDataAdapter("select * from Subscription where ApplicantName like'" + TextBox1.Text + "%'", con)
            Dim ds As New DataSet
            myadapter.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            con.Dispose()
            calculate()
            Label6.Text = DataGridView1.Rows.Count.ToString
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try

    End Sub
    
    Private Sub Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        calculate()
        Label6.Text = DataGridView1.Rows.Count.ToString
    End Sub
    Private Sub calculate()
        Try
            Dim price As Integer = 0
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Dim pp As Integer = Convert.ToInt32(DataGridView1.Rows(i).Cells(7).Value.ToString)
                price = price + pp

                Label5.Text = price.ToString + "  L.L"

            Next
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clicked = True
        Search_Query.Show()
        Me.Close()
    End Sub

    Private Sub Report_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            Search_Query.Show()
        End If

    End Sub
End Class