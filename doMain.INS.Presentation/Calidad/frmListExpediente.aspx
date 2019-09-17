<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListExpediente.aspx.cs" Inherits="doMain.INS.Presentation.Calidad.frmListCotizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server" EnableViewState="true">

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
          
          <h2>&nbsp;&nbsp;&nbsp;Seguimiento de solicitudes de servicios de cotizaciones</h2> 
         

       <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">   

         <ContentTemplate>

          <!-- Definicion de Hide -->          
          <asp:HiddenField ID="HideArea" runat="server" />          
          <asp:HiddenField ID="HideAccion" runat="server" />
          <asp:HiddenField ID="HideIdExpediente" runat="server" />        

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
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-info"/>
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
                                             <asp:CalendarExtender ID="CalExtFecCotiz" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFilFecCotiz" TargetControlID="txtFilFecCotiz"></asp:CalendarExtender>   
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
                                            <asp:Label ID="lblFilProcede" runat="server" Text="Procede:"></asp:Label>
                                        </td>
                                        <td>
                                           <asp:DropDownList ID="ddlFilProcede" runat="server" Width="100px" AutoPostBack="false"></asp:DropDownList>   
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
                            <td colspan="2"  style="width:150px;">                                
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-info"/>
                                &nbsp;                                 
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" OnClientClick="return ShowConfirm('Está seguro que desea enviar los expedientes?', this.id);"  CssClass="btn btn-info"/>
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

               <div class="messagealert" id="alert_container">
               </div>

           </div>

          <!-- Opcion grilla de expediente -->
          <div class="div-View-grid">

               <div style="width:100%; overflow-y:auto">

                   <asp:GridView ID="gdvExpediente"
                             AutoGenerateColumns="False" 
                             ShowHeaderWhenEmpty="True"                                   
                             DataKeyNames="IDEXPEDIENTE, CodigoCotizacion, Alerta, Estado"
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

                    <asp:BoundField DataField="FechaCotizacion" HeaderText="Fecha Cotizacion" DataFormatString="{0:dd/MM/yyyy}">
                       <ItemStyle HorizontalAlign="center" Width="50px" />
                    </asp:BoundField>

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

                    <asp:BoundField DataField="DocumentoFisico" HeaderText="Doc. Físico">
                            <ItemStyle HorizontalAlign="center" Width="60px" />
                    </asp:BoundField>

                  <asp:BoundField DataField="DocumentoAnexo" HeaderText="Doc. Anexo">
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

                    <asp:BoundField DataField="Ensayo.Nombre" HeaderText="Ensayos">
                          <ItemStyle HorizontalAlign="center" Width="60%" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Evaluador.Nombre" HeaderText="Analista">
                          <ItemStyle HorizontalAlign="center" Width="200px" />
                    </asp:BoundField>

                     <asp:BoundField DataField="Evaluacion.Procede" HeaderText="Procede">
                          <ItemStyle HorizontalAlign="center" Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Situacion" HeaderText="Situación">
                            <ItemStyle HorizontalAlign="center" Width="80px" />
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

                    <asp:BoundField DataField="MotivoAnulacion" HeaderText="Motivo Anulación">
                        
                        <ItemStyle HorizontalAlign="center" Width="100px" />
                    </asp:BoundField>
                  
                    <asp:TemplateField HeaderText="Registrar Evaluación" ShowHeader="false">
                        <ItemTemplate>                            
                           <asp:LinkButton ID="btnGrvEvaluar" runat="server"  ToolTip="Evaluar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="Evaluar"><i class="fa fa-thermometer-quarter" aria-hidden="true"></i></asp:LinkButton>                                
                        </ItemTemplate>
                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Anular Cotización" ShowHeader="false">
                        <ItemTemplate>                       
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btnGrvAnular" runat="server"  ToolTip="Anular" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="Anular" OnClientClick="return ShowConfirm('Está seguro que desea anular el expediente?', this.id);"><i class="fa fa-ban" aria-hidden="true"></i></asp:LinkButton>                                  
                                </ContentTemplate>
                                <Triggers>                                                                                                          
                                    
                                </Triggers>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                                <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
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
                                 <asp:DropDownList ID="ddlTipoCliente" runat="server" AutoPostBack="false"  Width="200px" Enabled="false"></asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblCotizacion" runat="server" Text="Nro. Cotización:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtCotizacion" runat="server" Width="200px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="LblFecCotizacion" runat="server" Text="Fecha Cotización:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtFecCotizacion" runat="server" Width="200px" onKeyUp="this.value=formateafecha(this.value);" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalFecCotiza" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFecCotizacion" TargetControlID="txtFecCotizacion"></asp:CalendarExtender>   
                                 <asp:ImageButton ID="imgCalendario4"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
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
                                 <asp:Label ID="lblCorreo" runat="server" Text="Correo:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtCorreo" runat="server" Width="200px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblDocFisico" runat="server" Text="Doc. Físico:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtDocFisico" runat="server" Width="200px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td>
                                 <asp:Label ID="lblDocAnexo" runat="server" Text="Doc. Anexo:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtDocAnexo" runat="server" Width="200px"></asp:TextBox>
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

          <!-- Modal Anulacion Cotizacion -->
          <asp:ModalPopupExtender ID="btnCotAnulacion_ModalPopupExtender" runat="server"
            BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
            PopupControlID="pnlAnulacionCotizacion" 
            TargetControlID="HideAnulacion" 
            CancelControlID="btnCerrarAnulacion">
            </asp:ModalPopupExtender>    
          <asp:HiddenField ID="HideAnulacion" runat="server" />

          <!-------------------------------------Pop Up Anulacion Cotizacion ---------------------------> 
          <asp:Panel ID="pnlAnulacionCotizacion" runat="server" BackColor="White" CssClass="PopupOperacion2">
              <div style="width: 400px; height: 200px;">
                  <table style="margin: 10px 0 0 20px;">
                      <tr>
                           <td colspan="2">     
                                <h3>DATOS DE ANULACION DE COTIZACION</h3>                                           
                                <br />
                            </td>                                             
                      </tr>
                      <tr>
                          <td>
                             <asp:Label ID="lblAnulacion" runat="server" Text="Motivo Anulación:"></asp:Label>
                          </td>
                          <td>
                              <asp:TextBox ID="txtanulacion" runat="server" Width="220px" Height="60px" TextMode="MultiLine" onKeyUp="Mayuscula(this);"></asp:TextBox>
                              <br />
                              <br />
                          </td>
                      </tr>
                      <tr>
                           <td colspan="2">
                              <asp:Button ID="btnGrabarAnulacion" runat="server" Text="Grabar" OnClick="btnGrabarAnulacion_Click" />      
                              &nbsp;&nbsp;
                              <asp:Button ID="btnCerrarAnulacion" runat="server" Text="Cerrar" OnClick="btnCerrarAnulacion_Click" />      
                          </td>                             
                      </tr>

                  </table>
              </div>
          </asp:Panel>

          <!-- Modal Registro Evaluacion -->          
          <asp:ModalPopupExtender ID="btnRegEvaluacion_ModalPopupExtender" runat="server"
            BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
            PopupControlID="pnlRegistroEvaluacion" 
            TargetControlID="HideEvaluacion" 
            CancelControlID="btnCerrarAnulacion">
            </asp:ModalPopupExtender>    
          <asp:HiddenField ID="HideEvaluacion" runat="server" />
           
          <!-------------------------------------Pop Up Registro Evaluacion ---------------------------> 
          <asp:Panel ID="pnlRegistroEvaluacion" runat="server" BackColor="White" CssClass="PopupOperacion2">
               <div style="width: 450px; height: 450px;">
                    <table style="margin: 10px 0 0 20px;">
                        <tr>
                           <td colspan="2">     
                                <h3>DATOS DE REGISTRO DE EVALUACION</h3>                                           
                                <br />
                            </td>                                             
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCotEval" runat="server" Text="Nro. Cotización:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtCotEval" runat="server" Width="200px" Enabled="false"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEnsayo" runat="server" Text="Ensayo:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                               <asp:DropDownList ID="ddlEnsayo" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblEvaluador" runat="server" Text="Evaluador:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                               <asp:DropDownList ID="ddlEvaluador" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProcede" runat="server" Text="Procede:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                               <asp:DropDownList ID="ddlProcede" runat="server" AutoPostBack="true"  Width="100px"  OnSelectedIndexChanged="ddlProcede_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>                              
                            <td>
                                <asp:Label ID="lblSituacion" runat="server" Text="Situación:"></asp:Label>
                            </td>                  
                            <td style="text-align:left;">
                                 <asp:DropDownList ID="ddlSituacion" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                            </td>                                
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblObserSituacion" runat="server" Text="Observación:"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtObserSituacion" runat="server" Width="200px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:left;">
                                <asp:Label ID="lblMotivos" runat="server" Text="Motivos:" Font-Bold="true"></asp:Label>
                            </td>                                                        
                        </tr>
                        <tr>                            
                            <td style="text-align:left; width:100px;">
                                <asp:CheckBox ID="chkReactivo" runat="server"  Text=" Reactivo"/>                                                                                            
                            </td>                            
                            <td  style="text-align:left;">
                                <asp:TextBox ID="txtReactivo" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width:100px;">
                                <asp:CheckBox ID="chkEquipo" runat="server"  Text=" Equipo"/>                            
                            </td>
                            <td  style="text-align:left;">
                                <asp:TextBox ID="txtEquipo" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>                            
                            <td style="text-align:left; width:100px;">
                                 <asp:CheckBox ID="chkInstalacion" runat="server"  Text=" Instalación"/>                            
                            </td>
                            <td  style="text-align:left;">                             
                                <asp:TextBox ID="txtInstalacion" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left; width:100px;">
                                <asp:CheckBox ID="chkInsumo" runat="server"  Text=" Insumo"/>                            
                            </td>
                            <td  style="text-align:left;">
                                <asp:TextBox ID="txtInsumo" runat="server" Width="200px"></asp:TextBox>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                           <td colspan="2">
                              <asp:Button ID="btnGrabarEvaluacion" runat="server" Text="Grabar" OnClick="btnGrabarEvaluacion_Click" />      
                              &nbsp;&nbsp;
                              <asp:Button ID="btnCerrarEvaluacion" runat="server" Text="Cerrar" OnClick="btnCerrarEvaluacion_Click" />      
                          </td>                             
                      </tr>
                    </table>
                </div>
          </asp:Panel>

          </ContentTemplate>

           <Triggers>               
               <asp:PostBackTrigger ControlID="btnCerrarCoti" />
               <asp:PostBackTrigger ControlID="btnCerrarEvaluacion" />
               <asp:PostBackTrigger ControlID="btnCerrarAnulacion" />
               <asp:PostBackTrigger ControlID="btnExportarExcel" />                                              
               <asp:AsyncPostBackTrigger ControlID="btnEnviar" EventName="Click" />
               <asp:PostBackTrigger ControlID="gdvExpediente" />
           </Triggers>

        </asp:UpdatePanel>

      </div>

</asp:Content>
