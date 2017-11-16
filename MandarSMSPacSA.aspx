<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MandarSMSPacSA.aspx.cs" Inherits="MandarSMSPacSA" %>

<asp:Content ID="ContentLogonSA" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    
        
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
        <h1>
        Mandar SMS para Paciente

        </h1>
        <p>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>

        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Usuário(Paciente):"></asp:Label>
            <asp:TextBox ID="txtPaciente" runat="server" MaxLength="30"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtPaciente" ErrorMessage="Digite o usuário do paciente!"></asp:RequiredFieldValidator>

        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Mensagem:"></asp:Label>
            <asp:TextBox ID="txtMensagem" runat="server" TextMode="MultiLine"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfvMensagem" runat="server" ControlToValidate="txtMensagem" ErrorMessage="Digite a mensagem para o paciente!"></asp:RequiredFieldValidator>

        </p>
        <p>
            <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" />

        </p>
        <p>&nbsp;</p>
        

    </div>
  </asp:content>
