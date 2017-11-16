<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CadPaciente.aspx.cs" Inherits="CadPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        Para agilizar o atendimento e melhor atender você, é possível realizar o cadastro de sua casa.</p>
    <p>
        Basta informar os dados cabíveis ao seu cadastro e clicar em &quot;Terminar&quot;</p>
        <asp:Label ID="lblErro" runat="server" ForeColor="Red"></asp:Label>
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Nome de usuário:"></asp:Label></td>
            <td><asp:TextBox ID="txtUsuario" runat="server" MaxLength="30"></asp:TextBox> <asp:RequiredFieldValidator ID="vldUsuario" runat="server" ErrorMessage="Nome de usuário não informado" ControlToValidate="txtUsuario" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Senha: "></asp:Label></td>
            <td><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox> <asp:RequiredFieldValidator ID="vldSenha" runat="server" ErrorMessage="Senha não informada" ControlToValidate="txtSenha" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Nome (completo):"></asp:Label></td>
            <td><asp:TextBox ID="txtNome" runat="server" MaxLength="50"></asp:TextBox> <asp:RequiredFieldValidator ID="vldNome" runat="server" ErrorMessage="Nome não informado" ControlToValidate="txtNome" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="Endereço:"></asp:Label></td>
            <td><asp:TextBox ID="txtEndereco" runat="server" MaxLength="50"></asp:TextBox> <asp:RequiredFieldValidator ID="vldEndereco" runat="server" ErrorMessage="Endereço não informado" ControlToValidate="txtEndereco" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>;</td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Data de nascimento:"></asp:Label></td>
            <td><asp:TextBox ID="txtNascimento" runat="server" TextMode="Date" ValidateRequestMode="Enabled"></asp:TextBox> <asp:RequiredFieldValidator ID="vldNascimento" runat="server" ControlToValidate="txtNascimento" ErrorMessage="Data de nascimento não informada"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="E-mail:"></asp:Label></td>
            <td><asp:TextBox ID="txtEmail" runat="server" MaxLength="30"></asp:TextBox> <asp:RequiredFieldValidator ID="vldEmail" runat="server" ErrorMessage="E-mail não informado" ControlToValidate="txtEmail" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label7" runat="server" Text="Telefone:"></asp:Label></td>
            <td><asp:TextBox ID="txtTelefone" runat="server" TextMode="Phone"></asp:TextBox> <asp:RequiredFieldValidator ID="vldTelefone" runat="server" ErrorMessage="Telefone não informado" ControlToValidate="txtTelefone" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label8" runat="server" Text="Celular:"></asp:Label></td>
            <td><asp:TextBox ID="txtCelular" runat="server" TextMode="Phone"></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCelular" ErrorMessage="Celular não informado"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label9" runat="server" Text="Foto:"></asp:Label></td>
            <td><asp:FileUpload ID="fupFoto" runat="server" Width="343px" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Nenhuma foto foi enviada" ControlToValidate="fupFoto"></asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <p>
        <asp:Button ID="btnTerminar" runat="server" OnClick="btnTerminar_Click" Text="Terminar" />
    </p>
<p>
        <asp:SqlDataSource ID="Clinica" runat="server" ConnectionString="<%$ ConnectionStrings:PRII16191ConnectionString %>" SelectCommand="SELECT [codAvaliacao], [nivelSatisfacao], [descricao] FROM [AvaliacaoConsulta]"></asp:SqlDataSource>
    </p>
</asp:Content>

