Imports System.Data.SqlClient
Imports System.IO

Imports System.Globalization

Public Class frmPOS
    Dim st2 As String
    Private TextBoxOrder As New Dictionary(Of TextBox, TextBox)()
    Declare Function Wow64DisableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Declare Function Wow64EnableWow64FsRedirection Lib "kernel32" (ByRef oldvalue As Long) As Boolean
    Sub Reset()
        txtCID.Text = ""
        txtRemarks.Text = ""
        txtCustomerName.Text = ""
        txtAmount.Text = ""
        txtCostPrice.Text = ""
        txtCustomerID.Text = ""
        txtDiscountAmount.Text = ""
        txtDiscountPer.Text = ""
        txtMargin.Text = ""
        txtInvoiceNo.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtQty.Text = ""
        txtSellingPrice.Text = ""
        txtTotalAmount.Text = ""
        txtTotalQty.Text = ""
        txtVAT.Text = ""
        txtVATAmount.Text = ""
        txtGrandTotal.Text = ""
        txtTotalPayment.Text = ""
        txtPaymentDue.Text = ""
        dtpInvoiceDate.Value = Today
        txtBarcode.Focus()
        btnDelete.Enabled = False
        btnUpdate.Enabled = False
        btnSave.Enabled = True
        btnRemove.Enabled = False
        btnAdd.Enabled = True
        btnRemove1.Enabled = False
        btnAdd1.Enabled = True
        btnPrint.Enabled = False
        txtContactNo.ReadOnly = False
        txtCustomerName.ReadOnly = False
        txtContactNo.Text = ""
        Button1.Enabled = True
        txtCustomerType.Text = ""
        txtSalesmanID.Text = ""
        txtSalesman.Text = ""
        txtSM_ID.Text = ""
        auto()
        lblSet.Text = "Allowed"
        lblUnit.Text = "Unit"
        txtBarcode.Focus()
        txtBarcode.Select()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        Clear()
        Clear1()
    End Sub
    Private Function GenerateID() As String
        con = New SqlConnection(cs)
        Dim value As String = "0000"
        Try
            ' Fetch the latest ID from the database
            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 Inv_ID FROM InvoiceInfo ORDER BY Inv_ID DESC", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr.HasRows Then
                rdr.Read()
                value = rdr.Item("Inv_ID")
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
            txtInvoiceNo.Text = "PB-" + GenerateID()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub btnSelect_Click(sender As System.Object, e As System.EventArgs) Handles btnSelect.Click
        frmCustomerRecord2.lblSet.Text = "Billing"
        frmCustomerRecord2.lblUser.Text = lblUser.Text
        frmCustomerRecord2.Reset()
        frmCustomerRecord2.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectionInv.Click
        frmCurrentStock.lblSet.Text = "Billing"
        frmCurrentStock.Reset()
        frmCurrentStock.ShowDialog()
    End Sub
    Sub Compute()
        Dim num1, num2, num3, num4, num5 As Double
        txtMargin.Text = (Val(txtSellingPrice.Text) - Val(txtCostPrice.Text)) * Val(txtQty.Text)
        num1 = CDbl(Val(txtQty.Text) * Val(txtSellingPrice.Text))
        num1 = Math.Round(num1, 2)
        txtAmount.Text = num1
        num2 = CDbl((Val(txtAmount.Text) * Val(txtDiscountPer.Text)) / 100)
        num2 = Math.Round(num2, 2)
        txtDiscountAmount.Text = num2
        num3 = Val(txtAmount.Text) - Val(txtDiscountAmount.Text)
        num4 = CDbl((Val(txtVAT.Text) * Val(num3)) / 100)
        num4 = Math.Round(num4, 2)
        txtVATAmount.Text = num4
        num5 = CDbl(Val(txtAmount.Text) + Val(txtVATAmount.Text) - Val(txtDiscountAmount.Text))
        num5 = Math.Round(num5, 2)
        txtTotalAmount.Text = num5
    End Sub

    Private Sub txtQty_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtQty.TextChanged
        Compute()
    End Sub

    Private Sub txtQty_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtQty.Text
            Dim selectionStart = Me.txtQty.SelectionStart
            Dim selectionLength = Me.txtQty.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                'Reject an integer that is longer than 16 digits.
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                'Reject a real number with two many decimal places.
                e.Handled = False
            End If
        Else
            'Reject all other characters.
            e.Handled = True
        End If
    End Sub
    Public Function GrandTotal() As Double
        Dim sum As Double = 0
        Try
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(12).Value
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return sum
    End Function
    Public Function TotalPayment() As Double
        Dim sum As Double = 0
        Try
            For Each r As DataGridViewRow In Me.DataGridView2.Rows
                sum = sum + r.Cells(1).Value
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return sum
    End Function
    Sub Print()
        Try
            If txtCustomerType.Text <> "Non Regular" Then
                Cursor = Cursors.WaitCursor
                Timer1.Enabled = True
                Dim rpt As New rptInvoice 'The report you created.
                Dim myConnection As SqlConnection
                Dim MyCommand, MyCommand1 As New SqlCommand()
                Dim myDA, myDA1 As New SqlDataAdapter()
                Dim myDS As New DataSet 'The DataSet you created.
                myConnection = New SqlConnection(cs)
                MyCommand.Connection = myConnection
                MyCommand1.Connection = myConnection
                MyCommand.CommandText = "SELECT SalesUnit, Customer.ID, Customer.Name, Customer.Gender, Customer.Address, Customer.City, Customer.State, Customer.ZipCode, Customer.ContactNo, Customer.EmailID, Customer.Remarks,Customer.Photo, InvoiceInfo.Inv_ID, InvoiceInfo.InvoiceNo, InvoiceInfo.InvoiceDate, InvoiceInfo.CustomerID , InvoiceInfo.GrandTotal, InvoiceInfo.TotalPaid, InvoiceInfo.Balance, Invoice_Product.IPo_ID, Invoice_Product.InvoiceID, Invoice_Product.ProductID, Invoice_Product.CostPrice, Invoice_Product.SellingPrice, Invoice_Product.Margin,Invoice_Product.Qty, Invoice_Product.Amount, Invoice_Product.DiscountPer, Invoice_Product.Discount, Invoice_Product.VATPer, Invoice_Product.VAT, Invoice_Product.TotalAmount, Product.PID,Product.ProductCode, Product.ProductName FROM Customer INNER JOIN InvoiceInfo ON Customer.ID = InvoiceInfo.CustomerID INNER JOIN Invoice_Product ON InvoiceInfo.Inv_ID = Invoice_Product.InvoiceID INNER JOIN Product ON Invoice_Product.ProductID = Product.PID where InvoiceInfo.Invoiceno=@d1"
                MyCommand.Parameters.AddWithValue("@d1", txtInvoiceNo.Text)
                MyCommand1.CommandText = "SELECT * from Company"
                MyCommand.CommandType = CommandType.Text
                MyCommand1.CommandType = CommandType.Text
                myDA.SelectCommand = MyCommand
                myDA1.SelectCommand = MyCommand1
                myDA.Fill(myDS, "InvoiceInfo")
                myDA.Fill(myDS, "Invoice_Product")
                myDA.Fill(myDS, "Customer")
                myDA.Fill(myDS, "Product")
                myDA1.Fill(myDS, "Company")
                rpt.SetDataSource(myDS)
                rpt.SetParameterValue("p1", txtCustomerID.Text)
                rpt.SetParameterValue("p2", Today)
                frmSalesInvoice_View.CrystalReportViewer1.ReportSource = rpt
                frmSalesInvoice_View.txtCustomerType.Text = txtCustomerType.Text
                frmSalesInvoice_View.txtCustomerID.Text = txtCustomerID.Text
                frmSalesInvoice_View.txtInvoiceNo.Text = txtInvoiceNo.Text
                frmSalesInvoice_View.ShowDialog()
            End If
            If txtCustomerType.Text = "Non Regular" Then
                Cursor = Cursors.WaitCursor
                Timer1.Enabled = True
                Dim rpt As New rptInvoice2 'The report you created.
                Dim myConnection As SqlConnection
                Dim MyCommand, MyCommand1 As New SqlCommand()
                Dim myDA, myDA1 As New SqlDataAdapter()
                Dim myDS As New DataSet 'The DataSet you created.
                myConnection = New SqlConnection(cs)
                MyCommand.Connection = myConnection
                MyCommand1.Connection = myConnection
                MyCommand.CommandText = "SELECT SalesUnit, Customer.ID, Customer.Name, Customer.Gender, Customer.Address, Customer.City, Customer.State, Customer.ZipCode, Customer.ContactNo, Customer.EmailID, Customer.Remarks,Customer.Photo, InvoiceInfo.Inv_ID, InvoiceInfo.InvoiceNo, InvoiceInfo.InvoiceDate, InvoiceInfo.CustomerID , InvoiceInfo.GrandTotal, InvoiceInfo.TotalPaid, InvoiceInfo.Balance, Invoice_Product.IPo_ID, Invoice_Product.InvoiceID, Invoice_Product.ProductID, Invoice_Product.CostPrice, Invoice_Product.SellingPrice, Invoice_Product.Margin,Invoice_Product.Qty, Invoice_Product.Amount, Invoice_Product.DiscountPer, Invoice_Product.Discount, Invoice_Product.VATPer, Invoice_Product.VAT, Invoice_Product.TotalAmount, Product.PID,Product.ProductCode, Product.ProductName FROM Customer INNER JOIN InvoiceInfo ON Customer.ID = InvoiceInfo.CustomerID INNER JOIN Invoice_Product ON InvoiceInfo.Inv_ID = Invoice_Product.InvoiceID INNER JOIN Product ON Invoice_Product.ProductID = Product.PID where InvoiceInfo.Invoiceno=@d1"
                MyCommand.Parameters.AddWithValue("@d1", txtInvoiceNo.Text)
                MyCommand1.CommandText = "SELECT * from Company"
                MyCommand.CommandType = CommandType.Text
                MyCommand1.CommandType = CommandType.Text
                myDA.SelectCommand = MyCommand
                myDA1.SelectCommand = MyCommand1
                myDA.Fill(myDS, "InvoiceInfo")
                myDA.Fill(myDS, "Invoice_Product")
                myDA.Fill(myDS, "Customer")
                myDA.Fill(myDS, "Product")
                myDA1.Fill(myDS, "Company")
                rpt.SetDataSource(myDS)
                rpt.SetParameterValue("p1", txtCustomerID.Text)
                rpt.SetParameterValue("p2", Today)
                frmSalesInvoice_View.CrystalReportViewer1.ReportSource = rpt
                frmSalesInvoice_View.txtCustomerType.Text = txtCustomerType.Text
                frmSalesInvoice_View.txtCustomerID.Text = txtCustomerID.Text
                frmSalesInvoice_View.txtInvoiceNo.Text = txtInvoiceNo.Text
                frmSalesInvoice_View.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            If txtProductCode.Text = "" Then
                MessageBox.Show("Please retrieve product code", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtProductCode.Focus()
                Exit Sub
            End If
            If txtBarcode.Text = "" Then
                MessageBox.Show("Please retrieve barcode", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtBarcode.Focus()
                Exit Sub
            End If
            If Len(Trim(txtSellingPrice.Text)) = 0 Then
                MessageBox.Show("Please enter price", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtSellingPrice.Focus()
                Exit Sub
            End If
            If Len(Trim(txtDiscountPer.Text)) = 0 Then
                MessageBox.Show("Please enter discount %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtDiscountPer.Focus()
                Exit Sub
            End If
            If Len(Trim(txtVAT.Text)) = 0 Then
                MessageBox.Show("Please enter vat %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtVAT.Focus()
                Exit Sub
            End If
            If txtQty.Text = "" Then
                MessageBox.Show("Please enter quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If
            If txtQty.Text = 0 Then
                MessageBox.Show("Quantity can not be zero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If
            If DataGridView1.Rows.Count = 0 Then
                DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtCostPrice.Text), Val(txtSellingPrice.Text), Val(txtMargin.Text), Val(txtQty.Text), Val(txtAmount.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVAT.Text), Val(txtVATAmount.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text))
                Dim k As Double = 0
                k = GrandTotal()
                k = Math.Round(k, 2)
                txtGrandTotal.Text = k
                Compute1()
                Clear()
                Exit Sub
            End If
            For Each r As DataGridViewRow In DataGridView1.Rows
                If r.Cells(0).Value = txtProductCode.Text And r.Cells(2).Value = txtBarcode.Text Then
                    r.Cells(0).Value = txtProductCode.Text
                    r.Cells(1).Value = txtProductName.Text
                    r.Cells(2).Value = txtBarcode.Text
                    r.Cells(3).Value = Val(txtCostPrice.Text)
                    r.Cells(4).Value = Val(txtSellingPrice.Text)
                    r.Cells(5).Value = Val(txtMargin.Text)
                    r.Cells(6).Value = Val(r.Cells(6).Value) + Val(txtQty.Text)
                    r.Cells(7).Value = Val(r.Cells(7).Value) + Val(txtAmount.Text)
                    r.Cells(8).Value = Val(txtDiscountPer.Text)
                    r.Cells(9).Value = Val(r.Cells(9).Value) + Val(txtDiscountAmount.Text)
                    r.Cells(10).Value = Val(txtVAT.Text)
                    r.Cells(11).Value = Val(r.Cells(11).Value) + Val(txtVATAmount.Text)
                    r.Cells(12).Value = Val(r.Cells(12).Value) + Val(txtTotalAmount.Text)
                    r.Cells(13).Value = Val(txtProductID.Text)
                    Dim i As Double = 0
                    i = GrandTotal()
                    i = Math.Round(i, 2)
                    txtGrandTotal.Text = i
                    Compute1()
                    Clear()
                    Exit Sub
                End If
            Next
            DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtCostPrice.Text), Val(txtSellingPrice.Text), Val(txtMargin.Text), Val(txtQty.Text), Val(txtAmount.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVAT.Text), Val(txtVATAmount.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text))
            Dim j As Double = 0
            j = GrandTotal()
            j = Math.Round(j, 2)
            txtGrandTotal.Text = j
            Compute1()
            Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Clear()
        txtBarcode.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtCostPrice.Text = ""
        txtSellingPrice.Text = ""
        txtMargin.Text = ""
        txtQty.Text = ""
        txtAmount.Text = ""
        txtDiscountPer.Text = ""
        txtDiscountAmount.Text = ""
        txtVAT.Text = ""
        txtVATAmount.Text = ""
        txtTotalAmount.Text = ""
        btnAdd.Enabled = True
        btnRemove.Enabled = False
        btnListUpdate.Enabled = False
        lblUnit.Text = "Unit"
        txtBarcode.Focus()
    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        Try
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next
            Dim k As Double = 0
            k = GrandTotal()
            k = Math.Round(k, 2)
            txtGrandTotal.Text = k
            Compute()
            Compute1()
            Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick

        If (Me.DataGridView1.Rows.Count > 0) Then
            If lblSet.Text = "Not Allowed" Then
                btnRemove.Enabled = False
                btnListUpdate.Enabled = False
            Else
                btnRemove.Enabled = True
                btnListUpdate.Enabled = True
            End If
            Me.btnAdd.Enabled = False
            Dim row As DataGridViewRow = Me.DataGridView1.SelectedRows.Item(0)
            Me.txtProductCode.Text = (row.Cells.Item(0).Value)
            Me.txtProductName.Text = (row.Cells.Item(1).Value)
            Me.txtBarcode.Text = (row.Cells.Item(2).Value)
            Me.txtCostPrice.Text = (row.Cells.Item(3).Value)
            Me.txtSellingPrice.Text = (row.Cells.Item(4).Value)
            Me.txtMargin.Text = (row.Cells.Item(5).Value)
            Me.txtQty.Text = (row.Cells.Item(6).Value)
            Me.txtAmount.Text = (row.Cells.Item(7).Value)
            Me.txtDiscountPer.Text = (row.Cells.Item(8).Value)
            Me.txtDiscountAmount.Text = (row.Cells.Item(9).Value)
            Me.txtVAT.Text = (row.Cells.Item(10).Value)
            Me.txtVATAmount.Text = (row.Cells.Item(11).Value)
            Me.txtTotalAmount.Text = (row.Cells.Item(12).Value)
            Me.txtProductID.Text = (row.Cells.Item(13).Value)
        End If
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
            Dim cl As String = "select Inv_ID from Invoiceinfo,SalesReturn where SalesReturn.SalesID=InvoiceInfo.Inv_ID and Inv_ID=@d1"
            cmd = New SqlCommand(cl)
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
            rdr = cmd.ExecuteReader()
            If rdr.Read Then
                MessageBox.Show("Unable to delete..Already in use in Sales Return", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If Not rdr Is Nothing Then
                    rdr.Close()
                End If
                Exit Sub
            End If
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cq As String = "delete from InvoiceInfo where Inv_ID=@d1"
            cmd = New SqlCommand(cq)
            cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
            cmd.Connection = con
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        con = New SqlConnection(cs)
                        con.Open()
                        Dim cb4 As String = "update Temp_stock set qty = qty + (" & row.Cells(6).Value & ") where ProductID=@d1 and Barcode=@d2"
                        cmd = New SqlCommand(cb4)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@d1", Val(row.Cells(13).Value))
                        cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End If
                Next
                LedgerDelete(txtInvoiceNo.Text, "Sales")
                LedgerDelete(txtInvoiceNo.Text, "Payment")
                Dim st As String = "deleted the bill (Products) having invoice no. '" & txtInvoiceNo.Text & "'"
                LogFunc(lblUser.Text, st)
                MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Reset()
                RefreshRecords()
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
    Sub Compute1()
        Dim i As Double = 0
        i = Val(txtGrandTotal.Text) - Val(txtTotalPayment.Text)
        i = Math.Round(i, 2)
        txtPaymentDue.Text = i
    End Sub
    Private Function GenerateID1() As String
        con = New SqlConnection(cs)
        Dim value As String = "0000"
        Try
            ' Fetch the latest ID from the database
            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 ID FROM Customer ORDER BY ID DESC", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr.HasRows Then
                rdr.Read()
                value = rdr.Item("ID")
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
    Sub auto1()
        Try
            txtCID.Text = GenerateID1()
            txtCustomerID.Text = "C-" + GenerateID1()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        If Len(Trim(txtSalesmanID.Text)) = 0 Then
            MessageBox.Show("Please retrieve salesman id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Button1.Focus()
            Exit Sub
        End If
        If Len(Trim(txtCustomerName.Text)) = 0 Then
            MessageBox.Show("Please retrieve or enter customer details", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Len(Trim(txtContactNo.Text)) = 0 Then
            MessageBox.Show("Please Enter Contact No.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtContactNo.Focus()
            Exit Sub
        End If
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("sorry no product added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If DataGridView2.Rows.Count = 0 Then
            MessageBox.Show("sorry no payment info added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Val(txtTotalPayment.Text) > Val(txtGrandTotal.Text) Then
            MessageBox.Show("Total payment can not be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            con = New SqlConnection(cs)
            con.Open()
            Dim ctn1 As String = "select * from Company"
            cmd = New SqlCommand(ctn1)
            cmd.Connection = con
            rdr = cmd.ExecuteReader()

            If Not rdr.Read() Then
                MessageBox.Show("Add company profile first in master entry", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                Return
            End If
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim con As New SqlConnection(cs)
                con.Open()
                Dim cmd As New SqlCommand("SELECT Qty from Temp_Stock where ProductID=@d1 and Barcode=@d2", con)
                cmd.Parameters.AddWithValue("@d1", Val(row.Cells(13).Value))
                cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As DataSet = New DataSet()
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtTotalQty.Text = ds.Tables(0).Rows(0)("Qty")
                    If CInt(Val(row.Cells(6).Value)) > Val(txtTotalQty.Text) Then
                        MessageBox.Show("added qty. to cart are more than" & vbCrLf & "available qty. of product code '" & row.Cells(0).Value.ToString() & "' and Product Name='" & row.Cells(1).Value & "' having barcode='" & row.Cells(2).Value & "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                con.Close()
            Next
            If txtCustomerName.ReadOnly = False Then
                auto1()
                con = New SqlConnection(cs)
                con.Open()
                Dim cbn As String = "insert into Customer(ID, CustomerID, [Name], Gender, Address, City, ContactNo, EmailID,Remarks,State,ZipCode,Photo,CustomerType) Values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,'Non Regular')"
                cmd = New SqlCommand(cbn)
                cmd.Parameters.AddWithValue("@d1", Val(txtCID.Text))
                cmd.Parameters.AddWithValue("@d2", txtCustomerID.Text)
                cmd.Parameters.AddWithValue("@d3", txtCustomerName.Text)
                cmd.Parameters.AddWithValue("@d4", "")
                cmd.Parameters.AddWithValue("@d5", "")
                cmd.Parameters.AddWithValue("@d6", "")
                cmd.Parameters.AddWithValue("@d7", txtContactNo.Text)
                cmd.Parameters.AddWithValue("@d8", "")
                cmd.Parameters.AddWithValue("@d9", "")
                cmd.Parameters.AddWithValue("@d10", "")
                cmd.Parameters.AddWithValue("@d11", "")
                cmd.Connection = con
                Dim ms As New MemoryStream()
                Dim bmpImage As New Bitmap(My.Resources.photo)
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@d12", SqlDbType.Image)
                p.Value = data
                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()
                con.Close()
                txtCustomerType.Text = "Non Regular"
            End If

            con = New SqlConnection(cs)
            con.Open()
            Dim cb As String = "insert into InvoiceInfo( Inv_ID, InvoiceNo, InvoiceDate, CustomerID, GrandTotal, TotalPaid, Balance, Remarks,SalesmanID) Values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8," & txtSM_ID.Text & ")"
            cmd = New SqlCommand(cb)
            cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
            cmd.Parameters.AddWithValue("@d2", txtInvoiceNo.Text)
            cmd.Parameters.AddWithValue("@d3", dtpInvoiceDate.Value.Date)
            cmd.Parameters.AddWithValue("@d4", Val(txtCID.Text))
            cmd.Parameters.AddWithValue("@d5", Val(txtGrandTotal.Text))
            cmd.Parameters.AddWithValue("@d6", Val(txtTotalPayment.Text))
            cmd.Parameters.AddWithValue("@d7", Val(txtPaymentDue.Text))
            cmd.Parameters.AddWithValue("@d8", txtRemarks.Text)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()
            Dim sum As Decimal = 0
            Dim commission As Decimal
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(7).Value
            Next
            commission = Val((sum * Val(txtCommissionPer.Text)) / 100)
            con = New SqlConnection(cs)
            con.Open()
            Dim sql As String = "insert into Salesman_Commission(InvoiceID,CommissionPer,Commission) Values (" & Val(txtID.Text) & "," & Val(txtCommissionPer.Text) & "," & Val(commission) & ")"
            cmd = New SqlCommand(sql)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cb1 As String = "insert into Invoice_Product(InvoiceID,Barcode, CostPrice, SellingPrice, Margin, Qty, Amount, DiscountPer, Discount, VATPer, VAT, TotalAmount,ProductID) VALUES (" & txtID.Text & " ,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15)"
            cmd = New SqlCommand(cb1)
            cmd.Connection = con
            ' Prepare command for repeated execution
            cmd.Prepare()
            ' Data to be inserted
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    cmd.Parameters.AddWithValue("@d4", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@d5", Val(row.Cells(3).Value))
                    cmd.Parameters.AddWithValue("@d6", Val(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@d7", Val(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@d8", Val(row.Cells(6).Value))
                    cmd.Parameters.AddWithValue("@d9", Val(row.Cells(7).Value))
                    cmd.Parameters.AddWithValue("@d10", Val(row.Cells(8).Value))
                    cmd.Parameters.AddWithValue("@d11", Val(row.Cells(9).Value))
                    cmd.Parameters.AddWithValue("@d12", Val(row.Cells(10).Value))
                    cmd.Parameters.AddWithValue("@d13", Val(row.Cells(11).Value))
                    cmd.Parameters.AddWithValue("@d14", Val(row.Cells(12).Value))
                    cmd.Parameters.AddWithValue("@d15", Val(row.Cells(13).Value))
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If
            Next
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cb2 As String = "insert into Invoice_Payment(InvoiceID,PaymentMode,TotalPaid,PaymentDate) VALUES (" & txtID.Text & " ,@d4,@d5,@d6)"
            cmd = New SqlCommand(cb2)
            cmd.Connection = con
            ' Prepare command for repeated execution
            cmd.Prepare()
            ' Data to be inserted
            For Each row As DataGridViewRow In DataGridView2.Rows
                If Not row.IsNewRow Then
                    cmd.Parameters.AddWithValue("@d4", row.Cells(0).Value)
                    cmd.Parameters.AddWithValue("@d5", Val(row.Cells(1).Value))
                    cmd.Parameters.AddWithValue("@d6", row.Cells(2).Value)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If
            Next
            con.Close()
            LedgerSave(dtpInvoiceDate.Value.Date, txtCustomerName.Text, txtInvoiceNo.Text, "Sales", Val(txtGrandTotal.Text), 0, txtCustomerID.Text)
            For Each row As DataGridViewRow In DataGridView2.Rows
                If Not row.IsNewRow Then
                    If row.Cells(0).Value = "By Cash" Then
                        LedgerSave(Convert.ToDateTime(row.Cells(2).Value), "Cash Account", txtInvoiceNo.Text, "Payment", 0, Val(row.Cells(1).Value), txtCustomerID.Text)
                    End If
                    If row.Cells(0).Value = "By Cheque" Or row.Cells(0).Value = "By Credit Card" Or row.Cells(0).Value = "By Debit Card" Then
                        LedgerSave(Convert.ToDateTime(row.Cells(2).Value), "Bank Account", txtInvoiceNo.Text, "Payment", 0, Val(row.Cells(1).Value), txtCustomerID.Text)
                    End If
                End If
            Next
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    con = New SqlConnection(cs)
                    con.Open()
                    Dim cb4 As String = "update Temp_stock set qty = qty - (" & row.Cells(6).Value & ") where ProductID=@d1 and Barcode=@d2"
                    cmd = New SqlCommand(cb4)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@d1", Val(row.Cells(13).Value))
                    cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                    cmd.ExecuteNonQuery()
                    con.Close()
                End If
            Next
            con.Close()
            Dim st As String = "added the new bill (Products) having invoice no. '" & txtInvoiceNo.Text & "'"
            LogFunc(lblUser.Text, st)
            If CheckForInternetConnection() = True Then
                con = New SqlConnection(cs)
                con.Open()
                Dim ctn As String = "select RTRIM(APIURL) from SMSSetting where IsDefault='Yes' and IsEnabled='Yes'"
                cmd = New SqlCommand(ctn)
                cmd.Connection = con
                rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    st2 = rdr.GetValue(0)
                    Dim st3 As String = "Hello, " & txtCustomerName.Text & " you have successfully purchased the products having invoice no. " & txtInvoiceNo.Text & ""
                    SMSFunc(txtContactNo.Text, st3, st2)
                    SMS(st3)
                    If (rdr IsNot Nothing) Then
                        rdr.Close()
                    End If
                End If
            End If
            con.Close()
            btnSave.Enabled = False
            RefreshRecords()
            MessageBox.Show("Successfully done", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Print()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click
        If Len(Trim(txtSalesmanID.Text)) = 0 Then
            MessageBox.Show("Please retrieve salesman id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Button1.Focus()
            Exit Sub
        End If
        If Len(Trim(txtCustomerName.Text)) = 0 Then
            MessageBox.Show("Please retrieve customer details", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("sorry no product added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If DataGridView2.Rows.Count = 0 Then
            MessageBox.Show("sorry no payment info added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Val(txtTotalPayment.Text) > Val(txtGrandTotal.Text) Then
            MessageBox.Show("Total payment can not be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            con = New SqlConnection(cs)
            con.Open()
            Dim cb As String = "Update InvoiceInfo set InvoiceNo=@d2, CustomerID=@d4, GrandTotal=@d5, TotalPaid=@d6, Balance=@d7, Remarks=@d8,SalesmanID=" & txtSM_ID.Text & " where INV_ID=@d1"
            cmd = New SqlCommand(cb)
            cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
            cmd.Parameters.AddWithValue("@d2", txtInvoiceNo.Text)
            cmd.Parameters.AddWithValue("@d4", Val(txtCID.Text))
            cmd.Parameters.AddWithValue("@d5", Val(txtGrandTotal.Text))
            cmd.Parameters.AddWithValue("@d6", Val(txtTotalPayment.Text))
            cmd.Parameters.AddWithValue("@d7", Val(txtPaymentDue.Text))
            cmd.Parameters.AddWithValue("@d8", txtRemarks.Text)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cq As String = "delete from Invoice_Payment where InvoiceID=@d1"
            cmd = New SqlCommand(cq)
            cmd.Parameters.AddWithValue("@d1", Val(txtID.Text))
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cb2 As String = "insert into Invoice_Payment(InvoiceID,PaymentMode,TotalPaid,PaymentDate) VALUES (" & txtID.Text & " ,@d4,@d5,@d6)"
            cmd = New SqlCommand(cb2)
            cmd.Connection = con
            ' Prepare command for repeated execution
            cmd.Prepare()
            ' Data to be inserted
            For Each row As DataGridViewRow In DataGridView2.Rows
                If Not row.IsNewRow Then
                    cmd.Parameters.AddWithValue("@d4", row.Cells(0).Value)
                    cmd.Parameters.AddWithValue("@d5", Val(row.Cells(1).Value))
                    cmd.Parameters.AddWithValue("@d6", row.Cells(2).Value)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If
            Next
            con.Close()
            LedgerDelete(txtInvoiceNo.Text, "Sales")
            LedgerDelete(txtInvoiceNo.Text, "Payment")
            LedgerSave(dtpInvoiceDate.Value.Date, txtCustomerName.Text, txtInvoiceNo.Text, "Sales", Val(txtGrandTotal.Text), 0, txtCustomerID.Text)
            For Each row As DataGridViewRow In DataGridView2.Rows
                If Not row.IsNewRow Then
                    If row.Cells(0).Value = "By Cash" Then
                        LedgerSave(Convert.ToDateTime(row.Cells(2).Value), "Cash Account", txtInvoiceNo.Text, "Payment", 0, Val(row.Cells(1).Value), txtCustomerID.Text)
                    End If
                    If row.Cells(0).Value = "By Cheque" Or row.Cells(0).Value = "By Credit Card" Or row.Cells(0).Value = "By Debit Card" Then
                        LedgerSave(Convert.ToDateTime(row.Cells(2).Value), "Bank Account", txtInvoiceNo.Text, "Payment", 0, Val(row.Cells(1).Value), txtCustomerID.Text)
                    End If
                End If
            Next
            Dim st As String = "updated the bill (Products) having invoice no. '" & txtInvoiceNo.Text & "'"
            LogFunc(lblUser.Text, st)
            btnUpdate.Enabled = False
            MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetData.Click
        frmSalesInvoiceRecord.lblSet.Text = "Sales Invoice"
        frmSalesInvoiceRecord.Reset()
        frmSalesInvoiceRecord.ShowDialog()
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub


    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Cursor = Cursors.Default
        Timer1.Enabled = False
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub

    Private Sub btnAdd1_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd1.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("sorry no product added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If cmbPaymentMode.Text = "" Then
                MessageBox.Show("Please select payment mode", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbPaymentMode.Focus()
                Exit Sub
            End If
            If txtPayment.Text = "" Then
                MessageBox.Show("Please enter payment", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPayment.Focus()
                Exit Sub
            End If
            DataGridView2.Rows.Add(cmbPaymentMode.Text, Val(txtPayment.Text), dtpPaymentDate.Value.Date)
            Dim j As Double = 0
            j = TotalPayment()
            j = Math.Round(j, 2)
            txtTotalPayment.Text = j
            Compute1()
            Clear1()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Clear1()
        cmbPaymentMode.SelectedIndex = -1
        txtPayment.Text = "0.00"
        dtpPaymentDate.Text = Today
        btnAdd1.Enabled = True
        btnRemove1.Enabled = False
        btnListUpdate1.Enabled = False
    End Sub
    Private Sub btnRemove1_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove1.Click
        Try
            For Each row As DataGridViewRow In DataGridView2.SelectedRows
                DataGridView2.Rows.Remove(row)
            Next
            Dim k As Double = 0
            k = TotalPayment()
            k = Math.Round(k, 2)
            txtTotalPayment.Text = k
            Compute1()
            Compute()
            Clear1()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPayment_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtPayment.Text
            Dim selectionStart = Me.txtPayment.SelectionStart
            Dim selectionLength = Me.txtPayment.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                'Reject an integer that is longer than 16 digits.
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                'Reject a real number with two many decimal places.
                e.Handled = False
            End If
        Else
            'Reject all other characters.
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView2_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView2.MouseClick
        btnRemove1.Enabled = True
        If (Me.DataGridView2.Rows.Count > 0) Then
            Me.btnRemove1.Enabled = True
            Me.btnListUpdate1.Enabled = True
            Me.btnAdd1.Enabled = False
            Dim row As DataGridViewRow = Me.DataGridView2.SelectedRows.Item(0)
            Me.cmbPaymentMode.Text = (row.Cells.Item(0).Value)
            Me.txtPayment.Text = (row.Cells.Item(1).Value)
            Me.dtpPaymentDate.Text = (row.Cells.Item(2).Value)
        End If
    End Sub

    Private Sub btnListReset1_Click(sender As System.Object, e As System.EventArgs) Handles btnListReset1.Click
        Clear1()
    End Sub

    Private Sub btnListReset_Click(sender As System.Object, e As System.EventArgs) Handles btnListReset.Click
        Clear()
    End Sub

    Private Sub DataGridView2_RowPostPaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Dim strRowNumber As String = (e.RowIndex + 1).ToString()
        Dim size As SizeF = e.Graphics.MeasureString(strRowNumber, Me.Font)
        If DataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)) Then
            DataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ButtonHighlight
        e.Graphics.DrawString(strRowNumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))

    End Sub

    Private Sub btnListUpdate1_Click(sender As System.Object, e As System.EventArgs) Handles btnListUpdate1.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("sorry no product added to cart", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If cmbPaymentMode.Text = "" Then
                MessageBox.Show("Please select payment mode", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbPaymentMode.Focus()
                Exit Sub
            End If
            If txtPayment.Text = "" Then
                MessageBox.Show("Please enter payment", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPayment.Focus()
                Exit Sub
            End If
            For Each row As DataGridViewRow In DataGridView2.SelectedRows
                DataGridView2.Rows.Remove(row)
            Next
            DataGridView2.Rows.Add(cmbPaymentMode.Text, Val(txtPayment.Text), dtpPaymentDate.Value.Date)
            Dim j As Double = 0
            j = TotalPayment()
            j = Math.Round(j, 2)
            txtTotalPayment.Text = j
            Compute1()
            Clear1()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnListUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnListUpdate.Click
        Try
            If txtProductCode.Text = "" Then
                MessageBox.Show("Please retrieve product code", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtProductCode.Focus()
                Exit Sub
            End If
            If txtBarcode.Text = "" Then
                MessageBox.Show("Please retrieve barcode", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtBarcode.Focus()
                Exit Sub
            End If
            If Len(Trim(txtSellingPrice.Text)) = 0 Then
                MessageBox.Show("Please enter price", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtSellingPrice.Focus()
                Exit Sub
            End If
            If Len(Trim(txtDiscountPer.Text)) = 0 Then
                MessageBox.Show("Please enter discount %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtDiscountPer.Focus()
                Exit Sub
            End If
            If Len(Trim(txtVAT.Text)) = 0 Then
                MessageBox.Show("Please enter vat %", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtVAT.Focus()
                Exit Sub
            End If
            If txtQty.Text = "" Then
                MessageBox.Show("Please enter quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If
            If txtQty.Text = 0 Then
                MessageBox.Show("Quantity can not be zero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If

            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next
            DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtCostPrice.Text), Val(txtSellingPrice.Text), Val(txtMargin.Text), Val(txtQty.Text), Val(txtAmount.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVAT.Text), Val(txtVATAmount.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text))
            Dim k As Double = 0
            k = GrandTotal()
            k = Math.Round(k, 2)
            txtGrandTotal.Text = k
            Compute1()
            Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtSellingPrice_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSellingPrice.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtSellingPrice.Text
            Dim selectionStart = Me.txtSellingPrice.SelectionStart
            Dim selectionLength = Me.txtSellingPrice.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                'Reject an integer that is longer than 16 digits.
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                'Reject a real number with two many decimal places.
                e.Handled = False
            End If
        Else
            'Reject all other characters.
            e.Handled = True
        End If
    End Sub

    Private Sub txtDiscountPer_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscountPer.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtDiscountPer.Text
            Dim selectionStart = Me.txtDiscountPer.SelectionStart
            Dim selectionLength = Me.txtDiscountPer.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                'Reject an integer that is longer than 16 digits.
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                'Reject a real number with two many decimal places.
                e.Handled = False
            End If
        Else
            'Reject all other characters.
            e.Handled = True
        End If
    End Sub

    Private Sub txtVAT_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtVAT.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtVAT.Text
            Dim selectionStart = Me.txtVAT.SelectionStart
            Dim selectionLength = Me.txtVAT.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                'Reject an integer that is longer than 16 digits.
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                'Reject a real number with two many decimal places.
                e.Handled = False
            End If
        Else
            'Reject all other characters.
            e.Handled = True
        End If
    End Sub

    Private Sub txtDiscountPer_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDiscountPer.TextChanged
        Compute()
    End Sub

    Private Sub txtVAT_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVAT.TextChanged
        Compute()
    End Sub

    Private Sub txtSellingPrice_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSellingPrice.TextChanged
        Compute()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        frmSalesmanRecord.lblSet.Text = "Billing"
        frmSalesmanRecord.Reset()
        frmSalesmanRecord.ShowDialog()
    End Sub

    Private Sub cmbPaymentMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPaymentMode.SelectedIndexChanged
        If cmbPaymentMode.SelectedIndex = 4 Or cmbPaymentMode.SelectedIndex = 5 Or cmbPaymentMode.SelectedIndex = 6 Or cmbPaymentMode.SelectedIndex = 7 Or cmbPaymentMode.SelectedIndex = 8 Or cmbPaymentMode.SelectedIndex = 9 Then
            txtPayment.Text = "0.00"
            txtPayment.ReadOnly = True
        Else
            txtPayment.ReadOnly = False
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        lblDateTime.Text = Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt")
    End Sub

    Private Sub btnMinimize_Click(sender As System.Object, e As System.EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMaximise_Click(sender As System.Object, e As System.EventArgs) Handles btnMaximise.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            Me.StartPosition = FormStartPosition.CenterScreen
            btnMaximise.Image = My.Resources.Maximise_32X32
        Else
            Me.WindowState = FormWindowState.Maximized
            btnMaximise.Image = My.Resources.User_Interface_Restore_Window_icon__1_
        End If
    End Sub

    Private Sub btnKeyboard_Click(sender As System.Object, e As System.EventArgs) Handles btnKeyboard.Click
        OSKeyboard()
    End Sub
    Sub OSKeyboard()
        Dim old As Long
        If Environment.Is64BitOperatingSystem Then
            If Wow64DisableWow64FsRedirection(old) Then
                Process.Start("osk.exe")
                Wow64EnableWow64FsRedirection(old)
            End If
        Else
            Process.Start("osk.exe")
        End If
    End Sub
   
    Private Sub txtBarcode_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBarcode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                con = New SqlConnection(cs)
                con.Open()
                cmd = con.CreateCommand()
                cmd.CommandText = "SELECT PID, RTRIM(Product.ProductCode),RTRIM(ProductName),(CostPrice),(SellingPrice),(Discount),(VAT),RTRIM(SalesUnit) from Temp_Stock,Product where Product.PID=Temp_Stock.ProductID and Qty > 0 and Temp_Stock.Barcode=@d1 "
                cmd.Parameters.AddWithValue("@d1", txtBarcode.Text)
                rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    txtProductID.Text = rdr.GetValue(0)
                    txtProductCode.Text = rdr.GetValue(1)
                    txtProductName.Text = rdr.GetValue(2)
                    txtCostPrice.Text = rdr.GetValue(3)
                    txtSellingPrice.Text = rdr.GetValue(4)
                    txtDiscountPer.Text = rdr.GetValue(5)
                    txtVAT.Text = rdr.GetValue(6)
                    lblUnit.Text = rdr.GetValue(7)
                    txtQty.Text = 1
                    If DataGridView1.Rows.Count = 0 Then
                        DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtCostPrice.Text), Val(txtSellingPrice.Text), Val(txtMargin.Text), Val(txtQty.Text), Val(txtAmount.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVAT.Text), Val(txtVATAmount.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text))
                        Dim k As Double = 0
                        k = GrandTotal()
                        k = Math.Round(k, 2)
                        txtGrandTotal.Text = k
                        Compute1()
                        Clear()
                        Exit Sub
                    End If
                    For Each r As DataGridViewRow In DataGridView1.Rows
                        If r.Cells(0).Value = txtProductCode.Text And r.Cells(2).Value = txtBarcode.Text Then
                            r.Cells(0).Value = txtProductCode.Text
                            r.Cells(1).Value = txtProductName.Text
                            r.Cells(2).Value = txtBarcode.Text
                            r.Cells(3).Value = Val(txtCostPrice.Text)
                            r.Cells(4).Value = Val(txtSellingPrice.Text)
                            r.Cells(5).Value = Val(txtMargin.Text)
                            r.Cells(6).Value = Val(r.Cells(6).Value) + Val(txtQty.Text)
                            r.Cells(7).Value = Val(r.Cells(7).Value) + Val(txtAmount.Text)
                            r.Cells(8).Value = Val(txtDiscountPer.Text)
                            r.Cells(9).Value = Val(r.Cells(9).Value) + Val(txtDiscountAmount.Text)
                            r.Cells(10).Value = Val(txtVAT.Text)
                            r.Cells(11).Value = Val(r.Cells(11).Value) + Val(txtVATAmount.Text)
                            r.Cells(12).Value = Val(r.Cells(12).Value) + Val(txtTotalAmount.Text)
                            r.Cells(13).Value = Val(txtProductID.Text)
                            Dim i As Double = 0
                            i = GrandTotal()
                            i = Math.Round(i, 2)
                            txtGrandTotal.Text = i
                            Compute1()
                            Clear()
                            Exit Sub
                        End If
                    Next
                    DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtCostPrice.Text), Val(txtSellingPrice.Text), Val(txtMargin.Text), Val(txtQty.Text), Val(txtAmount.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVAT.Text), Val(txtVATAmount.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text))
                    Dim j As Double = 0
                    j = GrandTotal()
                    j = Math.Round(j, 2)
                    txtGrandTotal.Text = j
                    Compute1()
                    Clear()
                End If
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub btnScanItems_Click(sender As System.Object, e As System.EventArgs) Handles btnScanItems.Click
        txtBarcode.Focus()
    End Sub

    Private Sub txtBarcode_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcode.KeyPress
        If (e.KeyChar < Chr(48) Or e.KeyChar > Chr(57)) And e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
        frmMainMenu.lblUser.Text = lblUser.Text
        frmMainMenu.lblUserType.Text = lblUserType.Text
        frmMainMenu.Show()
    End Sub

    Private Sub txtPaymentDue_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtPaymentDue.TextChanged

    End Sub

    Private Sub Panel3_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class
