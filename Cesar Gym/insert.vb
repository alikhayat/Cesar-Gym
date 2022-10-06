Imports System.Data.OleDb

Public Class insert
    Dim rs As New Resizer
    Dim pass As String = ""
    Dim link As String = "facebook.com/"
    Dim comprice As String = ""
    Dim date1 As Date
    Dim clicked As Boolean = False
    Private Sub insert_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        clear()
        checkboxfill()
        Label14.Visible = False
        
        mode()

        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        DateTimePicker3.CustomFormat = "dd/MM/yyyy"
        DateTimePicker2.CustomFormat = "dd/MM/yyyy"

    End Sub
    
    Private Sub checkboxfill()

        Try
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT Name from Categories", myConnToAccess)


            da.Fill(dt)
            CheckedListBox1.DataSource = dt
            CheckedListBox1.ValueMember = "Name"
            CheckedListBox1.DisplayMember = "Name"
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    CheckedListBox1.Items.Add(CStr(dt.Rows(i).Item(0)))
                Next
            End If
            myConnToAccess.Close()
        Catch ex As Exception


        End Try

    End Sub
    Private Function blank() As Boolean
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox9.Text = "" Or ComboBox1.SelectedIndex = -1 Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or CheckBox1.Checked = False And CheckBox2.Checked = False Or DateTimePicker1.Value.ToString = "" Or DateTimePicker2.Value.ToString = "" Or DateTimePicker3.Value.ToString = "" Or Label14.Text = "taken" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim d2 As String = DateTimePicker2.Value.ToString("dd/MM/yyyy")
            Dim d1 As String = DateTimePicker1.Value.ToString("dd/MM/yyyy")
            Dim birthday As String = DateTimePicker3.Value.ToString("dd/MM/yyyy")
            Dim fullname As String = TextBox1.Text + " " + TextBox2.Text
            If My.Settings.mode = "insert" Then
                Dim datee As Integer = Convert.ToInt32(Label6.Text)


                If CheckedListBox1.CheckedItems.Count > 0 And blank() = False And datee > 0 And Label14.Text = "Available" Then
                    'insert data
                    Dim objcmd As New System.Data.OleDb.OleDbCommand
                    Dim objcmdd As New System.Data.OleDb.OleDbCommand
                    Dim con As New OleDb.OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                    con.Open()
                    link = link + TextBox6.Text

                    My.Settings.ClientId += 1
                    Dim strSql = ("insert into Applicants values ('" + My.Settings.ClientId.ToString + "', '" + TextBox1.Text + "', '" + TextBox2.Text + "', '" + ComboBox1.Text + "', '" + TextBox4.Text + "', '" + TextBox5.Text + "', '" + link + "', '" + TextBox7.Text + "', '" + birthday.ToString + "')")
                    objcmd = New OleDbCommand(strSql, con)
                    objcmd.ExecuteNonQuery()

                    con.Close()
                    Dim price As String = TextBox9.Text






                    Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


                    myConnToAccess.Open()
                    Dim classes As String = ""
                    'fetch checked subscriptions
                    For Each Item As DataRowView In CheckedListBox1.CheckedItems


                        classes = Item(0).ToString() + " ,  "


                    Next




                    My.Settings.SubId += 1
                    Dim shift As String = ""
                    If CheckBox1.Checked = True Then
                        shift = CheckBox1.Text
                    ElseIf CheckBox2.Checked = True Then
                        shift = CheckBox2.Text

                    End If
                    'fetch password
                    'If pass = "" Then
                    '    For i As Integer = 0 To 20
                    '        If GeneratePass() = False Then
                    '            i += 1
                    '        Else
                    '            Exit For
                    '        End If
                    '    Next
                    'End If


                    Dim strSqll = ("insert into Subscription values ('" + My.Settings.SubId.ToString + "', '" + classes + "', '" + DateTimePicker1.Value.ToString("dd/MM/yyyy") + "', '" + DateTimePicker2.Value.ToString("dd/MM/yyyy") + "', '" + My.Settings.ClientId.ToString + "', '" + fullname + "', '" + shift + "', '" + TextBox9.Text + "', '" + TextBox8.Text + "', '" + Label6.Text + "', '" + TextBox3.Text + "')")

                    objcmdd = New OleDbCommand(strSqll, myConnToAccess)
                    objcmdd.ExecuteNonQuery()
                    myConnToAccess.Close()
                    MsgBox("Added, Price: " + price + " L.L")
                    myConnToAccess.Close()

                    Me.Close()





                End If

            ElseIf My.Settings.mode = "edit" Then

                If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or ComboBox1.SelectedIndex = -1 Then
                    MsgBox("fill needed fields")
                Else

                    Dim queryy As String = "UPDATE [Applicants] SET [Name]=? , [Last]=? , [Gender]=? , [PhoneNo]=? , [emgNo]=? , [FacebookLink]=? , [Adderess]=? , [BirthDate]=? WHERE [ID]=? "
                    Using con = New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

                        Using cmd = New OleDbCommand(queryy, con)
                            con.Open()
                            cmd.Parameters.AddWithValue("@p1", TextBox1.Text)
                            cmd.Parameters.AddWithValue("@p2", TextBox2.Text)
                            cmd.Parameters.AddWithValue("@p3", ComboBox1.Text)
                            cmd.Parameters.AddWithValue("@p4", TextBox4.Text)
                            cmd.Parameters.AddWithValue("@p5", TextBox5.Text)
                            cmd.Parameters.AddWithValue("@p6", TextBox6.Text)
                            cmd.Parameters.AddWithValue("@p7", TextBox7.Text)
                            cmd.Parameters.AddWithValue("@p8", DateTimePicker3.Value)


                            cmd.Parameters.AddWithValue("@p9", Label17.Text)
                            cmd.ExecuteNonQuery()
                            con.Close()

                            MsgBox("Edited")
                            Me.Close()
                        End Using
                    End Using

                End If

            ElseIf My.Settings.mode = "renewal" Then


                If TextBox3.Text = "" Or TextBox9.Text = "" Or TextBox8.Text = "" Or Label14.Text <> "Available" Or CheckedListBox1.CheckedItems.Count = 0 Then
                    MsgBox("fill needed fields")
                Else
                    Dim classes As String = ""
                    'fetch checked subscriptions
                    For Each Item As DataRowView In CheckedListBox1.CheckedItems


                        classes = Item(0).ToString() + " ,  "


                    Next
                    Dim shift As String = ""
                    If CheckBox1.Checked = True Then
                        shift = CheckBox1.Text
                    ElseIf CheckBox2.Checked = True Then
                        shift = CheckBox2.Text

                    End If
                    Dim query As String = "UPDATE [Subscription] SET [Category]=? , [StartDate]=? , [EndDate]=? , [Shift]=? , [Price]=? , [Password]=? , [DaysLeft]=? , [LogonsLeft]=? WHERE [ApplicantId]=? "
                    Using con = New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

                        Using cmd = New OleDbCommand(query, con)
                            con.Open()
                            cmd.Parameters.AddWithValue("@p1", classes)
                            cmd.Parameters.AddWithValue("@p2", DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                            cmd.Parameters.AddWithValue("@p3", DateTimePicker2.Value.ToString("dd/MM/yyyy"))
                            cmd.Parameters.AddWithValue("@p4", shift)
                            cmd.Parameters.AddWithValue("@p5", TextBox9.Text)
                            cmd.Parameters.AddWithValue("@p6", TextBox8.Text)
                            cmd.Parameters.AddWithValue("@p7", Label6.Text)
                            cmd.Parameters.AddWithValue("@p8", TextBox3.Text)


                            cmd.Parameters.AddWithValue("@p9", Label17.Text)
                            cmd.ExecuteNonQuery()
                            con.Close()

                            MsgBox("Added, Price: " + TextBox9.Text + " L.L")
                            Me.Close()
                        End Using
                    End Using

                End If
            End If
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        









    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim d1 As Date = DateTimePicker1.Value
        Dim d2 As Date = DateTimePicker2.Value
        Label6.Text = (d2 - d1).Days.ToString
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Dim d1 As Date = DateTimePicker1.Value
        Dim d2 As Date = DateTimePicker2.Value
        Label6.Text = (d2 - d1).Days.ToString
    End Sub
    Private Function GeneratePass() As Boolean
        Dim random As New Random
        Dim password As New System.Text.StringBuilder
        For i As Int32 = 0 To 3
            password.Append(Chr(random.Next(48, 57)))

        Next
        pass = password.ToString
        Dim dt = New DataTable()
        Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


        myConnToAccess.Open()

        Dim da = New OleDbDataAdapter("SELECT Password from Subscription", myConnToAccess)
        da.Fill(dt)
        Dim exists As Boolean = False
        For Each row As DataRow In dt.Rows

            If dt.Rows()(0).ToString = password.ToString Then

                exists = True

            End If

        Next
        myConnToAccess.Close()
        If exists = True Then
            Return False
        ElseIf exists = False Then
            Return True

        End If
    End Function
    Private Sub clear()
        Try
            TextBox1.Clear()
            TextBox2.Clear()
            ComboBox1.SelectedIndex = -1
            TextBox4.Clear()
            TextBox8.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            For i As Integer = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next

        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        validatee()
    End Sub
    Private Sub validatee()

        If TextBox8.Text <> "" And TextBox8.TextLength - 4 = 0 Then
            Dim pass As String = TextBox8.Text
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT Password from Subscription", myConnToAccess)
            da.Fill(dt)
            Dim exists As Boolean = False
            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString = pass Then
                    exists = True
                End If

            Next

            myConnToAccess.Close()

            If exists = True Then
                Label14.Visible = True
                Label14.Text = "taken"
                Label14.ForeColor = Color.Red
            Else
                Label14.Visible = True
                Label14.Text = "Available"
                Label14.ForeColor = Color.Green
            End If
        End If
        


    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress, TextBox5.KeyPress, TextBox4.KeyPress, TextBox3.KeyPress, TextBox9.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub mode()
        If My.Settings.mode = "edit" Then
            Try
                GroupBox3.Visible = False
                GroupBox4.Visible = False
                GroupBox1.Visible = True
                Label16.Visible = True
                Label17.Visible = True

                TextBox1.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Label17.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                TextBox2.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(2).Value.ToString
                TextBox4.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(4).Value.ToString
                If ClientSelection.DataGridView1.SelectedRows(0).Cells(3).Value.ToString = "Male" Then
                    ComboBox1.SelectedIndex = 0
                ElseIf ClientSelection.DataGridView1.SelectedRows(0).Cells(3).Value.ToString = "Female" Then
                    ComboBox1.SelectedIndex = 1
                End If

                TextBox5.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(5).Value.ToString
                TextBox6.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(6).Value.ToString.Split("/")(1)
                TextBox7.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(7).Value.ToString
                DateTimePicker3.Value = ClientSelection.DataGridView1.SelectedRows(0).Cells(8).Value.ToString

                TextBox1.Enabled = True
                TextBox2.Enabled = True
                TextBox4.Enabled = True
                TextBox5.Enabled = True
                TextBox6.Enabled = True
                TextBox7.Enabled = True
                ComboBox1.Enabled = True
                Button2.Enabled = True
                DateTimePicker3.Enabled = True
                Button1.Text = "Update"
            Catch ex As Exception

            End Try

        ElseIf My.Settings.mode = "renewal" Then
            Try
                GroupBox3.Visible = True
                GroupBox4.Visible = True
                GroupBox1.Visible = True
                DateTimePicker1.Value = ClientSelection.DataGridView1.SelectedRows(0).Cells(2).Value
                DateTimePicker2.Value = ClientSelection.DataGridView1.SelectedRows(0).Cells(3).Value
                Label17.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(4).Value.ToString
                TextBox1.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(5).Value.ToString
                TextBox3.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(10).Value.ToString
                TextBox8.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(8).Value.ToString
                If ClientSelection.DataGridView1.SelectedRows(0).Cells(6).Value.ToString = "Day Shift" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox2.Checked = True
                End If
                TextBox9.Text = ClientSelection.DataGridView1.SelectedRows(0).Cells(7).Value.ToString
                Label6.Text = (DateTimePicker2.Value - DateTimePicker1.Value).Days.ToString
                Label16.Visible = True
                Label17.Visible = True

                TextBox1.Enabled = False
                TextBox2.Enabled = False
                TextBox4.Enabled = False
                TextBox5.Enabled = False
                TextBox6.Enabled = False
                TextBox7.Enabled = False
                TextBox8.Enabled = False
                Label14.Text = "Available"
                ComboBox1.Enabled = False
                DateTimePicker3.Enabled = False
                Button2.Enabled = False
                Button1.Text = "Renew"
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        ElseIf My.Settings.mode = "insert" Then
            GroupBox3.Visible = True
            GroupBox4.Visible = True
            GroupBox1.Visible = True
            Label16.Visible = False
            Label17.Visible = False

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            ComboBox1.Enabled = True
            DateTimePicker3.Enabled = True
            Button2.Enabled = True
            Button1.Text = "Submit"
        End If
        
    End Sub
    
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Settings.mode = "insert" Then
            clicked = True
            AdminTab.Show()
            Me.Close()
        Else
            ClientSelection.Show()
            Me.Close()
        End If
       
    End Sub

    Private Sub insert_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            If My.Settings.mode = "insert" Then
                AdminTab.Visible = True

            Else
                ClientSelection.Show()

            End If
        End If
        My.Settings.Save()
    End Sub
End Class