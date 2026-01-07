Imports MySql.Data.MySqlClient
Public Class SaleModel
    Public Property idTrans As String
    Public Property saleDate As DateTime
    Public Property totalSale As Double
    Public Property totalAmount As Integer
    Public Property customerName As String
    Public Property notes As String
    Public Property createdBy As Integer
    Public Property details As List(Of SaleDetailModel)

    Public Shared Function Insert(sale As SaleModel) As Boolean
        Using conn = Koneksi.OpenConnection()

            Try
                ' Insert Master - hanya kolom yang ada di tabel
                Dim cmd As New MySqlCommand("INSERT INTO sale (idTrans, saleDate) VALUES (@kode, @tanggal)", conn)

                cmd.Parameters.AddWithValue("@kode", sale.idTrans)
                cmd.Parameters.AddWithValue("@tanggal", sale.saleDate)
                cmd.ExecuteNonQuery()

                Dim saleId = sale.idTrans

                ' Insert Detail & Update Stock
                For Each d In sale.details
                    ' Insert detail
                    Dim cmdDet As New MySqlCommand("INSERT INTO saledetail (idSale, itemID, qtySale, price, subtotal) " &
                        "VALUES (@sale_id, @item_id, @qty, @price, @subtotal)", conn)

                    cmdDet.Parameters.AddWithValue("@sale_id", saleId)
                    cmdDet.Parameters.AddWithValue("@item_id", d.ProductId)
                    cmdDet.Parameters.AddWithValue("@qty", d.Qty)
                    cmdDet.Parameters.AddWithValue("@price", d.Price)
                    cmdDet.Parameters.AddWithValue("@subtotal", d.Subtotal)
                    cmdDet.ExecuteNonQuery()

                    ' Update stock (kurangi stok)
                    Dim cmdUpdateStock As New MySqlCommand("UPDATE items SET stock = stock - @qty WHERE id = @item_id", conn)
                    cmdUpdateStock.Parameters.AddWithValue("@qty", d.Qty)
                    cmdUpdateStock.Parameters.AddWithValue("@item_id", d.ProductId)
                    cmdUpdateStock.ExecuteNonQuery()
                Next

                Return True

            Catch ex As Exception
                MessageBox.Show("Error Insert Sale: " & ex.Message)
                Return False
            End Try
        End Using
    End Function


    Public Shared Function GenerateKodeTransaksi() As String
        Dim kodeBaru As String = "TRX0001"
        Try
            Using conn = Koneksi.OpenConnection()

                Dim cmd As New MySqlCommand("SELECT idTrans FROM sale ORDER BY idTrans DESC LIMIT 1", conn)
                Dim rd = cmd.ExecuteReader()

                If rd.Read() Then
                    Dim kodeLama As String = rd("idTrans")
                    Dim nomor As Integer = CInt(kodeLama.Substring(3)) + 1
                    kodeBaru = "TRX" & nomor.ToString("0000")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error generate kode: " & ex.Message)
        End Try
        Return kodeBaru
    End Function
End Class
