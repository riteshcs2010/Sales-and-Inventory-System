Imports System.IO
Imports System.Net

Module ModCS
    Dim st As String
    Public Const LicenseValidationUrl = "https://licenses.actavista.us"
    Public Const LicensePurchaseUrl = "https://license.actavista.net"
    Public Const UpdaterUrl = "https://actavista.org/updates/salesandinventorymgt.xml"
    Public Function ReadCS() As String
        Using sr As StreamReader = New StreamReader(Application.StartupPath & "\SQLSettings.dat")
            st = sr.ReadLine()
        End Using
        Return st
    End Function
    Public ReadOnly cs As String = Decrypt(ReadCS())
End Module
