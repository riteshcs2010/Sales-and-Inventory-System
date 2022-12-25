<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContactMe
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmContactMe))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.lnkEmail = New System.Windows.Forms.LinkLabel()
        Me.lnkWebsite = New System.Windows.Forms.LinkLabel()
        Me.lnkSocial = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.DarkViolet
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Palatino Linotype", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnClose.Location = New System.Drawing.Point(127, 215)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(130, 37)
        Me.btnClose.TabIndex = 111
        Me.btnClose.Text = "&OK"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(7, 158)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(88, 26)
        Me.label1.TabIndex = 118
        Me.label1.Text = "Website:"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.Color.OrangeRed
        Me.label4.Location = New System.Drawing.Point(7, 109)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(74, 26)
        Me.label4.TabIndex = 117
        Me.label4.Text = "Email: "
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.DarkViolet
        Me.label2.Location = New System.Drawing.Point(7, 60)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(107, 26)
        Me.label2.TabIndex = 115
        Me.label2.Text = "Facebook :"
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.OrangeRed
        Me.label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.White
        Me.label3.Location = New System.Drawing.Point(-2, 0)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(425, 50)
        Me.label3.TabIndex = 112
        Me.label3.Text = "Contact Us"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lnkEmail
        '
        Me.lnkEmail.AutoSize = True
        Me.lnkEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkEmail.Location = New System.Drawing.Point(123, 110)
        Me.lnkEmail.Name = "lnkEmail"
        Me.lnkEmail.Size = New System.Drawing.Size(216, 24)
        Me.lnkEmail.TabIndex = 121
        Me.lnkEmail.TabStop = True
        Me.lnkEmail.Text = "support@actavista.org"
        '
        'lnkWebsite
        '
        Me.lnkWebsite.AutoSize = True
        Me.lnkWebsite.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkWebsite.Location = New System.Drawing.Point(123, 159)
        Me.lnkWebsite.Name = "lnkWebsite"
        Me.lnkWebsite.Size = New System.Drawing.Size(126, 24)
        Me.lnkWebsite.TabIndex = 122
        Me.lnkWebsite.TabStop = True
        Me.lnkWebsite.Text = "actavista.org"
        '
        'lnkSocial
        '
        Me.lnkSocial.AutoSize = True
        Me.lnkSocial.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkSocial.Location = New System.Drawing.Point(123, 61)
        Me.lnkSocial.Name = "lnkSocial"
        Me.lnkSocial.Size = New System.Drawing.Size(263, 24)
        Me.lnkSocial.TabIndex = 123
        Me.lnkSocial.TabStop = True
        Me.lnkSocial.Text = "facebook.com/actavista.org"
        '
        'frmContactMe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(423, 271)
        Me.ControlBox = False
        Me.Controls.Add(Me.lnkSocial)
        Me.Controls.Add(Me.lnkWebsite)
        Me.Controls.Add(Me.lnkEmail)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmContactMe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contact Us"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents lnkEmail As LinkLabel
    Friend WithEvents lnkWebsite As LinkLabel
    Friend WithEvents lnkSocial As LinkLabel
End Class
