<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Defaultsession.aspx.cs" Inherits="Defaultsession" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   
    <style type="text/css">


.dxeButtonEditButton_DevEx,
.dxeSpinLargeIncButton_DevEx,
.dxeSpinLargeDecButton_DevEx,
.dxeSpinIncButton_DevEx,
.dxeSpinDecButton_DevEx
{
	background: White none;
}

.dxeButtonEditButton_DevEx
{
	border-top-width: 0;
	border-right-width: 0;
	border-bottom-width: 0;
	border-left-width: 1px;
}
.dxeButtonEditButton_DevEx,
.dxeButtonEdit_DevEx .dxeSBC
{
	border-style: solid;
	border-color: Transparent;
	-border-color: White;
}

.dxeButtonEditButton_DevEx,
.dxeCalendarButton_DevEx,
.dxeSpinIncButton_DevEx,
.dxeSpinDecButton_DevEx,
.dxeSpinLargeIncButton_DevEx,
.dxeSpinLargeDecButton_DevEx
{
	vertical-align: middle;
	cursor: pointer;
}
        .style1
        {
            width: 260px;
        }
        .auto-style2
        {
            width: 1229px;
        }
        .auto-style3
        {
            width: 34px;
            height: 32px;
        }
        .auto-style4
        {
            width: 272px;
        }
        .auto-style5
        {
            height: 25px;
        }
        .auto-style6
        {
            width: 811px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxeLyGroup_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style2" colspan="4">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">EMPRESAS CADASTRADAS</div> 

            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                            <dx1:ASPxButton ID="btSalvar" runat="server" ClientInstanceName="btSalvar" 
                                onclick="btSalvar_Click" Text="Entrar" Theme="iOS" Width="100px" 
                                ValidationGroup="ValidaSelecao" ToolTip="Entrar">
                            </dx1:ASPxButton>

            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="font-size: 25px">
                <img alt="" class="auto-style3" src="Images/Imagem25.png" />
                Administrador. Selecione uma empresa para administrar</td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="4"></td>
        </tr>
        <tr>
            <td align="center" class="auto-style4">&nbsp;</td>
            <td align="center" class="auto-style6">
                <dx1:ASPxRadioButtonList ID="rlbOrgao" runat="server" 
                    ClientInstanceName="rlbOrgao" Theme="MetropolisBlue" ValueType="System.String">
                    <ValidationSettings ValidationGroup="ValidaSelecao">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx1:ASPxRadioButtonList>
            </td>
            <td align="center">&nbsp;</td>
            <td align="center">&nbsp;</td>
        </tr>
    </table>
    <table class="dxflInternalEditorTable_DevEx">
        <tr>
            <td style="background-color: #FFFFFF; color: #FFFFFF; font-size: 25px;" class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="color: #FFFFFF; font-size: 25px;" align="center" class="auto-style2">
                            &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="background-color: #FFFFFF; color: #666666; font-size: 25px;" align="center" class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="auto-style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

