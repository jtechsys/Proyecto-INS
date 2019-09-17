<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListFisicoquimica.aspx.cs" Inherits="doMain.INS.Presentation.Calidad.frmListFisicoquimica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" EnableViewState="true">
    
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
           
         <h2>&nbsp;&nbsp;&nbsp;Consulta de Fisicoquímica</h2> 

        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">
            <ContentTemplate>

                <!-- Definicion de Hide -->          
                <asp:HiddenField ID="HideArea" runat="server" />          
                <asp:HiddenField ID="HideAccion" runat="server" />
                <asp:HiddenField ID="HideIdExpediente" runat="server" />    
                <asp:HiddenField ID="HideIdLaboratorio" runat="server" />           
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
                    <td colspan="2"  style="width:350px;">                                                                                                                     
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
                                     showheader="true"               
                                     OnRowCommand="gdvExpediente_RowCommand"   
                                     OnRowDataBound ="gdvExpediente_RowDataBound"         
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

                           <asp:TemplateField HeaderText="#" ShowHeader="false" HeaderStyle-HorizontalAlign="Center">                        
                                <ItemTemplate>
                                    <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Center" Width="3%"/>
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

                           <asp:BoundField DataField="DCI" HeaderText="DCI">
                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                           </asp:BoundField>

                           <asp:BoundField DataField="Clasificacion" HeaderText="Clasificacion">
                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                           </asp:BoundField>

                           <asp:BoundField DataField="Red" HeaderText="Red">
                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                           </asp:BoundField>

                           <asp:BoundField DataField="Norma" HeaderText="Norma">
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

                           <asp:TemplateField HeaderText="Fecha Ingreso SIGEL" ShowHeader="false">
                                 <ItemTemplate> 
                                     <asp:Label ID="lblgvFecIngSIGEL" runat="server" Text='<%# Bind("FechaIngresoSIGEL","{0:d}") %>'></asp:Label> 
                                 </ItemTemplate>
                                 <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="100px"></ItemStyle>
                           </asp:TemplateField>  

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

                           <asp:TemplateField HeaderText="Registrar Evaluación" ShowHeader="false">
                               <ItemTemplate>
                                   <table>
                                       <tr>
                                           <td>
                                               <asp:Label ID="lblNroEval" runat="server" Text='<%# Bind("NroEvaluacion") %>'></asp:Label>   
                                           </td>
                                           <td>
                                               <asp:LinkButton ID="btnEvaluar" runat="server" ToolTip="Registrar Evaluación Laboratorio"  CommandName="Evaluar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ><i class="fa fa-flask" aria-hidden="true"></i></asp:LinkButton>
                                           </td>
                                       </tr>                               
                                   </table>                           
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

                 <!-- Modal Registro Fisicoquimico -->
                 <asp:ModalPopupExtender ID="btnRegistro_ModalPopupExtender" runat="server" BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                 PopupControlID="PnlRegistro" TargetControlID="HideRegistro" CancelControlID="btnCerrar">  </asp:ModalPopupExtender>    
                 <asp:HiddenField ID="HideRegistro" runat="server" /> 

                 <!-------------------------------------Pop Up Registro Fisicoquimico ---------------------------> 
                 <asp:Panel ID="PnlRegistro" runat="server" BackColor="White" CssClass="PopupOperacion2">
             <div style="width: 650px; height: 450px;">   
                 <table style="margin: 10px 0 0 20px;" class="popupmaster-table">
                     <tr>
                         <td colspan="5">     
                            <h3>REGISTRO DE FISICOQUIMICO</h3>                                                                              
                         </td>                                             
                    </tr>
                     <tr>                        
                         <td>
                              <asp:Label ID="lblDCI" runat="server" Text="DCI:"></asp:Label>
                         </td>
                         <td>
                              <asp:TextBox ID="txtDCI" runat="server" Width="200px"></asp:TextBox>
                         </td>                         
                     </tr>
                     <tr>
                         <td>
                              <asp:Label ID="lblClasificacion" runat="server" Text="Clasificación:"></asp:Label>
                         </td>
                         <td>
                              <asp:DropDownList ID="ddlClasificacion" runat="server" AutoPostBack="false"  Width="200px"></asp:DropDownList>
                         </td>      
                     </tr>
                     <tr>
                         <td>
                              <asp:Label ID="lblRed" runat="server" Text="DCI:"></asp:Label>
                         </td>
                         <td>
                              <asp:TextBox ID="txtRed" runat="server" Width="200px"></asp:TextBox>
                         </td>  
                     </tr>
                     <tr>
                          <td>
                              <asp:Label ID="lblNorma" runat="server" Text="Norma:"></asp:Label>
                         </td>
                         <td>
                              <asp:TextBox ID="txtNorma" runat="server" Width="200px"></asp:TextBox>
                         </td>  
                     </tr>
                     <tr>
                         <td>
                              <asp:Label ID="lblFecIngreso" runat="server" Text="Fecha Ingreso:"></asp:Label>
                         </td>
                         <td>
                              <asp:TextBox ID="txtFecIngreso" runat="server" Width="200px"></asp:TextBox>
                         </td> 
                     </tr>
                     <tr>
                         <td>
                              <asp:Label ID="lblFecSigel" runat="server" Text="Fecha Ingreso SIGEL:"></asp:Label>
                         </td>
                         <td>
                              <asp:TextBox ID="txtFecSigel" runat="server" Width="200px"></asp:TextBox>
                         </td> 
                     </tr>
                     <tr>
                         <td colspan="2">
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" />      
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" />      
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
                                <asp:LinkButton  ID="ImgAddReprog" runat="server" ToolTip="Registro de Préstamo" ValidationGroup="GrabarPrestamo" OnClick="ImgAddReprog_Click"><i class="fa fa-plus-square verde"></i></asp:LinkButton> 
                                &nbsp;&nbsp;
                                <asp:LinkButton  ID="ImgActReprog" runat="server" ToolTip="Actualiza datos de Reprogramación" OnClick="ImgActReprog_Click"><i class="fa fa-refresh"><span>Actualizar</span></i></asp:LinkButton>                                           
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
                            <td colspan="4" style="text-align:center;">                               
                                <asp:Button ID="btnCerrarREP" runat="server" Text="Cerrar"/>
                            </td> 
                        </tr>
                    </table>
                </div>
         </asp:Panel>

                <!-- Modal Registro Fisicoquimico -->
                <asp:ModalPopupExtender ID="btnEnsayo_ModalPopupExtender" runat="server" BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                PopupControlID="PnlLaboratorio" TargetControlID="HideLab" CancelControlID="btnCerrarLab">  </asp:ModalPopupExtender>    
                <asp:HiddenField ID="HideLab" runat="server" />   
         
                <!-------------------------------------Pop Up Registro Laboratorio ---------------------------> 
                <asp:Panel ID="PnlLaboratorio" runat="server" BackColor="White" CssClass="PopupOperacion2">
            
             <div style="width: 930px; height: 650px;">   
                   
                 <table style="margin: 10px 0 0 20px;" class="popupmaster-table">
                      <tr>
                         <td colspan="4">     
                              <h3>REGISTRO DE ENSAYO</h3>                                                                              
                          </td>                                             
                      </tr>
                      <tr>
                         <td>
                            <asp:Label ID="lblEExpediente" runat="server" Text="Expediente:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEExpediente" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblEOrden" runat="server" Text="Orden Servicio:"></asp:Label>
                        </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtEOrden" runat="server" Width="150px"></asp:TextBox>
                        </td>
                     </tr>
                      <tr>
                         <td>
                             <asp:Label ID="lblEEnsayo" runat="server" Text="Ensayo:"></asp:Label>
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlEEnsayo" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                         </td>
                          <td>
                             <asp:Label ID="lblEAnalista" runat="server" Text="Analista:"></asp:Label>
                         </td>
                         <td>
                            <asp:DropDownList ID="ddlEAnalista" runat="server" AutoPostBack="false"  Width="140px"></asp:DropDownList>
                         </td>
                     </tr>
                      <tr>
                        <td>
                            <asp:Label ID="lblEFecVenc" runat="server" Text="Fecha Vencimiento:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtEFecVenc" runat="server" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecVenc" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEFecVenc" TargetControlID="txtEFecVenc"></asp:CalendarExtender>   
                            <asp:ImageButton ID="ImgCal1"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                        </td>
                        <td>
                            <asp:Label ID="lblEFecEntrega" runat="server" Text="Fecha Entrega Máx:"></asp:Label>
                        </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtEFecEntrega" runat="server" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecEntrega" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtEFecEntrega" TargetControlID="txtEFecEntrega"></asp:CalendarExtender>   
                            <asp:ImageButton ID="ImgCal2"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>
                        </td>
                     </tr>
                      <tr>
                        <td>
                            <asp:Label ID="lblEConfirmacion" runat="server" Text="Confirmación:"></asp:Label>
                        </td>
                         <td>
                             <asp:TextBox ID="txtEConfirmacion" runat="server" Width="150px"></asp:TextBox>
                         </td>
                         <td>
                            <asp:Label ID="lblEPesquisa" runat="server" Text="Pesquisa:"></asp:Label>
                        </td>
                         <td>
                             <asp:TextBox ID="txtEPesquisa" runat="server" Width="150px"></asp:TextBox>
                         </td>
                     </tr>
                      <tr>
                         <td>
                            <asp:Label ID="lblEEnsayoHPLC" runat="server" Text="Ensayo HPLC:"></asp:Label>
                        </td>
                         <td>
                             <asp:TextBox ID="txtEEnsayoHPLC" runat="server" Width="150px"></asp:TextBox>
                         </td>
                         <td>
                            <asp:Label ID="lblECondicion" runat="server" Text="Condición:"></asp:Label>
                        </td>
                         <td>
                             <asp:TextBox ID="txtECondicion" runat="server" Width="150px"></asp:TextBox>
                         </td>
                     </tr>
                      <tr>
                         <td>
                             <asp:Label ID="lblEObservacion" runat="server" Text="Observación:"></asp:Label>
                         </td>
                         <td colspan="3" style="text-align:left;">
                              <asp:TextBox ID="txtEObservacion" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>
                              &nbsp;
                              <asp:LinkButton  ID="ImgAddEval" runat="server" OnClick="ImgAddEval_Click" ToolTip="Registro de Préstamo" ValidationGroup="GrabarPrestamo"><i class="fa fa-plus-square verde"></i></asp:LinkButton> 
                              &nbsp;&nbsp;
                              <asp:LinkButton  ID="ImgActEval" runat="server" OnClick="ImgActEval_Click" ToolTip="Actualiza datos de Reprogramación"><i class="fa fa-refresh"><span>Actualizar</span></i></asp:LinkButton>                                           
                              <br />
                         </td>
                     </tr>
                      <tr>
                         <td colspan="4">
                              <h4>Listado de Evaluaciones</h4>
                         </td>
                      </tr>
                      <tr>
                         <td colspan="4">
                             <div id="divScroll" onscroll="saveScrollPos();" style="height: 300px;width:850px; overflow: auto; overflow-x: scroll; overflow-y: scroll;">                                                 
                                 <asp:GridView ID="gdvLaboratorio"
                                             AutoGenerateColumns="False" 
                                             ShowHeaderWhenEmpty="True"                                   
                                             DataKeyNames="IDLABORATORIO, IDEXPEDIENTE"
                                             CellPadding="4" 
                                             PageSize ="10"
                                             allowpaging="false" 
                                             CssClass="gridStyle" 
                                             ShowFooter="True"                                                                  
                                             EmptyDataText="No Existen Datos ..."                          
                                             style="text-align: center" 
                                             Width="100%"                                             
                                             showheader="true"                                             
                                             OnRowCommand="gdvLaboratorio_RowCommand"    
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

                                               <asp:BoundField DataField="ordenservicio" HeaderText="Orden Servicio">
                                                    <ItemStyle HorizontalAlign="center" Width="20px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Ensayo.Nombre" HeaderText="Ensayo">
                                                    <ItemStyle HorizontalAlign="center" Width="40px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Evaluador.Nombre" HeaderText="Analista">
                                                    <ItemStyle HorizontalAlign="center" Width="40px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Venc." DataFormatString="{0:dd/MM/yyyy}">
                                                     <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField>

                                               <asp:BoundField DataField="FechaEntregaMax" HeaderText="Fecha Entrega" DataFormatString="{0:dd/MM/yyyy}">
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Confirmacion" HeaderText="Confirmación">
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Pesquisa" HeaderText="Pesquisa">
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="EnsayoHPLC" HeaderText="Ensayo HPLC">
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField>                                               

                                               <asp:BoundField DataField="Condicion" HeaderText="Condición">
                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                               </asp:BoundField> 

                                               <asp:BoundField DataField="Observaciones" HeaderText="Observación">
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
                              <asp:Button ID="btnCerrarLab" runat="server" Text="Cerrar"/>
                          </td> 
                     </tr>

                 </table>

             </div>

         </asp:Panel>
    
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportarExcel"/>
                <asp:PostBackTrigger ControlID="gdvExpediente"/>
            </Triggers>
        </asp:UpdatePanel>

     </div>       

</asp:Content>

