<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TempForm
    Inherits System.Windows.Forms.Form

    'Form, bileşen listesini temizlemeyi bırakmayı geçersiz kılar.
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

    'Windows Form Tasarımcısı tarafından gerektirilir
    Private components As System.ComponentModel.IContainer

    'NOT: Aşağıdaki yordam Windows Form Tasarımcısı için gereklidir
    'Windows Form Tasarımcısı kullanılarak değiştirilebilir.  
    'Kod düzenleyicisini kullanarak değiştirmeyin.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RealIP_RichTextBox = New System.Windows.Forms.RichTextBox()
        Me.VPNRichTextBox = New System.Windows.Forms.RichTextBox()
        Me.RealTimeIP_RichTextBox = New System.Windows.Forms.RichTextBox()
        Me.CheckURLRichTextBox = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'RealIP_RichTextBox
        '
        Me.RealIP_RichTextBox.Location = New System.Drawing.Point(12, 55)
        Me.RealIP_RichTextBox.Name = "RealIP_RichTextBox"
        Me.RealIP_RichTextBox.Size = New System.Drawing.Size(289, 286)
        Me.RealIP_RichTextBox.TabIndex = 0
        Me.RealIP_RichTextBox.Text = ""
        '
        'VPNRichTextBox
        '
        Me.VPNRichTextBox.Location = New System.Drawing.Point(307, 55)
        Me.VPNRichTextBox.Name = "VPNRichTextBox"
        Me.VPNRichTextBox.Size = New System.Drawing.Size(175, 286)
        Me.VPNRichTextBox.TabIndex = 1
        Me.VPNRichTextBox.Text = ""
        '
        'RealTimeIP_RichTextBox
        '
        Me.RealTimeIP_RichTextBox.Location = New System.Drawing.Point(488, 55)
        Me.RealTimeIP_RichTextBox.Name = "RealTimeIP_RichTextBox"
        Me.RealTimeIP_RichTextBox.Size = New System.Drawing.Size(175, 286)
        Me.RealTimeIP_RichTextBox.TabIndex = 2
        Me.RealTimeIP_RichTextBox.Text = ""
        '
        'CheckURLRichTextBox
        '
        Me.CheckURLRichTextBox.Location = New System.Drawing.Point(669, 55)
        Me.CheckURLRichTextBox.Name = "CheckURLRichTextBox"
        Me.CheckURLRichTextBox.Size = New System.Drawing.Size(119, 286)
        Me.CheckURLRichTextBox.TabIndex = 3
        Me.CheckURLRichTextBox.Text = ""
        '
        'TempForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.CheckURLRichTextBox)
        Me.Controls.Add(Me.RealTimeIP_RichTextBox)
        Me.Controls.Add(Me.VPNRichTextBox)
        Me.Controls.Add(Me.RealIP_RichTextBox)
        Me.Name = "TempForm"
        Me.Text = "TempForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RealIP_RichTextBox As RichTextBox
    Friend WithEvents VPNRichTextBox As RichTextBox
    Friend WithEvents RealTimeIP_RichTextBox As RichTextBox
    Friend WithEvents CheckURLRichTextBox As RichTextBox
End Class
