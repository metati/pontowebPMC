﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmInformacaoDiaria.aspx.cs" Inherits="frmInformacaoDiaria" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .style4
    {
        width: 60px;
    }
    .style5
    {
        width: 328px;
    }
    .style6
    {
        width: 101px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
    function PegarDadosGrid() 
    {
        gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.GetFocusedRowIndex(), 'DSInformacaoDiaria', RepassarValoresGrid);

        memInf.SetText("Aguarde...");
    }
    function RepassarValoresGrid(valor)
    {
        memInf.SetText(valor);
    }
</script>
    <table class="style1">
    <tr>
        <td colspan="3">
            Lançar Informações Diárias</td>
    </tr>
    <tr>
        <td class="style4">
            Setor:</td>
        <td class="style5">
            <asp:DropDownList ID="ddlSetor" runat="server" AppendDataBoundItems="True" 
                DataMember="TBSetor" DataTextField="DSSetor" 
                DataValueField="IDSetor" style="margin-left: 0px" Width="300px">
            </asp:DropDownList>
        </td>
        <td>
            <dx:ASPxCheckBox ID="cbSetores" runat="server" ClientIDMode="AutoID" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" 
                Text="Todos os Setores" TextSpacing="2px">
            </dx:ASPxCheckBox>
        </td>
    </tr>
</table>
<table class="style1">
    <tr>
        <td>
            <dx:ASPxGridView ID="gridInformacaoDiaria" runat="server" 
                AutoGenerateColumns="False" ClientIDMode="AutoID" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                DataMember="vwInformacaoDiaria" KeyFieldName="TBInformacaoDiaria" 
                Width="600px" 
                
                onbeforecolumnsortinggrouping="gridInformacaoDiaria_BeforeColumnSortingGrouping" 
                ClientInstanceName="gridInformacaoDiaria">
                <ClientSideEvents RowDblClick="function(s, e) {
	PegarDadosGrid();
}" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0">
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="TBInformacaoDiaria" Visible="False" 
                        VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Descrição" FieldName="DSInformacaoDiaria" 
                        VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="IDSetor" Visible="False" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Data" FieldName="DTInformacaoDiaria" 
                        VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsPager ShowDefaultImages="False">
                    <AllButton Text="All">
                    </AllButton>
                    <NextPageButton Text="Próximo &gt;">
                    </NextPageButton>
                    <PrevPageButton Text="&lt; Anteriro">
                    </PrevPageButton>
                    <Summary Text="Página {0} de {1} ({2} )" />
                </SettingsPager>
                <Settings ShowFilterRow="True" ShowVerticalScrollBar="True" />
                <SettingsLoadingPanel Text="Aguarde...&amp;hellip;" />
                <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                    <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                    </LoadingPanelOnStatusBar>
                    <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                    </LoadingPanel>
                </Images>
                <ImagesFilterControl>
                    <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                    </LoadingPanel>
                </ImagesFilterControl>
                <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                    CssPostfix="PlasticBlue">
                    <Header ImageSpacing="10px" SortingImageSpacing="10px">
                    </Header>
                </Styles>
                <StylesEditors>
                    <CalendarHeader Spacing="11px">
                    </CalendarHeader>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
        </td>
        <td valign="top">
            <dx:ASPxMemo ID="memoInformacao" runat="server" Height="200px" Width="320px" 
                ClientInstanceName="memInf" ClientIDMode="AutoID" 
                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                <ValidationSettings CausesValidation="True" SetFocusOnError="True">
                    <RequiredField ErrorText="" IsRequired="True" />
                    <ErrorFrameStyle ImageSpacing="4px">
                        <ErrorTextPaddings PaddingLeft="4px" />
                    </ErrorFrameStyle>
                </ValidationSettings>
            </dx:ASPxMemo>
            <table class="style1">
                <tr>
                    <td class="style6">
                        <dx:ASPxButton ID="btSalvar" runat="server" ClientIDMode="AutoID" 
                            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Salvar" 
                            Width="100px" onclick="btSalvar_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btCancelar" runat="server" ClientIDMode="AutoID" 
                            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Text="Cancelar" 
                            Width="100px">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
