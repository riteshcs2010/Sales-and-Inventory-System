Imports System.Data.SqlClient
Imports System.IO

Imports Microsoft.SqlServer.Management.Smo
Imports System.Globalization

Public Class frmMainMenu
    Dim Filename As String
    Dim i1, i2 As Integer

    Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        frmAbout.ShowDialog()
    End Sub
    Sub Backup()
        Try
            Dim dt As DateTime = Today
            Dim destdir As String = "SIS_DB " & System.DateTime.Now.ToString("dd-MM-yyyy_h-mm-ss") & ".bak"
            Dim objdlg As New SaveFileDialog
            objdlg.FileName = destdir
            objdlg.ShowDialog()
            Filename = objdlg.FileName
            Cursor = Cursors.WaitCursor
            Timer2.Enabled = True
            con = New SqlConnection(cs)
            con.Open()
            Dim cb As String = "backup database SIS_DB to disk='" & Filename & "'with init,stats=10"
            cmd = New SqlCommand(cb)
            cmd.Connection = con
            cmd.ExecuteReader()
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BackupToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BackupToolStripMenuItem.Click
        Backup()
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        Cursor = Cursors.Default
        Timer2.Enabled = False
    End Sub
    Public Sub recovery()
        Try
            With OpenFileDialog1
                .Filter = ("DB Backup File|*.bak;")
                .FilterIndex = 4
            End With
            'Clear the file name
            OpenFileDialog1.FileName = ""

            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                Cursor = Cursors.WaitCursor
                Timer2.Enabled = True
                SqlConnection.ClearAllPools()
                con = New SqlConnection(cs)
                con.Open()
                Dim cb As String = "USE Master ALTER DATABASE SIS_DB SET Single_User WITH Rollback Immediate Restore database SIS_DB FROM disk='" & OpenFileDialog1.FileName & "' WITH REPLACE ALTER DATABASE SIS_DB SET Multi_User "
                cmd = New SqlCommand(cb)
                cmd.Connection = con
                cmd.ExecuteReader()
                con.Close()
                Dim st As String = "Sucessfully performed the restore"
                LogFunc(lblUser.Text, st)
                MessageBox.Show("Successfully performed", "Database Restore", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RestoreToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RestoreToolStripMenuItem.Click
        recovery()
    End Sub

    Private Sub RegistrationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RegistrationToolStripMenuItem.Click
        frmRegistration.lblUser.Text = lblUser.Text
        frmRegistration.Reset()
        frmRegistration.ShowDialog()
    End Sub

    Private Sub LogsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogsToolStripMenuItem.Click
        frmLogs.Reset()
        frmLogs.lblUser.Text = lblUser.Text
        frmLogs.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        lblDateTime.Text = Now.ToString("dddd, dd MMMM yyyy hh:mm:ss tt")
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("Calc.exe")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NotepadToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NotepadToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("Notepad.exe")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WordpadToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles WordpadToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("wordpad.exe")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MSWordToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MSWordToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("winword.exe")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TaskManagerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TaskManagerToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("TaskMgr.exe")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SystemInfoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SystemInfoToolStripMenuItem.Click
        frmSystemInfo.ShowDialog()
    End Sub
    Sub LogOut()
        frmPurchaseEntry.Hide()
        frmProduct.Hide()
        Dim st As String = "Successfully logged out"
        LogFunc(lblUser.Text, st)
        Me.Hide()
        frmLogin.Show()
        frmLogin.UserID.Text = ""
        frmLogin.Password.Text = ""
        frmLogin.UserID.Focus()
    End Sub
    Private Sub LogoutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Try
            If MessageBox.Show("Do you really want to logout from application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If MessageBox.Show("Do you want backup database before logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Backup()
                    LogOut()
                Else
                    LogOut()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmMainMenu_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub CompanyInfoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CompanyInfoToolStripMenuItem.Click
        frmCompany.lblUser.Text = lblUser.Text
        frmCompany.Reset()
        frmCompany.ShowDialog()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CustomerToolStripMenuItem.Click
        frmCustomer.lblUser.Text = lblUser.Text
        frmCustomer.Reset()
        frmCustomer.ShowDialog()
    End Sub

    Private Sub CategoryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CategoryToolStripMenuItem.Click
        frmCategory.lblUser.Text = lblUser.Text
        frmCategory.Reset()
        frmCategory.ShowDialog()
    End Sub

    Private Sub SubCategoryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SubCategoryToolStripMenuItem.Click
        frmSubCategory.lblUser.Text = lblUser.Text
        frmSubCategory.Reset()
        frmSubCategory.ShowDialog()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SupplierToolStripMenuItem.Click
        frmSupplier.lblUser.Text = lblUser.Text
        frmSupplier.Reset()
        frmSupplier.ShowDialog()
    End Sub

    Private Sub CustomerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SupplierToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)
        frmSupplierRecord.Reset()
        frmSupplierRecord.ShowDialog()
    End Sub

    Public Sub Getdata()
        Try
            con = New SqlConnection(cs)
            con.Open()
            cmd = New SqlCommand("SELECT RTRIM(Product.ProductCode),RTRIM(ProductName),RTRIM(Temp_Stock.Barcode),CostPrice,SellingPrice,Discount,VAT,RTRIM(Convert(nvarchar(50),Qty) + ' ' + Convert(Nvarchar(50),SalesUnit)) FROM UnitMaster INNER JOIN Product ON UnitMaster.Unit = Product.SalesUnit INNER JOIN Temp_Stock ON Product.PID = Temp_Stock.ProductID and qty > 0  order by Productcode", con)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            DataGridView1.Rows.Clear()
            While (rdr.Read() = True)
                DataGridView1.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7))
            End While
            con.Close()
            cmd.Dispose()
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                con = New SqlConnection(cs)
                con.Open()
                Dim ct As String = "select ReorderPoint from Product where ProductCode=@d1"
                cmd = New SqlCommand(ct)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@d1", r.Cells(0).Value.ToString())
                rdr = cmd.ExecuteReader()
                If (rdr.Read()) Then

                    i1 = rdr.GetValue(0)
                End If
                con.Close()
                cmd.Dispose()
                con = New SqlConnection(cs)
                con.Open()
                Dim ct1 As String = "select sum(Qty) from Product,Temp_Stock where Product.PID=Temp_Stock.ProductID and ProductCode=@d1"
                cmd = New SqlCommand(ct1)
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@d1", r.Cells(0).Value.ToString())
                rdr = cmd.ExecuteReader()
                If (rdr.Read()) Then
                    i2 = rdr.GetValue(0)
                End If
                con.Close()
                If i2 < i1 Then
                    r.DefaultCellStyle.BackColor = Color.Red
                End If
                con.Close()
                cmd.Dispose()
            Next

            DataGridView1.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub Reset()
        txtProductName.Text = ""
        Getdata()
    End Sub
    Private Function HandleRegistry() As Boolean
        Dim firstRunDate As Date
        Dim st As Date = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\S1", "Set", Nothing)
        firstRunDate = st
        If firstRunDate = Nothing Then
            firstRunDate = System.DateTime.Today.Date
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\S1", "Set", firstRunDate)
        ElseIf (Now - firstRunDate).Days > 2 Then
            MessageBox.Show(firstRunDate)
            Return False
        End If
        If (firstRunDate - System.DateTime.Now).Days > 2 Then
            Return False
        End If
        Return True
    End Function
    Private Sub frmMainMenu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim result As Boolean = HandleRegistry()
        'If result = False Then 'something went wrong
        '    MessageBox.Show("Trial expired" & vbCrLf & "for purchasing the full version of software call us at +919630014949", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End
        'End If
        If lblUserType.Text = "Admin" Then
            MasterEntryToolStripMenuItem.Enabled = True
            RegistrationToolStripMenuItem.Enabled = True
            LogsToolStripMenuItem.Enabled = True
            DatabaseToolStripMenuItem.Enabled = True
            CustomerToolStripMenuItem.Enabled = True
            SupplierToolStripMenuItem.Enabled = True
            ProductToolStripMenuItem.Enabled = True
            StockToolStripMenuItem.Enabled = True
            ServiceToolStripMenuItem.Enabled = True
            StockInToolStripMenuItem.Enabled = True
            BillingToolStripMenuItem.Enabled = True
            QuotationToolStripMenuItem.Enabled = True
            Recordstool.Enabled = True
            ReportsTool.Enabled = True
            VoucherToolStripMenuItem.Enabled = True
            SalesmanToolStripMenuItem3.Enabled = True
            SendSMSToolStripMenuItem.Enabled = True
            SalesReturnToolStripMenuItem.Enabled = True
            PaymentToolStripMenuItem.Enabled = True
        End If
        If lblUserType.Text = "Sales Person" Then
            MasterEntryToolStripMenuItem.Enabled = False
            RegistrationToolStripMenuItem.Enabled = False
            LogsToolStripMenuItem.Enabled = False
            DatabaseToolStripMenuItem.Enabled = False
            CustomerToolStripMenuItem.Enabled = True
            SupplierToolStripMenuItem.Enabled = False
            ProductToolStripMenuItem.Enabled = False
            StockToolStripMenuItem.Enabled = False
            ServiceToolStripMenuItem.Enabled = True
            StockInToolStripMenuItem.Enabled = True
            BillingToolStripMenuItem.Enabled = True
            QuotationToolStripMenuItem.Enabled = True
            Recordstool.Enabled = False
            ReportsTool.Enabled = False
            VoucherToolStripMenuItem.Enabled = False
            SalesmanToolStripMenuItem3.Enabled = False
            SendSMSToolStripMenuItem.Enabled = False
            SalesReturnToolStripMenuItem.Enabled = False
            PaymentToolStripMenuItem.Enabled = False
        End If
        If lblUserType.Text = "Inventory Manager" Then
            MasterEntryToolStripMenuItem.Enabled = False
            RegistrationToolStripMenuItem.Enabled = False
            LogsToolStripMenuItem.Enabled = False
            DatabaseToolStripMenuItem.Enabled = False
            CustomerToolStripMenuItem.Enabled = False
            SupplierToolStripMenuItem.Enabled = False
            ProductToolStripMenuItem.Enabled = True
            StockToolStripMenuItem.Enabled = True
            ServiceToolStripMenuItem.Enabled = False
            StockInToolStripMenuItem.Enabled = True
            BillingToolStripMenuItem.Enabled = False
            QuotationToolStripMenuItem.Enabled = False
            Recordstool.Enabled = False
            ReportsTool.Enabled = False
            VoucherToolStripMenuItem.Enabled = False
            SalesmanToolStripMenuItem3.Enabled = False
            SendSMSToolStripMenuItem.Enabled = False
            PaymentToolStripMenuItem.Enabled = False
        End If
        Getdata()
        DataGridView1.ClearSelection()
        DataGridView1.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub StockToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub StockInToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StockInToolStripMenuItem.Click
        frmCurrentStock.Reset()
        frmCurrentStock.ShowDialog()
    End Sub

    Private Sub btnExportExcel_Click(sender As System.Object, e As System.EventArgs) Handles btnExportExcel.Click
        ExportExcel(DataGridView1)
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub ContactsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ContactsToolStripMenuItem.Click
        frmContacts.lblUser.Text = lblUser.Text
        frmContacts.Reset()
        frmContacts.ShowDialog()
    End Sub

    Private Sub IndividualToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)
        frmProductRecord.Reset()
        frmProductRecord.ShowDialog()
    End Sub

    Private Sub ProductToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ProductToolStripMenuItem.Click
        frmProduct.lblUser.Text = lblUser.Text
        frmProduct.lblUserType.Text = lblUserType.Text
        frmProduct.Reset()
        frmProduct.ShowDialog()
    End Sub

    Private Sub ProductToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ServiceToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub QuotationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles QuotationToolStripMenuItem.Click
        frmQuotation.lblUser.Text = lblUser.Text
        frmQuotation.lblUserType.Text = lblUserType.Text
        frmQuotation.Reset()
        frmQuotation.ShowDialog()
    End Sub

    Private Sub QuotationToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles POSToolStripMenuItem.Click
        Me.Hide()
        frmPOS.lblUser.Text = lblUser.Text
        frmPOS.lblUserType.Text = lblUserType.Text
        frmPOS.Reset()
        frmPOS.Show()
    End Sub

    Private Sub BillingProductsServiceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SMSSettingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub



    Private Sub VoucherToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles VoucherToolStripMenuItem.Click
        frmVoucher.Reset()
        frmVoucher.lblUser.Text = lblUser.Text
        frmVoucher.ShowDialog()
    End Sub

    Private Sub ExpenditureToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub CreditorsAndDebtorsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SQLServerSettingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmSqlServerSetting.Reset()
        frmSqlServerSetting.lblSet.Text = "Main Form"
        frmSqlServerSetting.ShowDialog()
    End Sub

    Private Sub PurchaseDaybookToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmPurchaseDaybook.Reset()
        frmPurchaseDaybook.ShowDialog()
    End Sub

    Private Sub GeneralLedgerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmGeneralLedger.Reset()
        frmGeneralLedger.ShowDialog()
    End Sub

    Private Sub GeneralDaybookToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmGeneralDayBook.Reset()
        frmGeneralDayBook.ShowDialog()
    End Sub

    Private Sub PaymentToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PaymentToolStripMenuItem.Click
        frmPayment.lblUser.Text = lblUser.Text
        frmPayment.Reset()
        frmPayment.ShowDialog()
    End Sub

    Private Sub PaymentsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmTrialBalance.Reset()
        frmTrialBalance.ShowDialog()
    End Sub

    Private Sub SupplierLedgerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub CustomerLedgerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmCustomerLedger.Reset()
        frmCustomerLedger.ShowDialog()
    End Sub

    Private Sub SMSToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub


    Private Sub SalesmanToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles SalesmanToolStripMenuItem3.Click
        frmSalesman.Reset()
        frmSalesman.lblUser.Text = lblUser.Text
        frmSalesman.ShowDialog()
    End Sub

    Private Sub SalesmanLedgerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        frmSalesmanLedger.Reset()
        frmSalesmanLedger.ShowDialog()
    End Sub

    Private Sub SalesmanCommissionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub TaxToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SalesmanToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub



    Private Sub PurchaseEntryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseEntryToolStripMenuItem.Click
        frmPurchaseEntry.lblUser.Text = lblUser.Text
        frmPurchaseEntry.lblUserType.Text = lblUserType.Text
        frmPurchaseEntry.Reset()
        frmPurchaseEntry.ShowDialog()
    End Sub

    Private Sub PurchaseReturnToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem.Click
        frmPurchaseReturn.lblUser.Text = lblUser.Text
        frmPurchaseReturn.lblUserType.Text = lblUserType.Text
        frmPurchaseReturn.Reset()
        frmPurchaseReturn.ShowDialog()
    End Sub

    Private Sub EmailSettingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ServiceCreationToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ServiceCreationToolStripMenuItem.Click
        frmServices.lblUser.Text = lblUser.Text
        frmServices.lblUserType.Text = lblUserType.Text
        frmServices.Reset()
        frmServices.ShowDialog()
    End Sub

    Private Sub ServiceBillingToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ServiceBillingToolStripMenuItem1.Click
        frmServiceBilling.lblUser.Text = lblUser.Text
        frmServiceBilling.lblUserType.Text = lblUserType.Text
        frmServiceBilling.Reset()
        frmServiceBilling.ShowDialog()
    End Sub

    Private Sub SalesReturnToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SalesReturnToolStripMenuItem.Click
        frmSalesReturn.lblUser.Text = lblUser.Text
        frmSalesReturn.lblUserType.Text = lblUserType.Text
        frmSalesReturn.Reset()
        frmSalesReturn.ShowDialog()
    End Sub

    Private Sub txtProductName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtProductName.KeyDown

    End Sub

    Private Sub UnitMasterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UnitMasterToolStripMenuItem.Click
        frmUnit.lblUser.Text = lblUser.Text
        frmUnit.Reset()
        frmUnit.ShowDialog()
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem.Click
        frmPurchaseOrder.lblUser.Text = lblUser.Text
        frmPurchaseOrder.Reset()
        frmPurchaseOrder.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SalesToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SalesToolStripMenuItem1.Click
        frmSendSMS_Sales.lblUser.Text = lblUser.Text
        frmSendSMS_Sales.Reset()
        frmSendSMS_Sales.ShowDialog()
    End Sub

    Private Sub ServicesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ServicesToolStripMenuItem.Click
        frmSendSMS_Services.lblUser.Text = lblUser.Text
        frmSendSMS_Services.Reset()
        frmSendSMS_Services.ShowDialog()
    End Sub

    Private Sub SalesToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub SalesReturnToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub PurchaseReturnToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As System.Object, e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Dim strRowNumber As String = (e.RowIndex + 1).ToString()
        Dim size As SizeF = e.Graphics.MeasureString(strRowNumber, Me.Font)
        If DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)) Then
            DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20))
        End If
        Dim b As Brush = SystemBrushes.ControlText
        e.Graphics.DrawString(strRowNumber, Me.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))

    End Sub

    Private Sub RecoprdsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Recordstool.Click

    End Sub

    Private Sub txtProductName_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtProductName.TextChanged
        Try
            Try
                con = New SqlConnection(cs)
                con.Open()
                cmd = New SqlCommand("SELECT RTRIM(Product.ProductCode),RTRIM(ProductName),RTRIM(Temp_Stock.Barcode),CostPrice,SellingPrice,Discount,VAT,RTRIM(Convert(nvarchar(50),Qty) + ' ' + Convert(Nvarchar(50),SalesUnit)) from Temp_Stock,Product where Product.PID=Temp_Stock.ProductID and qty > 0 and ProductName like '%" & txtProductName.Text & "%' order by Productcode", con)
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                DataGridView1.Rows.Clear()
                While (rdr.Read() = True)
                    DataGridView1.Rows.Add(rdr(0), rdr(1), rdr(2), rdr(3), rdr(4), rdr(5), rdr(6), rdr(7))
                End While
                con.Close()
                cmd.Dispose()
                For Each r As DataGridViewRow In Me.DataGridView1.Rows
                    con = New SqlConnection(cs)
                    con.Open()
                    Dim ct As String = "select ReorderPoint from Product where ProductCode=@d1"
                    cmd = New SqlCommand(ct)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@d1", r.Cells(0).Value.ToString())
                    rdr = cmd.ExecuteReader()
                    If (rdr.Read()) Then

                        i1 = rdr.GetValue(0)
                    End If
                    con.Close()
                    cmd.Dispose()
                    con = New SqlConnection(cs)
                    con.Open()
                    Dim ct1 As String = "select sum(Qty) from Product,Temp_Stock where Product.PID=Temp_Stock.ProductID and ProductCode=@d1"
                    cmd = New SqlCommand(ct1)
                    cmd.Connection = con
                    cmd.Parameters.AddWithValue("@d1", r.Cells(0).Value.ToString())
                    rdr = cmd.ExecuteReader()
                    If (rdr.Read()) Then
                        i2 = rdr.GetValue(0)
                    End If
                    con.Close()
                    If i2 < i1 Then
                        r.DefaultCellStyle.BackColor = Color.Red
                    End If
                    con.Close()
                    cmd.Dispose()
                Next

                DataGridView1.ClearSelection()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub CustomersToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CustomersToolStripMenuItem.Click
        frmCustomerRecord1.Reset()
        frmCustomerRecord1.ShowDialog()
    End Sub

    Private Sub SalesmanToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles SalesmanToolStripMenuItem2.Click
        frmSalesmanRecord.Reset()
        frmSalesmanRecord.lblSet.Text = ""
        frmSalesmanRecord.ShowDialog()
    End Sub

    Private Sub SuppliersToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SuppliersToolStripMenuItem.Click
        frmSupplierRecord.Reset()
        frmSupplierRecord.ShowDialog()
    End Sub

    Private Sub ProductsToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles ProductsToolStripMenuItem.Click
        frmProductRecord.Reset()
        frmProductRecord.ShowDialog()
    End Sub

    Private Sub PurchasesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PurchasesToolStripMenuItem.Click
        frmPurchaseRecord.Reset()
        frmPurchaseRecord.ShowDialog()
    End Sub

    Private Sub PurchaseReturnToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem2.Click
        frmPurchaseReturnRecord.Reset()
        frmPurchaseReturnRecord.ShowDialog()
    End Sub

    Private Sub SalesToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles SalesToolStripMenuItem3.Click
        frmSalesInvoiceRecord.Reset()
        frmSalesInvoiceRecord.ShowDialog()
    End Sub

    Private Sub SalesReturnToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles SalesReturnToolStripMenuItem2.Click
        frmSalesReturnRecord.Reset()
        frmSalesReturnRecord.ShowDialog()
    End Sub

    Private Sub ServicesToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ServicesToolStripMenuItem1.Click
        frmServicesRecord.Reset()
        frmServicesRecord.ShowDialog()
    End Sub

    Private Sub QuotationsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles QuotationsToolStripMenuItem.Click
        frmQuotationRecord1.Reset()
        frmQuotationRecord1.ShowDialog()
    End Sub

    Private Sub ServiceBillingToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles ServiceBillingToolStripMenuItem2.Click
        frmServiceBillingRecord.Reset()
        frmServiceBillingRecord.ShowDialog()
    End Sub

    Private Sub PaymentsToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles PaymentsToolStripMenuItem1.Click
        frmPaymentRecord.Reset()
        frmPaymentRecord.ShowDialog()
    End Sub

    Private Sub SMSToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SMSToolStripMenuItem1.Click
        frmSMS.Reset()
        frmSMS.lblUser.Text = lblUser.Text
        frmSMS.ShowDialog()
    End Sub

    Private Sub ServiceBillingToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles ServiceBillingToolStripMenuItem3.Click
        frmServiceDoneReport.Reset()
        frmServiceDoneReport.ShowDialog()
    End Sub

    Private Sub SalesToolStripMenuItem2_Click_1(sender As System.Object, e As System.EventArgs) Handles SalesToolStripMenuItem2.Click
        frmSalesReport.Reset()
        frmSalesReport.ShowDialog()
    End Sub

    Private Sub PurchaseToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseToolStripMenuItem1.Click
        frmPurchaseReport.Reset()
        frmPurchaseReport.ShowDialog()
    End Sub

    Private Sub StockInAndStockOutToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles StockInAndStockOutToolStripMenuItem1.Click
        frmStockInAndOutReport.ShowDialog()
    End Sub

    Private Sub ExpenditureToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ExpenditureToolStripMenuItem1.Click
        frmVoucherReport.Reset()
        frmVoucherReport.ShowDialog()
    End Sub

    Private Sub DebtorsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DebtorsToolStripMenuItem.Click
        frmDebtorsReport.ShowDialog()
    End Sub

    Private Sub ProfitAndLossToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ProfitAndLossToolStripMenuItem1.Click
        frmProfitAndLossReport.Reset()
        frmProfitAndLossReport.ShowDialog()
    End Sub

    Private Sub BestAndLowSellingItemsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BestAndLowSellingItemsToolStripMenuItem.Click
        frmBestAndLowSellingItemsReport.Reset()
        frmBestAndLowSellingItemsReport.ShowDialog()
    End Sub

    Private Sub DayBookToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub DayBookToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles DayBookToolStripMenuItem1.Click
        frmPurchaseDaybook.Reset()
        frmPurchaseDaybook.ShowDialog()
    End Sub

    Private Sub SupplierLedgerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SupplierLedgerToolStripMenuItem1.Click
        frmSupplierLedger.Reset()
        frmSupplierLedger.ShowDialog()
    End Sub

    Private Sub GeneralLedgerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles GeneralLedgerToolStripMenuItem1.Click
      frmGeneralLedger.Reset()
        frmGeneralLedger.ShowDialog()
    End Sub

    Private Sub SalesmanLedgerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SalesmanLedgerToolStripMenuItem1.Click
        frmSalesmanLedger.Reset()
        frmSalesmanLedger.ShowDialog()
    End Sub

    Private Sub SalesmanCommissionToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SalesmanCommissionToolStripMenuItem1.Click
        frmSalesmanCommmissionReport.Reset()
        frmSalesmanCommmissionReport.ShowDialog()
    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles TrialBalanceToolStripMenuItem.Click
        frmTrialBalance.Reset()
        frmTrialBalance.ShowDialog()
    End Sub

    Private Sub TaxToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles TaxToolStripMenuItem1.Click
        frmTaxReport.Reset()
        frmTaxReport.ShowDialog()
    End Sub

    Private Sub BarcodeLabelPrintingToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles BarcodeLabelPrintingToolStripMenuItem1.Click
        frmBarcodeLabelPrinting.Reset()
        frmBarcodeLabelPrinting.ShowDialog()
    End Sub

    Private Sub CustomerLedgerToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles CustomerLedgerToolStripMenuItem1.Click
        frmCustomerLedger.Reset()
        frmCustomerLedger.ShowDialog()
    End Sub

    Private Sub SendSMSToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SendSMSToolStripMenuItem.Click

    End Sub

    Private Sub SMSSettingToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SMSSettingToolStripMenuItem1.Click
        frmSMSSetting.Reset()
        frmSMSSetting.ShowDialog()
    End Sub

    Private Sub EmailSettingToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles EmailSettingToolStripMenuItem1.Click
        frmEmailSetting.Reset()
        frmEmailSetting.ShowDialog()
    End Sub

    Private Sub SMSToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles SMSToolStripMenuItem.Click
        frmSMSSetting.Reset()
        frmSMSSetting.ShowDialog()
    End Sub

    Private Sub EmailToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EmailToolStripMenuItem.Click
        frmEmailSetting.Reset()
        frmEmailSetting.ShowDialog()
    End Sub

    Private Sub RecoveryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RecoveryToolStripMenuItem.Click
        recovery()
    End Sub

    Private Sub DatabaseBackupToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DatabaseBackupToolStripMenuItem.Click
        Backup()
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As System.Object, e As System.EventArgs) Handles VoucherToolStrip.Click
        frmVoucher.Reset()
        frmVoucher.lblUser.Text = lblUser.Text
        frmVoucher.ShowDialog()
    End Sub

    Private Sub CustomerToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles CustomerToolStrip.Click
        frmCustomer.lblUser.Text = lblUser.Text
        frmCustomer.Reset()
        frmCustomer.ShowDialog()
    End Sub

    Private Sub SalesManToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles SalesManToolStrip.Click
        frmSalesman.Reset()
        frmSalesman.lblUser.Text = lblUser.Text
        frmSalesman.ShowDialog()
    End Sub

    Private Sub SupplierToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles SupplierToolStrip.Click
        frmSupplier.lblUser.Text = lblUser.Text
        frmSupplier.Reset()
        frmSupplier.ShowDialog()
    End Sub

    Private Sub ProductToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles ProductToolStrip.Click
        frmProduct.lblUser.Text = lblUser.Text
        frmProduct.lblUserType.Text = lblUserType.Text
        frmProduct.Reset()
        frmProduct.ShowDialog()
    End Sub

    Private Sub StockToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StockToolStripMenuItem.Click

    End Sub

    Private Sub PurchaseEntryToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseEntryToolStripMenuItem1.Click
        frmPurchaseEntry.lblUser.Text = lblUser.Text
        frmPurchaseEntry.lblUserType.Text = lblUserType.Text
        frmPurchaseEntry.Reset()
        frmPurchaseEntry.ShowDialog()
    End Sub

    Private Sub PurchaseOrderToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem1.Click
        frmPurchaseOrder.lblUser.Text = lblUser.Text
        frmPurchaseOrder.Reset()
        frmPurchaseOrder.ShowDialog()
    End Sub

    Private Sub PurchaseReturnToolStripMenuItem1_Click_1(sender As System.Object, e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem1.Click
        frmPurchaseReturn.lblUser.Text = lblUser.Text
        frmPurchaseReturn.lblUserType.Text = lblUserType.Text
        frmPurchaseReturn.Reset()
        frmPurchaseReturn.ShowDialog()
    End Sub

    Private Sub QuotationToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles QuotationToolStrip.Click
        frmQuotation.lblUser.Text = lblUser.Text
        frmQuotation.lblUserType.Text = lblUserType.Text
        frmQuotation.Reset()
        frmQuotation.ShowDialog()
    End Sub

    Private Sub ServiceBillingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ServiceBillingToolStripMenuItem.Click
        frmServiceBilling.lblUser.Text = lblUser.Text
        frmServiceBilling.lblUserType.Text = lblUserType.Text
        frmServiceBilling.Reset()
        frmServiceBilling.ShowDialog()
    End Sub

    Private Sub ServiceCreationToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ServiceCreationToolStripMenuItem1.Click
        frmServices.lblUser.Text = lblUser.Text
        frmServices.lblUserType.Text = lblUserType.Text
        frmServices.Reset()
        frmServices.ShowDialog()
    End Sub

    Private Sub SPToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles SPToolStrip.Click
        frmPayment.lblUser.Text = lblUser.Text
        frmPayment.Reset()
        frmPayment.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frmContactMe.ShowDialog()
    End Sub

    Private Sub GeneralDaybookToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles GeneralDaybookToolStripMenuItem1.Click
        frmGeneralDayBook.Reset()
        frmGeneralDayBook.ShowDialog()
    End Sub

    Private Sub CreditorsReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CreditorsReportToolStripMenuItem.Click
        frmcreditorsReport.ShowDialog()
    End Sub
End Class