Imports System.Data.OleDb

Public Class Config
    Dim rs As New Resizer
    Dim clicked As Boolean = False
    Private Sub Config_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        LineShape1.Visible = False
        LineShape2.Visible = False
        DataGridView1.Visible = False
        ComboBox1.Visible = False
        Button4.Visible = False
        TextBox1.Visible = False
        Label4.Visible = False
        Label5.Visible = False
    End Sub
    Private Sub Config_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clicked = True
        Categories.Show()
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim old As String = InputBox("Enter Old Password", "Enter Old Password")
        If old = My.Settings.pass Then
            Dim neww As String = InputBox("Enter new Password", "Enter new Password")
            My.Settings.pass = neww
            Me.Close()
            AdminTab.Close()
        Else
            MsgBox("wrong password")
            Me.Close()
            AdminTab.Close()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ds = New DataSet
        Dim tables = ds.Tables
        Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

        Try
            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT Name from Categories", myConnToAccess)


            da.Fill(ds, "Categories")
            Dim view1 As New DataView(tables(0))
            With ComboBox1
                .DataSource = ds.Tables("Categories")
                .DisplayMember = "Name"
                .ValueMember = "Name"


                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With
            myConnToAccess.Close()
            myConnToAccess.Dispose()
        Catch ex As Exception

        End Try
        LineShape1.Visible = True
        LineShape2.Visible = True
        Button4.Visible = True
        TextBox1.Visible = True
        Label4.Visible = True
        Label5.Visible = True
        ComboBox1.Visible = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If InputBox("Enter password", "Enter password") = My.Settings.pass And TextBox1.Text <> "" Then
                Dim ds = New DataSet
                Dim tables = ds.Tables
                Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                Dim da = New OleDbDataAdapter("SELECT * from Categories", myConnToAccess)
                myConnToAccess.Open()
                Dim dt = New DataTable()
                da.Fill(dt)
                DataGridView1.DataSource = dt
                Dim timeshift As String = ""
                Dim price As String = ""
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells(1).Value.ToString = ComboBox1.Text Then
                        timeshift = row.Cells(2).Value.ToString

                    End If
                Next
                Dim objcmd As New System.Data.OleDb.OleDbCommand
                Dim objcmdd As New System.Data.OleDb.OleDbCommand
                My.Settings.SubId += 1
                Dim strSqll = ("insert into Subscription values ('" + My.Settings.SubId.ToString + "', '" + ComboBox1.Text + "', '" + Today.ToShortDateString + "', '" + Today.ToShortDateString + "', '" + "0" + "', '" + "Daily" + "', '" + timeshift + "', '" + TextBox1.Text + "', '" + "Daily" + "', '" + "Daily" + "', '" + "Daily" + "')")

                objcmdd = New OleDbCommand(strSqll, myConnToAccess)
                objcmdd.ExecuteNonQuery()
                My.Settings.LogId += 1
                Dim strSql = ("insert into Loggings values ('" + My.Settings.LogId.ToString + "', '" + "Daily" + "', '" + "0" + "', '" + Today.ToShortDateString + "', '" + Now.ToShortTimeString + "', '" + ComboBox1.Text + "')")
                objcmd = New OleDbCommand(strSql, myConnToAccess)
                objcmd.ExecuteNonQuery()
                myConnToAccess.Close()
                myConnToAccess.Dispose()
            End If
            Me.Close()
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        clicked = True
        AdminTab.Show()
        Me.Close()
    End Sub

    Private Sub Config_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            AdminTab.Show()
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            ClientPortal.opendoor()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            ClientPortal.closedoor()
        Catch ex As Exception

        End Try
    End Sub
End Class