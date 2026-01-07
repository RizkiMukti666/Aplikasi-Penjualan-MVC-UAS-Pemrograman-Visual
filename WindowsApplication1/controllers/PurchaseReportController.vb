Public Class PurchaseReportController
    Public Function GetPurchaseReport() As DataTable
        Return PurchaseReport.GetAll()
    End Function
End Class
