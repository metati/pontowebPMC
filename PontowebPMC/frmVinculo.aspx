﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmVinculo.aspx.cs" Inherits="frmVinculo" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" style="font-size: medium" 
                    Text="lbTextoUsuario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <dx1:ASPxRadioButtonList ID="rlbVinculo" runat="server" 
                    ClientInstanceName="rlbVinculo" Theme="DevEx" ValueType="System.String">
                    <ValidationSettings ValidationGroup="ValidaVinculo">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx1:ASPxRadioButtonList>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td>
                            <dx1:ASPxButton ID="btSalvarVinculo" runat="server" ClientInstanceName="btSalvarVinculo" 
                                onclick="btSalvar_Click" Text="Ok" Theme="DevEx" Width="100px" 
                    ValidationGroup="ValidaVinculo">
                            </dx1:ASPxButton>
                        </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
