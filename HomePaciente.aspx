<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HomePaciente.aspx.cs" Inherits="HomePaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Menu ID="MenuPac" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
        </asp:Menu>    


<asp:Panel ID="pnlVerificarConsultas" runat="server">
        <a href="UltimasConsultasPac.aspx">
            <asp:Image ID="imgVerificarConsultas" runat="server" Height="62px" Width="86px" />
            <br />

            Verificar consultas agendadas
        </a>        
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlAvaliarConsultas" runat="server">
        <a href="AvaliacaoConsulta.aspx">
            <asp:Image ID="imgAvaliarConsultas" runat="server" Height="62px" Width="86px" />
            <br />

            Avaliar consultas realizadas
        </a>        
    </asp:Panel>
</asp:Content>

