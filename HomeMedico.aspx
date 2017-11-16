<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HomeMedico.aspx.cs" Inherits="HomeMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
         <h1> Bem Vindo, ao Sistema, Médico! </h1>

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
</asp:Content>
