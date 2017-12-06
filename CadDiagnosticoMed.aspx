<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CadDiagnosticoMed.aspx.cs" Inherits="CadDiagnosticoMed" %>




<asp:Content ID="ContentCadDiagnosticoMed" ContentPlaceHolderID="MainContent" Runat="Server">




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
             <StaticMenuItemStyle HorizontalPadding="10px" />
         </asp:Menu>         
        </h1>
        <h1>Cadastrar Diagnostico do Médico de Determinado Paciente</h1>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
         <table>
             <tr>
                 <td>
            <asp:Label ID="Label1" runat="server" Text="Nome de Usuário do Paciente:"></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="30" AutoPostBack="True" OnTextChanged="txtUsuario_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Digite o usuário do paciente!" ValidationGroup="txtUsuario"></asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td>
            <asp:Label ID="lblDia" runat="server" Text="Selecione o dia da consulta:"></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtData" runat="server" MaxLength="10" TextMode="Date" AutoPostBack="True" OnTextChanged="txtData_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="txtData" ErrorMessage="Digite o dia da consulta!"></asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblHorario" runat="server" Text="Selecione a hora da consulta:" Visible="False"></asp:Label>
                 </td>
                 <td>
             <asp:DropDownList ID="ddlHorarios" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHorarios_SelectedIndexChanged" Visible="False">
                 <asp:ListItem>Selecione um horário</asp:ListItem>
             </asp:DropDownList>
                 </td>
             </tr>
             <tr>
                 <td>
            <asp:Label ID="lblDiagnostico" runat="server" Text="Informe o diagnóstico:" Visible="False"></asp:Label>
                 </td>
                 <td>
            <asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Height="134px" Width="491px" AutoPostBack="True" OnTextChanged="txtDiagnostico_TextChanged" Visible="False"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvDiagnostico" runat="server" ControlToValidate="txtDiagnostico" ErrorMessage="Digite o diagnóstico!"></asp:RequiredFieldValidator>
                 </td>
             </tr>
         </table>
        <p>
            <asp:Button ID="btnCadastrar" runat="server" OnClick="btnCadastrar_Click" Text="Cadastrar" Visible="False" />
            </p>
        
    </div>
  </asp:content>