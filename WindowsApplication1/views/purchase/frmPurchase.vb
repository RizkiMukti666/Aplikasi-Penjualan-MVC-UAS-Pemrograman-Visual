Imports System.Windows.Forms

Public Class frmPurchase
    Inherits Form

    Private txtPurchaseID As TextBox
    Private dtpPurchaseDate As DateTimePicker
    Private cboSupplier As ComboBox
    Private txtNotes As TextBox
    Private WithEvents dgvItems As DataGridView
    Private dgvCart As DataGridView
    Private WithEvents txtQty As TextBox
    Private WithEvents txtPurchasePrice As TextBox
    Private lblTotal As Label
    Private lblSubtotal As Label
    Private txtSubtotal As TextBox
    Private WithEvents btnAddToCart As Button
    Private WithEvents btnRemoveFromCart As Button
    Private WithEvents btnSave As Button
    Private WithEvents btnClear As Button
    Private WithEvents btnClose As Button

    Private lblPurchaseID As Label
    Private lblPurchaseDate As Label
    Private lblSupplier As Label
    Private lblNotes As Label
    Private lblQty As Label
    Private lblPurchasePrice As Label
    Private lblTitle As Label
    Private lblTitleItems As Label
    Private lblTitleCart As Label
    Private lblSelectedItem As Label
    Private txtSelectedItem As TextBox

    Private pnlTop As Panel
    Private pnlHeader As Panel
    Private pnlItems As Panel
    Private pnlCart As Panel
    Private pnlBottom As Panel

    Private _controller As PurchaseController
    Private _itemController As ItemController
    Private _total As Integer = 0

    Public Sub New()
        InitializeComponent()
        _controller = New PurchaseController()
        _itemController = New ItemController()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        Me.Text = "Transaksi Pembelian"
        Me.Size = New Size(1100, 700)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Panel Top
        pnlTop = New Panel()
        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 50
        pnlTop.BackColor = Color.FromArgb(51, 122, 183)

        lblTitle = New Label()
        lblTitle.Text = "TRANSAKSI PEMBELIAN"
        lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(20, 12)
        pnlTop.Controls.Add(lblTitle)

        ' Panel Header
        pnlHeader = New Panel()
        pnlHeader.Location = New Point(10, 60)
        pnlHeader.Size = New Size(1060, 100)

        lblPurchaseID = New Label()
        lblPurchaseID.Text = "No. Pembelian"
        lblPurchaseID.Location = New Point(10, 10)
        lblPurchaseID.AutoSize = True

        txtPurchaseID = New TextBox()
        txtPurchaseID.Location = New Point(120, 7)
        txtPurchaseID.Size = New Size(120, 25)
        txtPurchaseID.ReadOnly = True
        txtPurchaseID.BackColor = Color.LightGray

        lblPurchaseDate = New Label()
        lblPurchaseDate.Text = "Tanggal"
        lblPurchaseDate.Location = New Point(260, 10)
        lblPurchaseDate.AutoSize = True

        dtpPurchaseDate = New DateTimePicker()
        dtpPurchaseDate.Location = New Point(330, 7)
        dtpPurchaseDate.Size = New Size(150, 25)
        dtpPurchaseDate.Format = DateTimePickerFormat.Short

        lblSupplier = New Label()
        lblSupplier.Text = "Supplier"
        lblSupplier.Location = New Point(10, 45)
        lblSupplier.AutoSize = True

        cboSupplier = New ComboBox()
        cboSupplier.Location = New Point(120, 42)
        cboSupplier.Size = New Size(300, 25)
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList

        lblNotes = New Label()
        lblNotes.Text = "Catatan"
        lblNotes.Location = New Point(450, 45)
        lblNotes.AutoSize = True

        txtNotes = New TextBox()
        txtNotes.Location = New Point(510, 42)
        txtNotes.Size = New Size(300, 25)

        pnlHeader.Controls.AddRange({lblPurchaseID, txtPurchaseID, lblPurchaseDate, dtpPurchaseDate,
                                    lblSupplier, cboSupplier, lblNotes, txtNotes})

        ' Panel Items (Left)
        pnlItems = New Panel()
        pnlItems.Location = New Point(10, 165)
        pnlItems.Size = New Size(520, 400)
        pnlItems.BorderStyle = BorderStyle.FixedSingle

        lblTitleItems = New Label()
        lblTitleItems.Text = "DAFTAR BARANG"
        lblTitleItems.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitleItems.Location = New Point(5, 5)
        lblTitleItems.AutoSize = True

        dgvItems = New DataGridView()
        dgvItems.Location = New Point(5, 30)
        dgvItems.Size = New Size(505, 200)
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvItems.MultiSelect = False
        dgvItems.ReadOnly = True
        dgvItems.AllowUserToAddRows = False
        dgvItems.BackgroundColor = Color.White
        dgvItems.RowHeadersVisible = False

        lblSelectedItem = New Label()
        lblSelectedItem.Text = "Barang Dipilih:"
        lblSelectedItem.Location = New Point(5, 240)
        lblSelectedItem.AutoSize = True
        lblSelectedItem.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        txtSelectedItem = New TextBox()
        txtSelectedItem.Location = New Point(100, 237)
        txtSelectedItem.Size = New Size(300, 25)
        txtSelectedItem.ReadOnly = True
        txtSelectedItem.BackColor = Color.LightYellow

        lblQty = New Label()
        lblQty.Text = "Qty:"
        lblQty.Location = New Point(5, 275)
        lblQty.AutoSize = True

        txtQty = New TextBox()
        txtQty.Location = New Point(100, 272)
        txtQty.Size = New Size(80, 25)
        txtQty.Text = "1"

        lblPurchasePrice = New Label()
        lblPurchasePrice.Text = "Harga Beli:"
        lblPurchasePrice.Location = New Point(5, 310)
        lblPurchasePrice.AutoSize = True

        txtPurchasePrice = New TextBox()
        txtPurchasePrice.Location = New Point(100, 307)
        txtPurchasePrice.Size = New Size(120, 25)
        txtPurchasePrice.Text = "0"

        lblSubtotal = New Label()
        lblSubtotal.Text = "Subtotal:"
        lblSubtotal.Location = New Point(240, 310)
        lblSubtotal.AutoSize = True

        txtSubtotal = New TextBox()
        txtSubtotal.Location = New Point(300, 307)
        txtSubtotal.Size = New Size(120, 25)
        txtSubtotal.ReadOnly = True
        txtSubtotal.BackColor = Color.LightGreen
        txtSubtotal.Text = "0"
        txtSubtotal.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        btnAddToCart = New Button()
        btnAddToCart.Text = "Tambah ke Keranjang >>"
        btnAddToCart.Location = New Point(5, 350)
        btnAddToCart.Size = New Size(200, 35)
        btnAddToCart.BackColor = Color.FromArgb(92, 184, 92)
        btnAddToCart.ForeColor = Color.White
        btnAddToCart.FlatStyle = FlatStyle.Flat
        btnAddToCart.Cursor = Cursors.Hand
        btnAddToCart.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        pnlItems.Controls.AddRange({lblTitleItems, dgvItems, lblSelectedItem, txtSelectedItem,
                                   lblQty, txtQty, lblPurchasePrice, txtPurchasePrice,
                                   lblSubtotal, txtSubtotal, btnAddToCart})

        ' Panel Cart (Right)
        pnlCart = New Panel()
        pnlCart.Location = New Point(540, 165)
        pnlCart.Size = New Size(530, 400)
        pnlCart.BorderStyle = BorderStyle.FixedSingle

        lblTitleCart = New Label()
        lblTitleCart.Text = "KERANJANG PEMBELIAN"
        lblTitleCart.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitleCart.Location = New Point(5, 5)
        lblTitleCart.AutoSize = True

        dgvCart = New DataGridView()
        dgvCart.Location = New Point(5, 30)
        dgvCart.Size = New Size(515, 300)
        dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCart.MultiSelect = False
        dgvCart.ReadOnly = True
        dgvCart.AllowUserToAddRows = False
        dgvCart.BackgroundColor = Color.White
        dgvCart.RowHeadersVisible = False

        btnRemoveFromCart = New Button()
        btnRemoveFromCart.Text = "Hapus Item"
        btnRemoveFromCart.Location = New Point(5, 340)
        btnRemoveFromCart.Size = New Size(120, 35)
        btnRemoveFromCart.BackColor = Color.FromArgb(217, 83, 79)
        btnRemoveFromCart.ForeColor = Color.White
        btnRemoveFromCart.FlatStyle = FlatStyle.Flat
        btnRemoveFromCart.Cursor = Cursors.Hand

        lblTotal = New Label()
        lblTotal.Text = "TOTAL: Rp 0"
        lblTotal.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTotal.ForeColor = Color.FromArgb(51, 122, 183)
        lblTotal.Location = New Point(280, 345)
        lblTotal.AutoSize = True

        pnlCart.Controls.AddRange({lblTitleCart, dgvCart, btnRemoveFromCart, lblTotal})

        ' Panel Bottom
        pnlBottom = New Panel()
        pnlBottom.Location = New Point(10, 575)
        pnlBottom.Size = New Size(1060, 50)

        btnSave = New Button()
        btnSave.Text = "SIMPAN TRANSAKSI"
        btnSave.Location = New Point(350, 5)
        btnSave.Size = New Size(180, 40)
        btnSave.BackColor = Color.FromArgb(51, 122, 183)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnSave.Cursor = Cursors.Hand

        btnClear = New Button()
        btnClear.Text = "BERSIHKAN"
        btnClear.Location = New Point(540, 5)
        btnClear.Size = New Size(120, 40)
        btnClear.BackColor = Color.FromArgb(240, 173, 78)
        btnClear.ForeColor = Color.White
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Cursor = Cursors.Hand

        btnClose = New Button()
        btnClose.Text = "TUTUP"
        btnClose.Location = New Point(670, 5)
        btnClose.Size = New Size(100, 40)
        btnClose.BackColor = Color.Gray
        btnClose.ForeColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Cursor = Cursors.Hand

        pnlBottom.Controls.AddRange({btnSave, btnClear, btnClose})

        Me.Controls.AddRange({pnlTop, pnlHeader, pnlItems, pnlCart, pnlBottom})

        Me.ResumeLayout(False)
    End Sub

    Private Sub frmPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
        LoadItems()
        GenerateNewID()
        InitCartGrid()
    End Sub

    Private Sub GenerateNewID()
        txtPurchaseID.Text = _controller.GenerateNewID()
    End Sub

    Private Sub LoadSuppliers()
        Dim suppliers = _controller.GetSuppliers()
        cboSupplier.Items.Clear()
        cboSupplier.DisplayMember = "SupplierName"
        cboSupplier.ValueMember = "Id"
        cboSupplier.DataSource = suppliers
    End Sub

    Private Sub LoadItems()
        dgvItems.DataSource = _itemController.LoadItems()
        If dgvItems.Columns.Contains("id") Then dgvItems.Columns("id").Visible = False
        If dgvItems.Columns.Contains("itemCate") Then dgvItems.Columns("itemCate").Visible = False
        If dgvItems.Columns.Contains("minStock") Then dgvItems.Columns("minStock").Visible = False
    End Sub

    Private Sub InitCartGrid()
        dgvCart.Columns.Clear()
        dgvCart.Columns.Add("ItemID", "ID")
        dgvCart.Columns.Add("ItemCode", "Kode")
        dgvCart.Columns.Add("ItemName", "Nama Barang")
        dgvCart.Columns.Add("Qty", "Qty")
        dgvCart.Columns.Add("Price", "Harga Beli")
        dgvCart.Columns.Add("Subtotal", "Subtotal")

        dgvCart.Columns("ItemID").Visible = False
        dgvCart.Columns("ItemCode").Width = 70
        dgvCart.Columns("Qty").Width = 50
        dgvCart.Columns("Price").DefaultCellStyle.Format = "N0"
        dgvCart.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvCart.Columns("Subtotal").DefaultCellStyle.Format = "N0"
        dgvCart.Columns("Subtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub dgvItems_SelectionChanged(sender As Object, e As EventArgs) Handles dgvItems.SelectionChanged
        If dgvItems.SelectedRows.Count > 0 AndAlso dgvItems.SelectedRows(0).Cells("itemDesc").Value IsNot Nothing Then
            Dim row = dgvItems.SelectedRows(0)
            txtSelectedItem.Text = row.Cells("itemDesc").Value.ToString()
            txtPurchasePrice.Text = row.Cells("salesPrice").Value.ToString()
            txtQty.Text = "1"
            CalculateSubtotal()
        End If
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        CalculateSubtotal()
    End Sub

    Private Sub txtPurchasePrice_TextChanged(sender As Object, e As EventArgs) Handles txtPurchasePrice.TextChanged
        CalculateSubtotal()
    End Sub

    Private Sub CalculateSubtotal()
        If txtQty Is Nothing OrElse txtPurchasePrice Is Nothing OrElse txtSubtotal Is Nothing Then Return

        Dim qty As Integer = 0
        Dim price As Integer = 0

        Integer.TryParse(txtQty.Text, qty)
        Integer.TryParse(txtPurchasePrice.Text, price)

        Dim subtotal As Integer = qty * price
        txtSubtotal.Text = subtotal.ToString("N0")
    End Sub

    Private Sub btnAddToCart_Click(sender As Object, e As EventArgs) Handles btnAddToCart.Click
        If dgvItems.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih barang terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim qty As Integer
        Dim price As Integer

        If Not Integer.TryParse(txtQty.Text, qty) OrElse qty <= 0 Then
            MessageBox.Show("Qty harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQty.Focus()
            Return
        End If

        If Not Integer.TryParse(txtPurchasePrice.Text, price) OrElse price <= 0 Then
            MessageBox.Show("Harga beli harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPurchasePrice.Focus()
            Return
        End If

        Dim selectedRow = dgvItems.SelectedRows(0)
        Dim itemID As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)
        Dim itemCode As String = selectedRow.Cells("itemID").Value.ToString()
        Dim itemName As String = selectedRow.Cells("itemDesc").Value.ToString()

        For Each row As DataGridViewRow In dgvCart.Rows
            If Convert.ToInt32(row.Cells("ItemID").Value) = itemID Then
                MessageBox.Show("Barang sudah ada di keranjang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim subtotal As Integer = qty * price
        dgvCart.Rows.Add(itemID, itemCode, itemName, qty, price, subtotal)

        UpdateTotal()
        txtQty.Text = "1"
        CalculateSubtotal()
    End Sub

    Private Sub btnRemoveFromCart_Click(sender As Object, e As EventArgs) Handles btnRemoveFromCart.Click
        If dgvCart.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih item yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        dgvCart.Rows.Remove(dgvCart.SelectedRows(0))
        UpdateTotal()
    End Sub

    Private Sub UpdateTotal()
        _total = 0
        For Each row As DataGridViewRow In dgvCart.Rows
            _total += Convert.ToInt32(row.Cells("Subtotal").Value)
        Next
        lblTotal.Text = "TOTAL: Rp " & _total.ToString("N0")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboSupplier.SelectedIndex < 0 Then
            MessageBox.Show("Pilih supplier terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dgvCart.Rows.Count = 0 Then
            MessageBox.Show("Keranjang masih kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Simpan transaksi pembelian?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim purchase As New Purchase()
        purchase.IdPurchase = txtPurchaseID.Text
        purchase.PurchaseDate = dtpPurchaseDate.Value
        purchase.SupplierID = DirectCast(cboSupplier.SelectedItem, Supplier).Id
        purchase.TotalAmount = _total
        purchase.Notes = txtNotes.Text.Trim()

        If User.IsLoggedIn() Then
            purchase.CreatedBy = User.CurrentUser.Id
        End If

        For Each row As DataGridViewRow In dgvCart.Rows
            Dim detail As New PurchaseDetail()
            detail.ItemID = Convert.ToInt32(row.Cells("ItemID").Value)
            detail.QtyPurchase = Convert.ToInt32(row.Cells("Qty").Value)
            detail.PurchasePrice = Convert.ToInt32(row.Cells("Price").Value)
            detail.Subtotal = Convert.ToInt32(row.Cells("Subtotal").Value)
            purchase.Details.Add(detail)
        Next

        If _controller.Insert(purchase) Then
            MessageBox.Show("Transaksi pembelian berhasil disimpan!" & vbCrLf & "Stok barang telah diupdate.",
                           "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearForm()
        Else
            MessageBox.Show("Gagal menyimpan transaksi!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If MessageBox.Show("Bersihkan form?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            ClearForm()
        End If
    End Sub

    Private Sub ClearForm()
        GenerateNewID()
        dtpPurchaseDate.Value = DateTime.Now
        cboSupplier.SelectedIndex = If(cboSupplier.Items.Count > 0, 0, -1)
        txtNotes.Clear()
        dgvCart.Rows.Clear()
        txtQty.Text = "1"
        txtPurchasePrice.Text = "0"
        txtSubtotal.Text = "0"
        txtSelectedItem.Text = ""
        _total = 0
        lblTotal.Text = "TOTAL: Rp 0"
        LoadItems()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
