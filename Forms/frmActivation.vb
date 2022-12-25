Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports LicenseModuleHelper.Library
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class frmActivation

    Private Sub frmActivation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtHardwareID.Text = HardwareId
        txtActivationID.Select()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        frmActivationBtnSaveClickEvents(Me, frmLogin, txtActivationID, cs, LicenseValidationUrl)
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        End
    End Sub
    Private Sub lnkPurchaseLicense_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkPurchaseLicense.LinkClicked
        frmActivationLnkPurchaseLicenseClickEvent(LicensePurchaseUrl, txtActivationID)
    End Sub
End Class
