Public Class frmListItem
    Dim controller As New ItemController
    Private Sub frmListItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGrid()
    End Sub
    Sub LoadGrid()
        DataGridView1.AutoGenerateColumns = False
        Dim dt As DataTable = controller.LoadItems()

        DataGridView1.DataSource = dt
        DataGridView1.Columns("salesPrice").HeaderCell.Style.Alignment =
    DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dv As DataView = CType(DataGridView1.DataSource, DataTable).DefaultView
        dv.RowFilter = "itemDesc LIKE '%" & txtSearch.Text & "%' OR categoryDesc LIKE '%" & txtSearch.Text & "%'"
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New frmItemInput()
        If frm.ShowDialog() = DialogResult.OK Then
            LoadGrid()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then Return

        Dim id = DataGridView1.SelectedRows(0).Cells("id").Value

        If MessageBox.Show("Hapus item ini?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            controller.Delete(id)
            LoadGrid()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If DataGridView1.SelectedRows.Count = 0 Then Return

        Dim id As Integer = CInt(DataGridView1.SelectedRows(0).Cells("ID").Value)
        Dim item_selected = controller.GetItemById(id)

        Dim frm As New frmItemInput(item_selected)
        If frm.ShowDialog() = DialogResult.OK Then
            LoadGrid()
        End If
    End Sub
End Class