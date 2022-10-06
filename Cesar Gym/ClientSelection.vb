Imports System.Data.OleDb

Public Class ClientSelection
    Dim rs As New Resizer

    Dim data As String = ""
    Dim clicked As Boolean = False
    Private Sub ClientSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rs.FindAllControls(Me)
        TextBox1.TabStop = True
        TextBox1.TabIndex = 0
        fill()
        Button1.Visible = False
        RadioButton1.Checked = False
        RadioButton2.Checked = True
        Button2.Visible = True
    End Sub
    Private Sub ClientSelection_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
    Private Sub fill()
        Try
            If RadioButton1.Checked = False Then
                data = "Applicants"
            Else
                data = "Subscription"
            End If
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT * from " + data, myConnToAccess)


            da.Fill(dt)
            DataGridView1.DataSource = dt
            myConnToAccess.Close()
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim days As Integer = (Convert.ToDateTime(row.Cells(3).Value.ToString) - Today).Days - 1
                row.Cells(9).Value = days.ToString
            Next
            DataGridView1.Refresh()
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            Label3.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Dim parameter As String = ""
            If data = "Applicants" Then
                parameter = "Name"
            Else
                parameter = "ApplicantName"
            End If
            Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
            con.Open()
            Dim myadapter As New OleDb.OleDbDataAdapter("select * from " + data + " where " + parameter + " like'" + TextBox1.Text + "%'", con)
            Dim ds As New DataSet
            myadapter.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            con.Dispose()
        Catch ex As Exception

        End Try
        

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.mode = "renewal"

        clicked = True
        insert.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.mode = "edit"
        clicked = True
        insert.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Facebook.WebBrowser1.Navigate(DataGridView1.SelectedRows(0).Cells(6).Value.ToString)
            clicked = True
            Facebook.Show()
            Me.Close()

        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox1.Clear()
            RadioButton2.Checked = False
            Button1.Visible = True
            Button2.Visible = False
            Button5.Visible = True
            Button3.Visible = False
            Button6.Visible = True
            fill()
        End If
        
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            TextBox1.Clear()
            Button1.Visible = False
            Button2.Visible = True
            Button5.Visible = False
            Button3.Visible = True
            RadioButton1.Checked = False
            Button6.Visible = False
            fill()
        End If
        
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clicked = True
        AdminTab.Show()
        Me.Close()
    End Sub

    Private Sub ClientSelection_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            AdminTab.Show()
        End If

    End Sub

    
   
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If RadioButton1.Checked = True Then
            schedule.Label1.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
            schedule.Label2.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString
            schedule.ShowDialog()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If Not DataGridView1.SelectedRows.Count < 1 Then
                Dim applicant As String = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                Dim NewPass As String = InputBox("Input new password", "Input new password")
                If NewPass.Length <> 4 Then
                    MsgBox("Please enter a 4 digit password")

                Else
                    Dim query As String = "UPDATE [Subscription] SET [Password]=?  WHERE [Id]=? "
                    Using con = New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

                        Using cmd = New OleDbCommand(query, con)
                            con.Open()
                            cmd.Parameters.AddWithValue("@p1", NewPass)
                            cmd.Parameters.AddWithValue("@p2", applicant)

                            cmd.ExecuteNonQuery()
                            con.Close()
                            con.Dispose()
                            MsgBox("Changed")

                            Me.Close()
                        End Using
                    End Using
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub
End Class