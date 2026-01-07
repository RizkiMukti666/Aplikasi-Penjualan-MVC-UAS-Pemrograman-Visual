Public Class CategoryController
    Public Function LoadCategory() As DataTable
        Return CategoryModel.getAll()
    End Function

    Public Function getCategory(id As Integer) As CategoryModel
        Return CategoryModel.GetCategoryById(id)
    End Function

    Public Function Create(category As CategoryModel) As Boolean
        If Not Validatecategory(category) Then Return False
        Return category.Createcategory(category)
    End Function

    ' UPDATE
    Public Function Update(category As CategoryModel) As Boolean
        If Not Validatecategory(category) Then Return False
        Return category.Updatecategory(category)
    End Function

    ' DELETE
    Public Function Delete(id As Integer) As Boolean
        Return New CategoryModel().DeleteCategory(id)
    End Function

    Private Function Validatecategory(category As CategoryModel) As Boolean

        If String.IsNullOrWhiteSpace(category.category_desc) Then
            MessageBox.Show("category Description tidak boleh kosong")
            Return False
        End If

        Return True
    End Function
End Class
