Imports System.Data.SqlClient
Imports System.IO

Public Class frmSalesReturn
    Dim str As String
    Dim st As String
    Private Function GenerateID() As String
        con = New SqlConnection(cs)
        Dim value As String = "0000"
        Try
            ' Fetch the latest ID from the database
            con.Open()
            cmd = New SqlCommand("SELECT TOP 1 SR_ID FROM SalesReturn ORDER BY SR_ID DESC", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If rdr.HasRows Then
                rdr.Read()
                value = rdr.Item("SR_ID")
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
    Sub Reset()
        txtSRNO.Text = ""
        txtSRID.Text = ""
        dtpSRDate.Text = Today
        dtpSalesDate.Text = Today
        txtSalesID.Text = ""
        txtSalesInvoiceNo.Text = ""
        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        txtcust_ID.Text = ""
        txtGrandTotal.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = False
        DataGridView1.Enabled = True
        btnAdd.Enabled = True
        pnlCalc.Enabled = True
        btnRemove.Enabled = False
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        Clear()
        btnSelection.Enabled = True
        lblSet.Text = ""
        auto()
    End Sub

    Sub auto()
        Try
            txtSRID.Text = GenerateID()
            txtSRNO.Text = "SR-" + GenerateID()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub btnSelection_Click(sender As System.Object, e As System.EventArgs) Handles btnSelection.Click
        frmSalesInvoiceRecord.lblSet.Text = "Sales Return"
        frmSalesInvoiceRecord.Reset()
        frmSalesInvoiceRecord.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Public Function GrandTotal() As Double
        Dim sum As Double = 0
        Try
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                sum = sum + r.Cells(10).Value
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return sum
    End Function
    Sub Clear()
        txtBarcode.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtQty.Text = ""
        txtPrice.Text = ""
        txtDiscountPer.Text = ""
        txtDiscountAmount.Text = ""
        txtVATPer.Text = ""
        txtVATAmount.Text = ""
        txtReturnQty.Text = ""
        txtTotalAmount.Text = ""
        txtCostPrice.Text = ""
        txtMargin.Text = ""
        btnAdd.Enabled = True
        btnRemove.Enabled = False
    End Sub
    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            If txtProductName.Text = "" Then
                MessageBox.Show("Please retrieve product name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtProductName.Focus()
                Exit Sub
            End If
            If txtBarcode.Text = "" Then
                MessageBox.Show("Please retrieve Barcode", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtBarcode.Focus()
                Exit Sub
            End If
            If txtQty.Text = "" Then
                MessageBox.Show("Please retrieve quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If
            If Val(txtQty.Text = 0) Then
                MessageBox.Show("Quantity can not be zero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtQty.Focus()
                Exit Sub
            End If
            If txtReturnQty.Text = "" Then
                MessageBox.Show("Please Enter Return Quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReturnQty.Focus()
                Exit Sub
            End If
            If Val(txtReturnQty.Text) = 0 Then
                MessageBox.Show("Return quantity can not be zero", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReturnQty.Focus()
                Exit Sub
            End If
            If Val(txtReturnQty.Text) > Val(txtQty.Text) Then
                MessageBox.Show("Return Quantity can not be greater than Sales quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtReturnQty.Text = ""
                txtReturnQty.Focus()
                Exit Sub
            End If
            If DataGridView1.Rows.Count = 0 Then
                DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtPrice.Text), Val(txtQty.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVATPer.Text), Val(txtVATAmount.Text), Val(txtReturnQty.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text), Val(txtCostPrice.Text), Val(txtMargin.Text) * Val(txtReturnQty.Text))
                Dim k As Double = 0
                k = GrandTotal()
                k = Math.Round(k, 2)
                txtGrandTotal.Text = k
                Clear()
                Exit Sub
            End If
            For Each row As DataGridViewRow In DataGridView1.Rows
                If txtBarcode.Text = row.Cells(2).Value Then
                    MessageBox.Show("Same barcode already added in grid", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtBarcode.Focus()
                    Exit Sub
                End If
                If txtBarcode.Text = row.Cells(2).Value And txtProductID.Text = row.Cells(11).Value Then
                    MessageBox.Show("Record already added in grid", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtBarcode.Focus()
                    Exit Sub
                End If
            Next
            DataGridView1.Rows.Add(txtProductCode.Text, txtProductName.Text, txtBarcode.Text, Val(txtPrice.Text), Val(txtQty.Text), Val(txtDiscountPer.Text), Val(txtDiscountAmount.Text), Val(txtVATPer.Text), Val(txtVATAmount.Text), Val(txtReturnQty.Text), Val(txtTotalAmount.Text), Val(txtProductID.Text), Val(txtCostPrice.Text), Val(txtMargin.Text) * Val(txtReturnQty.Text))
            Dim k1 As Double = 0
            k1 = GrandTotal()
            k1 = Math.Round(k1, 2)
            txtGrandTotal.Text = k1
            Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            btnRemove.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtRetuenQty_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtReturnQty.TextChanged
        Dim i As Double = 0
        i = CDbl(Val(txtReturnQty.Text) * Val(txtPrice.Text))
        i = Math.Round(i, 2)
        txtTotalAmount.Text = i
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            If Len(Trim(txtSalesInvoiceNo.Text)) = 0 Then
                MessageBox.Show("Please retrieve Sales Info", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtSalesInvoiceNo.Focus()
                Exit Sub
            End If
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("Sorry no returned product info added to grid", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            con = New SqlConnection(cs)
            con.Open()
            Dim ct As String = "select SalesID from SalesReturn where SalesID=@d1"
            cmd = New SqlCommand(ct)
            cmd.Parameters.AddWithValue("@d1", Val(txtSalesID.Text))
            cmd.Connection = con
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                MessageBox.Show("The items from selected Sales have been Already Returned", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                Return
            End If
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cb As String = "insert into  SalesReturn(SR_ID, SRNo, Date,SalesID,GrandTotal) VALUES (@d1,@d2,@d3,@d4,@d5)"
            cmd = New SqlCommand(cb)
            cmd.Parameters.AddWithValue("@d1", Val(txtSRID.Text))
            cmd.Parameters.AddWithValue("@d2", txtSRNO.Text)
            cmd.Parameters.AddWithValue("@d3", dtpSRDate.Value.Date)
            cmd.Parameters.AddWithValue("@d4", Val(txtSalesID.Text))
            cmd.Parameters.AddWithValue("@d5", Val(txtGrandTotal.Text))
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
            con = New SqlConnection(cs)
            con.Open()
            Dim cb1 As String = "insert into SalesReturn_Join(SalesReturnID,Barcode,Price, Qty, DiscountPer, Discount, VATPer, VAT,ReturnQty, TotalAmount,ProductID,CostPrice,Margin) VALUES (" & txtSRID.Text & ",@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)"
            cmd = New SqlCommand(cb1)
            cmd.Connection = con
            ' Prepare command for repeated execution
            cmd.Prepare()
            ' Data to be inserted
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@d3", Val(row.Cells(3).Value))
                    cmd.Parameters.AddWithValue("@d4", Val(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@d5", Val(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@d6", Val(row.Cells(6).Value))
                    cmd.Parameters.AddWithValue("@d7", Val(row.Cells(7).Value))
                    cmd.Parameters.AddWithValue("@d8", Val(row.Cells(8).Value))
                    cmd.Parameters.AddWithValue("@d9", Val(row.Cells(9).Value))
                    cmd.Parameters.AddWithValue("@d10", Val(row.Cells(10).Value))
                    cmd.Parameters.AddWithValue("@d11", Val(row.Cells(11).Value))
                    cmd.Parameters.AddWithValue("@d12", Val(row.Cells(12).Value))
                    cmd.Parameters.AddWithValue("@d13", Val(row.Cells(13).Value))
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End If
            Next
            con.Close()
            For Each row As DataGridViewRow In DataGridView1.Rows
                con = New SqlConnection(cs)
                con.Open()
                Dim cb2 As String = "Update Temp_Stock set Qty=Qty + " & Val(row.Cells(9).Value) & " where ProductID=@d1 and Barcode=@d2"
                cmd = New SqlCommand(cb2)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@d1", Val(row.Cells(11).Value))
                cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                cmd.ExecuteReader()
                con.Close()
            Next
            LedgerSave(dtpSRDate.Value.Date, txtCustomerName.Text, txtSRNO.Text, "Sales Return", Val(txtGrandTotal.Text), 0, txtCustomerID.Text)
            LedgerSave(dtpSRDate.Value.Date, "Cash Account", txtSRNO.Text, "Sales Return from " & txtCustomerName.Text & "", 0, Val(txtGrandTotal.Text), txtCustomerID.Text)
            LogFunc(lblUser.Text, "added the new Sales return record having SR No. '" & txtSRNO.Text & "'")
            MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnSave.Enabled = False
            con.Close()
            RefreshRecords()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteRecord()
        Try
            Dim RowsAffected As Integer = 0
            con = New SqlConnection(cs)
            con.Open()
            Dim cq As String = "delete from SalesReturn where SR_ID=@d1"
            cmd = New SqlCommand(cq)
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@d1", Val(txtSRID.Text))
            RowsAffected = cmd.ExecuteNonQuery()
            If RowsAffected > 0 Then
                For Each row As DataGridViewRow In DataGridView1.Rows
                    con = New SqlConnection(cs)
                    con.Open()
                    Dim cb2 As String = "Update Temp_Stock set Qty=Qty - " & Val(row.Cells(9).Value) & " where ProductID=@d1 and Barcode=@d2"
                    cmd = New SqlCommand(cb2)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@d1", Val(row.Cells(11).Value))
                    cmd.Parameters.AddWithValue("@d2", row.Cells(2).Value)
                    cmd.ExecuteReader()
                    con.Close()
                Next
                LedgerDelete(txtSRNO.Text, "Sales Return From " & txtCustomerName.Text & "")
                LedgerDelete(txtSRNO.Text, "Sales Return")
                Dim st As String = "deleted the Sales Return record having SR No. '" & txtSRNO.Text & "'"
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
    Private Sub txtReturnQty_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtReturnQty.KeyPress
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
            'Allow all control characters.
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = Me.txtReturnQty.Text
            Dim selectionStart = Me.txtReturnQty.SelectionStart
            Dim selectionLength = Me.txtReturnQty.SelectionLength

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

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            If MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                DeleteRecord()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        Reset()

    End Sub

    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetData.Click
        Me.Reset()
        frmSalesReturnRecord.lblSet.Text = "SR"
        frmSalesReturnRecord.Reset()
        frmSalesReturnRecord.ShowDialog()
    End Sub

    Private Sub DataGridView2_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView2.MouseClick
        Try
            If DataGridView2.Rows.Count > 0 Then
                Clear()
                Dim dr As DataGridViewRow = DataGridView2.SelectedRows(0)
                txtProductID.Text = dr.Cells(11).Value.ToString()
                txtProductCode.Text = dr.Cells(0).Value.ToString()
                txtProductName.Text = dr.Cells(1).Value.ToString()
                txtBarcode.Text = dr.Cells(2).Value.ToString()
                txtPrice.Text = dr.Cells(3).Value.ToString()
                txtQty.Text = dr.Cells(4).Value.ToString()
                txtDiscountPer.Text = dr.Cells(6).Value.ToString()
                txtDiscountAmount.Text = dr.Cells(7).Value.ToString()
                txtVATPer.Text = dr.Cells(8).Value.ToString()
                txtVATAmount.Text = dr.Cells(9).Value.ToString()
                txtCostPrice.Text = dr.Cells(12).Value.ToString()
                txtMargin.Text = Val(dr.Cells(13).Value) / Val(dr.Cells(4).Value)
                txtReturnQty.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub DataGridView1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick
        If DataGridView1.Rows.Count > 0 Then
            If lblSet.Text = "Not allowed" Then
                btnRemove.Enabled = False
            Else
                btnRemove.Enabled = True
            End If
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

    Private Sub frmSalesReturn_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
