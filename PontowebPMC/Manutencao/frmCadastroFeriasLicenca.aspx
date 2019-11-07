<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadastroFeriasLicenca.aspx.cs" Inherits="Manutencao_frmCadastroFeriasLicenca" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function FechapopDeletar() {
            popDeletar.Hide();
        }
        function AbrepopDeletar(IDFerias, IDUsuario, MesReferencia, DTFimFerias, TPFerias) 
        {
            if (IDFerias != 0) 
            {
                document.getElementById("<%=CampoOculto.ClientID %>").value = IDFerias;
                document.getElementById("<%=IDUsuario.ClientID %>").value = IDUsuario;
                document.getElementById("<%=MesReferencia.ClientID %>").value = MesReferencia;
                document.getElementById("<%=DTinicial.ClientID %>").value = MesReferencia;
                document.getElementById("<%=DTFinal.ClientID %>").value = DTFimFerias;

                document.getElementById("<%=TPFerias.ClientID %>").value = TPFerias;

                popDeletar.Show();
            }
                
        }
        function PesquisaGridFerias() 
        {
            var Opcao = document.getElementById("<%=ddlSetor.ClientID %>").value;
            
            if (Opcao == 0) {
                window.alert('Escolha um setor válido.');
            }
            else {
                gridFerias.PerformCallback();
            }
        }
        function AbrePopFerias() {

            var Opcao = document.getElementById("<%=ddlSetor.ClientID %>").value;
            document.getElementById("<%=CampoOculto.ClientID %>").value = 1; //Salva Cadastro
            if (Opcao == 0) {
                window.alert('Escolha um setor válido.');
            }
            else {
                LimpaCampos();
                popManutencao.Show();
            }
        }
        function LimpaCampos() 
        {
            document.getElementById("<%=ddlUsuario.ClientID %>").value = 0;
            deInicio.SetText('');
            deFim.SetText('');
        }
        function FechaPopSalva() {

            var Opcao = document.getElementById("<%=ddlUsuario.ClientID %>").value;
            var Opcao1 = document.getElementById("<%=ddlUsuario.ClientID %>").value;
            var Opecao2 = document.getElementById("<%=ddlTipoFerias.ClientID %>").value;

            if (Opcao1 == 0 || Opecao2 == 0 || Opcao == 0) 
            {
                window.alert('Preencha todos os campos corretamente');
            }
            else 
            {

                popManutencao.Hide();
            }
        }
        function FechaPop() {
            popManutencao.Hide();
        }

        function AbrirPopAlteracao(valor, valor1, valor2, valor3, valor4, valor5) 
        {
                document.getElementById("<%=CampoOculto.ClientID %>").value = valor3; //Salva Alteracao
            
                LimpaCampos();
                deInicio.SetText(valor);
                deFim.SetText(valor1);

                //Campos Ocultos
                document.getElementById("<%=DTinicial.ClientID %>").value = valor;
                document.getElementById("<%=DTFinal.ClientID %>").value = valor1;
                document.getElementById("<%=TPFerias.ClientID %>").value = valor4;
                document.getElementById("<%=IDUsuario.ClientID %>").value = valor2;
                document.getElementById("<%=IDFerias.ClientID %>").value = valor5
                //----------

                document.getElementById("<%=ddlUsuario.ClientID %>").value = valor2;
                document.getElementById("<%=ddlTipoFerias.ClientID %>").value = valor4

                popManutencao.Show();            
        }
        function RecebeValoresGrid() 
        {
            gridFerias.GetRowValues(gridFerias.GetFocusedRowIndex(), 'DTInicioFerias1;DTFimFerias1;IDUsuario', PegarValoresGrid);
            
            deInicio.SetText('Aguarde');
            deFim.SetText('Aguarde');
        }
        function PegarValoresGrid(valor) 
        {

            LimpaCampos();
            deInicio.SetText(valor[0]);
            deFim.SetText(valor[1]);

            document.getElementById("<%=ddlUsuario.ClientID %>").value = valor[2];
            document.getElementById("<%=CampoOculto.ClientID %>").value = 2; //Salva Alteracao

            popManutencao.Show();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 560px;
        }
        .style2
        {
            width: 97px;
        }
        .style3
        {
            width: 100px;
        }
        .style4
        {
            width: 426px;
        }
        .style5
        {
            width: 50px;
        }
        .style6
        {
            width: 96px;
        }
        .style7
        {
            width: 88px;
        }
        .auto-style3
        {
            width: 19px;
            height: 29px;
        }
        .auto-style4
        {
            width: 121%;
            height: 115px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">LANÇAR FÉRIAS</div> 

            </td>
        </tr>
        <tr>
            <td style="font-size: 16px; font-weight: normal;"><img alt="" class="auto-style3" src="../Images/Imagem27.png" /> Lançamento de Férias, Licença Prêmio e outros.</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 16px" align="center">
    <table class="style1">
        <tr>
            <td class="style5">
                Setor:</td>
            <td class="style4">
                <asp:DropDownList ID="ddlSetor" runat="server" AppendDataBoundItems="True" 
                    DataMember="TBSetor" DataTextField="DSSetor" DataValueField="IDSetor" 
                    Width="480px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSetor_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style2">
                <dx:ASPxButton ID="btFiltar" runat="server" 
                    ClientInstanceName="btFiltrar" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Filtrar" 
                    AutoPostBack="False" Width="100px" Theme="iOS" ToolTip="Filtrar">
                    <ClientSideEvents Click="function(s, e) {
	PesquisaGridFerias();
}" />
                </dx:ASPxButton>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="ddlSetor" ErrorMessage="Selecione um setor válido"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td colspan="2">
                <dx:ASPxGridView ID="gridFerias" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridFerias" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    DataMember="vwFerias" KeyFieldName="IDFerias" Width="900px" 
                    oncustomcallback="gridFerias_CustomCallback" 
                    ondetailrowexpandedchanged="gridFerias_DetailRowExpandedChanged" 
                    ondetailschanged="gridFerias_DetailsChanged" 
                    onpageindexchanged="gridFerias_PageIndexChanged" 
                    onbeforecolumnsortinggrouping="gridFerias_BeforeColumnSortingGrouping" 
                    onheaderfilterfillitems="gridFerias_HeaderFilterFillItems" 
                    onprocesscolumnautofilter="gridFerias_ProcessColumnAutoFilter">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Editar" VisibleIndex="1" Width="50px">
                            <Settings ShowFilterRowMenu="False" />
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="AbrepopDeletar('<%# Eval("IDFerias") %>','<%# Eval("IDUsuario") %>','<%# Eval("DTInicioFerias1") %>','<%# Eval("DTFimFerias1") %>','<%# Eval("IDTPFerias") %>')">Excluir</a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDFerias" Visible="False" 
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDUsuario" Visible="False" 
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Nome" VisibleIndex="4">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDStatus" Visible="False" 
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDSetor" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Início" FieldName="DTInicioFerias1" 
                            VisibleIndex="8">
                            <Settings AllowAutoFilter="False" ShowFilterRowMenu="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fim" FieldName="DTFimFerias1" 
                            VisibleIndex="10">
                            <Settings AllowAutoFilter="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tipo" FieldName="DSTPFerias" 
                            VisibleIndex="5">
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDTPFerias" Visible="False" 
                            VisibleIndex="11">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager>
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Próx.&gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Ant">
                        </PrevPageButton>
                        <Summary Text="Pág. {0} de {1} ({2} itens)" />
                    </SettingsPager>
                    <Settings ShowVerticalScrollBar="True" ShowFilterRow="True" />
                    <SettingsText EmptyDataRow="Sem dados para exibir" />
                    <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                        <LoadingPanelOnStatusBar Url="~/App_Themes/DevEx/GridView/StatusBarLoading.gif">
                        </LoadingPanelOnStatusBar>
                        <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                        </LoadingPanel>
                    </Images>
                    <ImagesFilterControl>
                        <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                        </LoadingPanel>
                    </ImagesFilterControl>
                    <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" 
                        CssPostfix="DevEx">
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                        <AlternatingRow Enabled="True">
                        </AlternatingRow>
                        <LoadingPanel ImageSpacing="5px">
                        </LoadingPanel>
                    </Styles>
                    <StylesEditors ButtonEditCellSpacing="0">
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td colspan="2">
                <table class="style1">
                    <tr>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            <dx:ASPxButton ID="btNovo" runat="server" 
                                ClientInstanceName="btIncluir" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Incluir" 
                                Width="100px" Theme="iOS" ToolTip="Incluir">
                                <ClientSideEvents Click="function(s, e) {
	AbrePopFerias();
}" />
                            </dx:ASPxButton>
                        </td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            <dx:ASPxButton ID="btCancelar" runat="server" 
                                ClientInstanceName="btVoltar" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" 
                                Width="100px" CausesValidation="False" onclick="btCancelar_Click" Theme="iOS" ToolTip="Voltar">
                            </dx:ASPxButton>
                        </td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td class="style3">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="CampoOculto" runat="server" />
    <asp:HiddenField ID="IDUsuario" runat="server" />
    <asp:HiddenField ID="TPFerias" runat="server" />
    <asp:HiddenField ID="MesReferencia" runat="server" />
    <asp:HiddenField ID="DTFinal" runat="server" />
    <asp:HiddenField ID="IDFerias" runat="server" />
    <asp:HiddenField ID="DTinicial" runat="server" />
    <dx:ASPxPopupControl ID="popDeletar" runat="server" 
        ClientInstanceName="popDeletar" CloseAction="None" 
        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
        HeaderText="Exclusão de Registro" Modal="True" PopupAction="None" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="350px" 
        EnableHotTrack="False">
        <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
        </LoadingPanelImage>
        <HeaderStyle>
        <Paddings PaddingLeft="7px" />
        </HeaderStyle>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td>
                Deseja Realmente excluir o registro selecionado ?</td>
        </tr>
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right" class="style7">
                            <dx:ASPxButton ID="btConfirmar" runat="server" 
                                ClientInstanceName="btConfirmar" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="OK" 
                                Width="100px" OnClick="btConfirmar_Click" CausesValidation="False" Theme="iOS" ToolTip="OK">
                                <ClientSideEvents Click="function(s, e) {
	FechapopDeletar();
}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btNaoConfirmar" runat="server" 
                                ClientInstanceName="btNaoConfirmar" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cancelar" 
                                Width="100px" AutoPostBack="False" Theme="iOS" ToolTip="Cancelar">
                                <ClientSideEvents Click="function(s, e) {
	FechapopDeletar();
}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            </dx:PopupControlContentControl>
</ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="popManutencao" runat="server" 
        ClientInstanceName="popManutencao" CloseAction="None" 
        CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
        HeaderText="Manutenção de Férias, Licença Prêmio e outros" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="720px" 
        EnableHotTrack="False">
        <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
        </LoadingPanelImage>
        <HeaderStyle>
        <Paddings PaddingLeft="7px" />
        </HeaderStyle>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <table class="style1">
        <tr>
            <td>
                Servidor:</td>
            <td>
                <asp:DropDownList ID="ddlUsuario" runat="server" AppendDataBoundItems="True" 
                    Width="550px" DataMember="vwNomeUsuario" DataTextField="Nome" 
                    DataValueField="IDUsuario">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="ddlUsuario" ErrorMessage="***"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Tipo:</td>
            <td>
                <asp:DropDownList ID="ddlTipoFerias" runat="server" AppendDataBoundItems="True" 
                    DataMember="TBTipoFerias" DataTextField="DSTPFerias" 
                    DataValueField="IDTPFerias" Width="550px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Início:</td>
            <td>
                <dx:ASPxDateEdit ID="deInicio" runat="server" ClientInstanceName="deInicio" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" Spacing="0" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px">
                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                        <HeaderStyle Spacing="1px" />
                    </CalendarProperties>
                    <ValidationSettings ValidationGroup="ValidaGrupoUP">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="deInicio" ErrorMessage="***"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Fim:</td>
            <td>
                <dx:ASPxDateEdit ID="deFim" runat="server" 
                    ClientInstanceName="deFim" 
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="300px" 
                    Spacing="0">
                    <CalendarProperties ClearButtonText="Limpar" TodayButtonText="Hoje">
                        <HeaderStyle Spacing="1px" />
                    </CalendarProperties>
                    <ValidationSettings ValidationGroup="ValidaGrupoUP">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="deFim" ErrorMessage="***"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <table class="style1">
                    <tr>
                        <td class="style6">
                            <dx:ASPxButton ID="btSalvar" runat="server" 
                                ClientInstanceName="btSalvar" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" 
                                OnClick="btSalvar_Click" Width="100px" Theme="iOS" ToolTip="Salvar">
                               <ClientSideEvents Click="function(s, e) 
{
	if(ASPxClientEdit.ValidateGroup('ValidaGrupoUP')) FechaPopSalva();
}" />

                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btCancel" runat="server" 
                                ClientInstanceName="btCancel" 
                                CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" 
                                SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cancelar" 
                                Width="100px" AutoPostBack="False" Theme="iOS" ToolTip="Cancelar">
                                <ClientSideEvents Click="function(s, e) {
	FechaPop();
}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            </dx:PopupControlContentControl>
</ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>

