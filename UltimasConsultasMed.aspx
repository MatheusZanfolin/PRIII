<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UltimasConsultasMed.aspx.cs" Inherits="UltimasConsultasMed" %>

<asp:Content ID="ContentUltimasConsultasMed" ContentPlaceHolderID="MainContent" Runat="Server">



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
        </h1>
        <h1>Últimas Consultas do Médico com Determinado Paciente</h1>
        <asp:Label ID="lblErro" runat="server" Font-Bold="False" Font-Names="Times New Roman" Font-Overline="False" Font-Size="20pt" Font-Underline="True" ForeColor="Red"></asp:Label>
            <table>
                <tr>
                    <td>
            <asp:Label ID="Label1" runat="server" Text="Usuário do Paciente:"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="txtUsuario" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Digite o usuário do paciente!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
            <asp:Label ID="Label2" runat="server" Text="Número de consultas para mostrar:"></asp:Label>
                    </td>
                    <td>
            <asp:TextBox ID="txtNumero" runat="server" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ControlToValidate="txtNumero" ErrorMessage="Digite o número de consultas!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        <p>
            <asp:Table ID="tabDados" runat="server">
            </asp:Table>
        </p>
        
        <p>
            <asp:Button ID="btnGeraRelatorio" runat="server" Text="Gerar Relatório" OnClick="btnGeraRelatorio_Click" />
            &nbsp;<asp:Button ID="btnRedefinir" runat="server" OnClick="btnRedefinir_Click" Text="Redefinir configuração" />
        </p>

    </div>
  </asp:content>