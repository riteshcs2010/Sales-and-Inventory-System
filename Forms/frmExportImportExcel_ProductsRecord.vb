Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO

Public Class frmExportImportExcel_ProductsRecord
    Private Function GenerateID() As String
        con = New SqlConnection(cs)
        Dim value As String = "0000"
        Try
            ' Fetch the latest ID from the database
            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 PID FROM Product ORDER BY PID DESC", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr.HasRows Then
                rdr.Read()
                value = rdr.Item("PID")
            End If
            rdr.Close()
            ' Increase the ID by 1
            value += 1
            ' Because incrementing a string with an integer removes 0's
            ' we need to replace them. If necessary.
            If value <= 9 Then 'Value is between 0 and 10
                value = "000" & value
            ElseIf value <= 99 Then 'Value is between 9 and 100
                value = "00" & value
            ElseIf value <= 999 Then 'Value is between 999 and 1000
                value = "0" & value
            End If
        Catch ex As Exception
            ' If an error occurs, check the connection state and close it if necessary.
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            value = "0000"
        End Try
        Return value
    End Function
    Sub auto()
        Try
            txtID.Text = GenerateID()
            txtProductCode.Text = "P-" + GenerateID()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Public Sub Getdata()

        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("Select RTRIM(Productname), SubCategoryID,RTRIM(CategoryName),RTRIM(SubCategoryName), CostPrice,SellingPrice, Discount, VAT, ReorderPoint,RTRIM(Barcode),OpeningStock,RTRIM(PurchaseUnit),RTRIM(Salesunit) from Category,SubCategory,Product where Category.CategoryName=SubCategory.Category and Product.SubCategoryID=SubCategory.ID order by ProductName", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmLogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Getdata()
    End Sub
    Sub Reset()
        txtProductName.Text = ""
        txtCategory.Text = ""
        DataGridView1.DataSource = Nothing
        DataGridView1.Visible = False
        Getdata()
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


    Private Sub txtGuestName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProductName.TextChanged
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("Select RTRIM(Productname), SubCategoryID,RTRIM(CategoryName),RTRIM(SubCategoryName), CostPrice,SellingPrice, Discount, VAT, ReorderPoint,RTRIM(Barcode),OpeningStock,RTRIM(PurchaseUnit),RTRIM(Salesunit) from Category,SubCategory,Product where Category.CategoryName=SubCategory.Category and Product.SubCategoryID=SubCategory.ID and ProductName like '%" & txtProductName.Text & "%' order by Productname", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCity_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCategory.TextChanged
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("Select RTRIM(Productname), SubCategoryID,RTRIM(CategoryName),RTRIM(SubCategoryName), CostPrice,SellingPrice, Discount, VAT, ReorderPoint,RTRIM(Barcode),OpeningStock,RTRIM(PurchaseUnit),RTRIM(Salesunit) from Category,SubCategory,Product where Category.CategoryName=SubCategory.Category and Product.SubCategoryID=SubCategory.ID and CategoryName like '%" & txtCategory.Text & "%' order by CategoryName", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs)
        Close()
        frmProduct.auto()
        frmProduct.Fill()
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExportExcel.Click
        ExportExcel(dgw)
    End Sub

    Private Sub btnImportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnImportExcel.Click
        Try
            Dim OpenFileDialog As New OpenFileDialog
            OpenFileDialog.Filter = "Excel Files | *.xlsx; *.xls;| All Files (*.*)| *.*"
            If OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso OpenFileDialog.FileName <> "" Then
                Cursor = Cursors.WaitCursor
                Timer1.Enabled = True
                Dim Pathname As String = OpenFileDialog.FileName
                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Pathname + ";Extended Properties=Excel 8.0;")
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyConnection.Open()
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                DataGridView1.Visible = True
                DataGridView1.DataSource = DtSet.Tables(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Cursor = Cursors.Default
        Timer1.Enabled = False
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Dim strRowNumber As String = (e.RowIndex + 1).ToString()
        Dim size As SizeF = e.Graphics.MeasureString(strRowNumber, Me.Font)
        If DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)) Then
            DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ButtonHighlight
        e.Graphics.DrawString(strRowNumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            If DataGridView1.RowCount = Nothing Then
                MessageBox.Show("Sorry nothing to save.." & vbCrLf & "Please retrieve data in datagridview", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            For i As Integer = 0 To Me.DataGridView1.RowCount - 1
                For j As Integer = i + 1 To Me.DataGridView1.RowCount - 1
                    If DataGridView1.Rows(i).Cells(10).Value = DataGridView1.Rows(j).Cells(10).Value Then
                        MessageBox.Show("duplicate Barcode value " & DataGridView1.Rows(i).Cells(10).Value)
                        Return
                    End If
                Next
            Next
          
            For Each row As DataGridViewRow In DataGridView1.Rows
             con = New SqlConnection(cs)
                con.Open()
                Dim ct As String = "select barcode from Product Where Barcode=@d1"
                cmd = New SqlCommand(ct)
                cmd.Parameters.AddWithValue("@d1", Val(row.Cells(10).Value))
                cmd.Connection = con
                rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    MessageBox.Show("Barcode Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    If (rdr IsNot Nothing) Then
                        rdr.Close()
                    End If
                    con.Close()
                    Return
                End If
            Next
            For Each row As DataGridViewRow In DataGridView1.Rows

                con = New SqlConnection(cs)
                con.Open()
                Dim ct5 As String = "select barcode from Temp_Stock Where Barcode=@d1"
                cmd = New SqlCommand(ct5)
                cmd.Parameters.AddWithValue("@d1", Val(row.Cells(10).Value))
                cmd.Connection = con
                rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    MessageBox.Show("Barcode Already Exists Please Enter New Barcode", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    If (rdr IsNot Nothing) Then
                        rdr.Close()
                    End If
                    con.Close()
                    Return
                End If
            Next

            For Each row As DataGridViewRow In DataGridView1.Rows


                Cursor = Cursors.WaitCursor
                Timer1.Enabled = True
                auto()
                con = New SqlConnection(cs)
                con.Open()
                Dim cb As String = "insert into Product(PID,ProductCode, Productname, SubCategoryID, Description, CostPrice, SellingPrice, Discount, VAT, ReorderPoint,OpeningStock,Barcode,PurchaseUnit,SalesUnit) VALUES (" & txtID.Text & ",@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)"
                cmd = New SqlCommand(cb)
                cmd.Parameters.AddWithValue("@d1", txtProductCode.Text)
                cmd.Parameters.AddWithValue("@d2", row.Cells(0).Value.ToString())
                cmd.Parameters.AddWithValue("@d3", row.Cells(1).Value)
                cmd.Parameters.AddWithValue("@d4", row.Cells(4).Value.ToString())
                cmd.Parameters.AddWithValue("@d5", Val(row.Cells(5).Value))
                cmd.Parameters.AddWithValue("@d6", Val(row.Cells(6).Value))
                cmd.Parameters.AddWithValue("@d7", Val(row.Cells(7).Value))
                cmd.Parameters.AddWithValue("@d8", Val(row.Cells(8).Value))
                cmd.Parameters.AddWithValue("@d9", Val(row.Cells(9).Value))
                cmd.Parameters.AddWithValue("@d10", Val(row.Cells(11).Value))
                cmd.Parameters.AddWithValue("@d11", row.Cells(10).Value.ToString())
                cmd.Parameters.AddWithValue("@d12", row.Cells(12).Value.ToString())
                cmd.Parameters.AddWithValue("@d13", row.Cells(13).Value.ToString())
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                SqlConnection.ClearAllPools()
                con = New SqlConnection(cs)
                con.Open()
                Dim cb2 As String = "insert into Temp_Stock(ProductID,Qty,Barcode) VALUES (@d1,@d2,@d3)"
                cmd = New SqlCommand(cb2)
                cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
                cmd.Parameters.AddWithValue("@d2", Val(row.Cells(11).Value))
                cmd.Parameters.AddWithValue("@d3", Val(row.Cells(10).Value).ToString())
                cmd.Connection = con
                cmd.ExecuteNonQuery()
                con.Close()
                SqlConnection.ClearAllPools()
                con = New SqlConnection(cs)
                con.Open()
                Dim ck1 As String = "insert into Product_Join(ProductID,Photo) VALUES (" & txtID.Text & ",@img)"
                cmd = New SqlCommand(ck1)
                cmd.Connection = con
                Dim ms As New MemoryStream()
                Dim bmpImage As New Bitmap(Sales_and_Inventory_System.My.Resources.Resources._12)
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@img", SqlDbType.Image)
                p.Value = data
                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()
                con.Close()
            Next
            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DataGridView1.DataSource = Nothing
            Reset()
        Catch ex As SqlException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        
    End Sub
End Class
