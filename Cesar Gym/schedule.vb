Imports System.Data.OleDb

Public Class schedule
    Dim exists As Boolean = False
    Dim lastFocusedTextBox As TextBox
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If exists = False Then
            Dim objcmd As New System.Data.OleDb.OleDbCommand

            Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
            con.Open()
            My.Settings.sched += 1
            Dim strSql = ("insert into schedule values ('" + My.Settings.sched.ToString + "', '" + Label1.Text + "', '" + Label2.Text + "', '" + TextBox1.Text + "', '" + TextBox2.Text + "', '" + TextBox3.Text + "', '" + TextBox4.Text + "', '" + TextBox5.Text + "', '" + TextBox6.Text + "')")
            objcmd = New OleDbCommand(strSql, con)
            objcmd.ExecuteNonQuery()
            con.Close()
            con.Dispose()
            MsgBox("Added")
            clear()
            Me.Close()
        ElseIf exists = True Then
            Dim queryy As String = "UPDATE [schedule] SET [Day1]=? , [Day2]=? , [Day3]=? , [Day4]=? , [Day5]=? , [Day6]=? WHERE [ClientId]=? "
            Using con = New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

                Using cmd = New OleDbCommand(queryy, con)
                    con.Open()
                    cmd.Parameters.AddWithValue("@p1", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@p2", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@p3", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@p4", TextBox4.Text)
                    cmd.Parameters.AddWithValue("@p5", TextBox5.Text)
                    cmd.Parameters.AddWithValue("@p6", TextBox6.Text)
                    


                    cmd.Parameters.AddWithValue("@p7", Label1.Text)
                    cmd.ExecuteNonQuery()
                    con.Close()
                    con.Dispose()
                    MsgBox("Edited")
                    clear()
                    Me.Close()
                End Using
            End Using

        End If
    End Sub

    Private Sub schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        Try
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT * from schedule", myConnToAccess)


            da.Fill(dt)
            DataGridView1.DataSource = dt
            myConnToAccess.Close()
            DataGridView1.Visible = False
            For Each row As DataGridViewRow In DataGridView1.Rows

                If row.Cells(1).Value.ToString = Label1.Text Then
                    exists = True
                    TextBox1.Text = row.Cells(3).Value.ToString
                    TextBox2.Text = row.Cells(4).Value.ToString
                    TextBox3.Text = row.Cells(5).Value.ToString
                    TextBox4.Text = row.Cells(6).Value.ToString
                    TextBox5.Text = row.Cells(7).Value.ToString
                    TextBox6.Text = row.Cells(8).Value.ToString
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()

    End Sub

    
    Private Sub TextBoxes_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter, TextBox2.Enter
        lastFocusedTextBox = DirectCast(sender, TextBox)
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click, Label14.Click, Label13.Click, Label12.Click, Label11.Click, Label10.Click, Label9.Click
        Dim lbl As Label = CType(sender, Label)
        Dim tex As String = lbl.Text
        If lastFocusedTextBox IsNot Nothing Then
            lastFocusedTextBox.Text += " " + lbl.Text
        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub
End Class