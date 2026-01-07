Public Class PurchaseController
    ''' <summary>
    ''' Generate new purchase ID
    ''' </summary>
    Public Function GenerateNewID() As String
        Return Purchase.GenerateNewID()
    End Function

    ''' <summary>
    ''' Insert purchase with details
    ''' </summary>
    Public Function Insert(purchase As Purchase) As Boolean
        Return purchase.Insert()
    End Function

    ''' <summary>
    ''' Get all purchases
    ''' </summary>
    Public Function GetAll() As DataTable
        Return Purchase.GetAll()
    End Function

    ''' <summary>
    ''' Get purchase by ID
    ''' </summary>
    Public Function GetById(idPurchase As String) As Purchase
        Return Purchase.GetById(idPurchase)
    End Function

    ''' <summary>
    ''' Get all suppliers for combo box
    ''' </summary>
    Public Function GetSuppliers() As List(Of Supplier)
        Return Supplier.GetAll()
    End Function
End Class
