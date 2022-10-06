Imports System.Data.OleDb
Imports System.IO.Ports

Public Class ClientPortal
    Dim closed As Boolean = False


    Private Sub ClientPortal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        fillb()
        Label21.Text = ""
        Label22.Text = ""
        Label23.Text = ""
        Label24.Text = ""
        Label25.Text = ""
        Label26.Text = ""
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(1).Value.ToString = Label13.Text Then
                    Label21.Text = row.Cells(3).Value.ToString
                    Label22.Text = row.Cells(4).Value.ToString
                    Label23.Text = row.Cells(5).Value.ToString
                    Label24.Text = row.Cells(6).Value.ToString
                    Label25.Text = row.Cells(7).Value.ToString
                    Label26.Text = row.Cells(8).Value.ToString
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
   
    
    Private Sub fillb()
        Try
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT * from Loggings", myConnToAccess)


            da.Fill(dt)
            DataGridView2.DataSource = dt

            Dim dtt = New DataTable()
            

            Dim daa = New OleDbDataAdapter("SELECT * from schedule", myConnToAccess)


            daa.Fill(dtt)
            DataGridView1.DataSource = dtt
            myConnToAccess.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim objcmd As New System.Data.OleDb.OleDbCommand

        Try
            Dim same As Boolean = False
            For Each roww As DataGridViewRow In DataGridView2.Rows
                Dim day As Date = Today.ToShortDateString
                If Label13.Text = roww.Cells(2).Value.ToString And roww.Cells(3).Value.ToString = day.ToString Then
                    same = True

                    Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() + "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                    con.Open()
                    My.Settings.LogId += 1

                    Dim strSql = ("insert into Loggings values ('" + My.Settings.LogId.ToString + "', '" + Label1.Text + "', '" + Label13.Text + "', '" + day.ToString("dd'/'MM'/'yyyy") + "', '" + Now.ToShortTimeString + "', '" + Label5.Text + "')")
                    objcmd = New OleDbCommand(strSql, con)
                    objcmd.ExecuteNonQuery()

                    con.Close()

                    ClientGate.Label1.ResetText()

                    'Dim screen As Screen
                    'screen = screen.AllScreens(1)
                    'ClientGate.StartPosition = FormStartPosition.Manual
                    'ClientGate.Location = screen.Bounds.Location + New Point(100, 100)
                    'ClientGate.Show()
                    'Me.Close()
                End If
            Next

            If same = False Then
                Dim dayy As Date = Today.ToShortDateString



                Dim conn As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                conn.Open()

                My.Settings.LogId += 1
                Dim strSqll = ("insert into Loggings values ('" + My.Settings.LogId.ToString + "', '" + Label1.Text + "', '" + Label13.Text + "', '" + dayy.ToString("dd'/'MM'/'yyyy") + "', '" + Now.ToShortTimeString + "', '" + Label5.Text + "')")
                objcmd = New OleDbCommand(strSqll, conn)
                objcmd.ExecuteNonQuery()


                Dim left As String = (Convert.ToInt32(Label9.Text) - 1).ToString
                Dim left1 As String = (Convert.ToInt32(Label11.Text) - 1).ToString
                Dim queryy As String = "UPDATE [Subscription] SET [DaysLeft]=? , [LogonsLeft]=?  WHERE [ID]=? "
                Using conn

                    Using cmd = New OleDbCommand(queryy, conn)

                        cmd.Parameters.AddWithValue("@p1", left)
                        cmd.Parameters.AddWithValue("@p2", left1)


                        cmd.Parameters.AddWithValue("@p3", Label14.Text)
                        cmd.ExecuteNonQuery()
                        conn.Close()

                        ClientGate.Label1.ResetText()


                    End Using
                End Using


            End If
            closed = True
            opendoorr()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        My.Settings.Save()





    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        closed = True
        Dim screen As Screen
        screen = screen.AllScreens(1)
        ClientGate.StartPosition = FormStartPosition.Manual
        ClientGate.Location = screen.Bounds.Location + New Point(100, 100)
        ClientGate.Show()
        Me.Close()

    End Sub

    Private Sub ClientPortal_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If closed = False Then
            Dim screen As Screen
            screen = screen.AllScreens(1)
            ClientGate.StartPosition = FormStartPosition.Manual
            ClientGate.Location = screen.Bounds.Location + New Point(100, 100)
            ClientGate.Show()
            Me.Close()
        End If
        
    End Sub
    Public Sub opendoor()
        Try
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
        Catch ex As Exception
            MsgBox("check port")
        End Try
        
    End Sub
    Public Sub closedoor()
        Try
            Dim mySerialPort1 As New SerialPort
            Dim close As Byte() = {&H55, &H1, &H11, &H0, &H0, &H0, &H1, &H68}

            With mySerialPort1
                .PortName = "Com3"  'use whatever name you need for your application
                .RtsEnable = True
                .DataBits = 8             'this is required for binary data
                .BaudRate = 9600      'use whatever speed is required
                .Parity = IO.Ports.Parity.None    'this is required for binary data
                .Open()
                mySerialPort1.Write(close, 0, close.Length)
                .Close()
            End With
        Catch ex As Exception
            MsgBox("check port")
        End Try
       
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Enabled = False
            closedoor()
            Dim screen As Screen
            screen = screen.AllScreens(1)
            ClientGate.StartPosition = FormStartPosition.Manual
            ClientGate.Location = screen.Bounds.Location + New Point(100, 100)
            ClientGate.Show()
            Me.Close()
            Timer1.Dispose()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub opendoorr()
        Try
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
            Timer1.Enabled = True
        Catch ex As Exception
            MsgBox("check port")
        End Try

    End Sub
End Class