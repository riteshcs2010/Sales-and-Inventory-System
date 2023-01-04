Imports System.Data.SqlClient
Imports System.IO

Public Class frmCompany

    Sub Reset()
        txtShopNumber.Text = ""
        txtShopOwner.Text = ""
        txtLicenseNumber.Text = ""
        txtTIN.Text = ""
        txtEmailID.Text = ""
        txtContactNo.Text = ""
        txtCompanyName.Text = ""
        txtCIN.Text = ""
        txtAddress.Text = ""
        PictureBox1.Image = Sales_and_Inventory_System.My.Resources.Resources.images__5_
        txtCompanyName.Focus()
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtShopNumber.Text = "" Then
            MessageBox.Show("Please enter Shop Number name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtShopNumber.Focus()
            Return
        End If
        If txtShopOwner.Text = "" Then
            MessageBox.Show("Please enter Shop Owner Name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtShopOwner.Focus()
            Return
        End If
        If txtCompanyName.Text = "" Then
            MessageBox.Show("Please enter company name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCompanyName.Focus()
            Return
        End If
        If txtAddress.Text = "" Then
            MessageBox.Show("Please enter address", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAddress.Focus()
            Return
        End If
        If txtContactNo.Text = "" Then
            MessageBox.Show("Please enter contact no.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContactNo.Focus()
            Return
        End If

        If txtEmailID.Text = "" Then
            MessageBox.Show("Please enter email id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmailID.Focus()
            Return
        End If
        Try
            con = New SqlConnection(cs)
            con.Open()
            'Dim ct As String = "select count(*) from company Having count(*) >= 1"
            Dim ct As String = "select ShopID from company where ShopID= @d1"
            cmd = New SqlCommand(ct)
            cmd.Parameters.AddWithValue("@d1", txtShopNumber.Text)
            cmd.Connection = con
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Record Already Exists" & vbCrLf & "please update the company info", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
                Exit Sub
            End If
            con = New SqlConnection(cs)
            con.Open()

            Dim cb As String = "insert into company(ShopID,CompanyOwner,companyName, Address, ContactNo, EmailID, TIN, LicenseNumber, CIN, Logo) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)"
            cmd = New SqlCommand(cb)
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@d1", Convert.ToInt32(txtShopNumber.Text))
            cmd.Parameters.AddWithValue("@d2", txtShopOwner.Text)
            cmd.Parameters.AddWithValue("@d3", txtCompanyName.Text)
            cmd.Parameters.AddWithValue("@d4", txtAddress.Text)
            cmd.Parameters.AddWithValue("@d5", txtContactNo.Text)
            cmd.Parameters.AddWithValue("@d6", txtEmailID.Text)
            cmd.Parameters.AddWithValue("@d7", txtTIN.Text)
            cmd.Parameters.AddWithValue("@d8", txtLicenseNumber.Text)
            cmd.Parameters.AddWithValue("@d9", txtCIN.Text)
            Dim ms As New MemoryStream()
            Dim bmpImage As New Bitmap(PictureBox1.Image)
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@d10", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
            cmd.ExecuteNonQuery()
            con.Close()
            Dim st As String = "added the company '" & txtCompanyName.Text & "' info"
            LogFunc(lblUser.Text, st)
            MessageBox.Show("Successfully saved", "Company Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnSave.Enabled = False
            Getdata()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub


    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If txtCompanyName.Text = "" Then
            MessageBox.Show("Please enter company name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCompanyName.Focus()
            Return
        End If
        If txtAddress.Text = "" Then
            MessageBox.Show("Please enter address", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAddress.Focus()
            Return
        End If
        If txtContactNo.Text = "" Then
            MessageBox.Show("Please enter contact no.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContactNo.Focus()
            Return
        End If

        If txtEmailID.Text = "" Then
            MessageBox.Show("Please enter email id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmailID.Focus()
            Return
        End If
        Try

            con = New SqlConnection(cs)
            con.Open()
            Dim cb As String = "Update company set companyName=@d1, Address=@d2, ContactNo=@d3, EmailID=@d4, TIN=@d5, LicenseNumber=@d6, CIN=@d7, Logo=@d8 where ID=" & txtID.Text & ""
            cmd = New SqlCommand(cb)
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@d1", txtCompanyName.Text)
            cmd.Parameters.AddWithValue("@d2", txtAddress.Text)
            cmd.Parameters.AddWithValue("@d3", txtContactNo.Text)
            cmd.Parameters.AddWithValue("@d4", txtEmailID.Text)
            cmd.Parameters.AddWithValue("@d5", txtTIN.Text)
            cmd.Parameters.AddWithValue("@d6", txtLicenseNumber.Text)
            cmd.Parameters.AddWithValue("@d7", txtCIN.Text)
            Dim ms As New MemoryStream()
            Dim bmpImage As New Bitmap(PictureBox1.Image)
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New SqlParameter("@d8", SqlDbType.Image)
            p.Value = data
            cmd.Parameters.Add(p)
            cmd.ExecuteNonQuery()
            con.Close()
            Dim st As String = "updated the company '" & txtCompanyName.Text & "' info"
            LogFunc(lblUser.Text, st)
            MessageBox.Show("Successfully updated", "Company Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnUpdate.Enabled = False
            Getdata()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Public Sub Getdata()
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT RTRIM(ID), RTRIM(ShopID),RTRIM(CompanyOwner),RTRIM(companyName), RTRIM(Address), RTRIM(ContactNo), RTRIM(EmailID), RTRIM(TIN), RTRIM(LicenseNumber), RTRIM(CIN), Logo from company", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub


    Private Sub dgw_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles dgw.RowPostPaint
        Dim strRowNumber As String = (e.RowIndex + 1).ToString()
        Dim size As SizeF = e.Graphics.MeasureString(strRowNumber, Me.Font)
        If dgw.RowHeadersWidth < Convert.ToInt32((size.Width + 20)) Then
            dgw.RowHeadersWidth = Convert.ToInt32((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ButtonHighlight
        e.Graphics.DrawString(strRowNumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))

    End Sub

    Private Sub frmRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'SIS_DBDataSet.Company' table. You can move, or remove it, as needed.
        Me.CompanyTableAdapter.Fill(Me.SIS_DBDataSet.Company)
        Getdata()
    End Sub

    Private Sub dgw_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgw.MouseClick
        Try

            If dgw.Rows.Count > 0 Then
                Dim dr As DataGridViewRow = dgw.SelectedRows(0)
                txtID.Text = dr.Cells(0).Value.ToString()
                txtCompanyName.Text = dr.Cells(1).Value.ToString()
                txtAddress.Text = dr.Cells(2).Value.ToString()
                txtContactNo.Text = dr.Cells(3).Value.ToString()
                txtEmailID.Text = dr.Cells(4).Value.ToString()
                txtTIN.Text = dr.Cells(5).Value.ToString()
                txtLicenseNumber.Text = dr.Cells(6).Value.ToString()
                txtCIN.Text = dr.Cells(7).Value.ToString()
                Dim data As Byte() = DirectCast(dr.Cells(8).Value, Byte())
                Dim ms As New MemoryStream(data)
                Me.PictureBox1.Image = Image.FromStream(ms)
                btnUpdate.Enabled = True
                btnSave.Enabled = False
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With OpenFileDialog1
                .Filter = ("Images |*.png; *.bmp; *.jpg;*.jpeg; *.gif;*.ico;")
                .FilterIndex = 4
            End With
            'Clear the file name
            OpenFileDialog1.FileName = ""
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            If MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                DeleteRecord()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteRecord()

        Try

            Dim RowsAffected As Integer = 0
            con = New SqlConnection(cs)
            con.Open()
            Dim cq As String = "delete from company where ID=@d1"
            cmd = New SqlCommand(cq)
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@d1", val(txtID.Text))
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then
                Dim st As String = "deleted the company '" & txtCompanyName.Text & "'"
                LogFunc(lblUser.Text, st)
                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Getdata()
                Reset()
            Else
                MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Reset()
            End If
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub txtCompanyName_TextChanged(sender As Object, e As EventArgs) Handles txtCompanyName.TextChanged

    End Sub
End Class
