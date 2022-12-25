Imports System.Data.SqlClient

Imports System.IO

Public Class frmPurchaseOrderRecord1

    Public Sub Getdata()
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT Rtrim(PurchaseOrder.PO_ID),Rtrim(PurchaseOrder.PONumber),PurchaseOrder.Date,Rtrim(PurchaseOrder.Supplier_ID),Rtrim(Supplier.SupplierID),Rtrim(Supplier.Name),Rtrim(Supplier.Address),Rtrim(Supplier.City),Rtrim(Supplier.ContactNo),Rtrim(PurchaseOrder.Terms),Rtrim(PurchaseOrder.SubTotal),DiscPer,DiscAmt,VatPer,VatAmount,GrandTotal,RTRIM(PO_Status) FROM PurchaseOrder  INNER JOIN Supplier ON PurchaseOrder.Supplier_ID = Supplier.ID where PO_Status='Issued to supplier' order by Date", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12), rdr(13), rdr(14), rdr(15), rdr(16))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub frmLogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Getdata()
    End Sub

    Private Sub dgw_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles dgw.MouseClick
        Try
            If dgw.Rows.Count > 0 Then
                    Dim dr As DataGridViewRow = dgw.SelectedRows(0)
                frmPurchaseEntry.Reset()
                frmPurchaseEntry.Show()
                Me.Hide()
                frmPurchaseEntry.txtPurchaseOrderID.Text = dr.Cells(1).Value.ToString()
                frmPurchaseEntry.txtSup_ID.Text = dr.Cells(3).Value.ToString()
                frmPurchaseEntry.txtSupplierID.Text = dr.Cells(4).Value.ToString()
                frmPurchaseEntry.txtSupplierName.Text = dr.Cells(5).Value.ToString()
                frmPurchaseEntry.txtAddress.Text = dr.Cells(6).Value.ToString()
                frmPurchaseEntry.txtCity.Text = dr.Cells(7).Value.ToString()
                frmPurchaseEntry.txtContactNo.Text = dr.Cells(8).Value.ToString()
                frmPurchaseEntry.txtSubTotal.Text = dr.Cells(10).Value.ToString()
                frmPurchaseEntry.txtDiscPer.Text = dr.Cells(11).Value.ToString()
                frmPurchaseEntry.txtDisc.Text = dr.Cells(12).Value.ToString()
                frmPurchaseEntry.txtVATPer.Text = dr.Cells(13).Value.ToString()
                frmPurchaseEntry.txtVATAmt.Text = dr.Cells(14).Value.ToString()
                con = New SqlConnection(cs)
                con.Open()
                Dim sql As String = "SELECT PurchaseOrder_Join.ProductID,RTRIM(ProductCode), RTRIM(Product.ProductName),RTRIM(Product.Barcode), PurchaseOrder_Join.Qty, PurchaseOrder_Join.PricePerUnit, PurchaseOrder_Join.Amount FROM PurchaseOrder INNER JOIN PurchaseOrder_Join ON PurchaseOrder.PO_ID = PurchaseOrder_Join.PurchaseOrderID INNER JOIN Product ON PurchaseOrder_Join.ProductID = Product.PID  where PurchaseOrder_Join.PurchaseOrderID=" & dr.Cells(0).Value & ""
                cmd = New SqlCommand(sql, con)
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                frmPurchaseEntry.DataGridView1.Rows.Clear()
                While (rdr.Read() = True)
                    frmPurchaseEntry.DataGridView1.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6))
                End While
                con.Close()
                frmPurchaseEntry.GetSupplierInfo()
                frmPurchaseEntry.GetSupplierBalance()
                End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    Sub Reset()
        txtSupplierName.Text = ""
        dtpDateFrom.Text = Today
        dtpDateTo.Text = Today
        Getdata()
    End Sub



    Private Sub txtSupplierName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSupplierName.TextChanged

        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT Rtrim(PurchaseOrder.PO_ID),Rtrim(PurchaseOrder.PONumber),PurchaseOrder.Date,Rtrim(PurchaseOrder.Supplier_ID),Rtrim(Supplier.SupplierID),Rtrim(Supplier.Name),Rtrim(Supplier.Address),Rtrim(Supplier.City),Rtrim(Supplier.ContactNo),Rtrim(PurchaseOrder.Terms),Rtrim(PurchaseOrder.SubTotal),DiscPer,DiscAmt,VatPer,VatAmount,GrandTotal,RTRIM(PO_Status) FROM PurchaseOrder  INNER JOIN Supplier ON PurchaseOrder.Supplier_ID = Supplier.ID Where PO_Status='Issued to supplier' and Supplier.Name like '%" & txtSupplierName.Text & "%' order by Date", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12), rdr(13), rdr(14), rdr(15), rdr(16))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetData.Click

        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT Rtrim(PurchaseOrder.PO_ID),Rtrim(PurchaseOrder.PONumber),PurchaseOrder.Date,Rtrim(PurchaseOrder.Supplier_ID),Rtrim(Supplier.SupplierID),Rtrim(Supplier.Name),Rtrim(Supplier.Address),Rtrim(Supplier.City),Rtrim(Supplier.ContactNo),Rtrim(PurchaseOrder.Terms),Rtrim(PurchaseOrder.SubTotal),DiscPer,DiscAmt,VatPer,VatAmount,GrandTotal,RTRIM(PO_Status) FROM PurchaseOrder  INNER JOIN Supplier ON PurchaseOrder.Supplier_ID = Supplier.ID where PO_Status='Issued to supplier' and [Date] between @d1 and @d2 order by Date", con)
            cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "Date").Value = dtpDateFrom.Value.Date
            cmd.Parameters.Add("@d2", SqlDbType.NChar, 30, "Date").Value = dtpDateTo.Value.Date
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            While (rdr.Read() = True)
                dgw.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7), rdr(8), rdr(9), rdr(10), rdr(11), rdr(12), rdr(13), rdr(14), rdr(15), rdr(16))
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExportExcel.Click
        ExportExcel(dgw)
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs)
        Close()
    End Sub
End Class
