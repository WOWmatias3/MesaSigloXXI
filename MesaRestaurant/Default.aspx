<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MesaRestaurant._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Ingreso Mesa</h1>
        <p class="lead">Seleccione la mesa para continuar</p>
    </div>

    <div class="container">
        <div class="col-sm-12 col-md-6">
            <div class="col-sm-12">
                <h3>Nombre de Usuario</h3>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <br />
            </div>
            <div class="col-sm-12">
                <h3>Contraseña</h3>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <p>(solo para usuarios Autorizados)</p>
            </div>
            
        </div>
        <div class="col-sm-12 col-md-6">
            <h2 class="col-sm-12">Nº Mesa:&nbsp; </h2>
            <br />
            <div class="col-sm-12">
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="261px" DataSourceID="SqlDataSource1" DataTextField="ID_MESA" DataValueField="ID_MESA" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="20pt" Font-Strikeout="False" Font-Underline="False">
                <asp:ListItem Selected="True" Value="0">Seleccione mesa</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="lblComprobacion" Font-Size="Large" ForeColor="Red" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button class="btn btn-primary" ID="btnIngresar" runat="server" Text="Ingresar" Height="29px" Width="259px" OnClick="btnIngresar_Click" />
            </div>
            <br />
            <br />


                

            <p>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT ID_MESA FROM RESTAURANT.MESA WHERE (STATUS = 'vacio')"></asp:SqlDataSource>
            </p>
        </div>
    </div>

</asp:Content>
