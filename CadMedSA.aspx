<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CadMedSA.aspx.cs" Inherits="CadMed" %>

<asp:Content ID="ContentLogonSA" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <asp:Menu ID="MenuSA" runat="server" Orientation="Horizontal" BackColor="White">
            <Items>
                <asp:MenuItem Text="Cadastrar Médico" Value="Cadastrar Médico" NavigateUrl="~/CadMedSa.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Paciente" Value="Paciente">
                    <asp:MenuItem Text="Cadastrar" Value="Cadastrar" NavigateUrl="~/CadPacienteSA.aspx"></asp:MenuItem>
                    <asp:MenuItem Text="Mandar e-mail" Value="Mandar SMS" NavigateUrl="~/MandarSMSPacSA.aspx"></asp:MenuItem>
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
            <StaticMenuItemStyle HorizontalPadding="10px" />
        </asp:Menu>

        <h1>
        Cadastrar Médico</h1>

        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>

        <br />

        <table>
            <tr>
                <td><asp:Label ID="lblCRM" runat="server" Text="CRM:"></asp:Label> </td>
                <td><asp:TextBox ID="txtCRM" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvCrm" runat="server" ControlToValidate="txtCRM" ErrorMessage="Digite o crm do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>                           
                <td><asp:Label ID="lblNome" runat="server" Text="Nome completo:"></asp:Label></td>
                <td><asp:TextBox ID="txtNome" runat="server" MaxLength="50"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Digite o nome do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>                           
                <td><asp:Label ID="lblDataNasc" runat="server" Text="Data de Nascimento:"></asp:Label></td>
                <td><asp:TextBox ID="txtDataNasc" runat="server" TextMode="Date"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvDataNasc" runat="server" ControlToValidate="txtDataNasc" ErrorMessage="Digite a data de nascimento do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>                               
                <td><asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></td>
                <td><asp:TextBox ID="txtEmail" runat="server" MaxLength="30"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Digite o email do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>                            
                <td><asp:Label ID="lblCelular" runat="server" Text="Celular:"></asp:Label></td>
                <td><asp:TextBox ID="txtCelular" runat="server" TextMode="Phone"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvCelular" runat="server" ControlToValidate="txtCelular" ErrorMessage="Digite o celular do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblTelefone" runat="server" Text="Telefone:"></asp:Label></td>
                <td><asp:TextBox ID="txtTelefone" runat="server" TextMode="Phone"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvTelefone" runat="server" ControlToValidate="txtTelefone" ErrorMessage="Digite o telefone do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblSenha" runat="server" Text="Senha:"></asp:Label></td>
                <td><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" ErrorMessage="Digite a senha do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEspecialidade7" runat="server" Text="Especialidade:"></asp:Label></td>
                <td><asp:ListBox ID="lsbEspec" runat="server" Width="170px"></asp:ListBox> <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ControlToValidate="lsbEspec" ErrorMessage="Selecione a especialidade do médico!"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFoto" runat="server" Text="Foto:"></asp:Label></td>
                <td><asp:FileUpload ID="fupFoto" runat="server" /> <asp:RequiredFieldValidator ID="rfvFoto" runat="server" ControlToValidate="fupFoto" ErrorMessage="Envie a foto do médico!"></asp:RequiredFieldValidator></td>
            </tr>
        </table>
        <p>
            <asp:Label ID="lblStatusFoto" runat="server" EnableTheming="False">Aperte o botão &#39;Procurar&#39; para escolher o arquivo de foto...</asp:Label>
        </p>
        <p>
            <asp:Button ID="btnCadastrar" runat="server" OnClick="btnCadastrar_Click" Text="Cadastrar" />
        </p>

    </div>
  </asp:content>