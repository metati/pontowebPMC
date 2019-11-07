<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frmCadPontoEletronico.aspx.cs" Inherits="PontoEletronico_frmCadPontoEletronico" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style3 {
            width: 96px;
        }

        .style4 {
            width: 84px;
        }

        .auto-style5 {
            width: 482px;
        }

        .style5 {
            width: 100%;
        }

        .auto-style3 {
            height: 24px;
            width: 26px;
        }

        .NoStyle {
            background: none;
            border: 0;
            color: black;
            padding: 0;
            height: auto;
        }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/swfobject.js"></script>
    <script type="text/javascript" src="../Scripts/scriptcam.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtIp").mask("999.999.??9.??9");
        });

        function AbrePopSetor(IdPonto, nome, local) {
            debugger
            document.getElementById("<%=PontoID.ClientID %>").value = IdPonto;
            tbPontoTxt.SetText(IdPonto);
            txtPontoNome.SetText(nome);
            txtPontoLocal.SetText(local);
            gridSetor.PerformCallback('');
            popVinculoSetor.Show();
        }

        function AbrePopUsuarios(IdPonto, nome, local) {
            debugger
            document.getElementById("<%=PontoIDUsuario.ClientID %>").value = IdPonto;
            txtUserIdPonto.SetText(IdPonto);
            txtUserPontoNome.SetText(nome);
            txtUserPontoLocal.SetText(local);
            gridUsuarioColaborador.PerformCallback('');
            popUsuarioColaborador.Show();
        }

        function FechaPopSetor() {
            document.getElementById("<%=PontoID.ClientID %>").value = "";

            popVinculoSetor.Hide();
        }

        function FechaPopUsuarioColaborador() {
            document.getElementById("<%=PontoIDUsuario.ClientID %>").value = "";
            popUsuarioColaborador.Hide();
        }

        function onClick(index) {
            if (confirm('Desaja realmente excluir este item?.'))
                gridSetor.DeleteRow(index);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table class="dxflInternalEditorTable_MetropolisBlue">
        <tr>
            <td style="background-color: #CCCCCC; color: #FFFFFF; font-size: 30px;" class="auto-style3">
                <div style="text-align: center; width: 1224px; background-color: #CCCCCC; color: #FFFFFF;">CADASTRO DE PONTO ELETRÔNICO</div>

            </td>
        </tr>
        <tr>
            <td align="left" class="auto-style5">
                <strong style="font-size: 16px; font-weight: normal;">
                    <img alt="" class="auto-style3" src="../Images/Imagem43.png" />
                    Cadastro de Ponto Eletrônico 
                </strong>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td align="center">
                <dx:ASPxPageControl ID="ASPxPageControlPontoE" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                    CssPostfix="DevEx" Width="500px"
                    OnActiveTabChanged="ASPxPageControlPontoE_ActiveTabChanged"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                    <TabPages>
                        <dx:TabPage Text="Cadastro">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbCodigo" runat="server" ClientIDMode="AutoID" Text="Código:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtCodigo" Enabled="false" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbNome" runat="server" ClientIDMode="AutoID" Text="Nome:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtNome" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNome" runat="server" ControlToValidate="txtNome" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbLocal" runat="server" ClientIDMode="AutoID" Text="Local:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtLocal" onkeypress="return somenteNumeros(event)" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLocal" runat="server" ControlToValidate="txtLocal" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbIp" runat="server" ClientIDMode="AutoID" Text="IP:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtIp" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorIp" runat="server" ControlToValidate="txtIp" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbPorta" runat="server" ClientIDMode="AutoID" Text="Porta:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtPorta" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPorta" runat="server" ControlToValidate="txtPorta" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbUsuario" runat="server" ClientIDMode="AutoID" Text="Usuário:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtUsuario" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30">
                                                <dx:ASPxLabel ID="lbSenha" runat="server" ClientIDMode="AutoID" Text="Senha:" />
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtSenha" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td width="100"></td>
                                            <td width="300"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btSalvar" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btSalvar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Salvar" Theme="iOS" ToolTip="Salvar" ValidationGroup="ValidaSetor" Width="100px">
                                                </dx:ASPxButton>
                                            </td>
                                            <td width="100">
                                                <dx:ASPxButton ID="btLista" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btLista_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Lista" Theme="iOS" ToolTip="Lista" Width="100px">
                                                </dx:ASPxButton>
                                            </td>
                                            <td width="300">
                                                <dx:ASPxButton ID="btVoltarCad" runat="server" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btVoltarCad_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" Theme="iOS" ToolTip="Voltar" Width="100px">
                                                </dx:ASPxButton>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Lista">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <table class="style1">
                                        <tr>
                                            <td style="margin-left: 40px">
                                                <dx:ASPxGridView ID="gridPontoEletronico" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
                                                    CssPostfix="DevEx" DataMember="TBPontoEletronico" Width="900px" KeyFieldName="PontoEletronicoID" OnPageIndexChanged="gridPontoEletronico_PageIndexChanged">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Código" FieldName="PontoEletronicoID" ShowInCustomizationForm="True" Visible="true" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nome" FieldName="PontoEletronico_Nome" ShowInCustomizationForm="True" VisibleIndex="1" Width="200px">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Local" FieldName="PontoEletronico_Local" ShowInCustomizationForm="True" VisibleIndex="2" Width="200px">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IP" FieldName="PontoEletronico_Ip" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porta" FieldName="PontoEletronico_Porta" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Usuário" FieldName="PontoEletronico_Usuario" ShowInCustomizationForm="false" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Senha" FieldName="PontoEletronico_Senha" ShowInCustomizationForm="false" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="7" Width="40px">
                                                            <DataItemTemplate>
                                                                <a href="javascript:void(0);" onclick='AbrePopSetor(&#039;<%#Eval("PontoEletronicoID") %>&#039;,&#039;<%#Eval("PontoEletronico_Nome") %>&#039;,&#039;<%#Eval("PontoEletronico_Local") %>&#039;)'>
                                                                    <img src="../Icones/Novo.png" height="16px" width="16px" border="0" title="Vincular Setor" /></a>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="8" Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:ImageButton runat="server" ID="btnAtualizar" ToolTip="Atualizar Colaboradores" Height="19px" Width="19px" OnClick="btnAtualizar_Click" ImageUrl="~/Icones/refresh_arrow_1546.png"></asp:ImageButton>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="9" Width="40px">
                                                            <DataItemTemplate>
                                                                <a href="javascript:void(0);" onclick='AbrePopUsuarios(&#039;<%#Eval("PontoEletronicoID") %>&#039;,&#039;<%#Eval("PontoEletronico_Nome") %>&#039;,&#039;<%#Eval("PontoEletronico_Local") %>&#039;)'>
                                                                    <img src="../Icones/UsuariosColaboradores.png" height="26px" width="26px" border="0" title="Ver Colaboradores Vinculados" /></a>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" SortMode="Value" />
                                                    <SettingsPager PageSize="20">
                                                        <AllButton Text="All">
                                                        </AllButton>
                                                        <NextPageButton Text="Próx. &gt;">
                                                        </NextPageButton>
                                                        <PrevPageButton Text="&lt;Ant.">
                                                        </PrevPageButton>
                                                        <Summary AllPagesText="Page: {0} - {1} ({2} items)"
                                                            Text="Página {0} of {1} ({2} items)" />
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsLoadingPanel Text="Processando&amp;hellip;" />
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
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style3" style="text-align: right"></td>
                                                        <td class="style4"></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3" style="text-align: right">
                                                            <dx:ASPxButton ID="btCadastrar" runat="server" AutoPostBack="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btCadastrar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Cadastrar" Theme="iOS" ToolTip="Cadastrar" Width="100px">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td class="style4">
                                                            <dx:ASPxButton ID="btAlterar" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btAlterar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Alterar" Theme="iOS" ToolTip="Alterar" Width="100px">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btVoltar" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" OnClick="btVoltar_Click" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Text="Voltar" Theme="iOS" ToolTip="Voltar" Width="100px">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif">
                    </LoadingPanelImage>
                    <LoadingPanelStyle ImageSpacing="5px">
                    </LoadingPanelStyle>
                    <Paddings PaddingLeft="5px" PaddingRight="5px" Padding="2px" />
                    <ContentStyle>
                        <Paddings Padding="12px" />
                        <Border BorderWidth="1px" BorderColor="#9DA0AA" BorderStyle="Solid" />
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
    </table>

    <dx:ASPxPopupControl ID="popVinculoSetor" runat="server" ClientInstanceName="popVinculoSetor" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
        HeaderText="Vínculo Ponto Eletrônico/Setor" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="1000px" Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" EnableHotTrack="False" Height="100%">
        <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif"></LoadingPanelImage>
        <HeaderStyle>
            <Paddings PaddingLeft="7px" />
            <Paddings PaddingRight="6px"></Paddings>
        </HeaderStyle>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPageControl ID="tpVinculo" runat="server" ActiveTabIndex="0"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%"
                    ClientInstanceName="tpVinculo">
                    <TabPages>
                        <dx:TabPage Text="Vincular Setor">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <asp:HiddenField ID="PontoID" runat="server" />
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbVIdPonto" runat="server" ClientIDMode="AutoID" Text="Cód. Ponto:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="tbPontoTxt" ClientInstanceName="tbPontoTxt" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbPontoNome" runat="server" ClientIDMode="AutoID" Text="Nome:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtPontoNome" ClientInstanceName="txtPontoNome" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbPontoLocal" runat="server" ClientIDMode="AutoID" Text="Local:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtPontoLocal" ClientInstanceName="txtPontoLocal" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbVSetor" runat="server" ClientIDMode="AutoID" Text="Setor:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxComboBox ID="cbSetor" runat="server" ClientInstanceName="cbSetor" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                    EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains" Spacing="0" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" TextField="DSSetor" ValueField="IDSetor" Width="400px">
                                                    <LoadingPanelImage Url="~/App_Themes/DevEx/Editors/Loading.gif" />
                                                    <LoadingPanelStyle ImageSpacing="5px" />
                                                    <ValidationSettings ValidationGroup="ValidaVinculo">
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td style="text-align: right">
                                                <dx:ASPxButton ID="btnSalvarSetor" runat="server" AutoPostBack="False" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                                    Text="Adicionar" Width="100px" ClientInstanceName="btnSalvarSetor" OnClick="btnSalvarSetor_Click" Theme="iOS">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btCancelarGestor" runat="server" AutoPostBack="False" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                                    Text="Cancelar" Width="100px" ClientInstanceName="btCancelarSetor" Theme="iOS">
                                                    <ClientSideEvents Click="function(s, e) {FechaPopSetor();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>

                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gridSetor" runat="server"
                                                    AutoGenerateColumns="False" ClientInstanceName="gridSetor"
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                    DataMember="TBPontoEletronicoSetor" KeyFieldName="PontoEletronicoSetorID"
                                                    OnPageIndexChanged="gridSetor_PageIndexChanged" OnRowDeleting="gridSetor_RowDeleting1"
                                                    Width="800px">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Código" FieldName="PontoEletronicoSetorID" ShowInCustomizationForm="True" VisibleIndex="0" Width="40px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Cód. Setor" FieldName="IDSetor" ShowInCustomizationForm="True" VisibleIndex="1" Width="40px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Setor" FieldName="DSSetor" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="6" Width="40px" Caption="Excluir">
                                                            <EditFormSettings Visible="False" />
                                                            <DataItemTemplate>
                                                                <dx:ASPxImage ID="idDeletar" runat="server" ToolTip="Excluir" ImageUrl="../Icones/Delete.png" OnInit="Excluir_Init">
                                                                </dx:ASPxImage>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" SortMode="Value" />
                                                    <SettingsPager PageSize="5">
                                                        <AllButton Text="All">
                                                        </AllButton>
                                                        <NextPageButton Text="Próx. &gt;">
                                                        </NextPageButton>
                                                        <PrevPageButton Text="&lt;Ant.">
                                                        </PrevPageButton>
                                                        <Summary AllPagesText="Page: {0} - {1} ({2} items)"
                                                            Text="Página {0} of {1} ({2} items)" />
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsLoadingPanel Text="Processando&amp;hellip;" />
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
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </dx:PopupControlContentControl>

        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popUsuarioColaborador" runat="server" ClientInstanceName="popUsuarioColaborador" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
        HeaderText="Vínculo Ponto Eletrônico/Setor" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="1000px" Modal="True" PopupAction="MouseOver" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" EnableHotTrack="False" Height="100%">
        <LoadingPanelImage Url="~/App_Themes/DevEx/Web/Loading.gif"></LoadingPanelImage>
        <HeaderStyle>
            <Paddings PaddingLeft="7px" />
            <Paddings PaddingRight="6px"></Paddings>
        </HeaderStyle>
        <LoadingPanelStyle ImageSpacing="5px">
        </LoadingPanelStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                    SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100%"
                    ClientInstanceName="tpVinculo">
                    <TabPages>
                        <dx:TabPage Text="Usuários/Colaboradores">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <asp:HiddenField ID="PontoIDUsuario" runat="server" />
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbUserIdPonto" runat="server" ClientIDMode="AutoID" Text="Cód. Ponto:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtUserIdPonto" ClientInstanceName="txtUserIdPonto" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="100px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbUserPontoNome" runat="server" ClientIDMode="AutoID" Text="Nome:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtUserPontoNome" ClientInstanceName="txtUserPontoNome" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbUserPontoLocal" runat="server" ClientIDMode="AutoID" Text="Local:"></dx:ASPxLabel>
                                            </td>
                                            <td colspan="2">
                                                <dx:ASPxTextBox ID="txtUserPontoLocal" ClientInstanceName="txtUserPontoLocal" ReadOnly="true" runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css" Width="400px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gridUsuarioColaborador" runat="server"
                                                    AutoGenerateColumns="False" ClientInstanceName="gridUsuarioColaborador"
                                                    CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx"
                                                    DataMember="TBUsuarios" KeyFieldName="PIS"
                                                    OnPageIndexChanged="gridUsuarioColaborador_PageIndexChanged"
                                                    Width="800px" OnCustomCallback="gridUsuarioColaborador_CustomCallback">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Nome" FieldName="Nome" ShowInCustomizationForm="True" VisibleIndex="0" Width="40px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Pis" FieldName="PIS" ShowInCustomizationForm="True" VisibleIndex="1" Width="40px">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" SortMode="Value" />
                                                    <SettingsPager PageSize="5">
                                                        <AllButton Text="All">
                                                        </AllButton>
                                                        <NextPageButton Text="Próx. &gt;">
                                                        </NextPageButton>
                                                        <PrevPageButton Text="&lt;Ant.">
                                                        </PrevPageButton>
                                                        <Summary AllPagesText="Page: {0} - {1} ({2} items)"
                                                            Text="Página {0} of {1} ({2} items)" />
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsLoadingPanel Text="Processando&amp;hellip;" />
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
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnFecharUser" runat="server" AutoPostBack="False" CausesValidation="False" CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx" SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css"
                                                    Text="Fechar" Width="100px" ClientInstanceName="btnFecharUser" Theme="iOS">
                                                    <ClientSideEvents Click="function(s, e) {FechaPopUsuarioColaborador();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>

                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </dx:PopupControlContentControl>

        </ContentCollection>
    </dx:ASPxPopupControl>

</asp:Content>
