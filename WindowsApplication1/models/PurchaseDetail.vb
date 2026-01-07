Imports MySql.Data.MySqlClient

Public Class PurchaseDetail
    Public Property Id As Integer
    Public Property IdPurchase As String
    Public Property ItemID As Integer
    Public Property QtyPurchase As Integer
    Public Property PurchasePrice As Integer
    Public Property Subtotal As Integer

    ' Navigation property
    Public Property ItemName As String
    Public Property ItemUnit As String

    ''' <summary>
    ''' Get details by purchase ID
    ''' </summary>
    Public Shared Function GetByPurchaseId(idPurchase As String) As List(Of PurchaseDetail)
        Dim details As New List(Of PurchaseDetail)
        Dim query As String = "SELECT pd.*, i.itemDesc, i.unit FROM purchasedetail pd " &
                             "INNER JOIN items i ON pd.itemID = i.id " &
                             "WHERE pd.idPurchase = @idPurchase"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return details

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idPurchase", idPurchase)

                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        details.Add(New PurchaseDetail() With {
                            .Id = Convert.ToInt32(reader("id")),
                            .IdPurchase = reader("idPurchase").ToString(),
                            .ItemID = Convert.ToInt32(reader("itemID")),
                            .QtyPurchase = Convert.ToInt32(reader("qtyPurchase")),
                            .PurchasePrice = Convert.ToInt32(reader("purchasePrice")),
                            .Subtotal = Convert.ToInt32(reader("subtotal")),
                            .ItemName = reader("itemDesc").ToString(),
                            .ItemUnit = reader("unit").ToString()
                        })
                    End While
                End Using
            End Using
        End Using

        Return details
    End Function

    ''' <summary>
    ''' Get DataTable for display
    ''' </summary>
    Public Shared Function GetDataTableByPurchaseId(idPurchase As String) As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT pd.id, i.itemID as 'Kode', i.itemDesc as 'Nama Barang', " &
                             "pd.qtyPurchase as 'Qty', i.unit as 'Satuan', " &
                             "pd.purchasePrice as 'Harga Beli', pd.subtotal as 'Subtotal' " &
                             "FROM purchasedetail pd " &
                             "INNER JOIN items i ON pd.itemID = i.id " &
                             "WHERE pd.idPurchase = @idPurchase"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return dt

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idPurchase", idPurchase)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function
End Class
