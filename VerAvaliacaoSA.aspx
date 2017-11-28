<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VerAvaliacaoSA.aspx.cs" Inherits="VerAvaliacaoSA" %>

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
        
    <h1>    Ver Avaliações de Consulta pelos Pacientes

        </h1>
        <p>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>

        </p>
        <p>
            <asp:ListBox ID="lsbOpcao" runat="server" OnSelectedIndexChanged="lsbOpcao_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>Geral</asp:ListItem>
                <asp:ListItem>Ver por Especialidade</asp:ListItem>
                <asp:ListItem>Ver por Médico</asp:ListItem>
                <asp:ListItem>Ver por Paciente</asp:ListItem>
            </asp:ListBox>

            <asp:RequiredFieldValidator ID="rfvOpcao" runat="server" ControlToValidate="lsbOpcao" ErrorMessage="Selecione a sua opção!"></asp:RequiredFieldValidator>

        </p>
        <p>
            <asp:ListBox ID="lsbEspecialidade" runat="server" Enabled="False" OnSelectedIndexChanged="lsbEspecialidade_SelectedIndexChanged" AutoPostBack="True" Visible="False"></asp:ListBox>

            <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ControlToValidate="lsbEspecialidade" ErrorMessage="Selecione a especialidade!"></asp:RequiredFieldValidator>

        </p>
        <p>
            <asp:ListBox ID="lsbMedico" runat="server" Enabled="False" OnSelectedIndexChanged="lsbMedico_SelectedIndexChanged" AutoPostBack="True" Visible="False"></asp:ListBox>

            <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ControlToValidate="lsbMedico" ErrorMessage="Selecione o médico!"></asp:RequiredFieldValidator>

        </p>
        <p>
            <asp:ListBox ID="lsbPaciente" runat="server" Enabled="False" OnSelectedIndexChanged="lsbPaciente_SelectedIndexChanged" AutoPostBack="True" Visible="False"></asp:ListBox>
            <asp:RequiredFieldValidator ID="rfvPac" runat="server" ControlToValidate="lsbPaciente" ErrorMessage="Selecione o Paciente"></asp:RequiredFieldValidator>
            <asp:Table ID="tabDados" runat="server">
            </asp:Table>

        </p>
        

    </div>
  </asp:content>