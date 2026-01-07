Imports Microsoft.Reporting.WinForms
Imports System.Runtime.InteropServices

Public Class FormPurchaseReport

    Private _controller As PurchaseReportController

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function LoadLibrary(libname As String) As IntPtr
    End Function

    Private Sub LoadSqlServerTypes()
        Dim nativeBinaryPath As String = IO.Path.Combine(Application.StartupPath, "SqlServerTypes")
        Dim subDir As String = If(Environment.Is64BitProcess, "x64", "x86")
        Dim dllPath As String = IO.Path.Combine(nativeBinaryPath, subDir, "SqlServerSpatial140.dll")
        If IO.File.Exists(dllPath) Then
            LoadLibrary(dllPath)
        End If
    End Sub

    Private Sub FormPurchaseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSqlServerTypes()

        _controller = New PurchaseReportController()
        LoadReport()

        Me.ReportViewer1.RefreshReport()
    End Sub

    Sub LoadReport()
        Dim dt As DataTable = _controller.GetPurchaseReport()
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Reports\PurchaseReport.rdlc")
        Dim rds As New ReportDataSource("DataSet2", dt)
        ReportViewer1.LocalReport.DataSources.Add(rds)
        ReportViewer1.RefreshReport()
    End Sub
End Class
