Public Class SupplierController
    ''' <summary>
    ''' Get all suppliers
    ''' </summary>
    Public Function GetAll() As List(Of Supplier)
        Return Supplier.GetAll()
    End Function

    ''' <summary>
    ''' Get supplier by ID
    ''' </summary>
    Public Function GetById(id As Integer) As Supplier
        Return Supplier.GetById(id)
    End Function

    ''' <summary>
    ''' Generate new supplier ID
    ''' </summary>
    Public Function GenerateNewID() As String
        Return Supplier.GenerateNewID()
    End Function

    ''' <summary>
    ''' Insert new supplier
    ''' </summary>
    Public Function Insert(supplier As Supplier) As Boolean
        Return supplier.Insert()
    End Function

    ''' <summary>
    ''' Update supplier
    ''' </summary>
    Public Function Update(supplier As Supplier) As Boolean
        Return supplier.Update()
    End Function

    ''' <summary>
    ''' Delete supplier
    ''' </summary>
    Public Function Delete(id As Integer) As Boolean
        Return Supplier.Delete(id)
    End Function

    ''' <summary>
    ''' Get DataTable for grid
    ''' </summary>
    Public Function GetDataTable() As DataTable
        Return Supplier.GetDataTable()
    End Function
End Class
