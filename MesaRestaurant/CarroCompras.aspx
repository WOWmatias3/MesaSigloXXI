<%@ Page  Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarroCompras.aspx.cs" Inherits="MesaRestaurant.CarroCompras" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
.center {
  margin: auto;
  width: 80%;
  padding: 10px;
}
    .auto-style1 {
        margin: auto;
        width: 78%;
        padding: 10px;
    }
    .justify-content-around{-ms-flex-pack:distribute!important;justify-content:space-around!important}
</style>

<div class="row">
    <br />
    <h1 style="text-align:center">Carro de Compras</h1>
    <h1 style="text-align:left;margin-left:30px">Detalle</h1>

    <asp:Label ID="Label1" runat="server" Text="" Font-Size ="Large" Font-Bold="true"></asp:Label>
    <div class ="col-sm-12">
        <asp:GridView  ID="GridView1" runat="server" AutoGenerateColumns="False" Width="96%" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
            OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
             CssClass="table-responsive" EditRowStyle-HorizontalAlign="Center" EditRowStyle-VerticalAlign="Middle" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/imagenes/delete_102866.png" />
                <asp:BoundField DataField="codproducto" HeaderText="Codigo" ItemStyle-HorizontalAlign ="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="NombreProd" HeaderText="Nombre" />
                <asp:BoundField DataField="CatProducto" HeaderText="Categoria" />
                <asp:BoundField DataField="preproducto" HeaderText="Precio" />
                <asp:TemplateField HeaderText="Cantidad">
                <ItemTemplate>
                    <asp:Button class="btn btn-primary" ID="Button2" CommandName="bajar" runat="server" CommandArgument='<%# Container.DataItemIndex %>' Text="<"/>
                    <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="40px" Enabled="false" Text='<%# Eval("canproducto") %>'></asp:TextBox>
                    <asp:Button class="btn btn-primary" ID="Button3" CommandName="subir" runat="server" Text=">" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="subtotal" HeaderText="Sub Total" />
            </Columns>

<EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle"></EditRowStyle>
<EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle"></EmptyDataRowStyle>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<RowStyle HorizontalAlign="Center" BorderStyle="Double" BorderWidth="5px"></RowStyle>
        </asp:GridView>
        <br />
        <br />
        <div class="container">
            <div class="row">


                <div class="col-sm-5"></div>
                <div class="col-sm-2" style="align-items:center">
                    <asp:Button class="btn btn-primary center" ID="Button1" runat="server" Text="Realizar Pedido" OnClick="Button1_Click" Width="175px" />
                </div>
                <div class="col-sm-5"></div>
            </div>
        </div>
        <br />
        <br />
        <div style="margin-left: auto; margin-right: 20px; text-align: center;">
            <asp:Label ID="lblDetalle" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"
        CssClass="StrongText"></asp:Label>
        </div>
        <br />
        <asp:GridView  ID="GridView2" runat="server" AutoGenerateColumns="true" Width="96%" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
            OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting"
             CssClass="table-responsive" EditRowStyle-HorizontalAlign="Center" EditRowStyle-VerticalAlign="Middle" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-VerticalAlign="Middle">
<EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle"></EditRowStyle>
<EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle"></EmptyDataRowStyle>
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
<RowStyle HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="5px"></RowStyle>
        </asp:GridView>

        <br />
        <div style="margin-left: auto; margin-right: 20px; text-align: end;">
            <asp:Label ID="lblTotal" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"
        CssClass="StrongText"></asp:Label>
        </div>


        <div style="margin-left: auto; margin-right: 20px; text-align: center;">
            <asp:Label ID="Label4" runat="server"  Text="Seleccione el metodo de pago" Font-Bold="true" Font-Size="X-Large" CssClass="StrongText"></asp:Label>
        </div>

        <div class="col-sm-12 col-md-6">
             <asp:Label ID="Label2" style="text-align:center" runat="server" Text="Efectivo" Font-Bold="true" Font-Size="Medium" CssClass="StrongText"></asp:Label>
            <asp:RadioButton ID="RadioButton1" GroupName="metodopago" Text='<img src="/imagenes/efectivo.png" alt="img2" height ="200px"/>' runat="server" />
        </div>
        <div  class="col-sm-12 col-md-6">
            <asp:Label ID="Label3" runat="server" Text="Tarjeta" Font-Bold="true" Font-Size="Medium" CssClass="StrongText"></asp:Label>
            <asp:RadioButton ID="RadioButton2" GroupName="metodopago" Text='<img src="/imagenes/webpay.png" alt="img2" height ="200px"/>' runat="server" />
        </div>
        
        
        <div class="col-sm-5" ></div>
        <div class="col-sm-2" >
            <asp:Button class="btn btn-primary center" ID="Button5" Visible="false" runat="server" Text="Ir a Pagar" Width="100%" OnClick="Button5_Click" />
        </div>
        <div class="col-sm-5"></div>




  </div>
        

        <br />



    </div>

                


</asp:Content>
