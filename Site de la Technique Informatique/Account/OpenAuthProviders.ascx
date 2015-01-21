﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="OpenAuthProviders.ascx.vb" Inherits="Site_de_la_Technique_Informatique.OpenAuthProviders" %>

<div id="socialLoginList">
    <h4>Connectez-vous à l'aide d'un autre service.</h4>
    <hr />
    <asp:ListView runat="server" ID="providerDetails" ItemType="System.String"
        SelectMethod="GetProviderNames" ViewStateMode="Disabled">
        <ItemTemplate>
            <p>
                <button type="submit" class="btn btn-default" name="provider" value="<%#: Item %>"
                    title="Connexion à l'aide de votre <%#: Item %> compte.">
                    <%#: Item %>
                </button>
            </p>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div>
                <p>Aucun service d'authentification externe n'est configuré. Consultez <a href="http://go.microsoft.com/fwlink/?LinkId=252803">cet article</a> pour plus de détails sur la configuration de cette application ASP.NET afin qu'elle prenne en charge la connexion par l'intermédiaire de services externes.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</div>
