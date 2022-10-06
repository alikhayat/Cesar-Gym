Public Class Main
    Dim rs As New Resizer

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rs.FindAllControls(Me)
        If My.Settings.ID = 0 Then
            My.Settings.CatId = 4
            My.Settings.SubId = 60
            My.Settings.ClientId = 60
            My.Settings.LogId = 0
            My.Settings.sched = 0
            My.Settings.ID = 1
            My.Settings.Save()


        End If
    End Sub
    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
    Private Sub button1_MouseEnter(sender As Object, e As EventArgs) Handles button1.MouseEnter
        button1.BackColor = Color.Red


    End Sub
    Private Sub button1_MouseLeave(sender As Object, e As EventArgs) Handles button1.MouseLeave
        button1.BackColor = Color.Black
    End Sub


    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        Dim pass As String
        pass = InputBox("Enter password", "Enter password")

        If pass = My.Settings.pass Then
            AdminTab.Show()

        Else
            MsgBox("Wrong password")

        End If
    End Sub

    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim screen As Screen
        ''Show the form on second screen
        'screen = screen.AllScreens(1)
        'ClientGate.StartPosition = FormStartPosition.Manual
        'ClientGate.Location = screen.Bounds.Location + New Point(100, 100)
        ClientGate.Show()
    End Sub
End Class
