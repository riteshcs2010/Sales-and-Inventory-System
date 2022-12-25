Imports System.IO
Imports System.Runtime.InteropServices
Imports LicenseModuleHelper.Library

Public Class frmSplash

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        frmSplashTimerTickEvents(Me, frmActivation, frmLogin, frmSqlServerSetting, Timer1, txtHardwareID, ProgressBar1, lblSet)
    End Sub

    Private Sub frmSplash1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If File.Exists(Application.StartupPath & "\SQLSettings.dat") Then
            'Call the splash form load events handler and pass your connection string as shown below
            AutoUpdaterUrl = UpdaterUrl
            frmSplashLoadEvents(cs)
            txtHardwareID.Text = ArchitectureId
        End If
    End Sub

End Class