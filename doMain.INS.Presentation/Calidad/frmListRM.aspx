<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListRM.aspx.cs" Inherits="doMain.INS.Presentation.Calidad.frmListRM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagPrefix="cc1" Namespace="System.Web.UI" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server"  EnableViewState="true">           

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script src="../../Scripts/jsINS.js"></script>    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>            

     <script type="text/javascript">     

          var confirmed = false;                    
          function ShowConfirm(Mensaje, controlID) {                                                                                                            
              
              if (confirmed) { return true; }

              bootbox.confirm(Mensaje, function (result) {
                  if (result) {
                      if (controlID != null) {
                          var controlToClick = document.getElementById(controlID);

                          if (controlToClick != null) {
                              confirmed = true;
                              controlToClick.click();
                              confirmed = false;
                          }
                      }
                  }

              });

              return false;
          }
          function muestra_oculta(id) {
              if (document.getElementById) { //se obtiene el id
                  var el = document.getElementById(id); //se define la variable "el" igual a nuestro div
                  el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
              }
          }
          window.onload = function () {/*hace que se cargue la función lo que predetermina que div estará oculto hasta llamar a la función nuevamente*/
              muestra_oculta('contenido');/* "contenido_a_mostrar" es el nombre que le dimos al DIV */
          }             
          function Mayuscula(e) {
              e.value = e.value.toUpperCase();
          }
          function CheckAllEmp(Checkbox) {
              
              var GridView = Checkbox.parentNode.parentNode.parentNode;
              var inputList = GridView.getElementsByTagName("INPUT");

              for (var i = 0; i < inputList.length; i++) {
                  //Get the Cell To find out ColumnIndex
                  var row = inputList[i].parentNode.parentNode;

                  if (inputList[i].type == "checkbox" && Checkbox != inputList[i]) {
                      if (Checkbox.checked) {
                          //If the header checkbox is checked                         
                          inputList[i].checked = "checked";

                      }
                      else { inputList[i].checked = false; }
                  }
              }
          }
          function formateafecha(fecha) {
              var long = fecha.length;
              var dia;
              var mes;
              var ano;

              if ((long >= 2) && (primerslap == false)) {
                  dia = fecha.substr(0, 2);
                  if ((IsNumeric(dia) == true) && (dia <= 31) && (dia != "00")) { fecha = fecha.substr(0, 2) + "/" + fecha.substr(3, 7); primerslap = true; }
                  else { fecha = ""; primerslap = false; }
              }
              else {
                  dia = fecha.substr(0, 1);
                  if (IsNumeric(dia) == false)
                  { fecha = ""; }
                  if ((long <= 2) && (primerslap = true)) { fecha = fecha.substr(0, 1); primerslap = false; }
              }
              if ((long >= 5) && (segundoslap == false)) {
                  mes = fecha.substr(3, 2);
                  if ((IsNumeric(mes) == true) && (mes <= 12) && (mes != "00")) { fecha = fecha.substr(0, 5) + "/" + fecha.substr(6, 4); segundoslap = true; }
                  else { fecha = fecha.substr(0, 3);; segundoslap = false; }
              }
              else { if ((long <= 5) && (segundoslap = true)) { fecha = fecha.substr(0, 4); segundoslap = false; } }
              if (long >= 7) {
                  ano = fecha.substr(6, 4);
                  if (IsNumeric(ano) == false) { fecha = fecha.substr(0, 6); }
                  else { if (long == 10) { if ((ano == 0) || (ano < 1900) || (ano > 2100)) { fecha = fecha.substr(0, 6); } } }
              }

              if (long >= 10) {
                  fecha = fecha.substr(0, 10);
                  dia = fecha.substr(0, 2);
                  mes = fecha.substr(3, 2);
                  ano = fecha.substr(6, 4);
                  // Año no viciesto y es febrero y el dia es mayor a 28 
                  if ((ano % 4 != 0) && (mes == 02) && (dia > 28)) { fecha = fecha.substr(0, 2) + "/"; }
              }
              return (fecha);
          }

      </script>

    <div id="DivExpediente">

              <h2>&nbsp;&nbsp;&nbsp;Consulta de ingreso y salida de productos</h2> 

            <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">   
                <ContentTemplate>

                     <!-- Definicion de Hide -->          
                    <asp:HiddenField ID="HideArea" runat="server" />          
                    <asp:HiddenField ID="HideAccion" runat="server" />
                    <asp:HiddenField ID="HideIdExpediente" runat="server" />     
                    <asp:HiddenField ID="HideIdReprogramacion" runat="server" />     

                     <!-- Opcion Filtros de Busquedas -->
                     <div id="divfiltro">
                 <table>                  
                    <tr>
                        <td>                                                        
                            <asp:Label ID="lblFilExpediente" runat="server" Text="Expediente:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilExpediente" runat="server"  Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblFilCotizacion" runat="server" Text="Cotización:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFilCotizacion" runat="server"  Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info" OnClick="btnBuscar_Click"/>
                        </td>
                        <td colspan="4">

                        </td>
                    </tr>
                   <tr>
                       <td colspan="8">
                           <div class="titulo_boton">
                                Más filtros de búsqueda.
                                <a style='cursor: pointer;' onClick="muestra_oculta('contenido')" title="" class="boton_mostrar">Mostrar / Ocultar</a>
                           </div>
                           <div id="contenido">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFilTipoCliente" runat="server" Text="Tipo Cliente:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilTipoCliente" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilCliente" runat="server" Text="Cliente:"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtFilCliente" runat="server"  Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilProducto" runat="server" Text="Producto:"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtFilProducto" runat="server"  Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilFecCotiz" runat="server" Text="Fec. Cotización:"></asp:Label>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtFilFecCotiz" runat="server"  Width="100px"></asp:TextBox>
                                             <asp:CalendarExtender ID="CalFilFecCotiz" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFilFecCotiz" TargetControlID="txtFilFecCotiz"></asp:CalendarExtender>   
                                             <asp:ImageButton ID="ImgFecCotiz"  runat="server" CausesValidation="False" ImageUrl="../Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFilEstado" runat="server" Text="Estado:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilEstado" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilEnsayo" runat="server" Text="Ensayo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilEnsayo" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilAnalista" runat="server" Text="Analista:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilAnalista" runat="server" Width="150px" AutoPostBack="false"></asp:DropDownList>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilFecIngreso" runat="server" Text="Fecha Ingreso:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFilFecIngreso" runat="server"  Width="100px"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalFilFecIngreso" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFilFecIngreso" TargetControlID="txtFilFecIngreso"></asp:CalendarExtender>   
                                            <asp:ImageButton ID="ImgFecIngreso"  runat="server" CausesValidation="False" ImageUrl="../Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                        </td>
                                    </tr>
                                </table>
                           </div>
                       </td>
                   </tr>                    
               </table>              
              </div>

                     <!-- Opcion de Operaciones -->
                     <div id="divOperaciones">
                  <fieldset>
                     <table>
                        <tr>
                            <td colspan="2"  style="width:350px;">                                                                                             
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-info"/>
                                &nbsp;                                 
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar"  OnClientClick="return ShowConfirm('Está seguro que desea enviar los expedientes?', this.id);"  CssClass="btn btn-info"/>
                                &nbsp;
                                <asp:Button ID="btnRecibir" runat="server" Text="Recibir" OnClientClick="return ShowConfirm('Está seguro que desea recibir los expedientes?', this.id);"  CssClass="btn btn-info"/>
                                &nbsp;
                                <asp:Button ID="btnEstado" runat="server" Text="Estado"  CssClass="btn btn-info"/>
                            </td>                                                      
                            <td style="width:230px;">
                                <asp:Label ID="lblContador" runat="server" ForeColor="black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:330px;">
                                <asp:Label ID="lblFila" runat="server"  Font-Bold="true" Text="Filas por Página:"></asp:Label>
                                 &nbsp;
                                 <asp:DropDownList ID="ddlRows" runat="server" AutoPostBack="True"  Width="50px" onselectedindexchanged="ddlRows_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>
                                 &nbsp;
                                 <asp:Label ID="lblPagina" runat="server"  Font-Bold="true" Text="Página"></asp:Label> 
                                &nbsp;
                                <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="True" Width="60px" onselectedindexchanged="ddlPage_SelectedIndexChanged"></asp:DropDownList>    
                                &nbsp;
                                de
                                &nbsp;
                                <asp:Label ID="lblTotalPages" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:100px;">                                                                                                                                                           
                                <asp:Button ID="lnkbtnFirst" CssClass="GridPageFirstInactive" ToolTip="First" CommandName="First" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                &nbsp;
                                <asp:Button ID="lnkbtnPre" CssClass="GridPagePreviousInactive" ToolTip="Previous" CommandName="Previous" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                &nbsp;
                                <asp:Button ID="lnkbtnNext" CssClass="GridPageNextInactive" ToolTip="Next" runat="server" CommandName="Next" OnCommand="GetPageIndex"></asp:Button>
                                &nbsp;
                                <asp:Button ID="lnkbtnLast" CssClass="GridPageLastInactive" ToolTip="Last" runat="server" CommandName="Last" OnCommand="GetPageIndex"></asp:Button>
                            </td>
                            <td style="width:250px; text-align:center;">
                                <asp:Label ID="lblPlazo" runat="server" ForeColor="black" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:100px; text-align:left;">
                                <asp:ImageButton runat="server" ID="btnExportarExcel" DescriptionUrl="Exportacion Excel." AlternateText="Exportar expedientes a excel." ToolTip="Exportar expedientes a excel." ImageUrl="~/Images/ExcelExportar.png" Width="40px" Height="40px" OnClick="btnExportarExcel_Click"/>
                            </td>
                            
                        </tr>
                    </table>
                    </fieldset>
                    <div class="messagealert" id="alert_container"></div>          
               </div>
           
                     <!-- Opcion grilla de expediente -->        
                     <div class="div-View-grid">
                        <div style="width:100%; overflow-y:auto">                     
                    <asp:GridView ID="gdvExpediente"
                             AutoGenerateColumns="False" 
                             ShowHeaderWhenEmpty="True"                                   
                             DataKeyNames="IDEXPEDIENTE, CodigoCotizacion, CodigoExpediente, Alerta, Estado"
                             CellPadding="4" 
                             PageSize ="10"
                             allowpaging="false" 
                             CssClass="gridStyle" 
                             ShowFooter="True"                                                                  
                             EmptyDataText="No Existen Datos ..."                          
                             style="text-align: center" 
                             Width="100%"                            
                             OnRowCommand="gdvExpediente_RowCommand"
                             OnRowDataBound ="gdvExpediente_RowDataBound" 
                             showheader="true"                           
                             runat="server">
                 <PagerStyle CssClass="pagination-ys" />
                 <AlternatingRowStyle CssClass="alternatingrowStyle" />
                 <Columns>

                    <asp:TemplateField ItemStyle-Width="25px">
                        <HeaderTemplate>                                          
                            <asp:CheckBox ID="chkboxSelectAll"  runat="server" onclick="CheckAllEmp(this); "/>                                                                                   
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>                                        
                            <asp:CheckBox ID="chkboxSelect" runat="server" Visible="true"></asp:CheckBox>                                        
                        </ItemTemplate>
                    </asp:TemplateField>  

                   <asp:TemplateField>
                        <HeaderTemplate> # </HeaderTemplate>
                        <ItemTemplate>
                            <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                        </ItemTemplate>
                        <ItemStyle  HorizontalAlign="Center" Width="5%"/>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Actualizar" ShowHeader="false">
                        <ItemTemplate>                                        
                             <asp:LinkButton ID="btnActualizar" runat="server" ToolTip="Actualizar Archivo"  CommandName="Actualizar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ><i class="fa fa-pencil-square-o"></i></asp:LinkButton>                                         
                        </ItemTemplate>
                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                   </asp:TemplateField>

                   <asp:BoundField DataField="CodigoExpediente" HeaderText="Expediente">
                            <ItemStyle HorizontalAlign="center" Width="20px" />
                    </asp:BoundField>                  

                    <asp:BoundField DataField="CodigoCotizacion" HeaderText="Cotización">
                            <ItemStyle HorizontalAlign="center" Width="20px" />
                    </asp:BoundField>

                   <asp:TemplateField HeaderText="Fecha Cotizacion" ShowHeader="false">
                         <ItemTemplate> 
                             <asp:Label ID="lblgvFecCotizacion" runat="server" Text='<%# Bind("FechaCotizacion","{0:d}") %>'></asp:Label> 
                         </ItemTemplate>
                         <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>      

                    <asp:BoundField DataField="Cliente" HeaderText="Cliente">
                            <ItemStyle HorizontalAlign="center" Width="80px" />
                    </asp:BoundField>

                   <asp:BoundField DataField="TipoCliente" HeaderText="Tipo Cliente">
                            <ItemStyle HorizontalAlign="center" Width="50px" />
                    </asp:BoundField>                  

                    <asp:BoundField DataField="Producto" HeaderText="Producto">
                       <ItemStyle HorizontalAlign="center" Width="80px" />
                    </asp:BoundField>                    

                   <asp:BoundField DataField="Lote" HeaderText="Lote">
                       <ItemStyle HorizontalAlign="center" Width="100px" />
                   </asp:BoundField>

                   <asp:BoundField DataField="Correo" HeaderText="Correo">
                            <ItemStyle HorizontalAlign="center" Width="60px" />
                   </asp:BoundField>

                   <asp:BoundField DataField="ActaPesquisa" HeaderText="Acta Pesquisa">
                            <ItemStyle HorizontalAlign="center" Width="60px" />
                   </asp:BoundField>

                   <asp:BoundField DataField="Usuario.Login" HeaderText="Ubic/Usuario ">
                       <ItemStyle HorizontalAlign="center" Width="100px" />
                   </asp:BoundField>

                   <asp:TemplateField HeaderText="Estado" ShowHeader="false">
                         <ItemTemplate> 
                             <asp:Label ID="lblGvEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label> 
                         </ItemTemplate>
                         <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>                                    

                   <asp:TemplateField HeaderText="Fecha Ingreso" ShowHeader="false">
                         <ItemTemplate> 
                             <asp:Label ID="lblgvFecIngreso" runat="server" Text='<%# Bind("FechaIngreso","{0:d}") %>'></asp:Label> 
                         </ItemTemplate>
                         <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>  

                   <asp:BoundField DataField="DocumentoCustodia" HeaderText="Doc. Custodia">
                            <ItemStyle HorizontalAlign="center" Width="60px" />
                   </asp:BoundField>      
                     
                   <asp:TemplateField HeaderText="Fec Ingreso Lab." ShowHeader="false">
                         <ItemTemplate> 
                             <asp:Label ID="lblgvFecIngLab" runat="server" Text='<%# Bind("FechaIngresoLab","{0:d}") %>'></asp:Label> 
                         </ItemTemplate>
                         <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>       

                   <asp:TemplateField HeaderText="Fec Entrega Cliente." ShowHeader="false">
                         <ItemTemplate> 
                             <asp:Label ID="lblgvFecEntCliente" runat="server" Text='<%# Bind("FechaEntregaCliente","{0:d}") %>'></asp:Label> 
                         </ItemTemplate>
                         <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>
                  

                  <asp:TemplateField HeaderText="Fec Recep. IE." ShowHeader="false">
                       <ItemTemplate> 
                           <asp:Label ID="lblgvFecRecepcion" runat="server" Text='<%# Bind("FechaRecepcionIE","{0:d}") %>'></asp:Label> 
                       </ItemTemplate>
                       <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                   </asp:TemplateField>

                   <asp:BoundField DataField="InformeEnsayo" HeaderText="Inf. Ensayo">
                            <ItemStyle HorizontalAlign="center" Width="60px" />
                   </asp:BoundField>

                   <asp:TemplateField HeaderText="Alerta Interna" ShowHeader="false">
                        <ItemTemplate>                 
                            <table>
                                <tr>
                                    <td>
                                         <asp:Label ID="lblAlerta" runat="server" Text='<%# Bind("Alerta") %>'></asp:Label>   
                                    </td>
                                    <td>
                                        <asp:Image ID="ImgAlertaInt" runat="server" ImageAlign="Middle" Width="25px" Height="25px"/>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                    </asp:TemplateField>                                      

                   <asp:TemplateField HeaderText="Detalle R.M" ShowHeader="false">
                        <ItemTemplate>                                        
                             <asp:LinkButton ID="btnDetalle" runat="server" ToolTip="Detalle Recepcion Muestra"  CommandName="Mostrar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ><i class="fa fa-list-alt" aria-hidden="true"></i></asp:LinkButton>                                         
                        </ItemTemplate>
                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                   </asp:TemplateField>

                   <asp:TemplateField HeaderText="Reprogramar" ShowHeader="false">
                        <ItemTemplate>              
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNroReprog" runat="server" Text='<%# Bind("NroReprogramacion") %>'></asp:Label>   
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnReprogramar" runat="server" ToolTip="Registro de Reprogramación"  CommandName="Reprogramar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"><i class="fa fa-calendar-check-o" aria-hidden="true"></i></asp:LinkButton>                                         
                                    </td>
                                </tr>                                
                            </table>                                                       
                        </ItemTemplate>
                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                   </asp:TemplateField>

                 </Columns>
                 <EditRowStyle BackColor="#7C6F57" />
                 <FooterStyle CssClass="footerStyle" Font-Bold="True" />
                 <HeaderStyle CssClass="headerStyle" Font-Bold="True" />
                 
                 <RowStyle CssClass="rowStyle" />
                 <SelectedRowStyle CssClass="selectedrowStyle" Font-Bold="True" />
                 <SortedAscendingCellStyle BackColor="#F8FAFA" />
                 <SortedAscendingHeaderStyle BackColor="#246B61" />
                 <SortedDescendingCellStyle BackColor="#D4DFE1" />
                 <SortedDescendingHeaderStyle BackColor="#15524A" />
               </asp:GridView>
                </div>
                    </div> 

                    <!-- Modal Registro Cotizacion -->
                    <asp:ModalPopupExtender ID="btnCotizacion_ModalPopupExtender" runat="server"
                    BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                    PopupControlID="pnlRegistroCotizacion" 
                    TargetControlID="HideCotizacion" 
                    CancelControlID="btnCerrarCoti">
                    </asp:ModalPopupExtender>    
                    <asp:HiddenField ID="HideCotizacion" runat="server" />

                    <!-------------------------------------Pop Up Registro Cotizacion ---------------------------> 
                    <asp:Panel ID="pnlRegistroCotizacion" runat="server" BackColor="White" CssClass="PopupOperacion2">
                <div style="width: 400px; height: 550px;">   
                     <table style="margin: 10px 0 0 20px;">
                         <tr>
                           <td colspan="2">     
                                <h3>DATOS DE INGRESO</h3>                                           
                                <br />
                            </td>                                             
                         </tr>
                         <tr>
                             <td>
                                  <asp:Label ID="lblTipoCliente" runat="server" Text="Tipo Cliente:"></asp:Label>
                             </td>
                             <td>
                                 <asp:DropDownList ID="ddlTipoCliente" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblOficio" runat="server" Text="Nro. Oficio Solic:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtOficio" runat="server" Width="200px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="LblFecCotizacion" runat="server" Text="Fecha Oficio Solic:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtFecOficio" runat="server" Width="200px" onKeyUp="this.value=formateafecha(this.value);" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalFecCotiza" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFecOficio" TargetControlID="txtFecOficio"></asp:CalendarExtender>   
                                 <asp:ImageButton ID="imgCalendario4"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                             </td>
                         </tr>
                         <tr>
                              <td>
                                 <asp:Label ID="LblFecRecepcion" runat="server" Text="Fecha Recepcion:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtFecRecepcion" runat="server" Width="200px" onKeyUp="this.value=formateafecha(this.value);" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalFecRecepcion" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFecRecepcion" TargetControlID="txtFecRecepcion"></asp:CalendarExtender>   
                                 <asp:ImageButton ID="imgCalendario5"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblExpediente" runat="server" Text="Expediente:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtExpediente" runat="server" Width="200px" ClientIDMode="Static" MaxLength="10"></asp:TextBox>                                 
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblCliente" runat="server" Text="Cliente:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtCliente" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblProducto" runat="server" Text="Producto:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtProducto" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblLote" runat="server" Text="Lote:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtLote" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblFecVencimiento" runat="server" Text="Fecha Vencimiento:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtFecVencimiento" runat="server" Width="200px" Enabled="false" ClientIDMode="Static" onKeyUp="this.value=formateafecha(this.value);" MaxLength="10"></asp:TextBox>                                 
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblCorreo" runat="server" Text="Correo:" ></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtCorreo" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                             </td>
                         </tr>                                                 
                         <tr>
                             <td>
                                 <asp:Label ID="lblActa" runat="server" Text="Acta Pesquisa:" ></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtActa" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                             </td>
                         </tr>                         
                         <tr>
                             <td colspan="2">
                                 <asp:Button ID="btnGrabarCoti" runat="server" Text="Grabar" OnClick="btnGrabarCoti_Click" />      
                                 &nbsp;&nbsp;
                                 <asp:Button ID="btnCerrarCoti" runat="server" Text="Cerrar" OnClick="btnCerrarCoti_Click" />      
                             </td>                             
                         </tr>
                      </table>
                </div>
           </asp:Panel>

                    <!-- Modal Registro Recepcion Muestras -->
                    <asp:ModalPopupExtender ID="btnRM_ModalPopupExtender" runat="server" BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                     PopupControlID="PnlRegistroRM" TargetControlID="HideRM" CancelControlID="btnCerrarRM">  </asp:ModalPopupExtender>    
                    <asp:HiddenField ID="HideRM" runat="server" />

                    <!-------------------------------------Pop Up Registro Recepcion Muestra ---------------------------> 
                    <asp:Panel ID="PnlRegistroRM" runat="server" BackColor="White" CssClass="PopupOperacion2">
                 <div style="width: 750px; height: 560px;">   
                       <table style="margin: 10px 0 0 20px;">
                            <tr>
                                <td colspan="2">     
                                    <h3>DATOS DE INGRESO RECEPCION DE MUESTRAS</h3>                                                                              
                                </td>                                             
                            </tr>
                            <tr>
                               <td colspan="2">
                                   <asp:Label ID="lblTabExpediente" runat="server" Text="Expediente:" Font-Bold="true"></asp:Label>
                                   &nbsp;&nbsp;
                                   <asp:TextBox ID="txtTabExpediente" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                   <br />
                                   <br />
                               </td>                               
                           </tr>
                            <tr>
                               <td colspan="2">
                                     <asp:TabContainer ID="TabContRM" runat="server" ActiveTabIndex="0" Height="395px" Width="700px" CssClass="Tab">                                         
                                         <asp:TabPanel ID="TabCotizacion" runat="server" HeaderText="Cotizacion">  
                                             <ContentTemplate>
                                                 <table>                                                  
                                                   <tr>
                                                         <td colspan="4">
                                                             <h4>Datos de ingreso de Cotización</h4>
                                                         </td>                                                        
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="lblCotExpediente" runat="server" Text="Expediente:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCotExpediente" runat="server" Width="200px" ClientIDMode="Static" MaxLength="10"></asp:TextBox>                                 
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="lblCotTipCliente" runat="server" Text="Tipo Cliente:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCotTipCliente" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCotOficio" runat="server" Text="Nro. Oficio Solic:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCotOficio" runat="server" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCotFecOficio" runat="server" Text="Fecha Oficio Solic:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCotFecOficio" runat="server" Width="200px" onKeyUp="this.value=formateafecha(this.value);" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalCotFecOficio" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCotFecOficio" TargetControlID="txtCotFecOficio"></asp:CalendarExtender>   
                                                            <asp:ImageButton ID="imgCalendario10"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCotFecRecep" runat="server" Text="Fecha Recepcion:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCotFecRecep" runat="server" Width="200px" onKeyUp="this.value=formateafecha(this.value);" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalCotFecRecep" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtCotFecRecep" TargetControlID="txtCotFecRecep"></asp:CalendarExtender>   
                                                            <asp:ImageButton ID="imgCalendario11"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                        </td>
                                                    </tr>
                                                    
                                                 </table>
                                             </ContentTemplate>
                                         </asp:TabPanel>
                                         <asp:TabPanel ID="TabCustodia" runat="server" HeaderText="Custodia">  
                                              <ContentTemplate>
                                                 <table>
                                                    <tr>
                                                         <td colspan="4">
                                                             <h4>Datos de ingreso de Custodia</h4>
                                                         </td>                                                        
                                                    </tr>
                                                    <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCFec" runat="server" Text="Fecha Ingreso:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCFec" runat="server" Width="150px"></asp:TextBox>
                                                             <asp:CalendarExtender ID="CalFecIngreso" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtTCFec" TargetControlID="txtTCFec"></asp:CalendarExtender>   
                                                             <asp:ImageButton ID="imgCalendario1"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                         </td>
                                                         <td>
                                                             <asp:Label ID="lblTCDoc" runat="server" Text="Documento:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCDoc" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                    <tr>
                                                         <td>
                                                             <asp:Label ID="lblTCClaProd" runat="server" Text="Clase Producto:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:DropDownList ID="ddlTCClasProd" runat="server" AutoPostBack="false"  Width="160px"></asp:DropDownList>
                                                         </td>
                                                         <td>
                                                            <asp:Label ID="lblTCTipoProd" runat="server" Text="Tipo Producto:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:DropDownList ID="ddlTCTipoProd" runat="server" AutoPostBack="false"  Width="160px"></asp:DropDownList>
                                                         </td>                                                        
                                                     </tr>
                                                    <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCCant" runat="server" Text="Cantidad:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCCant" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td>
                                                             <asp:Label ID="lblTCCond" runat="server" Text="Condición Amb.:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                              <asp:TextBox ID="txtTCCond" runat="server" Width="150px"></asp:TextBox>
                                                             <br />
                                                             <br />
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td colspan =" 4">
                                                             <asp:Label ID="lblTCUbicacion" runat="server" Text="Ubicación" Font-Bold="true" Font-Underline="true"></asp:Label>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCContramuestra" runat="server" Text="Contramuestra:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCContramuestra" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td>
                                                             <asp:Label ID="lblTCCamFria" runat="server" Text="Cámara Fría:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                              <asp:TextBox ID="txtTCCamFria" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCPreCamara" runat="server" Text="Pre Cámara:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCPreCamara" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td>
                                                             <asp:Label ID="lblTCEstado" runat="server" Text="Estado:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                              <asp:TextBox ID="txtTCEstado" runat="server" Width="160px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                 </table>
                                              </ContentTemplate>
                                         </asp:TabPanel>
                                         <asp:TabPanel ID="TabContramuestra" runat="server" HeaderText="Contramuestra">  
                                             <ContentTemplate>
                                                <table>
                                                   <tr>
                                                       <td colspan="4">
                                                            <h5>Datos de ingreso de Contramuestra</h5>
                                                       </td>                                                        
                                                    </tr>
                                                    <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCMFecLab" runat="server" Text="Fecha Ingreso Lab:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMFecLab" runat="server" Width="150px"></asp:TextBox>
                                                             <asp:CalendarExtender ID="CalFecIngresoLab" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtTCMFecLab" TargetControlID="txtTCMFecLab"></asp:CalendarExtender>   
                                                             <asp:ImageButton ID="imgCalendario2"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                         </td>
                                                         <td>
                                                             <asp:Label ID="lblTCMFecEnt" runat="server" Text="Fecha Entrega Cliente:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMFecEnt" runat="server" Width="150px"></asp:TextBox>
                                                             <asp:CalendarExtender ID="CalFecEntrega" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtTCMFecEnt" TargetControlID="txtTCMFecEnt"></asp:CalendarExtender>   
                                                             <asp:ImageButton ID="imgCalendario3"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                       <td colspan="4">
                                                           <h5>Distribución de Muestras</h5>
                                                       </td> 
                                                    </tr>
                                                     <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCMCER" runat="server" Text="CER:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMCER" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTCMFQ" runat="server" Text="FQ:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMFQ" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTCMMic" runat="server" Text="Microbiología:"></asp:Label>
                                                        </td> 
                                                        <td colspan="3" style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMMic" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                       <td colspan="4">
                                                            <h5>Ingreso muestra retención</h5>
                                                       </td>                                                        
                                                    </tr>
                                                    <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCMOficio" runat="server" Text="Oficio:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">                                                             
                                                             <asp:TextBox ID="txtTCMOficio" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTCMCantidad" runat="server" Text="Cantidad:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox ID="txtTCMCCantidad" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                     <tr>
                                                       <td colspan="4">
                                                            <h5>Retiros 01</h5>
                                                       </td>                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTCMCArea" runat="server" Text="Area Solicitante:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:DropDownList ID="ddlTCMCArea" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTCMCCantidad1" runat="server" Text="Cantidad:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTCMCCantidad1" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                       <td colspan="4">
                                                            <h5>Retiros 02</h5>
                                                       </td>                                                        
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="lblTCMCArea2" runat="server" Text="Area Solicitante:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:DropDownList ID="ddlTCMCArea2" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTCMCCantidad2" runat="server" Text="Cantidad:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTCMCCantidad2" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                         <td>
                                                            <asp:Label ID="lblTCMCSaldo" runat="server" Text="Saldo:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTCMCSaldo" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTCMCUbicacion" runat="server" Text="Ubicación:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTCMCUbicacion" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           <asp:Label ID="lblTCMCEstado" runat="server" Text="Estado:"></asp:Label>
                                                        </td> 
                                                        <td colspan="3" style="text-align:left;">
                                                            <asp:DropDownList ID="ddlTCMEstado" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                              </ContentTemplate>
                                         </asp:TabPanel>
                                         <asp:TabPanel ID="TabFacturación" runat="server" HeaderText="Facturación">  
                                             <ContentTemplate>
                                                 <table>
                                                    <tr>
                                                       <td colspan="4">
                                                            <h5>Datos de ingreso de facturación</h5>
                                                       </td>                     
                                                    </tr>
                                                     <tr>
                                                         <td>
                                                            <asp:Label ID="lblTFFecha" runat="server" Text="Fecha Recepción I.E:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTFFecha" runat="server" Width="150px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalFecRecpIE" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtTFFecha" TargetControlID="txtTFFecha"></asp:CalendarExtender>   
                                                            <asp:ImageButton ID="imgCalendario7"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTFInforme" runat="server" Text="Informe Ensayo:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTFInforme" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                            <asp:Label ID="lblTFConclusion" runat="server" Text="Conclusión:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                             <asp:DropDownList ID="ddlTFConclusion" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTFProforma" runat="server" Text="Proforma:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTFProforma" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                          <td>
                                                            <asp:Label ID="lblTFFactura" runat="server" Text="Nro. Factura:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTFFactura" runat="server" Width="150px"></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:right;">
                                                             <asp:Label ID="lblTFFecEntrega" runat="server" Text="Fecha Entrega Pool:"></asp:Label>
                                                         </td>
                                                         <td style="text-align:left;">
                                                            <asp:TextBox ID="txtTFFecEntrega" runat="server" Width="150px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalFecTFEntrega" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtTFFecEntrega" TargetControlID="txtTFFecEntrega"></asp:CalendarExtender>   
                                                            <asp:ImageButton ID="imgCalendario6"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                        <td>
                                                           <asp:Label ID="lblTFMuestra" runat="server" Text="Muestras:"></asp:Label>
                                                        </td> 
                                                        <td colspan="3" style="text-align:left;">
                                                            <asp:DropDownList ID="ddlTFMuestra" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                                                        </td>
                                                     </tr>
                                                </table>
                                              </ContentTemplate>
                                         </asp:TabPanel>
                                     </asp:TabContainer>

                               </td>
                            </tr>
                           <tr>
                               <td colspan="2">
                                   <asp:Button ID="btnGrabarRM" runat="server" Text="Grabar" OnClick="btnGrabarRM_Click" />      
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCerrarRM" runat="server" Text="Cerrar"/>
                               </td>                               
                           </tr>

                       </table>
                 </div>
            </asp:Panel>

                   <!-- Modal Registro Reprogramacion -->
                    <asp:ModalPopupExtender ID="btnReprogramacion_ModalPopupExtender" runat="server" BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                    PopupControlID="PnlReprogramacion" TargetControlID="HideReprogramacion" CancelControlID="btnCerrarREP">  </asp:ModalPopupExtender>    
                    <asp:HiddenField ID="HideReprogramacion" runat="server" />

                  <!-------------------------------------Pop Up Registro Reprogramacion ---------------------------> 
                  <asp:Panel ID="PnlReprogramacion" runat="server" BackColor="White" CssClass="PopupOperacion2">
                <div style="width: 850px; height: 650px;">   
                    <table style="margin: 10px 0 0 20px;" class="popupmaster-table">
                        <tr>
                            <td colspan="5">     
                                <h3>REPROGRAMACION DE EVALUACION</h3>                                                                              
                            </td>                                             
                        </tr>
                        <tr>
                           <td colspan="5">
                                   <asp:Label ID="lblRepExpediente" runat="server" Text="Expediente:" Font-Bold="true"></asp:Label>
                                   &nbsp;&nbsp;
                                   <asp:TextBox ID="txtRepExpediente" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                   <br />
                                   <br />
                           </td>      
                        </tr>
                        <tr>
                           <td colspan="5">
                                <h4>Registro Reprogramación</h4>
                            </td>
                        </tr>
                        <tr>
                           <td>
                                <asp:Label ID="lblRepOficio" runat="server" Text="Número Oficio:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtRepOficio" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblRepFecOficio" runat="server" Text="Fecha Oficio:"></asp:Label>
                            </td>
                            <td style="text-align:left;" colspan="2">
                                <asp:TextBox ID="txtRepFecOficio" runat="server" Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalFecTFecOficio" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtRepFecOficio" TargetControlID="txtRepFecOficio"></asp:CalendarExtender>   
                                <asp:ImageButton ID="imgCalendario8"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="lblRepCorreo" runat="server" Text="Fecha Correo:"></asp:Label>
                             </td>
                             <td style="text-align:left;">
                                 <asp:TextBox ID="txtRepCorreo" runat="server" Width="150px"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalFecTFecCorreo" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtRepCorreo" TargetControlID="txtRepCorreo"></asp:CalendarExtender>   
                                 <asp:ImageButton ID="imgCalendario9"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                             </td>
                             <td>
                                 <asp:Label ID="lblRepEnsayo" runat="server" Text="Ensayo:"></asp:Label>
                             </td>
                             <td style="text-align:left;" colspan="2">
                                <asp:DropDownList ID="ddlRepEnsayo" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRepAnalista" runat="server" Text="Analista:"></asp:Label>
                            </td>
                            <td  colspan="4" style="text-align:left;">
                                <asp:DropDownList ID="ddlRepAnalista" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                           <td>
                                <asp:Label ID="lblRepPlazo" runat="server" Text="Plazo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRepPlazo" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblRepMotivo" runat="server" Text="Motivo Reprog.:"></asp:Label>
                            </td>
                            <td colspan="2">
                               <asp:DropDownList ID="ddlRepMotivo" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRepOficioRpta" runat="server" Text="Número Oficio Rpta:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtRepOficioRpta" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblRepFecOficioRpta" runat="server" Text="Fecha Oficio Rpta:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtRepFecOficioRpta" runat="server" Width="150px"></asp:TextBox>
                                &nbsp;
                                <asp:LinkButton  ID="ImgAddReprog" runat="server" OnClick="ImgAddReprog_Click" ToolTip="Registro de Préstamo" ValidationGroup="GrabarPrestamo"><i class="fa fa-plus-square verde"></i></asp:LinkButton> 
                                &nbsp;&nbsp;
                                <asp:LinkButton  ID="ImgActReprog" runat="server" OnClick="ImgActReprog_Click" ToolTip="Actualiza datos de Reprogramación"><i class="fa fa-refresh"><span>Actualizar</span></i></asp:LinkButton>                                           
                            </td>
                            <td style="text-align:left;">
                               
                            </td>

                        </tr>
                        <tr>
                            <td colspan="4">
                                <h4>Listado de Reprogramación</h4>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">                 
                                <div id="divScroll" onscroll="saveScrollPos();" style="height: 300px;width:800px; overflow: auto; overflow-x: scroll; overflow-y: scroll;">                                                 
                                    <asp:GridView ID="gdvReprogramacion"
                                             AutoGenerateColumns="False" 
                                             ShowHeaderWhenEmpty="True"                                   
                                             DataKeyNames="IDEXPEDIENTE, IDREPROGRAMACION"
                                             CellPadding="4" 
                                             PageSize ="10"
                                             allowpaging="false" 
                                             CssClass="gridStyle" 
                                             ShowFooter="True"                                                                  
                                             EmptyDataText="No Existen Datos ..."                          
                                             style="text-align: center" 
                                             Width="100%"                                             
                                             showheader="true"
                                             OnRowCommand="gdvReprogramacion_RowCommand"
                                             runat="server">
                                             <PagerStyle CssClass="pagination-ys" />
                                             <AlternatingRowStyle CssClass="alternatingrowStyle" />
                                             <Columns>

                                                <asp:TemplateField>
                                                    <HeaderTemplate> # </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                                    </ItemTemplate>
                                                    <ItemStyle  HorizontalAlign="Center" Width="5%"/>
                                                </asp:TemplateField>

                                               <asp:BoundField DataField="Oficio" HeaderText="Nro. Oficio">
                                                            <ItemStyle HorizontalAlign="center" Width="20px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="FechaOficio" HeaderText="Fecha Oficio" DataFormatString="{0:dd/MM/yyyy}">
                                                     <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField>

                                               <asp:BoundField DataField="FechaCorreo" HeaderText="Fecha Correo" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Ensayo.Nombre" HeaderText="Ensayo">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Evaluador.Nombre" HeaderText="Analista">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="DiaPlazo" HeaderText="Plazo">
                                                    <ItemStyle HorizontalAlign="center" Width="40px" />
                                               </asp:BoundField>                                               

                                               <asp:BoundField DataField="Motivo.Nombre" HeaderText="Motivo">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="OficioRpta" HeaderText="Oficio Rpta.">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField>

                                                <asp:BoundField DataField="FechaOficioRpta" HeaderText="Fecha Oficio Rpta." DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                               </asp:BoundField>

                                               <asp:TemplateField HeaderText="Actualizar" ShowHeader="false">
                                                    <ItemTemplate>                                        
                                                            <asp:LinkButton ID="btnActualizar" runat="server" ToolTip="Actualizar Archivo"  CommandName="Actualizar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ><i class="fa fa-pencil-square-o"></i></asp:LinkButton>                                         
                                                    </ItemTemplate>
                                                    <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Eliminar" ShowHeader="false">
                                                    <ItemTemplate>                                                            
                                                         <asp:LinkButton ID="btnEliinar" runat="server"  ToolTip="Eliminación de Reprogramación" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="Eliminar"><i class="fa fa-trash-o"></i></asp:LinkButton>                                                                                                                                
                                                    </ItemTemplate>
                                                    <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                                               </asp:TemplateField>

                                             </Columns>

                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle CssClass="footerStyle" Font-Bold="True" />
                                                <HeaderStyle CssClass="headerStyle" Font-Bold="True" />
                 
                                                <RowStyle CssClass="rowStyle" />
                                                <SelectedRowStyle CssClass="selectedrowStyle" Font-Bold="True" />
                                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                <SortedDescendingHeaderStyle BackColor="#15524A" />

                                          </asp:GridView>
                                </div>                                 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="btnGrabarREP" runat="server" Text="Grabar" />      
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCerrarREP" runat="server" Text="Cerrar"/>
                            </td> 
                        </tr>
                    </table>
                </div>
            </asp:Panel>

                 <!-- Modal Detalle Recepcion Muestras -->
                <asp:ModalPopupExtender ID="btnDetalle_ModalPopupExtender" runat="server" BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                PopupControlID="PnlDetalle" TargetControlID="HideDetalle" CancelControlID="btnCerrarDetalle">  </asp:ModalPopupExtender>    
                <asp:HiddenField ID="HideDetalle" runat="server" />

                <!-------------------------------------Pop Up Detalle Recepcion de Muestra ---------------------------> 
                <asp:Panel ID="PnlDetalle" runat="server" BackColor="White" CssClass="PopupOperacion2">
                <div style="width: 750px; height: 620px;">                       
                    <table style="margin: 10px 0 0 20px;">
                        <tr>
                            <td colspan="2">     
                                <h3>DETALLE REGISTRO DE RECEPCION DE MUESTRA</h3>                                                                              
                            </td>                                             
                        </tr>
                        <tr>
                            <td colspan="4">
                                   <asp:Label ID="lblTitExpediente" runat="server" Text="Expediente:" Font-Bold="true"></asp:Label>
                                   &nbsp;&nbsp;
                                    <asp:Label ID="lblRpExpediente" runat="server" Text="Expediente:"></asp:Label>
                                   <br />                                   
                           </td>  
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5><b>Datos de Custodia</b></h5>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Label ID="lblTitFecIngreso" runat="server" Text="Fecha Ingreso:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpFecIngreso" runat="server" Text="Fecha Ingreso:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitDocumento" runat="server" Text="Documento:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">                                
                               <asp:Label ID="lblTitRpDocumento" runat="server" Text="Documento:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitTipProducto" runat="server" Text="Tipo Producto:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpTipProducto" runat="server" Text="Tipo Producto:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitClaProducto" runat="server" Text="Clase Producto:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpClaProducto" runat="server" Text="Clase Producto:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitCantidad" runat="server" Text="Cantidad:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpCantidad" runat="server" Text="Cantidad:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitCondicion" runat="server" Text="Condición Amb.:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpCondicion" runat="server" Text="Condición Amb.:"></asp:Label>                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan =" 4">
                                <asp:Label ID="lblTitUbicacion" runat="server" Text="Ubicación" Font-Bold="true" Font-Underline="true"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Label ID="lblTitContramuestra" runat="server" Text="Contramuestra:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpContramuestra" runat="server" Text="Contramuestra:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitCamara" runat="server" Text="Cámara Fría:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpCamara" runat="server" Text="Cámara Fría:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Label ID="lblTitPreCamara" runat="server" Text="Pre Cámara:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpPreCamara" runat="server" Text="Pre Cámara:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitEstado" runat="server" Text="Estado:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpEstado" runat="server" Text="Estado:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5><b>Datos de ingreso de Contramuestra</b></h5>
                            </td>                                                        
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitFecIngLab" runat="server" Text="Fecha Ingreso Lab:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpFecIngLab" runat="server" Text="Fecha Ingreso Lab:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTitFecEntrega" runat="server" Text="Fecha Entrega Cliente:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpFecEntrega" runat="server" Text="Fecha Entrega Cliente:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5>Distribución de Muestras</h5>
                            </td> 
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitCER" runat="server" Text="CER:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpCER" runat="server" Text="CER:"></asp:Label>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblTitFQ" runat="server" Text="FQ:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpFQ" runat="server" Text="FQ:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitMicro" runat="server" Text="Microbiología:" Font-Bold="true"></asp:Label>
                            </td> 
                            <td colspan="3" style="text-align:left;">
                                <asp:Label ID="lblTitRpMicro" runat="server" Text="Microbiología:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5>Ingreso muestra retención</h5>
                            </td>                                                        
                        </tr>
                        <tr>
                                <td>
                                <asp:Label ID="lblTitOficio" runat="server" Text="Oficio:" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpOficio" runat="server" Text="Oficio:"></asp:Label>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTitCantidadRet" runat="server" Text="Cantidad:" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpCantidadRet" runat="server" Text="Cantidad:"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5>Retiros 01</h5>
                            </td>                                                        
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitArea" runat="server" Text="Area Solicitante:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpArea" runat="server" Text="Area Solicitante:"></asp:Label>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblTitCantidadRet1" runat="server" Text="Cantidad:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblTitRpCantidadRet1" runat="server" Text="Cantidad:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5>Retiros 02</h5>
                            </td>                                                        
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitArea2" runat="server" Text="Area Solicitante:" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpArea2" runat="server" Text="Area Solicitante:"></asp:Label>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTitCantidad2" runat="server" Text="Cantidad:" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpCantidad2" runat="server" Text="Cantidad:"></asp:Label>                                
                                </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Label ID="lblTitSaldo" runat="server" Text="Saldo:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                            <asp:Label ID="lblTitRpSaldo" runat="server" Text="Saldo:"></asp:Label>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblTitUbic" runat="server" Text="Ubicación:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                            <asp:Label ID="lblTitRpUbic" runat="server" Text="Ubicación:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitEst" runat="server" Text="Estado:"></asp:Label>
                            </td> 
                            <td colspan="3" style="text-align:left;">
                               <asp:Label ID="lblTitRpEst" runat="server" Text="Estado:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h5><b>Datos de ingreso de facturación</b></h5>
                            </td>                     
                        </tr>
                        <tr>
                                <td>
                                <asp:Label ID="lblTitFecRec" runat="server" Text="Fecha Recepción I.E:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpFecRec" runat="server" Text="Fecha Recepción I.E:"></asp:Label>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTitInfEns" runat="server" Text="Informe Ensayo:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpInfEns" runat="server" Text="Informe Ensayo:"></asp:Label>
                                </td>
                            </tr>
                        <tr>
                                <td>
                                    <asp:Label ID="lblTitConclusion" runat="server" Text="Conclusión:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpConclusion" runat="server" Text="Conclusión:"></asp:Label>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTitProforma" runat="server" Text="Proforma:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpProforma" runat="server" Text="Proforma:"></asp:Label>
                                </td>
                            </tr>
                        <tr>
                                <td>
                                    <asp:Label ID="lblTitFactura" runat="server" Text="Nro. Factura:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpFactura" runat="server" Text="Nro. Factura:"></asp:Label>
                                </td>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTitFecEntPool" runat="server" Text="Fecha Entrega Pool:"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblTitRpFecEntPool" runat="server" Text="Fecha Entrega Pool:"></asp:Label>
                                </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitMuestras" runat="server" Text="Muestras:"></asp:Label>
                            </td> 
                            <td colspan="3" style="text-align:left;">
                                <asp:Label ID="lblTitRpMuestras" runat="server" Text="Muestras:"></asp:Label>
                            </td>
                            </tr>
                        <tr>
                            <td colspan="4">
                                 <asp:Button ID="btnCerrarDetalle" runat="server" Text="Cerrar"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

           </ContentTemplate>                  

            <Triggers>                                                                                  
                <asp:PostBackTrigger ControlID="btnNuevo"/>
                <asp:PostBackTrigger ControlID="btnExportarExcel"/>
                <asp:PostBackTrigger ControlID="gdvExpediente"/>
            </Triggers>

           </asp:UpdatePanel>

    </div>


</asp:Content>

