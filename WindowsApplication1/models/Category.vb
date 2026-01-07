Imports MySql.Data.MySqlClient
Public Class CategoryModel
    Public Property id As Integer
    Public Property category_desc As String
    Public Shared Function getAll() As DataTable
        Dim dt As New DataTable
        Dim query As String = "SELECT * FROM category order by categoryDesc"

        Using conn = Koneksi.OpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function
    Public Shared Function GetCategoryById(id As Integer) As CategoryModel
        Dim dt As New DataTable()
        Dim query As String = "SELECT * FROM category WHERE id=@id"

        Using conn = Koneksi.OpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        If dt.Rows.Count = 0 Then Return Nothing

        Dim row = dt.Rows(0)
        Return New CategoryModel With {
           .id = row("id"),
           .category_desc = row("categoryDesc").ToString()
        }
    End Function

    Public Function CreateCategory(category As CategoryModel) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "INSERT INTO category (categoryDesc) VALUES (@cateDesc)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@cateDesc", category.category_desc)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ' UPDATE
    Public Function UpdateCategory(category As CategoryModel) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "UPDATE category SET categoryDesc=@cateDesc WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@cateDesc", category.category_desc)
                cmd.Parameters.AddWithValue("@id", category.id)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ' DELETE
    Public Function DeleteCategory(id As Integer) As Boolean
        Using conn = Koneksi.OpenConnection()
            Dim query = "DELETE FROM category WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function


End Class
