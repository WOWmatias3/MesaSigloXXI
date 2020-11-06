<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuPostre.aspx.cs" Inherits="MesaRestaurant.MenuPostre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <script type="text/javascript" src="Scripts/gridresponsive.js"></script>

<div class="container-fluid" style="background-color:beige"> 

    <%-- Hidden filed to set the ratio value--%>
    
    <h1 style="text-align:center">Menú</h1>
    <h1 style="text-align:left;margin-left:30px">Postres</h1>
        <br />
    <asp:Label style="text-align:center" Font-Size="Medium" Font-Bold="true" ID="lbPlatoAgregado" runat="server" Text=""></asp:Label>
    <br />
    <br /> 
                
        <div class="row" style="background-color:beige">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM &quot;PLATO&quot; where categoria = 'Postre' and habilitado = 'Si' and Stock_preparado &gt;0"></asp:SqlDataSource>
        </div>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" DataKeyField="id_plato" OnItemCommand="platocommand" >

        <ItemTemplate >
                <div class="col-sm-12 col-md-6" style="padding:15px; border-style: solid;">
                    <div class="col-sm-12 col-md-4">
                        <asp:Image runat="server" width="140" height="120" ImageUrl='<%# GetImage(Eval("IMAGEN")) %>' GenerateEmptyAlternateText="true" AlternateText="Imagen No encontrada"/>
                    </div>
                    <div class="col-sm-12 col-md-8" style="vertical-align:central;">
                        <div class="col-sm-12">
                            <asp:HiddenField id="lbCodigo" runat="server" Value ='<%# Eval("ID_PLATO") %>'/>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>
                        <div class="col-sm-12">
                            <asp:Label ID="NOMBRE_PLATOLabel" runat="server" Text='<%# Eval("NOMBRE_PLATO") %>' Font-Size="Medium" Font-Bold="True" Font-Italic="True" />
                        </div>
                        <div class="col-sm-12" style="height:85px">
                            <asp:Label ID="Label2" runat="server" Text= '<%# Eval("DESCRIPCION") %>' ></asp:Label> 
                        </div>
                        <div class="col-sm-12">
                            <p>Precio:$ <asp:Label ID="lbPrecio" runat="server" Text='<%# Eval("Precio") %>'></asp:Label></p>
                        </div>

                        <div class="col-sm-12">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Agregar Al Carro" OnClick="clickagregar" CommandName="Seleccionar"/>
                        </div>
                    </div>
                </div>
        </ItemTemplate>
    </asp:ListView>



</div>
</asp:Content>
