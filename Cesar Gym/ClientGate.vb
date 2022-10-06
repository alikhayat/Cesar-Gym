Imports System.Data.OleDb

Public Class ClientGate
    Dim rs As New Resizer
    Public changed As Boolean = False
    Public code As String


    Private Sub ClientGate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        filla()
    End Sub
    Private Sub ClientGate_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub ClientGate_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button10.Click
        Try
            Dim button = TryCast(sender, Button)
            Dim c As Char = button.Text
            'If Label1.Text = "Enter Pin Code" Or Char.IsNumber(Label1.Text.Chars(Label1.Text.Length - 1)) = False Or Label1.Text = "" Then

            'Label1.ResetText()
            'changed = True
            'Label1.Text += c
            If Label1.Text.Length <> 4 Then
                Label1.Text += c
            End If

        Catch ex As Exception

        End Try

    End Sub

    

    

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            filla()
            Dim name As String
            Dim categories As String
            Dim subscribed As String
            Dim logins As String
            Dim id As String
            Dim days As Integer
            Dim subid As String

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Label1.Text = row.Cells(8).Value.ToString Then
                    categories = row.Cells(1).Value.ToString
                    name = row.Cells(5).Value.ToString
                    subscribed = row.Cells(2).Value.ToString
                    days = (Convert.ToDateTime(row.Cells(3).Value.ToString) - Today).Days
                    id = row.Cells(4).Value.ToString
                    subid = row.Cells(0).Value.ToString
                    logins = row.Cells(10).Value.ToString

                    Dim day As Date = Date.Today
                    Dim subs As Date = Convert.ToDateTime(row.Cells(2).Value.ToString)
                    Select Case days
                        Case Is <= 0
                            MsgBox("Dear " + name + " your subscription is over")
                            Label1.ResetText()
                            Exit For
                        Case 1
                            MsgBox("Dear " + name + " you have one 1 day left")
                        Case 2
                            MsgBox("Dear " + name + " you have one 2 days left")
                        Case 3
                            MsgBox("Dear " + name + " you have one 3 days left")

                    End Select
                    'If days <= 0 Then
                    '    MsgBox("Dear " + name + " your subscription is over")
                    '    Label1.ResetText()
                    '    Exit For
                    'End If

                    ClientPortal.Label1.Text = name
                    ClientPortal.Label3.Text = day.ToString("dd'/'MM'/'yyyy")
                    ClientPortal.Label5.Text = categories.Substring(0, categories.Length - 2)
                    ClientPortal.Label7.Text = subscribed
                    ClientPortal.Label9.Text = days.ToString
                    ClientPortal.Label11.Text = logins
                    ClientPortal.Label13.Text = id
                    ClientPortal.Label14.Text = subid
                    'Dim screen As Screen
                    'Show the form on second screen
                    'screen = screen.AllScreens(1)
                    'ClientPortal.StartPosition = FormStartPosition.Manual
                    'ClientPortal.Location = screen.Bounds.Location + New Point(100, 100)

                    ClientPortal.Show()
                    Me.Close()
                End If
            Next
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try












    End Sub
    Private Sub filla()
        Try
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT * from Subscription", myConnToAccess)


            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub
    
    
    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles Button11.Click
        Try

            Label1.Text = Label1.Text.Substring(0, Label1.Text.Length - 1)
            
        Catch ex As Exception

        End Try
    End Sub

    
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class