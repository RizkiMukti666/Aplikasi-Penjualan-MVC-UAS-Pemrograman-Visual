Imports MySql.Data.MySqlClient

Public Class Purchase
    Public Property IdPurchase As String
    Public Property PurchaseDate As DateTime
    Public Property SupplierID As Integer
    Public Property TotalAmount As Integer
    Public Property Notes As String
    Public Property CreatedBy As Integer?
    Public Property CreatedDate As DateTime

    ' Navigation property
    Public Property Supplier As Supplier
    Public Property Details As List(Of PurchaseDetail)

    Public Sub New()
        Details = New List(Of PurchaseDetail)
    End Sub

    ''' <summary>
    ''' Generate new Purchase ID
    ''' </summary>
    Public Shared Function GenerateNewID() As String
        Dim newID As String = "PUR0001"
        Dim query As String = "SELECT idPurchase FROM purchase ORDER BY idPurchase DESC LIMIT 1"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return newID

            Using cmd As New MySqlCommand(query, conn)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Dim lastID As String = result.ToString()
                    Dim num As Integer = Integer.Parse(lastID.Substring(3)) + 1
                    newID = "PUR" & num.ToString("D4")
                End If
            End Using
        End Using

        Return newID
    End Function

    ''' <summary>
    ''' Insert purchase with details (transaction)
    ''' </summary>
    Public Function Insert() As Boolean
        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return False

            Using transaction = conn.BeginTransaction()
                Try
                    ' Insert purchase header
                    Dim queryHeader As String = "INSERT INTO purchase (idPurchase, purchaseDate, supplierID, totalAmount, notes, createdBy) " &
                                               "VALUES (@idPurchase, @purchaseDate, @supplierID, @totalAmount, @notes, @createdBy)"

                    Using cmd As New MySqlCommand(queryHeader, conn, transaction)
                        cmd.Parameters.AddWithValue("@idPurchase", Me.IdPurchase)
                        cmd.Parameters.AddWithValue("@purchaseDate", Me.PurchaseDate)
                        cmd.Parameters.AddWithValue("@supplierID", Me.SupplierID)
                        cmd.Parameters.AddWithValue("@totalAmount", Me.TotalAmount)
                        cmd.Parameters.AddWithValue("@notes", If(String.IsNullOrEmpty(Me.Notes), DBNull.Value, Me.Notes))
                        cmd.Parameters.AddWithValue("@createdBy", If(Me.CreatedBy.HasValue, Me.CreatedBy.Value, DBNull.Value))
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Insert purchase details and update stock
                    For Each detail In Me.Details
                        ' Insert detail
                        Dim queryDetail As String = "INSERT INTO purchasedetail (idPurchase, itemID, qtyPurchase, purchasePrice, subtotal) " &
                                                   "VALUES (@idPurchase, @itemID, @qtyPurchase, @purchasePrice, @subtotal)"

                        Using cmd As New MySqlCommand(queryDetail, conn, transaction)
                            cmd.Parameters.AddWithValue("@idPurchase", Me.IdPurchase)
                            cmd.Parameters.AddWithValue("@itemID", detail.ItemID)
                            cmd.Parameters.AddWithValue("@qtyPurchase", detail.QtyPurchase)
                            cmd.Parameters.AddWithValue("@purchasePrice", detail.PurchasePrice)
                            cmd.Parameters.AddWithValue("@subtotal", detail.Subtotal)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Update item stock (add stock)
                        Dim queryStock As String = "UPDATE items SET stock = stock + @qty WHERE id = @itemID"
                        Using cmd As New MySqlCommand(queryStock, conn, transaction)
                            cmd.Parameters.AddWithValue("@qty", detail.QtyPurchase)
                            cmd.Parameters.AddWithValue("@itemID", detail.ItemID)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    transaction.Commit()
                    Return True

                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End Try
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Get all purchases
    ''' </summary>
    Public Shared Function GetAll() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT p.idPurchase as 'No. Pembelian', p.purchaseDate as 'Tanggal', " &
                             "s.supplierName as 'Supplier', p.totalAmount as 'Total', p.notes as 'Catatan' " &
                             "FROM purchase p " &
                             "INNER JOIN supplier s ON p.supplierID = s.id " &
                             "ORDER BY p.purchaseDate DESC"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return dt

            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function

    ''' <summary>
    ''' Get purchase by ID with details
    ''' </summary>
    Public Shared Function GetById(idPurchase As String) As Purchase
        Dim purchase As Purchase = Nothing
        Dim query As String = "SELECT * FROM purchase WHERE idPurchase = @idPurchase"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return Nothing

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@idPurchase", idPurchase)

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        purchase = New Purchase() With {
                            .IdPurchase = reader("idPurchase").ToString(),
                            .PurchaseDate = Convert.ToDateTime(reader("purchaseDate")),
                            .SupplierID = Convert.ToInt32(reader("supplierID")),
                            .TotalAmount = Convert.ToInt32(reader("totalAmount")),
                            .Notes = If(IsDBNull(reader("notes")), "", reader("notes").ToString()),
                            .CreatedBy = If(IsDBNull(reader("createdBy")), Nothing, Convert.ToInt32(reader("createdBy"))),
                            .CreatedDate = Convert.ToDateTime(reader("createdDate"))
                        }
                    End If
                End Using
            End Using

            ' Get details
            If purchase IsNot Nothing Then
                purchase.Details = PurchaseDetail.GetByPurchaseId(idPurchase)
            End If
        End Using

        Return purchase
    End Function
End Class
