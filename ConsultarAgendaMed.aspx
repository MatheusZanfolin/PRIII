<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsultarAgendaMed.aspx.cs" Inherits="ConsultarAgendaMed" %>

<asp:Content ID="ContentConsultarAgendaMed" ContentPlaceHolderID="MainContent" Runat="Server">
 

<div>
    
        
        
         <asp:Menu ID="menuMed" runat="server" Orientation="Horizontal">
             <Items>
                 <asp:MenuItem Text="Paciente" Value="Paciente">
                     <asp:MenuItem NavigateUrl="~/CadDiagnosticoMed.aspx" Text="Cadastrar Diagnóstico" Value="Cadastrar Diagnóstico"></asp:MenuItem>
                     <asp:MenuItem NavigateUrl="~/UltimasConsultasMed.aspx" Text="Últimas Consultas com Determinado Paciente" Value="Últimas Consultas com Determinado Paciente"></asp:MenuItem>
                 </asp:MenuItem>
                 <asp:MenuItem NavigateUrl="~/ConsultarAgendaMed.aspx" Text="Consultar Agenda" Value="Consultar Agenda"></asp:MenuItem>
                 <asp:MenuItem NavigateUrl="~/MedicaoEstatisticas.aspx" Text="Estatísticas" Value="Estatísticas"></asp:MenuItem>
                 <asp:MenuItem NavigateUrl="~/LogonMedico.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
             </Items>
         </asp:Menu>         
        <h1>Consultar Agenda do Médico</h1>
        <p>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Número de dias:"></asp:Label>
            <asp:TextBox ID="txtNumero" runat="server" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDias" runat="server" ControlToValidate="txtNumero" ErrorMessage="Digite o número de dias!"></asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Table ID="tabDados" runat="server">
            </asp:Table>
            <h4>
                <asp:Label ID="Label2" runat="server" Text="Número de Consultas:"></asp:Label>
                <asp:Label ID="lblConsulta" runat="server"></asp:Label>
            </h4>
        </p>
        
        <p>
            <asp:Button ID="btnGeraRelatorio" runat="server" Text="Gerar Relatório" OnClick="btnGeraRelatorio_Click" />
            <asp:Button ID="btnRedefinir" runat="server" OnClick="btnRedefinir_Click" Text="Redefinir Configuração" />
        </p>

    </div>
  </asp:content>