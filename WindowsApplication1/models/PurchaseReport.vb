Imports MySql.Data.MySqlClient

Public Class PurchaseReport
    Public Shared Function GetAll() As DataTable
        Dim dt As New DataTable
        Dim query As String = "SELECT PD.id as ID, P.idPurchase as NOTA, P.purchaseDate as TGL_NOTA, " &
            "S.supplierName as SUPPLIER, I.itemID as KODE_BRG, I.itemDesc as NAMA_BRG, " &
            "PD.qtyPurchase AS QTY, PD.purchasePrice AS HARGA, I.unit, " &
            "PD.subtotal AS SUBTOTAL " &
            "FROM purchase P " &
            "INNER JOIN purchasedetail PD ON P.idPurchase = PD.idPurchase " &
            "INNER JOIN supplier S ON P.supplierID = S.id " &
            "LEFT JOIN items I ON PD.itemID = I.id " &
            "ORDER BY P.idPurchase, PD.id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return dt

            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function
End Class
