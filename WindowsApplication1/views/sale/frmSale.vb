Imports System.Windows.Forms

Public Class frmSale
    Inherits Form

    Private txtSaleID As TextBox
    Private dtpSaleDate As DateTimePicker
    Private txtCustomerName As TextBox
    Private txtNotes As TextBox
    Private WithEvents dgvItems As DataGridView
    Private dgvCart As DataGridView
    Private WithEvents txtQty As TextBox
    Private WithEvents txtSalePrice As TextBox
    Private lblTotal As Label
    Private lblSubtotal As Label
    Private txtSubtotal As TextBox
    Private WithEvents btnAddToCart As Button
    Private WithEvents btnRemoveFromCart As Button
    Private WithEvents btnSave As Button
    Private WithEvents btnClear As Button
    Private WithEvents btnClose As Button

    Private lblSaleID As Label
    Private lblSaleDate As Label
    Private lblCustomerName As Label
    Private lblNotes As Label
    Private lblQty As Label
    Private lblSalePrice As Label
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

    Private _controller As SaleController
    Private _itemController As ItemController
    Private _total As Integer = 0

    Public Sub New()
        InitializeComponent()
        _controller = New SaleController()
        _itemController = New ItemController()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        Me.Text = "Transaksi Penjualan"
        Me.Size = New Size(1100, 700)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Panel Top
        pnlTop = New Panel()
        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 50
        pnlTop.BackColor = Color.FromArgb(40, 167, 69)

        lblTitle = New Label()
        lblTitle.Text = "TRANSAKSI PENJUALAN"
        lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(20, 12)
        pnlTop.Controls.Add(lblTitle)

        ' Panel Header
        pnlHeader = New Panel()
        pnlHeader.Location = New Point(10, 60)
        pnlHeader.Size = New Size(1060, 100)

        lblSaleID = New Label()
        lblSaleID.Text = "No. Penjualan"
        lblSaleID.Location = New Point(10, 10)
        lblSaleID.AutoSize = True

        txtSaleID = New TextBox()
        txtSaleID.Location = New Point(120, 7)
        txtSaleID.Size = New Size(120, 25)
        txtSaleID.ReadOnly = True
        txtSaleID.BackColor = Color.LightGray

        lblSaleDate = New Label()
        lblSaleDate.Text = "Tanggal"
        lblSaleDate.Location = New Point(260, 10)
        lblSaleDate.AutoSize = True

        dtpSaleDate = New DateTimePicker()
        dtpSaleDate.Location = New Point(330, 7)
        dtpSaleDate.Size = New Size(150, 25)
        dtpSaleDate.Format = DateTimePickerFormat.Short

        lblCustomerName = New Label()
        lblCustomerName.Text = "Nama Pelanggan"
        lblCustomerName.Location = New Point(10, 45)
        lblCustomerName.AutoSize = True

        txtCustomerName = New TextBox()
        txtCustomerName.Location = New Point(120, 42)
        txtCustomerName.Size = New Size(300, 25)

        lblNotes = New Label()
        lblNotes.Text = "Catatan"
        lblNotes.Location = New Point(450, 45)
        lblNotes.AutoSize = True

        txtNotes = New TextBox()
        txtNotes.Location = New Point(510, 42)
        txtNotes.Size = New Size(300, 25)

        pnlHeader.Controls.AddRange({lblSaleID, txtSaleID, lblSaleDate, dtpSaleDate,
                                    lblCustomerName, txtCustomerName, lblNotes, txtNotes})

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

        lblSalePrice = New Label()
        lblSalePrice.Text = "Harga Jual:"
        lblSalePrice.Location = New Point(5, 310)
        lblSalePrice.AutoSize = True

        txtSalePrice = New TextBox()
        txtSalePrice.Location = New Point(100, 307)
        txtSalePrice.Size = New Size(120, 25)
        txtSalePrice.Text = "0"

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
        btnAddToCart.BackColor = Color.FromArgb(40, 167, 69)
        btnAddToCart.ForeColor = Color.White
        btnAddToCart.FlatStyle = FlatStyle.Flat
        btnAddToCart.Cursor = Cursors.Hand
        btnAddToCart.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        pnlItems.Controls.AddRange({lblTitleItems, dgvItems, lblSelectedItem, txtSelectedItem,
                                   lblQty, txtQty, lblSalePrice, txtSalePrice,
                                   lblSubtotal, txtSubtotal, btnAddToCart})

        ' Panel Cart (Right)
        pnlCart = New Panel()
        pnlCart.Location = New Point(540, 165)
        pnlCart.Size = New Size(530, 400)
        pnlCart.BorderStyle = BorderStyle.FixedSingle

        lblTitleCart = New Label()
        lblTitleCart.Text = "KERANJANG PENJUALAN"
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
        btnRemoveFromCart.BackColor = Color.FromArgb(220, 53, 69)
        btnRemoveFromCart.ForeColor = Color.White
        btnRemoveFromCart.FlatStyle = FlatStyle.Flat
        btnRemoveFromCart.Cursor = Cursors.Hand

        lblTotal = New Label()
        lblTotal.Text = "TOTAL: Rp 0"
        lblTotal.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTotal.ForeColor = Color.FromArgb(40, 167, 69)
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
        btnSave.BackColor = Color.FromArgb(40, 167, 69)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnSave.Cursor = Cursors.Hand

        btnClear = New Button()
        btnClear.Text = "BERSIHKAN"
        btnClear.Location = New Point(540, 5)
        btnClear.Size = New Size(120, 40)
        btnClear.BackColor = Color.FromArgb(255, 193, 7)
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

    Private Sub frmSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadItems()
        GenerateNewID()
        InitCartGrid()
    End Sub

    Private Sub GenerateNewID()
        txtSaleID.Text = _controller.generateCode()
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
        dgvCart.Columns.Add("Price", "Harga Jual")
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
            txtSalePrice.Text = row.Cells("salesPrice").Value.ToString()
            txtQty.Text = "1"
            CalculateSubtotal()
        End If
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        CalculateSubtotal()
    End Sub

    Private Sub txtSalePrice_TextChanged(sender As Object, e As EventArgs) Handles txtSalePrice.TextChanged
        CalculateSubtotal()
    End Sub

    Private Sub CalculateSubtotal()
        If txtQty Is Nothing OrElse txtSalePrice Is Nothing OrElse txtSubtotal Is Nothing Then Return

        Dim qty As Integer = 0
        Dim price As Integer = 0

        Integer.TryParse(txtQty.Text, qty)
        Integer.TryParse(txtSalePrice.Text, price)

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

        If Not Integer.TryParse(txtSalePrice.Text, price) OrElse price <= 0 Then
            MessageBox.Show("Harga jual harus berupa angka lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSalePrice.Focus()
            Return
        End If

        Dim selectedRow = dgvItems.SelectedRows(0)
        Dim itemID As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)
        Dim itemCode As String = selectedRow.Cells("itemID").Value.ToString()
        Dim itemName As String = selectedRow.Cells("itemDesc").Value.ToString()

        ' Cek stok
        Dim currentStock As Integer = Convert.ToInt32(selectedRow.Cells("stock").Value)
        If qty > currentStock Then
            MessageBox.Show("Stok tidak mencukupi! Stok tersedia: " & currentStock, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

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
        If dgvCart.Rows.Count = 0 Then
            MessageBox.Show("Keranjang masih kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Simpan transaksi penjualan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim sale As New SaleModel()
        sale.idTrans = txtSaleID.Text
        sale.saleDate = dtpSaleDate.Value
        sale.totalAmount = _total
        sale.customerName = txtCustomerName.Text.Trim()
        sale.notes = txtNotes.Text.Trim()

        If User.IsLoggedIn() Then
            sale.createdBy = User.CurrentUser.Id
        End If

        sale.details = New List(Of SaleDetailModel)()

        For Each row As DataGridViewRow In dgvCart.Rows
            Dim detail As New SaleDetailModel()
            detail.ProductId = Convert.ToInt32(row.Cells("ItemID").Value)
            detail.Qty = Convert.ToInt32(row.Cells("Qty").Value)
            detail.Price = Convert.ToInt32(row.Cells("Price").Value)
            detail.Subtotal = Convert.ToInt32(row.Cells("Subtotal").Value)
            sale.details.Add(detail)
        Next

        If _controller.SaveNew(sale) Then
            MessageBox.Show("Transaksi penjualan berhasil disimpan!" & vbCrLf & "Stok barang telah diupdate.",
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
        dtpSaleDate.Value = DateTime.Now
        txtCustomerName.Clear()
        txtNotes.Clear()
        dgvCart.Rows.Clear()
        txtQty.Text = "1"
        txtSalePrice.Text = "0"
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