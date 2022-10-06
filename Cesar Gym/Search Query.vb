Imports System.Data.OleDb

Public Class Search_Query

    Public ds As New DataSet
    Public da As New DataSet
    Dim clicked As Boolean = False
    Private Sub Search_Query_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox1.Clear()
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
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        DateTimePicker2.CustomFormat = "dd/MM/yyyy"
        DateTimePicker3.CustomFormat = "dd/MM/yyyy"
        DateTimePicker4.CustomFormat = "dd/MM/yyyy"
    End Sub
    

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clicked = True
        Try
            Dim d1 As Date = Convert.ToDateTime(DateTimePicker1.Value.ToString)
            Dim d2 As Date = Convert.ToDateTime(DateTimePicker2.Value.ToString)
            If CheckBox1.Checked = False Then
                Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim myadapter As New OleDb.OleDbDataAdapter("SELECT * from subscription where Category like'" + ComboBox1.Text + "%' And StartDate Between #" & _
                                                d1.ToString("dd/MM/yyyy") & "# And #" & _
                                                d2.ToString("dd/MM/yyyy") & "#", con)
                ds.Clear()
                myadapter.Fill(ds)
                Report.DataGridView1.DataSource = ds.Tables(0)
                con.Close()
                con.Dispose()
                Report.Label3.Visible = True
                Report.Label5.Visible = True
                Report.Label4.Text = "- Subscriptions:"
                Me.Visible = False
                Report.ShowDialog()

                Report.DataGridView1.ClearSelection()
            Else
                Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim myadapter As New OleDb.OleDbDataAdapter("SELECT * from subscription where Category like'" + ComboBox1.Text + "%'", con)
                ds.Clear()
                myadapter.Fill(ds)
                Report.DataGridView1.DataSource = ds.Tables(0)
                con.Close()
                con.Dispose()
                Report.Label3.Visible = True
                Report.Label5.Visible = True
                Report.Label4.Text = "- Subscriptions:"
                CheckBox1.Checked = False
                Me.Visible = False
                Report.ShowDialog()

                Report.DataGridView1.ClearSelection()
            End If

        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        Else
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clicked = True
        Try
            Dim d1 As Date = Convert.ToDateTime(DateTimePicker3.Value.ToString)
            Dim d2 As Date = Convert.ToDateTime(DateTimePicker4.Value.ToString)
            Dim d3 As Date = Convert.ToDateTime(DateTimePicker5.Value.ToString)
            Dim d4 As Date = Convert.ToDateTime(DateTimePicker6.Value.ToString)
            If CheckBox2.Checked = False And TextBox1.Text <> "" And CheckBox3.Checked = True Then
                Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim myadapter As New OleDb.OleDbDataAdapter("SELECT * from Loggings where ClientId like'" + TextBox1.Text.TrimEnd + "%' And Date Between #" & _
                                                d1.ToString("dd/MM/yyyy") & "# And #" & _
                                                d2.ToString("dd/MM/yyyy") & "#", con)
                da.Clear()
                myadapter.Fill(da)
                Report.DataGridView1.DataSource = da.Tables(0)
                con.Close()
                con.Dispose()
                Report.Label3.Visible = False
                Report.Label5.Visible = False
                Report.Label4.Text = "- Logins:"
                Me.Visible = False
                Report.ShowDialog()

                Report.DataGridView1.ClearSelection()
            ElseIf CheckBox2.Checked = False And TextBox1.Text <> "" And CheckBox4.Checked = True Then
                Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim myadapter As New OleDb.OleDbDataAdapter("SELECT * from Loggings where ClientId like'" + TextBox1.Text.TrimEnd + "%' And Time Between #" & _
                                                d3.ToString & "# And #" & _
                                                d4.ToString & "# And Date like'" + Today.ToString + "%'", con)
                da.Clear()
                myadapter.Fill(da)
                Report.DataGridView1.DataSource = da.Tables(0)
                con.Close()
                con.Dispose()
                Report.Label3.Visible = False
                Report.Label5.Visible = False
                Report.Label4.Text = "- Logins:"
                Me.Visible = False
                Report.ShowDialog()

                Report.DataGridView1.ClearSelection()
            Else
                Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim myadapter As New OleDb.OleDbDataAdapter("SELECT * from Loggings", con)
                da.Clear()
                myadapter.Fill(da)
                Report.DataGridView1.DataSource = da.Tables(0)
                con.Close()
                con.Dispose()
                Report.Label3.Visible = False
                Report.Label5.Visible = False
                Report.Label4.Text = "- Logins:"
                CheckBox2.Checked = False
                Me.Visible = False
                Report.ShowDialog()

                Report.DataGridView1.ClearSelection()
            End If

        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            DateTimePicker3.Enabled = False
            DateTimePicker4.Enabled = False
            DateTimePicker5.Enabled = False
            DateTimePicker6.Enabled = False
            TextBox1.Enabled = False
            CheckBox3.Enabled = False
            CheckBox4.Enabled = False
        Else
            DateTimePicker3.Enabled = True
            DateTimePicker4.Enabled = True
            DateTimePicker5.Enabled = True
            DateTimePicker6.Enabled = True
            TextBox1.Enabled = True
            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            DateTimePicker3.Enabled = True
            DateTimePicker4.Enabled = True
            DateTimePicker5.Enabled = False
            DateTimePicker6.Enabled = False
            TextBox1.Enabled = True
            CheckBox4.Checked = False
            CheckBox4.Enabled = False
        Else
            DateTimePicker3.Enabled = True
            DateTimePicker4.Enabled = True
            DateTimePicker5.Enabled = True
            DateTimePicker6.Enabled = True
            TextBox1.Enabled = True
            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            DateTimePicker3.Enabled = False
            DateTimePicker4.Enabled = False
            DateTimePicker5.Enabled = True
            DateTimePicker6.Enabled = True
            TextBox1.Enabled = True
            CheckBox3.Checked = False
            CheckBox3.Enabled = False
        Else
            DateTimePicker3.Enabled = True
            DateTimePicker4.Enabled = True
            DateTimePicker5.Enabled = True
            DateTimePicker6.Enabled = True
            TextBox1.Enabled = True
            CheckBox3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        clicked = True
        AdminTab.Show()
        Me.Close()
    End Sub

    Private Sub Search_Query_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            AdminTab.Show()
        End If

    End Sub
End Class