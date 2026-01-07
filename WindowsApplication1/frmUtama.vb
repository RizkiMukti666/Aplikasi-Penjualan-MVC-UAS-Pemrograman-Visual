Public Class frmUtama

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsToolStripMenuItem.Click
        Dim frm As New frmListItem

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem.Click
        Dim frm As New frmSale

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem1.Click
        Dim frm As New FormSalesReport

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub frmUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan informasi user yang login di title bar
        If User.IsLoggedIn() Then
            Me.Text = "Aplikasi Penjualan - " & User.CurrentUser.Fullname & " (" & User.CurrentUser.Role & ")"
        End If
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        Dim frm As New frmListSupplier

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PembelianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem.Click
        Dim frm As New frmPurchase

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PembelianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PembelianToolStripMenuItem1.Click
        Dim frm As New FormPurchaseReport

        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class