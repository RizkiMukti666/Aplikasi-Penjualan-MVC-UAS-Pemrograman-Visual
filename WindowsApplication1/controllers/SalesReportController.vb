Public Class SalesReportController
    Public Function getSalesReport() As DataTable
        Return SalesReport.getAll()
    End Function
End Class
