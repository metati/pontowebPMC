<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmMudaOrgao.aspx.cs" Inherits="Cadastro_frmMudaOrgao" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <script type="text/javascript">
        function AbrePopTrocaOrgao(IDUsuario) {
            coIDUsuarioTroca.Set('IDusuarioTroca', IDUsuario);
            pcMudaServidor.Show();
        }
        function BuscaServidor(IDUsuario) {
            gridBuscaServidor.PerformCallback();
        }
        function PreencheSetor() {
            cbSetorTroca.PerformCallback(cbEmpresa.GetValue());
        }
        function FechaPopTroca() {
            cbEmpresa.SetText('');
            cbSetorTroca.SetText('');
            pcMudaServidor.Hide();
        }
        function ApagaTextBox() {
            tbBuscapServidor.SetText('');
        }
        function SalvaTroca() {
            pcMudaServidor.Hide();
            gridBuscaServidor.PerformCallback('Op');
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 346px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 100px;
        }
        .auto-style3
        {
            height: 22px;
            width: 24px;
        }
        .auto-style4
        {
            width: 1124px;
        }
        .auto-style5
        {
            width: 259px;
        }
        .auto-style6
        {
            width: 549px;
        }
        .auto-style10
        {
            width: 172px;
        }
        .auto-style11
        {
            width: 209px;
            height: 30px;
        }
        .auto-style12
        {
            height: 30px;
        }
        .auto-style13
        {
            width: 209px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3" align="center">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">MANUTENÇÃO PARA MUDAR SERVIDOR DE EMPRESA</div> 

            </td>
        </tr>
        <tr>
            <td align="left" class="auto-style12"><img alt="" class="auto-style3" src="../Images/Imagem44.png" />Mudar servidor de órgão</td>
        </tr>
        <tr>
            <td align="center">
    <p dir="ltr">
        <table class="dxflInternalEditorTable_MetropolisBlue">
            <tr>
                <td class="auto-style13">&nbsp;</td>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="auto-style13">&nbsp;</td>
                <td align="justify" class="auto-style6" colspan="2">
                    <dx:ASPxRadioButtonList ID="rblOpcao" runat="server" EnableTheming="True" 
                        SelectedIndex="0" Theme="DevEx">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ApagaTextBox();
}" />
                        <Items>
                            <dx:ListEditItem Selected="True" Text="Buscar por Nome" Value="0" />
                            <dx:ListEditItem Text="Buscar por CPF" Value="1" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td align="center" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11"></td>
                <td class="auto-style12" colspan="4"></td>
            </tr>
            <tr>
                <td align="center" class="auto-style13">&nbsp;</td>
                <td align="justify" class="auto-style10" colspan="1">
                    <dx:ASPxTextBox ID="tbBuscapServidor" runat="server" 
                        ClientInstanceName="tbBuscapServidor" Theme="DevEx" Width="400px">
                        <ValidationSettings ValidationGroup="ValidaBusca">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td align="left" class="auto-style5" colspan="2">
                    <dx:ASPxButton ID="btBuscarServidorOrgao" runat="server" Text="Buscar" 
                        Theme="iOS" ValidationGroup="ValidaBusca" Width="100px" 
                        AutoPostBack="False" ClientInstanceName="btBuscarServidorOrgao" ToolTip="Buscar">
                        <ClientSideEvents Click="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ValidaBusca'))	
