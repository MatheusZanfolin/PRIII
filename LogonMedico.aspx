<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LogonMedico.aspx.cs" Inherits="LogonMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        Bem vindo de volta! Estamos felizes em recebe-lo. Basta inserir seu CRM, sua senha e entrar.
    </p>
    <p>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Size="20pt" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="CRM:"></asp:Label>
&nbsp;<asp:TextBox ID="txtCrm" runat="server" CssClass="txtVerde"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCRM" runat="server" ControlToValidate="txtCRM" ErrorMessage="Digite o CRM!"></asp:RequiredFieldValidator>
    </p>
    <p>
        Senha:<asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Digite a Senha!"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Button ID="btnConfirmar" runat="server" OnClick="btnConfirmar_Click" Text="Confirmar" />
    </p>


</asp:Content>

