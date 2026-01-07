Imports MySql.Data.MySqlClient

Public Class Supplier
    Public Property Id As Integer
    Public Property SupplierID As String
    Public Property SupplierName As String
    Public Property Address As String
    Public Property Phone As String
    Public Property Email As String
    Public Property ContactPerson As String
    Public Property IsActive As Boolean
    Public Property CreatedDate As DateTime

    ''' <summary>
    ''' Get all active suppliers
    ''' </summary>
    Public Shared Function GetAll() As List(Of Supplier)
        Dim suppliers As New List(Of Supplier)
        Dim query As String = "SELECT * FROM supplier WHERE isActive = 1 ORDER BY supplierName"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return suppliers

            Using cmd As New MySqlCommand(query, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        suppliers.Add(New Supplier() With {
                            .Id = Convert.ToInt32(reader("id")),
                            .SupplierID = reader("supplierID").ToString(),
                            .SupplierName = reader("supplierName").ToString(),
                            .Address = If(IsDBNull(reader("address")), "", reader("address").ToString()),
                            .Phone = If(IsDBNull(reader("phone")), "", reader("phone").ToString()),
                            .Email = If(IsDBNull(reader("email")), "", reader("email").ToString()),
                            .ContactPerson = If(IsDBNull(reader("contactPerson")), "", reader("contactPerson").ToString()),
                            .IsActive = Convert.ToBoolean(reader("isActive")),
                            .CreatedDate = Convert.ToDateTime(reader("createdDate"))
                        })
                    End While
                End Using
            End Using
        End Using

        Return suppliers
    End Function

    ''' <summary>
    ''' Get supplier by ID
    ''' </summary>
    Public Shared Function GetById(id As Integer) As Supplier
        Dim supplier As Supplier = Nothing
        Dim query As String = "SELECT * FROM supplier WHERE id = @id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return Nothing

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        supplier = New Supplier() With {
                            .Id = Convert.ToInt32(reader("id")),
                            .SupplierID = reader("supplierID").ToString(),
                            .SupplierName = reader("supplierName").ToString(),
                            .Address = If(IsDBNull(reader("address")), "", reader("address").ToString()),
                            .Phone = If(IsDBNull(reader("phone")), "", reader("phone").ToString()),
                            .Email = If(IsDBNull(reader("email")), "", reader("email").ToString()),
                            .ContactPerson = If(IsDBNull(reader("contactPerson")), "", reader("contactPerson").ToString()),
                            .IsActive = Convert.ToBoolean(reader("isActive")),
                            .CreatedDate = Convert.ToDateTime(reader("createdDate"))
                        }
                    End If
                End Using
            End Using
        End Using

        Return supplier
    End Function

    ''' <summary>
    ''' Generate new Supplier ID
    ''' </summary>
    Public Shared Function GenerateNewID() As String
        Dim newID As String = "SUP0001"
        Dim query As String = "SELECT supplierID FROM supplier ORDER BY id DESC LIMIT 1"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return newID

            Using cmd As New MySqlCommand(query, conn)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Dim lastID As String = result.ToString()
                    Dim num As Integer = Integer.Parse(lastID.Substring(3)) + 1
                    newID = "SUP" & num.ToString("D4")
                End If
            End Using
        End Using

        Return newID
    End Function

    ''' <summary>
    ''' Insert new supplier
    ''' </summary>
    Public Function Insert() As Boolean
        Dim query As String = "INSERT INTO supplier (supplierID, supplierName, address, phone, email, contactPerson) " &
                             "VALUES (@supplierID, @supplierName, @address, @phone, @email, @contactPerson)"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return False

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@supplierID", Me.SupplierID)
                cmd.Parameters.AddWithValue("@supplierName", Me.SupplierName)
                cmd.Parameters.AddWithValue("@address", If(String.IsNullOrEmpty(Me.Address), DBNull.Value, Me.Address))
                cmd.Parameters.AddWithValue("@phone", If(String.IsNullOrEmpty(Me.Phone), DBNull.Value, Me.Phone))
                cmd.Parameters.AddWithValue("@email", If(String.IsNullOrEmpty(Me.Email), DBNull.Value, Me.Email))
                cmd.Parameters.AddWithValue("@contactPerson", If(String.IsNullOrEmpty(Me.ContactPerson), DBNull.Value, Me.ContactPerson))

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Update supplier
    ''' </summary>
    Public Function Update() As Boolean
        Dim query As String = "UPDATE supplier SET supplierName = @supplierName, address = @address, " &
                             "phone = @phone, email = @email, contactPerson = @contactPerson WHERE id = @id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return False

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", Me.Id)
                cmd.Parameters.AddWithValue("@supplierName", Me.SupplierName)
                cmd.Parameters.AddWithValue("@address", If(String.IsNullOrEmpty(Me.Address), DBNull.Value, Me.Address))
                cmd.Parameters.AddWithValue("@phone", If(String.IsNullOrEmpty(Me.Phone), DBNull.Value, Me.Phone))
                cmd.Parameters.AddWithValue("@email", If(String.IsNullOrEmpty(Me.Email), DBNull.Value, Me.Email))
                cmd.Parameters.AddWithValue("@contactPerson", If(String.IsNullOrEmpty(Me.ContactPerson), DBNull.Value, Me.ContactPerson))

                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Delete (soft delete) supplier
    ''' </summary>
    Public Shared Function Delete(id As Integer) As Boolean
        Dim query As String = "UPDATE supplier SET isActive = 0 WHERE id = @id"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return False

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                Return cmd.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Get DataTable for DataGridView binding
    ''' </summary>
    Public Shared Function GetDataTable() As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT id, supplierID as 'Kode Supplier', supplierName as 'Nama Supplier', " &
                             "address as 'Alamat', phone as 'Telepon', email as 'Email', " &
                             "contactPerson as 'Contact Person' FROM supplier WHERE isActive = 1 ORDER BY supplierName"

        Using conn = Koneksi.OpenConnection()
            If conn Is Nothing Then Return dt

            Using cmd As New MySqlCommand(query, conn)
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using

        Return dt
    End Function
End Class
