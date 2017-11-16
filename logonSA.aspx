<%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="LogonSA.aspx.cs" Inherits="SA" %>


<asp:Content ID="ContentLogonSA" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="lblErro">
    
        <p>
        Bem vindo de volta! Estamos felizes em recebe-lo. Basta inserir seu usuário, sua senha e entrar.
    </p>
    <p aria-busy="False" style="color: #FF0000; font-family: &quot;Times New Roman&quot;, Times, serif; font-size: 20px;">
        <asp:Label ID="lblErro" runat="server"></asp:Label>
        </p>
    <p>
        Usuário:&nbsp;<asp:TextBox ID="txtUsuario" runat="server" CssClass="txtVerde" MaxLength="30"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Digite o usuário!"></asp:RequiredFieldValidator>
    </p>
        <p>
            Senha:&nbsp;
&nbsp;<asp:TextBox ID="txtSenha" runat="server" CssClass="txtVerde" MaxLength="30" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Digite a senha!"></asp:RequiredFieldValidator>
    </p>
        <p>
            <asp:Button ID="btnLogonSA" runat="server" Text="Entrar" OnClick="btnLogonSA_Click" />
    </p>


    </div>
   
    </asp:Content>