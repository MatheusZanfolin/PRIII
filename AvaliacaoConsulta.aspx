<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AvaliacaoConsulta.aspx.cs" Inherits="AvaliacaoConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Menu ID="MenuPac" runat="server" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Avaliar Consulta" Value="Avaliar Consulta" NavigateUrl="~/AvaliacaoConsulta.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Ultimas Consultas com Determinado Médico" Value="UltimasConsultasPac" NavigateUrl="~/UltimasConsultasPac.aspx"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/SolicitarConsulta.aspx" Text="Solicitar Consulta" Value="Solicitar Consulta"></asp:MenuItem>
                    
                
                <asp:MenuItem NavigateUrl="~/logonPac.aspx" Text="Logout" Value="Logout"></asp:MenuItem>
                    
                
            </Items>
        </asp:Menu>    

    <br />
    <h1>Avaliação da Consulta</h1>
        Ficamos felizes em ouvir sua opinião, quando terminar, por favor clique em &quot;Postar Avaliação&quot;<br />
        <asp:Label ID="lblMensagem" runat="server" Text="[lblErro]" ForeColor="Red"></asp:Label>
        <p>
        <asp:DropDownList ID="ddlConsulta" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        De 0 a 10, eu classificaria essa consulta como
        <asp:TextBox ID="txtNota" runat="server" TextMode="Number" min="0" max="10" step="1" Width="63px"></asp:TextBox>
    &nbsp;<asp:RequiredFieldValidator ID="vldNota" runat="server" ControlToValidate="txtComentario" ErrorMessage="Por favor, informe a nota"></asp:RequiredFieldValidator>
    </p>
    <p>
        Também gostaria de dizer que
        <asp:TextBox ID="txtComentario" runat="server" Height="100px" TextMode="MultiLine" Width="263px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnPostar" runat="server" Text="Postar Avaliação" OnClick="btnPostar_Click" />
    </p>
</asp:Content>

