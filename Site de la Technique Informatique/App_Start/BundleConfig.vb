Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' Pour plus d'informations sur le regroupement (Bundling), consultez http://go.microsoft.com/fwlink/?LinkID=303951
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Scripts/WebForms/WebForms.js",
                        "~/Scripts/WebForms/WebUIValidation.js",
                        "~/Scripts/WebForms/MenuStandards.js",
                        "~/Scripts/WebForms/Focus.js",
                        "~/Scripts/WebForms/GridView.js",
                        "~/Scripts/WebForms/DetailsView.js",
                        "~/Scripts/WebForms/TreeView.js",
                        "~/Scripts/WebForms/WebParts.js"))

        ' L'ordre est très important pour que ces fichiers fonctionnent, car ils ont des dépendances explicites
        bundles.Add(New ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"))

        ' La version Development de Modernizr vous permet de développer et d’apprendre. Ensuite, lorsque vous êtes
        ' prêt pour une implémentation dans un environnement de production, utilisez l’outil de génération que vous trouverez à l’adresse http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"))

        ' Affectez la valeur false à EnableOptimizations pour le débogage. Pour plus d'informations,
        ' consultez http://go.microsoft.com/fwlink/?LinkId=301862
        BundleTable.EnableOptimizations = True

        ScriptManager.ScriptResourceMapping.AddDefinition("respond", New ScriptResourceDefinition() With {
                .Path = "~/Scripts/respond.min.js",
                .DebugPath = "~/Scripts/respond.js"})

    End Sub
End Class
