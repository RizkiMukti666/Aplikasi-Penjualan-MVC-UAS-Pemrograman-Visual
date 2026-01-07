Imports System.Windows.Forms

Public Class frmSupplierInput
    Inherits Form

    Private txtSupplierID As TextBox
    Private txtSupplierName As TextBox
    Private txtAddress As TextBox
    Private txtPhone As TextBox
    Private txtEmail As TextBox
    Private txtContactPerson As TextBox
    Private WithEvents btnSave As Button
    Private WithEvents btnCancel As Button
    Private lblSupplierID As Label
    Private lblSupplierName As Label
    Private lblAddress As Label
    Private lblPhone As Label
    Private lblEmail As Label
    Private lblContactPerson As Label
    Private lblTitle As Label
    Private pnlTop As Panel

    Private _controller As SupplierController

    Public Property IsEditMode As Boolean = False
    Public Property SupplierID As Integer = 0

    Public Sub New()
        InitializeComponent()
        _controller = New SupplierController()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        ' Form settings
        Me.Text = "Input Supplier"
        Me.Size = New Size(500, 400)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Panel Top
        pnlTop = New Panel()
        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 50
        pnlTop.BackColor = Color.FromArgb(51, 122, 183)

        lblTitle = New Label()
        lblTitle.Text = "INPUT SUPPLIER"
        lblTitle.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(20, 12)
        pnlTop.Controls.Add(lblTitle)

        Dim yPos As Integer = 70

        ' Supplier ID
        lblSupplierID = New Label()
        lblSupplierID.Text = "Kode Supplier"
        lblSupplierID.Location = New Point(20, yPos)
        lblSupplierID.AutoSize = True

        txtSupplierID = New TextBox()
        txtSupplierID.Location = New Point(150, yPos - 3)
        txtSupplierID.Size = New Size(150, 25)
        txtSupplierID.ReadOnly = True
        txtSupplierID.BackColor = Color.LightGray

        yPos += 35

        ' Supplier Name
        lblSupplierName = New Label()
        lblSupplierName.Text = "Nama Supplier *"
        lblSupplierName.Location = New Point(20, yPos)
        lblSupplierName.AutoSize = True

        txtSupplierName = New TextBox()
        txtSupplierName.Location = New Point(150, yPos - 3)
        txtSupplierName.Size = New Size(300, 25)

        yPos += 35

        ' Address
        lblAddress = New Label()
        lblAddress.Text = "Alamat"
        lblAddress.Location = New Point(20, yPos)
        lblAddress.AutoSize = True

        txtAddress = New TextBox()
        txtAddress.Location = New Point(150, yPos - 3)
        txtAddress.Size = New Size(300, 50)
        txtAddress.Multiline = True

        yPos += 60

        ' Phone
        lblPhone = New Label()
        lblPhone.Text = "Telepon"
        lblPhone.Location = New Point(20, yPos)
        lblPhone.AutoSize = True

        txtPhone = New TextBox()
        txtPhone.Location = New Point(150, yPos - 3)
        txtPhone.Size = New Size(200, 25)

        yPos += 35

        ' Email
        lblEmail = New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(20, yPos)
        lblEmail.AutoSize = True

        txtEmail = New TextBox()
        txtEmail.Location = New Point(150, yPos - 3)
        txtEmail.Size = New Size(250, 25)

        yPos += 35

        ' Contact Person
        lblContactPerson = New Label()
        lblContactPerson.Text = "Contact Person"
        lblContactPerson.Location = New Point(20, yPos)
        lblContactPerson.AutoSize = True

        txtContactPerson = New TextBox()
        txtContactPerson.Location = New Point(150, yPos - 3)
        txtContactPerson.Size = New Size(250, 25)

        yPos += 50

        ' Buttons
        btnSave = New Button()
        btnSave.Text = "Simpan"
        btnSave.Size = New Size(100, 35)
        btnSave.Location = New Point(150, yPos)
        btnSave.BackColor = Color.FromArgb(51, 122, 183)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Cursor = Cursors.Hand

        btnCancel = New Button()
        btnCancel.Text = "Batal"
        btnCancel.Size = New Size(100, 35)
        btnCancel.Location = New Point(260, yPos)
        btnCancel.BackColor = Color.Gray
        btnCancel.ForeColor = Color.White
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Cursor = Cursors.Hand

        ' Add controls
        Me.Controls.AddRange({pnlTop, lblSupplierID, txtSupplierID, lblSupplierName, txtSupplierName,
                             lblAddress, txtAddress, lblPhone, txtPhone, lblEmail, txtEmail,
                             lblContactPerson, txtContactPerson, btnSave, btnCancel})

        Me.ResumeLayout(False)
    End Sub

    Private Sub frmSupplierInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsEditMode Then
            lblTitle.Text = "EDIT SUPPLIER"
            Me.Text = "Edit Supplier"
            LoadSupplierData()
        Else
            txtSupplierID.Text = _controller.GenerateNewID()
        End If
    End Sub

    Private Sub LoadSupplierData()
        Dim supplier = _controller.GetById(SupplierID)
        If supplier IsNot Nothing Then
            txtSupplierID.Text = supplier.SupplierID
            txtSupplierName.Text = supplier.SupplierName
            txtAddress.Text = supplier.Address
            txtPhone.Text = supplier.Phone
            txtEmail.Text = supplier.Email
            txtContactPerson.Text = supplier.ContactPerson
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validasi
        If String.IsNullOrWhiteSpace(txtSupplierName.Text) Then
            MessageBox.Show("Nama Supplier tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSupplierName.Focus()
            Return
        End If

        Dim supplier As New Supplier() With {
            .SupplierID = txtSupplierID.Text,
            .SupplierName = txtSupplierName.Text.Trim(),
            .Address = txtAddress.Text.Trim(),
            .Phone = txtPhone.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .ContactPerson = txtContactPerson.Text.Trim()
        }

        Dim success As Boolean
        If IsEditMode Then
            supplier.Id = SupplierID
            success = _controller.Update(supplier)
        Else
            success = _controller.Insert(supplier)
        End If

        If success Then
            MessageBox.Show("Data supplier berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Gagal menyimpan data supplier!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
