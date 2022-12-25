Imports System.Text.RegularExpressions
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient

Public Class frmSalesInvoice_View

    Private Sub txtEmailID_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmailID.KeyPress
        Dim ac As String = "@"
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Asc(e.KeyChar) < 97 Or Asc(e.KeyChar) > 122 Then
                If Asc(e.KeyChar) <> 46 And Asc(e.KeyChar) <> 95 Then
                    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                        If ac.IndexOf(e.KeyChar) = -1 Then
                            e.Handled = True

                        Else

                            If txtEmailID.Text.Contains("@") And e.KeyChar = "@" Then
                                e.Handled = True
                            End If

                        End If


                    End If
                End If
            End If

        End If
    End Sub

    Private Sub txtEmailID_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmailID.Validating
        Dim pattern As String = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$"
        Dim match As System.Text.RegularExpressions.Match = Regex.Match(txtEmailID.Text.Trim(), pattern, RegexOptions.IgnoreCase)
        If (match.Success) Then
        Else
            MessageBox.Show("Please enter a valid email id", "Checking", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtEmailID.Clear()
        End If
    End Sub

    Private Sub btnSendMail_Click(sender As System.Object, e As System.EventArgs) Handles btnSendMail.Click
        Try
            If txtEmailID.Text = "" Then
                MessageBox.Show("Please enter Email ID", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtEmailID.Focus()
                Exit Sub
            End If
            con = New SqlConnection(cs)
            con.Open()
            Dim sql As String = "select count(*) from EmailSetting Having count(*) <=0"
            cmd = New SqlCommand(sql)
            cmd.Connection = con
            rdr = cmd.ExecuteReader()
            If rdr.Read() Then
                frmCustomDialog3.ShowDialog()
                If (rdr IsNot Nothing) Then
                    rdr.Close()
                End If
                Return
            End If
            con.Close()
            If CheckForInternetConnection() = False Then
                MessageBox.Show("No active internet connection available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If CheckForInternetConnection() = True Then
                Cursor = Cursors.WaitCursor
                Timer1.Enabled = True
                con = New SqlConnection(cs)
                con.Open()
                Dim ctn As String = "select RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port) from EmailSetting where IsDefault='Yes' and IsActive='Yes'"
                cmd = New SqlCommand(ctn)
                cmd.Connection = con
                Dim rdr1 As SqlDataReader
                rdr1 = cmd.ExecuteReader()
                If rdr1.Read() Then
                     If txtCustomerType.Text <> "Non Regular" Then
                        Cursor = Cursors.WaitCursor
                        Timer1.Enabled = True
                        If (Not System.IO.Directory.Exists(Application.StartupPath & "\PDF Reports")) Then
                            System.IO.Directory.CreateDirectory(Application.StartupPath & "\PDF Reports")
                        End If
                        Dim pdfFile As String = Application.StartupPath & "\PDF Reports\SalesInvoice " & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".Pdf"

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
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdfFile)
                        SendMail1(rdr1.GetValue(0), txtEmailID.Text, "Please find the attachment below", pdfFile, "Sales Invoice", rdr1.GetValue(2), rdr1.GetValue(3), rdr1.GetValue(0), Decrypt(rdr1.GetValue(1)))
                        If (rdr1 IsNot Nothing) Then
                            rdr1.Close()
                        End If
                        rpt.Close()
                        rpt.Dispose()
                    End If
                    If txtCustomerType.Text = "Non Regular" Then
                        Cursor = Cursors.WaitCursor
                        Timer1.Enabled = True
                        If (Not System.IO.Directory.Exists(Application.StartupPath & "\PDF Reports")) Then
                            System.IO.Directory.CreateDirectory(Application.StartupPath & "\PDF Reports")
                        End If
                        Dim pdfFile As String = Application.StartupPath & "\PDF Reports\SalesInvoice " & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".Pdf"

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
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdfFile)
                        SendMail1(rdr1.GetValue(0), txtEmailID.Text, "Please find the attachment below", pdfFile, "Sales Invoice", rdr1.GetValue(2), rdr1.GetValue(3), rdr1.GetValue(0), Decrypt(rdr1.GetValue(1)))
                        If (rdr1 IsNot Nothing) Then
                            rdr1.Close()
                        End If
                        rpt.Close()
                        rpt.Dispose()
                    End If
                 
                    MessageBox.Show("Successfully send", "Mail", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Cursor = Cursors.Default
        Timer1.Enabled = False
    End Sub
End Class