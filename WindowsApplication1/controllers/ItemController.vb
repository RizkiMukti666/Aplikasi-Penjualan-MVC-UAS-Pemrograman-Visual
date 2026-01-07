Public Class ItemController
    Public Function LoadItems() As DataTable
        Return ItemModel.getAll()
    End Function

    Public Function GetItemById(id As Integer) As ItemModel
        Return ItemModel.GetItemById(id)
    End Function

    Public Function Create(item As ItemModel) As Boolean
        If Not ValidateItem(item) Then Return False
        Return item.CreateItem(item)
    End Function

    ' UPDATE
    Public Function Update(item As ItemModel) As Boolean
        If Not ValidateItem(item) Then Return False
        Return item.UpdateItem(item)
    End Function

    ' DELETE
    Public Function Delete(id As Integer) As Boolean
        Return New ItemModel().DeleteItem(id)
    End Function
    Private Function ValidateItem(item As ItemModel) As Boolean
        If String.IsNullOrWhiteSpace(item.itemID) Then
            MessageBox.Show("Item ID tidak boleh kosong")
            Return False
        End If

        If String.IsNullOrWhiteSpace(item.itemDesc) Then
            MessageBox.Show("Item Description tidak boleh kosong")
            Return False
        End If

        If item.salesPrice < 0 Then
            MessageBox.Show("Harga tidak boleh negatif")
            Return False
        End If

        Return True
    End Function
   

End Class
