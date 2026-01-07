Imports MySql.Data.MySqlClient

Public Class ItemModel
    Public Property id As Integer
    Public Property itemID As String
    Public Property itemDesc As String
    Public Property itemCate As Integer
    Public Property unit As String
    Public Property salesPrice As Integer
    Public Property minStock As Integer
    Public Shared Function getAll() As DataTable
        Dim dt As New DataTable
        Dim query As String = "SELECT items.*,categoryDesc " & _
        " FROM items LEFT JOIN category ON items.itemCate = category.id "

        Using conn = Koneksi.OpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt

    End Function
    Public Shared Function GetItemById(id As Integer) As ItemModel
        Dim dt As New DataTable()
        Dim query As String = "SELECT * FROM items WHERE id=@id"

        Using conn = Koneksi.OpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        If dt.Rows.Count = 0 Then Return Nothing

        Dim row = dt.Rows(0)

        Return New ItemModel With {
           .id = row("id"),
           .itemID = row("itemID").ToString(),
           .itemDesc = row("itemDesc").ToString(),
           .itemCate = Convert.ToInt32(row("itemCate")),
           .unit = row("unit").ToString(),
           .salesPrice = Convert.ToInt32(row("salesPrice")),
           .minStock = Convert.ToInt32(row("minStock"))
        }
    End Function

    ' CREATE
    Public Function CreateItem(item As ItemModel) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "INSERT INTO items (itemID, itemDesc, itemCate, unit, salesPrice, minStock) VALUES " & _
                "(@itemId, @itemDesc, @itemCate, @unit, @salesPrice,@minStock)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@itemId", item.itemID)
                cmd.Parameters.AddWithValue("@itemDesc", item.itemDesc)
                cmd.Parameters.AddWithValue("@itemCate", item.itemCate)
                cmd.Parameters.AddWithValue("@unit", item.unit)
                cmd.Parameters.AddWithValue("@salesPrice", item.salesPrice)
                cmd.Parameters.AddWithValue("@minStock", item.minStock)

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ' UPDATE
    Public Function UpdateItem(item As ItemModel) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "UPDATE items SET  " & _
                        " itemID=@itemId, itemDesc=@itemDesc, itemCate=@itemCate, unit=@unit, salesPrice=@salesPrice, " & _
                        " minStock=@minStock WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@itemId", item.itemID)
                cmd.Parameters.AddWithValue("@itemDesc", item.itemDesc)
                cmd.Parameters.AddWithValue("@itemCate", item.itemCate)
                cmd.Parameters.AddWithValue("@unit", item.unit)
                cmd.Parameters.AddWithValue("@salesPrice", item.salesPrice)
                cmd.Parameters.AddWithValue("@minStock", item.minStock)
                cmd.Parameters.AddWithValue("@id", item.id)

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ' DELETE
    Public Function DeleteItem(id As Integer) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "DELETE FROM items WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function
    Public Function GenerateItemCode() As String
        Dim newCode As String = "B0001"
        Dim query As String = "SELECT itemID FROM items ORDER BY itemID DESC LIMIT 1"

        Try
            Using conn = Koneksi.OpenConnection()
                Dim cmd As New MySqlCommand(query, conn)
                Dim lastCode As Object = cmd.ExecuteScalar()

                If lastCode IsNot Nothing AndAlso Not IsDBNull(lastCode) Then
                    Dim numberPart As Integer = CInt(Mid(lastCode.ToString(), 4))

                    numberPart += 1

                    ' Format ulang menjadi Bxxxx
                    newCode = "B" & numberPart.ToString("0000")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return newCode
    End Function


End Class
