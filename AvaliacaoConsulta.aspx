<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AvaliacaoConsulta.aspx.cs" Inherits="AvaliacaoConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Menu ID="MenuPac" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
            <StaticMenuItemStyle HorizontalPadding="10px" />
        </asp:Menu>    

    <br />
    <h1>Avaliação da Consulta</h1>
        Ficamos felizes em ouvir sua opinião, quando terminar, por favor clique em &quot;Postar Avaliação&quot;<br />
        <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
        <table>
            <tr>
                <td>
            <asp:Label ID="Label3" runat="server" Text="Consulta: "></asp:Label>
                </td>
                <td>
        <asp:DropDownList ID="ddlConsulta" runat="server">
        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Nota: "></asp:Label>
                </td>
                <td>
        <asp:TextBox ID="txtNota" runat="server" TextMode="Number" min="0" max="10" step="1" Width="63px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vldNota" runat="server" ControlToValidate="txtNota" ErrorMessage="Por favor, informe a nota"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;
                    <asp:RangeValidator ID="vldNumNota" runat="server" ControlToValidate="txtNota" ErrorMessage="Nota inválida" MaximumValue="10" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
        <asp:Label ID="Label2" runat="server" Text="Comentarios:"></asp:Label>
                </td>
                <td>
        <asp:TextBox ID="txtComentario" runat="server" Height="100px" TextMode="MultiLine" Width="263px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="vldComentarios" runat="server" ErrorMessage="Por favor, justifique sua nota" ControlToValidate="txtComentario"></asp:RequiredFieldValidator>
                </td>
            </tr>
    </table>
    <p>
        <asp:Button ID="btnPostar" runat="server" Text="Postar Avaliação" OnClick="btnPostar_Click" />
    </p>
</asp:Content>