BuscaServidor();
}" />
                    </dx:ASPxButton>
                </td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style13">&nbsp;</td>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="auto-style13">&nbsp;</td>
                <td align="justify" colspan="4">
                    <dx:ASPxGridView ID="gridBuscaServidor" runat="server" 
                        AutoGenerateColumns="False" ClientInstanceName="gridBuscaServidor" 
                        DataMember="vwusuariogrid" EnableTheming="True" KeyFieldName="IDUsuario" 
                        Theme="DevEx" Width="750px" 
                        oncustomcallback="gridBuscaServidor_CustomCallback" 
                        onpageindexchanged="gridBuscaServidor_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="80px">
                                                             <DataItemTemplate>
                                    <a href="javascript:void(0);" onclick="AbrePopTrocaOrgao('<%#Eval("IDUsuario") %>')">
                                        <img src="../Icones/Editar.png" height="16px" width="16px" border="0" />Mudar</a>
                                 </DataItemTemplate>  
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nome" FieldName="DSUsuario" 
                                VisibleIndex="1" Width="200px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Órgão" FieldName="DSEmpresa" 
                                VisibleIndex="3" Width="350px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Situação" FieldName="DSStatus" 
                                VisibleIndex="4" Width="50px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="CPF" FieldName="Login" VisibleIndex="2" 
                                Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="IDUsuario" Visible="False" 
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager>
                            <Summary Text="Página {0} de {1} ({2})" />
                        </SettingsPager>
                        <SettingsLoadingPanel Text="Buscando&amp;hellip;" />
                    </dx:ASPxGridView>
                </td>
            </tr>
            <tr>
                <td align="center" class="auto-style13">&nbsp;</td>
                <td align="center" colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="auto-style13">&nbsp;</td>
                <td align="left" colspan="4">
                                    <dx:ASPxButton ID="btVoltar" runat="server" 
                                        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                        Width="100px" onclick="btVoltar_Click" 
                        CausesValidation="False" Theme="iOS" ToolTip="Voltar">
                                    </dx:ASPxButton>
                                </td>
            </tr>
        </table>
        &nbsp;<table class="style2">
            <tr>
                <td class="auto-style4">
                    <dx:ASPxPopupControl ID="pcMudaServidor" runat="server" 
                        ClientInstanceName="pcMudaServidor" HeaderText="Mudar servidor de órgão" 
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                        RenderMode="Lightweight" Theme="DevEx" Width="550px">
                        <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style2">
        <tr>
            <td>
                Órgão:</td>
            <td>
                <dx:ASPxComboBox ID="cbEmpresa" runat="server" ClientInstanceName="cbEmpresa" 
                    IncrementalFilteringMode="Contains" TextField="DSEmpresa" Theme="DevEx" 
                    ValueField="IDEmpresa" Width="400px">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	PreencheSetor();
}" />
                    <ValidationSettings ValidationGroup="ValidaTroca">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Servidor:</td>
            <td>
                <dx:ASPxComboBox ID="cbSetorTroca" runat="server" 
                    ClientInstanceName="cbSetorTroca" IncrementalFilteringMode="Contains" 
                    TextField="DSSetor" Theme="DevEx" ValueField="IDSetor" Width="400px" 
                    OnCallback="cbSetorTroca_Callback">
                    <ValidationSettings ValidationGroup="ValidaTroca">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </td>
        </tr>
    </table>
    <table class="style2">
        <tr>
            <td class="style3">
                <dx:ASPxButton ID="btSalvarTroca" runat="server" 
                    ClientInstanceName="btSalvarTroca" Text="Salvar" Theme="iOS" 
                    ValidationGroup="ValidaTroca" Width="100px" ToolTip="Salvar">
                    <ClientSideEvents Click="function(s, e) {
	if(ASPxClientEdit.ValidateGroup('ValidaTroca'))	
SalvaTroca();
}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btCancelarTroca" runat="server" 
                    ClientInstanceName="btCancelarTroca" Text="Cancelar" Theme="DevEx" 
                    Width="100px">
                    <ClientSideEvents Click="function(s, e) {
	FechaPopTroca();
}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
                            </dx:PopupControlContentControl>
</ContentCollection>
                    </dx:ASPxPopupControl>
                                                <dx:ASPxHiddenField ID="coIDUsuarioTroca" 
                        runat="server" ClientInstanceName="coIDUsuarioTroca">
                    </dx:ASPxHiddenField>
                </td>
            </tr>
        </table>
    </p>
            </td>
        </tr>
    </table>
    <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">

            </td> 

            </td> 

            </td>
    </asp:Content>

