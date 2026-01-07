Public Class frmAddCategory

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim category As New CategoryModel With {
           .category_desc = txtCategoryDec.Text
       }

        Dim ctrl As New CategoryController
        ctrl.Create(category)

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class