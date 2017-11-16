<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CadPacienteSA.aspx.cs" Inherits="CadPacienteSA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
        <asp:Menu ID="MenuSA" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Cadastrar Médico" Value="Cadastrar Médico" NavigateUrl="~/CadMedSa.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Paciente" Value="Paciente">
                    <asp:MenuItem Text="Cadastrar" Value="Cadastrar" NavigateUrl="~/CadPacienteSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Mandar SMS" Value="Mandar SMS" NavigateUrl="~/MandarSMSPacSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Visualizar avaliações" Value="Visualizar avaliações" NavigateUrl="~/VerAvaliacaoSA.aspx"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Cadastrar Especialidade" Value="Cadastrar Especialidade" NavigateUrl="~/especialidadeSA.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Consulta" Value="Consulta">
                    <asp:MenuItem Text="Agendar" Value="Agendar" NavigateUrl="~/AgendarConsultaSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Visualizar" Value="Visualizar" NavigateUrl="~/StatusConsultaSA.aspx"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/ConsultasSolicitadas.aspx" Text="Solicitadas" Value="Solicitadas"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Estatísticas" Value="Estatísticas" NavigateUrl="~/MedicaoEstatisticas.aspx"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/CadSA.aspx" Text="Cadastrar outro Secretário" Value="Cadastrar outro Secretário"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/logonSA.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <h1>Confirmar Cadastro de Paciente</h1>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
        <asp:SqlDataSource ID="dsPendencias" runat="server" ConnectionString="<%$ ConnectionStrings:PRII16191ConnectionString %>" SelectCommand="SELECT [usuario], [usuarioPendente] FROM [usuariosPendentes_view]"></asp:SqlDataSource>
        <table>
            <tr>
                <td>
        
        <asp:Label ID="Label1" runat="server" Text="Nome de usuário:"></asp:Label>
                </td>
                <td>
        <asp:DropDownList ID="ddlPendentes" runat="server" DataSourceID="dsPendencias" DataTextField="usuarioPendente" DataValueField="usuario" OnSelectedIndexChanged="ddlPendentes_SelectedIndexChanged" OnLoad="ddlPendentes_Load" AutoPostBack="True">
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Endereço:"></asp:Label></td>
                <td><asp:TextBox ID="txtEndereco" runat="server" ReadOnly="True" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Data de nascimento:"></asp:Label></td>
                <td><asp:TextBox ID="txtNascimento" runat="server" TextMode="DateTime" ReadOnly="True" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label6" runat="server" Text="E-mail:"></asp:Label></td>
                <td><asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Telefone:"></asp:Label></td>
                <td><asp:TextBox ID="txtTelefone" runat="server" TextMode="Phone" ReadOnly="True" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label8" runat="server" Text="Celular:"></asp:Label></td>
                <td><asp:TextBox ID="txtCelular" runat="server" TextMode="Phone" ReadOnly="True" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Foto:"></asp:Label> </td>
                <td><asp:Image ID="imgFoto" runat="server" Height="112px" Width="120px" CssClass="frmCadPacienteSA" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label9" runat="server" Text="Comentários:"></asp:Label></td>
                <td><asp:TextBox ID="txtComentarios" runat="server" AutoPostBack="True" Height="97px" OnTextChanged="txtComentarios_TextChanged" TextMode="MultiLine" Width="273px" CssClass="frmCadPacienteSA"></asp:TextBox></td>
            </tr>
        </table>
        
        <br />

        <asp:Button ID="btnDeferir" runat="server" Text="Deferir" OnClick="btnDeferir_Click" />
        <asp:Button ID="btnIndeferir" runat="server" Text="Indeferir" OnClick="btnIndeferir_Click" Enabled="False" />
</asp:Content>

