Imports System.Windows.Forms

Public Class frmListSupplier
    Inherits Form

    Private dgvSupplier As DataGridView
    Private WithEvents btnAdd As Button
    Private WithEvents btnEdit As Button
    Private WithEvents btnDelete As Button
    Private WithEvents btnRefresh As Button
    Private lblTitle As Label
    Private pnlTop As Panel
    Private pnlButton As Panel

    Private _controller As SupplierController

    Public Sub New()
        InitializeComponent()
        _controller = New SupplierController()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        ' Form settings
        Me.Text = "Data Supplier"
        Me.Size = New Size(900, 500)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Panel Top
        pnlTop = New Panel()
        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 50
        pnlTop.BackColor = Color.FromArgb(51, 122, 183)

        ' Title
        lblTitle = New Label()
        lblTitle.Text = "DATA SUPPLIER"
        lblTitle.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(20, 12)
        pnlTop.Controls.Add(lblTitle)

        ' Panel Button
        pnlButton = New Panel()
        pnlButton.Dock = DockStyle.Top
        pnlButton.Height = 50
        pnlButton.Padding = New Padding(10)

        ' Button Add
        btnAdd = New Button()
        btnAdd.Text = "Tambah"
        btnAdd.Size = New Size(100, 30)
        btnAdd.Location = New Point(10, 10)
        btnAdd.BackColor = Color.FromArgb(92, 184, 92)
        btnAdd.ForeColor = Color.White
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.Cursor = Cursors.Hand

        ' Button Edit
        btnEdit = New Button()
        btnEdit.Text = "Edit"
        btnEdit.Size = New Size(100, 30)
        btnEdit.Location = New Point(120, 10)
        btnEdit.BackColor = Color.FromArgb(240, 173, 78)
        btnEdit.ForeColor = Color.White
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.Cursor = Cursors.Hand

        ' Button Delete
        btnDelete = New Button()
        btnDelete.Text = "Hapus"
        btnDelete.Size = New Size(100, 30)
        btnDelete.Location = New Point(230, 10)
        btnDelete.BackColor = Color.FromArgb(217, 83, 79)
        btnDelete.ForeColor = Color.White
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.Cursor = Cursors.Hand

        ' Button Refresh
        btnRefresh = New Button()
        btnRefresh.Text = "Refresh"
        btnRefresh.Size = New Size(100, 30)
        btnRefresh.Location = New Point(340, 10)
        btnRefresh.BackColor = Color.FromArgb(91, 192, 222)
        btnRefresh.ForeColor = Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Cursor = Cursors.Hand

        pnlButton.Controls.AddRange({btnAdd, btnEdit, btnDelete, btnRefresh})

        ' DataGridView
        dgvSupplier = New DataGridView()
        dgvSupplier.Dock = DockStyle.Fill
        dgvSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSupplier.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSupplier.MultiSelect = False
        dgvSupplier.ReadOnly = True
        dgvSupplier.AllowUserToAddRows = False
        dgvSupplier.AllowUserToDeleteRows = False
        dgvSupplier.BackgroundColor = Color.White
        dgvSupplier.RowHeadersVisible = False

        ' Add controls
        Me.Controls.Add(dgvSupplier)
        Me.Controls.Add(pnlButton)
        Me.Controls.Add(pnlTop)

        Me.ResumeLayout(False)
    End Sub

    Private Sub frmListSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()
        dgvSupplier.DataSource = _controller.GetDataTable()
        If dgvSupplier.Columns.Contains("id") Then
            dgvSupplier.Columns("id").Visible = False
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmSupplierInput()
        frm.IsEditMode = False
        If frm.ShowDialog() = DialogResult.OK Then
            LoadData()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgvSupplier.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih supplier yang akan diedit!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = Convert.ToInt32(dgvSupplier.SelectedRows(0).Cells("id").Value)
        Dim frm As New frmSupplierInput()
        frm.IsEditMode = True
        frm.SupplierID = id
        If frm.ShowDialog() = DialogResult.OK Then
            LoadData()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvSupplier.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih supplier yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = Convert.ToInt32(dgvSupplier.SelectedRows(0).Cells("id").Value)
        Dim supplierName As String = dgvSupplier.SelectedRows(0).Cells("Nama Supplier").Value.ToString()

        If MessageBox.Show("Apakah Anda yakin ingin menghapus supplier '" & supplierName & "'?",
                          "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            If _controller.Delete(id) Then
                MessageBox.Show("Supplier berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData()
            Else
                MessageBox.Show("Gagal menghapus supplier!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub
End Class
