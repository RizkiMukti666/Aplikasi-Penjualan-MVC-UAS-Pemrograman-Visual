Imports MySql.Data.MySqlClient

Public Class User
    Public Property Id As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property Fullname As String
    Public Property Email As String
    Public Property Role As String
    Public Property IsActive As Boolean
    Public Property CreatedDate As DateTime
    Public Property LastLogin As DateTime?

    ' Shared property untuk menyimpan user yang sedang login
    Public Shared CurrentUser As User = Nothing

    ''' <summary>
    ''' Fungsi untuk login user
    ''' </summary>
    Public Shared Function Login(username As String, password As String) As User
        Dim user As User = Nothing
        Dim query As String = "SELECT * FROM tb_user WHERE username = @username AND password = @password AND isActive = 1"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return Nothing

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        user = New User() With {
                            .Id = Convert.ToInt32(reader("id")),
                            .Username = reader("username").ToString(),
                            .Password = reader("password").ToString(),
                            .Fullname = reader("fullname").ToString(),
                            .Email = If(IsDBNull(reader("email")), "", reader("email").ToString()),
                            .Role = reader("role").ToString(),
                            .IsActive = Convert.ToBoolean(reader("isActive")),
                            .CreatedDate = Convert.ToDateTime(reader("createdDate")),
                            .LastLogin = If(IsDBNull(reader("lastLogin")), Nothing, Convert.ToDateTime(reader("lastLogin")))
                        }
                    End If
                End Using
            End Using

            ' Update last login jika berhasil login
            If user IsNot Nothing Then
                UpdateLastLogin(user.Id)
                CurrentUser = user
            End If
        End Using

        Return user
    End Function

    ''' <summary>
    ''' Update waktu login terakhir
    ''' </summary>
    Private Shared Sub UpdateLastLogin(userId As Integer)
        Dim query As String = "UPDATE tb_user SET lastLogin = NOW() WHERE id = @id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", userId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Logout user
    ''' </summary>
    Public Shared Sub Logout()
        CurrentUser = Nothing
    End Sub

    ''' <summary>
    ''' Cek apakah user sudah login
    ''' </summary>
    Public Shared Function IsLoggedIn() As Boolean
        Return CurrentUser IsNot Nothing
    End Function
End Class
