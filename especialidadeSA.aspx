<%@ Page Language="C#"  MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="especialidadeSA.aspx.cs" Inherits="especialidadeSA" %>


<asp:Content ID="ContentLogonSA" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    
        <asp:Menu ID="MenuSA" runat="server" Orientation="Horizontal">
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
        
        <asp:Label ID="lblErro" runat="server"></asp:Label>
        <h1>
            Cadastro de especialidade
        </h1>
        <p>
            <asp:Label runat="server" Text="Nome:"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtEspecialidade" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ControlToValidate="txtEspecialidade" ErrorMessage="Digite a especialidade do médico!"></asp:RequiredFieldValidator>
        <p>
            <asp:Button ID="btnCadastrar" runat="server" OnClick="btnCadastrar_Click" Text="Cadastrar" />
        <p>
    </div>
  </asp:content>