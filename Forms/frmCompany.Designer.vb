<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCompany
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompany))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgw = New System.Windows.Forms.DataGridView()
        Me.CompanyBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SIS_DBDataSet = New Sales_and_Inventory_System.SIS_DBDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtShopOwner = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtShopNumber = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtCIN = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLicenseNumber = New System.Windows.Forms.TextBox()
        Me.txtTIN = New System.Windows.Forms.TextBox()
        Me.txtEmailID = New System.Windows.Forms.TextBox()
        Me.txtContactNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.CompanyBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.CompanyTableAdapter = New Sales_and_Inventory_System.SIS_DBDataSetTableAdapters.CompanyTableAdapter()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShopID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LicenseNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompanyOwner = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.dgw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CompanyBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SIS_DBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.CompanyBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.dgw)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblUser)
        Me.Panel1.Controls.Add(Me.txtID)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Location = New System.Drawing.Point(-1, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1524, 824)
        Me.Panel1.TabIndex = 2
        '
        'dgw
        '
        Me.dgw.AllowUserToAddRows = False
        Me.dgw.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LavenderBlush
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LavenderBlush
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dgw.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgw.BackgroundColor = System.Drawing.Color.White
        Me.dgw.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.OrangeRed
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.OrangeRed
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgw.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgw.ColumnHeadersHeight = 24
        Me.dgw.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.ShopID, Me.LicenseNumber, Me.CompanyOwner, Me.Column1, Me.Column2, Me.Column4})
        Me.dgw.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgw.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgw.EnableHeadersVisualStyles = False
        Me.dgw.GridColor = System.Drawing.Color.White
        Me.dgw.Location = New System.Drawing.Point(8, 506)
        Me.dgw.Margin = New System.Windows.Forms.Padding(4)
        Me.dgw.MultiSelect = False
        Me.dgw.Name = "dgw"
        Me.dgw.ReadOnly = True
        Me.dgw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.OrangeRed
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.OrangeRed
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgw.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgw.RowHeadersWidth = 25
        Me.dgw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgw.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgw.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgw.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgw.Size = New System.Drawing.Size(1510, 312)
        Me.dgw.TabIndex = 1
        '
        'CompanyBindingSource
        '
        Me.CompanyBindingSource.DataMember = "Company"
        Me.CompanyBindingSource.DataSource = Me.SIS_DBDataSet
        '
        'SIS_DBDataSet
        '
        Me.SIS_DBDataSet.DataSetName = "SIS_DBDataSet"
        Me.SIS_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.DarkViolet
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(-1, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1525, 48)
        Me.Label1.TabIndex = 185
        Me.Label1.Text = "Add New Company "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(791, 14)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(51, 17)
        Me.lblUser.TabIndex = 5
        Me.lblUser.Text = "Label8"
        Me.lblUser.Visible = False
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(752, 14)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(89, 22)
        Me.txtID.TabIndex = 4
        Me.txtID.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.txtShopOwner)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.txtShopNumber)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.btnBrowse)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.txtAddress)
        Me.Panel4.Controls.Add(Me.txtCIN)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.txtLicenseNumber)
        Me.Panel4.Controls.Add(Me.txtTIN)
        Me.Panel4.Controls.Add(Me.txtEmailID)
        Me.Panel4.Controls.Add(Me.txtContactNo)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.txtCompanyName)
        Me.Panel4.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(3, 53)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(717, 445)
        Me.Panel4.TabIndex = 0
        '
        'txtShopOwner
        '
        Me.txtShopOwner.Location = New System.Drawing.Point(167, 62)
        Me.txtShopOwner.Name = "txtShopOwner"
        Me.txtShopOwner.Size = New System.Drawing.Size(327, 28)
        Me.txtShopOwner.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(109, 21)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Shop Owner :"
        '
        'txtShopNumber
        '
        Me.txtShopNumber.Location = New System.Drawing.Point(167, 14)
        Me.txtShopNumber.Name = "txtShopNumber"
        Me.txtShopNumber.Size = New System.Drawing.Size(327, 28)
        Me.txtShopNumber.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(117, 21)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Shop Number :"
        '
        'btnBrowse
        '
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(537, 143)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(133, 31)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "&Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = Global.Sales_and_Inventory_System.My.Resources.Resources.images__5_
        Me.PictureBox1.Location = New System.Drawing.Point(537, 14)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(133, 123)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 17
        Me.PictureBox1.TabStop = False
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(167, 149)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddress.Size = New System.Drawing.Size(327, 28)
        Me.txtAddress.TabIndex = 1
        '
        'txtCIN
        '
        Me.txtCIN.BackColor = System.Drawing.Color.White
        Me.txtCIN.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCIN.Location = New System.Drawing.Point(167, 380)
        Me.txtCIN.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCIN.Name = "txtCIN"
        Me.txtCIN.Size = New System.Drawing.Size(327, 28)
        Me.txtCIN.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 383)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 21)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "CIN :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 338)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 21)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "License Number:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 289)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 21)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "TIN :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 243)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 21)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Email ID :"
        '
        'txtLicenseNumber
        '
        Me.txtLicenseNumber.BackColor = System.Drawing.Color.White
        Me.txtLicenseNumber.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicenseNumber.Location = New System.Drawing.Point(168, 336)
        Me.txtLicenseNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLicenseNumber.Name = "txtLicenseNumber"
        Me.txtLicenseNumber.Size = New System.Drawing.Size(327, 28)
        Me.txtLicenseNumber.TabIndex = 5
        '
        'txtTIN
        '
        Me.txtTIN.BackColor = System.Drawing.Color.White
        Me.txtTIN.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTIN.Location = New System.Drawing.Point(168, 285)
        Me.txtTIN.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTIN.Name = "txtTIN"
        Me.txtTIN.Size = New System.Drawing.Size(327, 28)
        Me.txtTIN.TabIndex = 4
        '
        'txtEmailID
        '
        Me.txtEmailID.BackColor = System.Drawing.Color.White
        Me.txtEmailID.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailID.Location = New System.Drawing.Point(167, 239)
        Me.txtEmailID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEmailID.Name = "txtEmailID"
        Me.txtEmailID.Size = New System.Drawing.Size(327, 28)
        Me.txtEmailID.TabIndex = 3
        '
        'txtContactNo
        '
        Me.txtContactNo.BackColor = System.Drawing.Color.White
        Me.txtContactNo.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactNo.Location = New System.Drawing.Point(167, 192)
        Me.txtContactNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContactNo.Name = "txtContactNo"
        Me.txtContactNo.Size = New System.Drawing.Size(327, 28)
        Me.txtContactNo.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 195)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Contact No. :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 158)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Address :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 108)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 21)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Company Name :"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.BackColor = System.Drawing.Color.White
        Me.txtCompanyName.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.Location = New System.Drawing.Point(167, 106)
        Me.txtCompanyName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(327, 28)
        Me.txtCompanyName.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.btnDelete)
        Me.Panel3.Controls.Add(Me.btnUpdate)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnNew)
        Me.Panel3.Location = New System.Drawing.Point(743, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(143, 171)
        Me.Panel3.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Crimson
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnDelete.Location = New System.Drawing.Point(8, 124)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(120, 34)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.DarkViolet
        Me.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnUpdate.Location = New System.Drawing.Point(8, 85)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(120, 34)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "&Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.OrangeRed
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSave.Location = New System.Drawing.Point(8, 46)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 34)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.DarkViolet
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Palatino Linotype", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnNew.Location = New System.Drawing.Point(8, 7)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(120, 34)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'CompanyBindingSource1
        '
        Me.CompanyBindingSource1.DataMember = "Company"
        Me.CompanyBindingSource1.DataSource = Me.SIS_DBDataSet
        '
        'CompanyTableAdapter
        '
        Me.CompanyTableAdapter.ClearBeforeFill = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.MinimumWidth = 6
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 125
        '
        'ShopID
        '
        Me.ShopID.HeaderText = "ShopNumber"
        Me.ShopID.MinimumWidth = 6
        Me.ShopID.Name = "ShopID"
        Me.ShopID.ReadOnly = True
        Me.ShopID.Width = 125
        '
        'LicenseNumber
        '
        Me.LicenseNumber.HeaderText = "LicenseNumber"
        Me.LicenseNumber.MinimumWidth = 6
        Me.LicenseNumber.Name = "LicenseNumber"
        Me.LicenseNumber.ReadOnly = True
        Me.LicenseNumber.Width = 125
        '
        'CompanyOwner
        '
        Me.CompanyOwner.HeaderText = "Company Owner"
        Me.CompanyOwner.MinimumWidth = 6
        Me.CompanyOwner.Name = "CompanyOwner"
        Me.CompanyOwner.ReadOnly = True
        Me.CompanyOwner.Width = 125
        '
        'Column1
        '
        Me.Column1.HeaderText = "Company Name"
        Me.Column1.MinimumWidth = 6
        Me.Column1.Name = "CompanyName"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 125
        '
        'Column2
        '
        Me.Column2.HeaderText = "Address"
        Me.Column2.MinimumWidth = 6
        Me.Column2.Name = "Address"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 125
        '
        'Column4
        '
        Me.Column4.HeaderText = "Email ID"
        Me.Column4.MinimumWidth = 6
        Me.Column4.Name = "EmailID"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 125
        '
        'frmCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1560, 837)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCompany"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CompanyBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SIS_DBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.CompanyBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents dgw As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLicenseNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtTIN As System.Windows.Forms.TextBox
    Friend WithEvents txtEmailID As System.Windows.Forms.TextBox
    Friend WithEvents txtContactNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Public WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtCIN As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtShopNumber As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtShopOwner As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents SIS_DBDataSet As SIS_DBDataSet
    Friend WithEvents CompanyBindingSource As BindingSource
    Friend WithEvents CompanyTableAdapter As SIS_DBDataSetTableAdapters.CompanyTableAdapter
    Friend WithEvents CompanyBindingSource1 As BindingSource
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents ShopID As DataGridViewTextBoxColumn
    Friend WithEvents LicenseNumber As DataGridViewTextBoxColumn
    Friend WithEvents CompanyOwner As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
End Class
