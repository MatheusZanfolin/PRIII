<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center" id="cabecalho">
        <h2>Área de Serviço ao Paciente</h2>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Panel ID="pnlMedico" runat="server">
                    <a href="LogonMedico.aspx">
                        <img src="img/entrarP.jpg" class="img img-rounded img-principal" />                        

                        <br/>

                        <span class="texto-link text-center">
                            <center> <h2>Médico, entre aqui!</h2></center>                    
                        </span>
                    </a>                    
                </asp:Panel>
            </div>

            <div class="col-md-6">
                <asp:Panel ID="pnlPaciente" runat="server">
                    <a href="CadPaciente.aspx">
                    <img src="img/paciente.jpg" class="img img-rounded img-principal"/>

                    <br />

                    <span class="texto-link text-center">
                       <center><h2> Realize seu cadastro aqui!</h2> </center>
                    </span>
                        </a>
                </asp:Panel>
            </div>
        </div>
    </div>
      <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Panel ID="Panel3" runat="server">
                    <a href="About.aspx">
                        <img src="img/sobre.jpg" class="img img-rounded img-principal" />                        

                        <br/>

                        <span class="texto-link text-center">
                            <center> <h2>Um pouco sobre nós</h2></center>                    
                        </span>
                    </a>                    
                </asp:Panel>
            </div>
                        <div class="col-md-6">
                <asp:Panel ID="Panel4" runat="server">
                    <a href="Contact.aspx">
                    <img src="img/contato.jpg" class="img img-rounded img-principal"/>

                    <br />

                    <span class="texto-link text-center">
                       <center><h2> Contate-nos</h2> </center>
                    </span>
                        </a>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
