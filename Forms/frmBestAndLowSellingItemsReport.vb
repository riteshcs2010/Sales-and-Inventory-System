Imports System.Data.SqlClient


Public Class frmBestAndLowSellingItemsReport

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Cursor = Cursors.Default
        Timer1.Enabled = False
    End Sub

    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnStockIn.Click
        Try
            Cursor = Cursors.WaitCursor
            Timer1.Enabled = True
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT Product.ProductCode, Product.ProductName, SubCategory.Category, SubCategory.SubCategoryName, SUM(Invoice_Product.Qty) AS TotalQty FROM Product INNER JOIN Invoice_Product ON Product.PID = Invoice_Product.ProductID INNER JOIN SubCategory ON Product.SubCategoryID = SubCategory.ID INNER JOIN Category ON SubCategory.Category = Category.CategoryName INNER JOIN InvoiceInfo ON Invoice_Product.InvoiceID = InvoiceInfo.Inv_ID Where InvoiceDate between @d1 and @d2 GROUP BY Product.ProductCode, Product.ProductName, SubCategory.Category, SubCategory.SubCategoryName HAVING(SUM(Invoice_Product.Qty) > 0) ORDER BY TotalQty DESC", con)
            cmd.Parameters.AddWithValue("@d1", dtpDateFrom.Value.Date)
            cmd.Parameters.AddWithValue("@d2", dtpDateTo.Value.Date)
            adp = New SqlDataAdapter(cmd)
            dtable = New DataTable()
            adp.Fill(dtable)
            con.Close()
            ds = New DataSet()
            ds.Tables.Add(dtable)
            ds.WriteXmlSchema("BestSellingItems.xml")
            Dim rpt As New rptBestSellingItems
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("p1", Today)
            frmReport.CrystalReportViewer1.ReportSource = rpt
            frmReport.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnStockOut.Click
        Try
            Cursor = Cursors.WaitCursor
            Timer1.Enabled = True
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT Product.ProductCode, Product.ProductName, SubCategory.Category, SubCategory.SubCategoryName, SUM(Invoice_Product.Qty) AS TotalQty FROM Product INNER JOIN Invoice_Product ON Product.PID = Invoice_Product.ProductID INNER JOIN SubCategory ON Product.SubCategoryID = SubCategory.ID INNER JOIN Category ON SubCategory.Category = Category.CategoryName INNER JOIN InvoiceInfo ON Invoice_Product.InvoiceID = InvoiceInfo.Inv_ID Where InvoiceDate between @d1 and @d2 GROUP BY Product.ProductCode, Product.ProductName, SubCategory.Category, SubCategory.SubCategoryName HAVING(SUM(Invoice_Product.Qty) > 0) ORDER BY TotalQty ASC", con)
            cmd.Parameters.AddWithValue("@d1", dtpDateFrom.Value.Date)
            cmd.Parameters.AddWithValue("@d2", dtpDateTo.Value.Date)
            adp = New SqlDataAdapter(cmd)
            dtable = New DataTable()
            adp.Fill(dtable)
            con.Close()
            ds = New DataSet()
            ds.Tables.Add(dtable)
            ds.WriteXmlSchema("LowSellingItems.xml")
            Dim rpt As New rptLowSellingItems
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("p1", Today)
            frmReport.CrystalReportViewer1.ReportSource = rpt
            frmReport.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub Reset()
        dtpDateFrom.Value = Today
        dtpDateTo.Value = Today
    End Sub
    Private Sub frmStockInAndOutReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Reset()
    End Sub
End Class
