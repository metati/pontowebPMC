<%@ Page Title="Ponto na Rede - Sistema Gestor de Marcação de Ponto" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 697px;
            margin-right: 837px;
        }
        .style2
        {
            width: 104px;
        }
        .auto-style1
        {
            width: 224%;
            height: 115px;
        }
        .auto-style2
        {
            height: 50px;
            width: 440px;
        }
        .auto-style3
        {
            width: 234%;
            height: 115px;
        }
        .auto-style4
        {
            width: 111px;
        }
        .auto-style6
        {
            width: 20px;
            height: 22px;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2 style="color: #000000;">
        &nbsp;</h2>
    <h2 style="font-size: x-small">Bem-vindo(a) AO PONTONAREDE!</h2>
    <p style="background-color: #F8F8F8">
        &nbsp;</p>
    <table id="TBDivisao" class="style1">
        <tr>
            <td class="auto-style3">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style3">
                &nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                &nbsp;</td>
            <td class="auto-style1">
                &nbsp; &nbsp;</td>
            <td 
                
                style="background-image: url('../Images/logo_gov_MT_Brasao1_alt.png'); background-position: center center; background-repeat: no-repeat; " class="auto-style2">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" 
    RenderOuterTable="false" onauthenticate="LoginUser_Authenticate" 
                    FailureText="Usuário ou senha incorreta.">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="accountInfo" style="width: 360px; height: 360px;">
                <fieldset class="login" style="width: 360px">
                    <legend style="color: #000000; font-size: 20px;">||Acesso ao sistema</legend>
                    <p>
                        <asp:Label ID="PasswordLabel0" runat="server" AssociatedControlID="Password" ForeColor="#004080" style="color: #007C26; font-size: 16px;"><img alt="" class="auto-style6" src="../Images/Imagem18.png" />Login</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" Height="30px" Width="313px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="Informe o login de usuário." ToolTip="Login é Requirido." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" ForeColor="#004080" style="color: #007C26; font-size: 16px;"><img alt="" class="auto-style6" src="../Images/Imagem38.png" />Senha</asp:Label>
                        
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" Height="30px" Width="313px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Informe a senha." ToolTip="Senha Requerida." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        &nbsp;<table class="dxflInternalEditorTable_MetropolisBlue">
                            <tr>
                                <td class="auto-style4">&nbsp;</td>
                                <td>
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Font-Bold="True" ForeColor="White" Height="42px" OnClick="LoginButton_Click" style="margin-left: 15px" Text="Entrar" ToolTip="Entrar" ValidationGroup="LoginUserValidationGroup" Width="95px" BackColor="#007C26" />
                                </td>
                            </tr>
                        </table>
                    </p>
                </fieldset>
            </div>
        </LayoutTemplate>
    </asp:Login>
            </td>
            </td>
            <td 
                
                style="background-image: url('../Images/logo_gov_MT_Brasao1_alt.png'); background-position: center center; background-repeat: no-repeat; " class="auto-style2">&nbsp;</td>
        </tr>
    </table>
</asp:Content>