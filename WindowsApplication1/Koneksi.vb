Imports MySql.Data.MySqlClient

Module Koneksi
    Private ReadOnly server As String = "localhost"
    Private ReadOnly port As String = "33000"
    Private ReadOnly database As String = "dbpenjualan"
    Private ReadOnly username As String = "root"
    Private ReadOnly password As String = ""

    ' Fungsi utama untuk membuka koneksi 
    Public Function OpenConnection() As MySqlConnection
        Try
            Dim connString As String = String.Format("server={0};port={1};user id={2};password={3};database={4};", server, port, username, password, database)
            Dim conn As New MySqlConnection(connString)
            conn.Open()
            Return conn

        Catch ex As MySqlException
            MessageBox.Show("Koneksi gagal: " & ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' Fungsi untuk mendapatkan connection string
    Public Function GetConnectionString() As String
        Return String.Format("server={0};port={1};user id={2};password={3};database={4};", server, port, username, password, database)
    End Function
End Module
