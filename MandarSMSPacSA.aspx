<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MandarSMSPacSA.aspx.cs" Inherits="MandarSMSPacSA" %>

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
        <h1>
            Mandar Email</h1>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>

            <table>
                <tr>
                    <td>
            <asp:Label ID="Label1" runat="server" Text="Usuário(Paciente):"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="txtPaciente" runat="server" MaxLength="30"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtPaciente" ErrorMessage="Digite o usuário do paciente!"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label2" runat="server" Text="Mensagem:"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="txtMensagem" runat="server" TextMode="MultiLine" Height="90px" Width="235px"></asp:TextBox>

            <asp:RequiredFieldValidator ID="rfvMensagem" runat="server" ControlToValidate="txtMensagem" ErrorMessage="Digite a mensagem para o paciente!"></asp:RequiredFieldValidator>

                    </td>
                </tr>
            </table>
        <p>
            <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" />

        </p>
        

    </div>
  </asp:content>
