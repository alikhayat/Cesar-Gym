Imports System.Data.OleDb
Imports System.IO.Ports

Public Class AdminTab

    Private Sub AdminTab_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub AdminTab_Resize(sender As Object, e As EventArgs) Handles Me.Resize

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.mode = "insert"
        insert.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ClientSelection.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Search_Query.Show()
        Me.Visible = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Config.Show()
        Me.Visible = False
    End Sub



    Private Sub AdminTab_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Main.Show()
        Me.Visible = False
    End Sub




    Private Sub AdminTab_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                opendoor()
            ElseIf e.KeyCode = Keys.F2 Then
                closedoor()
            ElseIf e.KeyCode = Keys.F11 Then
                If InputBox("Enter password", "Enter password") = My.Settings.pass Then
                    Dim objcmd As New System.Data.OleDb.OleDbCommand
                    Dim objcmdd As New System.Data.OleDb.OleDbCommand
                    Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                    My.Settings.SubId += 1
                    Dim strSqll = ("insert into Subscription values ('" + My.Settings.SubId.ToString + "', '" + "BodyBuilding" + "', '" + Today.ToShortDateString + "', '" + Today.ToShortDateString + "', '" + "0" + "', '" + "Daily" + "', '" + "Daily" + "', '" + "5000" + "', '" + "Daily" + "', '" + "Daily" + "', '" + "Daily" + "')")
                    myConnToAccess.Open()
                    objcmdd = New OleDbCommand(strSqll, myConnToAccess)
                    objcmdd.ExecuteNonQuery()
                    My.Settings.LogId += 1
                    Dim strSql = ("insert into Loggings values ('" + My.Settings.LogId.ToString + "', '" + "Daily" + "', '" + "0" + "', '" + Today.ToShortDateString + "', '" + Now.ToShortTimeString + "', '" + "BodyBuilding" + "')")
                    objcmd = New OleDbCommand(strSql, myConnToAccess)
                    objcmd.ExecuteNonQuery()
                    myConnToAccess.Close()
                    myConnToAccess.Dispose()
                    opendoor()
                    Timer1.Enabled = True
                    MsgBox("Daily,BodyBuilding")
                End If
            ElseIf e.KeyCode = Keys.F12 Then
                If InputBox("Enter password", "Enter password") = My.Settings.pass Then
                    Dim objcmd As New System.Data.OleDb.OleDbCommand
                    Dim objcmdd As New System.Data.OleDb.OleDbCommand
                    Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                    My.Settings.SubId += 1
                    Dim strSqll = ("insert into Subscription values ('" + My.Settings.SubId.ToString + "', '" + "Zumba" + "', '" + Today.ToShortDateString + "', '" + Today.ToShortDateString + "', '" + "0" + "', '" + "Daily" + "', '" + "Daily" + "', '" + "10000" + "', '" + "Daily" + "', '" + "Daily" + "', '" + "Daily" + "')")
                    myConnToAccess.Open()
                    objcmdd = New OleDbCommand(strSqll, myConnToAccess)
                    objcmdd.ExecuteNonQuery()
                    My.Settings.LogId += 1
                    Dim strSql = ("insert into Loggings values ('" + My.Settings.LogId.ToString + "', '" + "Daily" + "', '" + "0" + "', '" + Today.ToShortDateString + "', '" + Now.ToShortTimeString + "', '" + "Zumba" + "')")
                    objcmd = New OleDbCommand(strSql, myConnToAccess)
                    objcmd.ExecuteNonQuery()
                    myConnToAccess.Close()
                    myConnToAccess.Dispose()
                    opendoor()
                    Timer1.Enabled = True
                    MsgBox("Daily,Zumba")
                End If
            End If
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        
    End Sub
    Public Sub opendoor()
        Dim mySerialPort As New SerialPort
        Dim open As Byte() = {&H55, &H1, &H12, &H0, &H0, &H0, &H1, &H69}
        With mySerialPort
            .PortName = "Com3"  'use whatever name you need for your application
            .RtsEnable = True
            .DataBits = 8             'this is required for binary data
            .BaudRate = 9600      'use whatever speed is required
            .Parity = IO.Ports.Parity.None    'this is required for binary data
            .Open()
            mySerialPort.Write(open, 0, open.Length)
            .Close()
        End With
    End Sub
    Public Sub closedoor()
        Dim mySerialPort As New SerialPort
        Dim close As Byte() = {&H55, &H1, &H11, &H0, &H0, &H0, &H1, &H68}

        With mySerialPort
            .PortName = "Com3"  'use whatever name you need for your application
            .RtsEnable = True
            .DataBits = 8             'this is required for binary data
            .BaudRate = 9600      'use whatever speed is required
            .Parity = IO.Ports.Parity.None    'this is required for binary data
            .Open()
            mySerialPort.Write(close, 0, close.Length)
            .Close()
        End With
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            closedoor()
            
        Catch ex As Exception

        End Try

    End Sub
End Class