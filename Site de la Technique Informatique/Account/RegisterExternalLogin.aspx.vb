﻿Imports System
Imports System.Security.Claims
Imports System.Web
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Partial Public Class RegisterExternalLogin
    Inherits System.Web.UI.Page
    Protected Property ProviderName() As String
        Get
            Return If(DirectCast(ViewState("ProviderName"), String), [String].Empty)
        End Get
        Private Set(value As String)
            ViewState("ProviderName") = value
        End Set
    End Property

    Protected Property ProviderAccountKey() As String
        Get
            Return If(DirectCast(ViewState("ProviderAccountKey"), String), [String].Empty)
        End Get
        Private Set(value As String)
            ViewState("ProviderAccountKey") = value
        End Set
    End Property

    Private Sub RedirectOnFail()
        Response.Redirect(If((User.Identity.IsAuthenticated), "~/Account/Manage", "~/Account/Login"))
    End Sub

    Protected Sub Page_Load() Handles Me.Load
        ' Traiter le résultat à partir d'un fournisseur d'authentification de la demande
        ProviderName = IdentityHelper.GetProviderNameFromRequest(Request)
        If [String].IsNullOrEmpty(ProviderName) Then
            RedirectOnFail()
            Return
        End If

        If Not IsPostBack Then
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo()
            If loginInfo Is Nothing Then
                RedirectOnFail()
                Return
            End If
            Dim appuser = manager.Find(loginInfo.Login)
            If appuser IsNot Nothing Then
                IdentityHelper.SignIn(manager, appuser, isPersistent:=False)
                IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
            ElseIf User.Identity.IsAuthenticated Then
                Dim verifiedloginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo(IdentityHelper.XsrfKey, User.Identity.GetUserId())
                If verifiedloginInfo Is Nothing Then
                    RedirectOnFail()
                    Return
                End If

                Dim result = manager.AddLogin(User.Identity.GetUserId(), verifiedloginInfo.Login)
                If result.Succeeded Then
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                Else
                    AddErrors(result)
                    Return
                End If
            Else
                email.Text = loginInfo.Email
            End If
        End If
    End Sub

    Protected Sub LogIn_Click(sender As Object, e As EventArgs)
        CreateAndLoginUser()
    End Sub

    Private Sub CreateAndLoginUser()
        If Not IsValid Then
            Return
        End If
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim user = New ApplicationUser() With {.UserName = email.Text, .Email = email.Text}
        Dim result = manager.Create(user)
        If Not result.Succeeded Then
            AddErrors(result)
            Return
        End If
        Dim loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo()
        If loginInfo Is Nothing Then
            RedirectOnFail()
            Return
        End If
        result = manager.AddLogin(user.Id, loginInfo.Login)
        If Not result.Succeeded Then
            AddErrors(result)
            Return
        End If
        IdentityHelper.SignIn(manager, user, False)

        ' Pour plus d'informations sur l'activation de la confirmation du compte et la réinitialisation du mot de passe, consultez http://go.microsoft.com/fwlink/?LinkID=320771
        ' Dim code = manager.GenerateEmailConfirmationToken(user.Id)
        ' Envoyer ce lien par e-mail : IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id)

        IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
        Return
    End Sub

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub
End Class