Public Class SaleController
    Public Function SaveNew(sale As SaleModel) As Boolean
        Return SaleModel.Insert(sale)
    End Function

    Public Function generateCode() As String
        Return SaleModel.GenerateKodeTransaksi()
    End Function

End Class
