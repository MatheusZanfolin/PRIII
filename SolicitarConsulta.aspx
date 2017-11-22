<%@ Page Language="C#"  MasterPageFile="~/Site.master"AutoEventWireup="true" CodeFile="SolicitarConsulta.aspx.cs" Inherits="SolicitarConsulta" %>

<asp:Content ID="ContentSolicitarConsultaSA" ContentPlaceHolderID="MainContent" Runat="Server">

    <div id="divErro">
    
        <asp:Menu ID="MenuPac" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
        </asp:Menu>    


        <br />
    <h1>Solicitar Consulta</h1>
        <asp:Label ID="lblErro" runat="server" ForeColor="Red"></asp:Label>
        <table>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Especialidade:"></asp:Label></td>
                <td><asp:ListBox ID="lsbEspec" runat="server" OnSelectedIndexChanged="lsbEspec_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox> <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ErrorMessage="Selecione uma especialidade" ValidationGroup="lsbEspec" ControlToValidate="lsbEspec"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Médico:"></asp:Label></td>
                <td><asp:ListBox ID="lsbMedico" runat="server" OnSelectedIndexChanged="lsbMedico_SelectedIndexChanged" Visible="False" AutoPostBack="True"></asp:ListBox> <asp:RequiredFieldValidator ID="rfvMedico" runat="server" ErrorMessage="Selecione uma médico!" ValidationGroup="lsbMedico" ControlToValidate="lsbMedico"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Dia:"></asp:Label></td>
                <td><asp:TextBox ID="txtData" runat="server" OnTextChanged="txtData_TextChanged" TextMode="Date" AutoPostBack="True"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvData" runat="server" ErrorMessage="Selecione um dia!" ValidationGroup="txtData" ControlToValidate="txtData"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Horários Disponíveis:"></asp:Label></td>
                <td><asp:ListBox ID="lsbHorarios" runat="server" Visible="False"></asp:ListBox> <asp:RequiredFieldValidator ID="rfvMedico2" runat="server" ErrorMessage="Selecione um horário!" ValidationGroup="lsbHorarios" ControlToValidate="lsbHorarios"></asp:RequiredFieldValidator></td>
            </tr>
        </table>
        <p>
          

            <asp:Button ID="btnAgendar" runat="server" OnClick="btnAgendar_Click" Text="Agendar" />
          

    </p>
        <p>
        
    </p>


    </div>
   
    </asp:Content>