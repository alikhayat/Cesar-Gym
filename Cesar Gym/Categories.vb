Imports System.Data.OleDb

Public Class Categories
    Dim rs As New Resizer
    Dim clicked As Boolean = False
    Private Sub Config_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rs.FindAllControls(Me)
        ComboBox3.SelectedIndex = -1
        hidden()
        clear()

        Dim ds = New DataSet
        Dim tables = ds.Tables
        Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

        
        
        fill()

    End Sub
    Private Sub Config_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
    Private Sub hidden()
        

        Label1.Visible = False
        
        Label5.Visible = False
        Label6.Visible = False
        Label11.Visible = False
        Label12.Visible = True
       
        TextBox1.Visible = False
        


        ComboBox1.Visible = False

        ComboBox3.Visible = True

        CheckBox1.Visible = False
        
    End Sub
    Private Sub clear()
        TextBox1.Clear()
        

        ComboBox1.SelectedIndex = -1
        
        Label11.Text = ""

        CheckBox1.Checked = False


    End Sub
    Private Sub InsertMode()
        

        Label1.Visible = True
        
        Label5.Visible = True
        Label6.Visible = False
        Label11.Visible = False
       
        TextBox1.Visible = True
        


        ComboBox1.Visible = True


        CheckBox1.Visible = True
        
    End Sub
    Private Sub EditMode()
        
        Label1.Visible = True
        
        Label5.Visible = True
        Label6.Visible = True
        Label11.Visible = True
       
        TextBox1.Visible = True
        
        ComboBox1.Visible = True


        CheckBox1.Visible = True
        
    End Sub
    Private Sub ViewMode()
        
        Label1.Visible = True
        
        Label5.Visible = True
        Label6.Visible = True
        Label11.Visible = True
        
        TextBox1.Visible = True
        

        ComboBox1.Visible = True


        CheckBox1.Visible = True
        
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        clear()
        If ComboBox3.SelectedIndex = -1 Then
            hidden()

        ElseIf ComboBox3.SelectedIndex = 0 Then
            InsertMode()
            enabledt()
        ElseIf ComboBox3.SelectedIndex = 1 Then
            EditMode()
            enabledt()
        ElseIf ComboBox3.SelectedIndex = 2 Then
            ViewMode()
            enabled()
        End If
    End Sub


    



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If blank() = False And ComboBox3.SelectedIndex = 0 Then

                Dim objcmd As New System.Data.OleDb.OleDbCommand
                Dim objcmdd As New System.Data.OleDb.OleDbCommand
                Dim con As New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")
                con.Open()
                Dim accepts As String = ""

                If CheckBox1.Checked = True Then
                    accepts = "true"
                End If

                My.Settings.CatId += 1
                Dim strSql = ("insert into Categories values ('" + My.Settings.CatId.ToString + "', '" + TextBox1.Text + "', '" + ComboBox1.Text + "', '" + accepts + "')")
                objcmd = New OleDbCommand(strSql, con)
                objcmd.ExecuteNonQuery()

                con.Close()

                clear()
                MsgBox("Added")
                fill()


            ElseIf blank() = False And ComboBox3.SelectedIndex = 1 Then
                Dim accepts As String = ""

                If CheckBox1.Checked = True Then
                    accepts = "true"
                End If

                Dim queryy As String = "UPDATE [Categories] SET [Name]=? , [TimeShift]=? , [AcceptsSchedule]=? WHERE [ID]=? "
                Using con = New OleDb.OleDbConnection("PROVIDER=Microsoft.JET.OLEDB.4.0;DATA SOURCE=" & Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")

                    Using cmd = New OleDbCommand(queryy, con)
                        con.Open()
                        cmd.Parameters.AddWithValue("@p1", TextBox1.Text)



                        cmd.Parameters.AddWithValue("@p2", ComboBox1.Text)

                        cmd.Parameters.AddWithValue("@p3", accepts)



                        cmd.Parameters.AddWithValue("@p9", Label11.Text)
                        cmd.ExecuteNonQuery()
                        con.Close()

                    End Using
                End Using



                clear()
                MsgBox("Edited")
                fill()

            End If
        Catch ex As Exception
            MsgBox(Convert.ToString(ex))
        End Try
        
        My.Settings.Save()





    End Sub
    Private Function blank() As Boolean

        If TextBox1.Text = "" Or ComboBox1.SelectedIndex = -1 Then
            Return True
        ElseIf TextBox1.Text = "" Or ComboBox1.SelectedIndex = -1 Then



            Return True
        Else
            Return False
        End If







    End Function
    Private Sub enabled()
        TextBox1.Enabled = False
        
        ComboBox1.Enabled = False

        CheckBox1.Enabled = False


    End Sub
    Private Sub enabledt()
        TextBox1.Enabled = True
        
        ComboBox1.Enabled = True

        CheckBox1.Enabled = True

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
    
    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
    Private Sub fill()
        Try
            Dim dt = New DataTable()
            Dim myConnToAccess = New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + Application.StartupPath() & "\Database.mdb;Jet OLEDB:Database Password=janitani;")


            myConnToAccess.Open()

            Dim da = New OleDbDataAdapter("SELECT * from Categories", myConnToAccess)


            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub
    
    


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            If ComboBox3.SelectedIndex <> 0 And ComboBox3.SelectedIndex <> -1 Then
                Label11.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                TextBox1.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                

                ComboBox1.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
                
                
                If DataGridView1.SelectedRows(0).Cells(6).Value.ToString <> "" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If
        Catch ex As Exception

        End Try
        
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers

        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
    
    
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clicked = True
        Config.Show()
        Me.Close()
    End Sub

    Private Sub Categories_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If clicked = False Then
            Config.Show()
        End If

    End Sub
End Class