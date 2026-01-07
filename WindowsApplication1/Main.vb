Module Main
    Public Sub main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Tampilkan form login terlebih dahulu
        Dim loginForm As New frmLogin()
        Dim result As DialogResult = loginForm.ShowDialog()

        ' Jika login berhasil, buka form utama
        If result = DialogResult.OK Then
            Application.Run(New frmUtama())
        End If
    End Sub
End Module
