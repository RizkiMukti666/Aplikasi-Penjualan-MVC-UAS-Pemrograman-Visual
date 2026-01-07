Imports MySql.Data.MySqlClient
Public Class SalesReport
    Public Shared Function GetAll() As DataTable
        Dim dt As New DataTable
        Dim query As String = "SELECT SD.id as ID, S.idTrans as NOTA, S.saleDate as TGL_NOTA, " &
            "I.itemID as KODE_BRG, I.itemDesc as NAMA_BRG, SD.qtySale AS QTY, " &
            "SD.price AS HARGA, I.unit, SD.subtotal AS SUBTOTAL " &
            "FROM sale S " &
            "INNER JOIN saledetail SD ON S.idTrans = SD.idSale " &
            "LEFT JOIN items I ON SD.itemID = I.id " &
            "ORDER BY S.idTrans, SD.id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return dt

            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function
End Class
