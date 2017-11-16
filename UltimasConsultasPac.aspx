<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UltimasConsultasPac.aspx.cs" Inherits="UltimasConsultasPac" %>

<asp:Content ID="ContentUltimasConsultasPac" ContentPlaceHolderID="MainContent" Runat="Server">

<div>
    
        
        
        <asp:Menu ID="MenuPac" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
        </asp:Menu>    

    <br /><h1>
        Últimas Consultas do Paciente com Determinado Médico</h1>
        <p>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
        </p>
        <p>
            
            <asp:Label ID="Label3" runat="server" Text="Escolha o médico:"></asp:Label>
            <asp:ListBox ID="lsbMedico" runat="server"></asp:ListBox>
            
            <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ControlToValidate="lsbMedico" ErrorMessage="Selecione o médico!"></asp:RequiredFieldValidator>
            
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Número de consultas para mostrar:"></asp:Label>
            <asp:TextBox ID="txtNumero" runat="server" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ControlToValidate="txtNumero" ErrorMessage="Digite o número de consultas!"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Table ID="tabDados" runat="server">
            </asp:Table>
        </p>
        <p>
            &nbsp;</p>
        
        <p>
            <asp:Button ID="btnGeraRelatorio" runat="server" Text="Gerar Relatório" OnClick="btnGeraRelatorio_Click" />
            <asp:Button ID="btnRedefinir" runat="server" OnClick="btnRedefinir_Click" Text="Redefinir configuração" />
        </p>

    </div>
  </asp:content>