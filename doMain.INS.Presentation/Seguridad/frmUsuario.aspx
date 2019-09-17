<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUsuario.aspx.cs" Inherits="doMain.INS.Presentation.Seguridad.frmUsuario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="System.Web.UI" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function Mayuscula(e) {
            e.value = e.value.toUpperCase();
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

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >   

         <ContentTemplate>

              <div id="PnlUsuario" runat="server">
                  
                 <h3><b>&nbsp;&nbsp;&nbsp;&nbsp;CONSULTA DE USUARIOS</b></h3>                      
                 <!-- Controles Hidde -->
                 <asp:HiddenField ID="HideAccion" runat="server" />
                 <asp:HiddenField ID="HideIdUsuario" runat="server" />
                 <asp:HiddenField ID="HideTipoMsje" runat="server" /> 


                <!-- Opcion Filtros de Busquedas -->                  
                <div id="divfiltro">
                       <table>                           
                           <tr>
                              <td style="text-align:right">
                                 <asp:Label ID="lblUsuario" runat="server" Text="Apellido Paterno:"></asp:Label>
                              </td>       
                              <td>
                                <asp:TextBox ID="txtFiltro" runat="server" Width="150px" onKeyUp="Mayuscula(this);" Text=""></asp:TextBox>  
                             </td>
                             <td></td>
                             <td>
                                <asp:Button ID="btnBuscar" runat="server" Style="width: 100px;" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn btn-info"/>
                             </td>
                           </tr>  
                           <tr>
                               <td "text-align:right">
                                   <asp:Label ID="lblFArea" runat="server" Text="Area:"></asp:Label>
                               </td>
                               <td>
                                   <asp:DropDownList ID="ddlFArea" runat="server" AutoPostBack="false" Width="145px"></asp:DropDownList>
                               </td>
                               <td "text-align:right">
                                    <asp:Label ID="lblFRol" runat="server" Text="Rol:"></asp:Label>
                               </td>
                               <td>
                                   <asp:DropDownList ID="ddlFRol" runat="server" AutoPostBack="false" Width="145px"></asp:DropDownList>
                               </td>
                           </tr>                         
                       </table>
                  </div>

                <!-- Opcion de Operaciones -->
                <div id="divOperaciones">
                     <fieldset>
                         <table>
                             <tr>
                                 <td style="width:150px; text-align:right;">
                                      <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-info" />   
                                 </td>
                                 <td style="width:230px; text-align:right;">
                                     <asp:Label ID="lblContador" runat="server" ForeColor="black" Font-Bold="true"></asp:Label>
                                 </td>
                                 <td style="width:330px; text-align:right;">
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
                                <td style="width:600px; ">    
                                    &nbsp;&nbsp;
                                   <asp:Button ID="lnkbtnFirst" CssClass="GridPageFirstInactive" ToolTip="First" CommandName="First" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                   &nbsp;
                                   <asp:Button ID="lnkbtnPre" CssClass="GridPagePreviousInactive" ToolTip="Previous" CommandName="Previous" runat="server" OnCommand="GetPageIndex"></asp:Button>
                                   &nbsp;
                                   <asp:Button ID="lnkbtnNext" CssClass="GridPageNextInactive" ToolTip="Next" runat="server" CommandName="Next" OnCommand="GetPageIndex"></asp:Button>
                                   &nbsp;
                                   <asp:Button ID="lnkbtnLast" CssClass="GridPageLastInactive" ToolTip="Last" runat="server" CommandName="Last" OnCommand="GetPageIndex"></asp:Button>
                                </td>
                                 <td style="width:100px; text-align:right;">                                     
                                      <asp:ImageButton runat="server" ID="btnExportar" DescriptionUrl="Exportacion Excel." AlternateText="Exportar usuarios a excel." ToolTip="Exportar usuarios a excel." ImageUrl="~/Images/ExcelExportar.png" Width="40px" Height="40px" OnClick="btnExportar_Click"/>
                                 </td>
                             </tr>
                         </table>
                     </fieldset>
                 </div>                       

                <!--CONTROLES MODAL POPUPEXTENDER -->
                <asp:ModalPopupExtender ID="btnUsuario_ModalPopupExtender" runat="server"
                BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                PopupControlID="pnlRegistroUsuario" 
                TargetControlID="HideUsuario" 
                CancelControlID="btnCerrar">
                </asp:ModalPopupExtender>    
                <asp:HiddenField ID="HideUsuario" runat="server" />

                <!-- Ventana Mensaje Modal-->
                <asp:ModalPopupExtender ID="btnMsje_ModalPopupExtender" runat="server"
                BackgroundCssClass  ="ModalPopupBG" Enabled="True" 
                PopupControlID="PnlMsjConfirmacion" 
                TargetControlID="HideMsje" 
                CancelControlID="btnMsjCancelar">
                </asp:ModalPopupExtender>    
                <asp:HiddenField ID="HideMsje" runat="server" />          

                <!-- Opcion label de contador de registros de la grilla de expediente -->
                <div class="div-View-grid">                    
                    <div style="width:100%; overflow-y:auto">
                        <asp:GridView ID="gdvUsuario" 
                            runat="server"  
                            AutoGenerateColumns="False" 
                            ShowHeaderWhenEmpty="True"                                   
                            DataKeyNames="IDUSUARIO"
                            CellPadding="4" 
                            PageSize ="10"
                            allowpaging="True"                            
                            CssClass="gridStyle" 
                            showheader="true" 
                            ShowFooter="True"                                                                  
                            EmptyDataText="No Existen Datos ..."   
                            OnRowCommand ="gdvUsuario_RowCommand"            
                            OnPageIndexChanging  ="gdvUsuario_PageIndexChanging"                
                            style="text-align: center"                                             
                            Width="100%">
                               
                            <PagerStyle CssClass="pagination-ys" />
                            <AlternatingRowStyle CssClass="alternatingrowStyle" />
                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate> # </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Convert.ToInt32(DataBinder.Eval(Container, "DataItemIndex")) + 1%>
                                    </ItemTemplate>                                    
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="25px"/>
                                </asp:TemplateField>    

                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ApellidoPaterno" HeaderText="Apellido Paterno">
                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ApellidoMaterno" HeaderText="Apellido Materno">
                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DNI" HeaderText="DNI">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Login" HeaderText="Usuario">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Area" HeaderText="Area">
                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Celular" HeaderText="Celular">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MailTrabajo" HeaderText="Correo">
                                <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="FechaCaducidad" HeaderText="Fecha Caducidad" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="center" Width="50px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Rol.Nombre" HeaderText="Rol">
                                    <ItemStyle HorizontalAlign="center" Width="60px" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Actualizar" ShowHeader="false">
                                <ItemTemplate>                                                                     
                                    <asp:LinkButton ID="btnAnexActualizar" runat="server" ToolTip="Actualización de Usuario"  CommandName="Actualizar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" ><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                                </ItemTemplate>
                                        <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Eliminar" ShowHeader="false">
                                    <ItemTemplate>                                                                     
                                        <asp:LinkButton ID="btnAnexEliminar" runat="server"  ToolTip="Eliminación de Usuario" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="False" CommandName="Eliminar"><i class="fa fa-trash-o"></i></asp:LinkButton>
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

                <!-------------------------------------Pop Up Cargar Registro Usuario -------------------------->        
                <asp:Panel ID="pnlRegistroUsuario" runat="server" BackColor="White" CssClass="PopupOperacion2">
                <div class="popupmaster" style="width: 800px; height: 450px;">
                     <table style="margin: 10px 0 0 20px;" class="popupmaster-table">
                       <tr>
                            <td colspan="4">
                                <h3><b>REGISTRO USUARIO</b></h3>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" Width="40%" onKeyUp="Mayuscula(this);"></asp:TextBox>                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblApePaterno" runat="server" Text="Apellido Paterno:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApePaterno" runat="server" Width="40%" onKeyUp="Mayuscula(this);"></asp:TextBox>                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblApeMaterno" runat="server" Text="Apellido Materno:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApeMaterno" runat="server" Width="40%" onKeyUp="Mayuscula(this);"></asp:TextBox>                              
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="lblDNI" runat="server" Text="DNI:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDNI" runat="server" Width="40%" onKeyUp="Mayuscula(this);" MaxLength="8"></asp:TextBox>                              
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <asp:Label ID="lblArea" runat="server" Text="Area:"></asp:Label>
                            </td>
                            <td>
                               <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="false" Width="200px"></asp:DropDownList>
                               <br />                                        
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRol" runat="server" Text="Rol:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="false" Width="200px"></asp:DropDownList>
                                <br /> 
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="lblFecCaducidad" runat="server" Text="Fecha Caducidad:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecCaducidad" runat="server" Width="40%"></asp:TextBox>      
                                <asp:CalendarExtender ID="CalFecCaducidad" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtFecCaducidad" TargetControlID="txtFecCaducidad"></asp:CalendarExtender>   
                                <asp:ImageButton ID="ImgCal1"  runat="server" CausesValidation="False" ImageUrl="~/Images/calendario.png" ToolTip="Seleccionar fecha"  Visible="false"/>                        
                            </td>
                        </tr>    
                        <tr>
                            <td>
                                <asp:Label ID="lblCelular" runat="server" Text="Celular:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCelular" runat="server" Width="40%" onKeyUp="Mayuscula(this);"></asp:TextBox>                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorreo" runat="server" Text="Correo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcorreo" runat="server" Width="50%"></asp:TextBox>                              
                            </td>
                        </tr>
                                         
                        <tr>
                            <td colspan="2">                                        
                                <asp:Label ID="lblDatosArchivo" CssClass="lblTitulo" runat="server" Text="Credenciales de Acceso" Font-Bold="true" Font-Underline="true"></asp:Label>                                  
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRegUsuario" runat="server" Text="Usuario:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegUsuario" runat="server" Width="40%"></asp:TextBox>                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRegClave" runat="server" Text="Password:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegClave" runat="server" Width="40%" TextMode="Password"></asp:TextBox>    
                                <br />                          
                                <br />
                            </td>
                        </tr>
                        <tr>
                             <td colspan="4" style="text-align:center;"> 
                                  <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ValidationGroup="Grabar" OnClick="btnGrabar_Click"/>&nbsp;&nbsp;
                                  <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" />         
                            </td>                              
                        </tr>

                    </table>
                </div>
            </asp:Panel>                  

                <!-------------------------------------Pop Up Cargar Mensajes de Alertas --------------------------->    
                <asp:Panel ID="PnlMsjConfirmacion" runat="server" BackColor="White" CssClass="PopupOperacionDelete">
                <div class="Msj-title">
                    <asp:Label ID="lblMsjTitulo" runat="server" Text="Gestión Seguimiento de Expediente"></asp:Label>
                </div>
                <div class="Msj-body">
                    <asp:Label ID="lblMensaje" CssClass="texto" runat="server" Text="Está seguro que desea Eliminar el registro de Préstano"></asp:Label>
                    <asp:Button ID="btnMsjCancelar" CssClass="btn-right" runat="server" Text="No" />
                    <asp:Button ID="btnMsjAceptar" CssClass="btn-right" runat="server" Text="Si" OnClick="btnMsjAceptar_Click"/>                          
                </div>
            </asp:Panel>

           </div>

         </ContentTemplate>
         <Triggers>
              <asp:PostBackTrigger ControlID="btnExportar"/>
              <asp:PostBackTrigger ControlID="gdvUsuario"/>
         </Triggers>

     </asp:UpdatePanel>

</asp:Content>
