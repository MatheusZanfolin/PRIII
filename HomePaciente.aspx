<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HomePaciente.aspx.cs" Inherits="HomePaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblErro" runat="server" Font-Names="Times New Roman" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
    
    <asp:Panel ID="pnlPaciente" runat="server">
        <h1>Área do Paciente</h1>

        <asp:Menu ID="MenuPac" runat="server" OnMenuItemClick="MenuPac_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/UltimasConsultasPac.aspx" Text="Verificar consultas agendadas"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
            <StaticMenuItemStyle VerticalPadding="5px" />
        </asp:Menu>    
    </asp:Panel>

    <asp:Panel ID="pnlUsuario" runat="server">
        <h1>Área do usuário</h1>
        <asp:Panel ID="pnlSolicitacaoPendente" runat="server" Visible="False">
            Sua soclicitação ainda está sendo avaliada por nossa equipe. Desde já agradecemos pela paciência. Clique
            <asp:HyperLink ID="lnkRetorno" runat="server" NavigateUrl="~/LogonPac.aspx">aqui</asp:HyperLink>
            &nbsp;para retornar ao logon.</asp:Panel>
        <asp:Panel ID="pnlNovaSolicitacao" runat="server" Visible="False">
            Sua solicitação foi indeferida, caso deseje abrir uma nova,&nbsp;
            <asp:Button ID="btnNovaSolicitacao" runat="server" OnClick="btnNovaSolicitacao_Click" Text="Clique Aqui" />
            <br />
            <br />
            Para maior esclarecimento os apontamentos de nossa equipe acerca da requisição são:<br /> <br />

            <center> <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Height="160px" ReadOnly="True" Width="379px"></asp:TextBox> </center>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

