using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;


namespace doMain.INS.Business
{
    public class UsuarioBLL
    {

        public UsuarioBE ValidateAccess(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().ValidateAccess(objUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioBE> GetUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().GetUsuario(objUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioBE> ListUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().ListUsuario(objUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioBE> ExportUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().ExportUsuario(objUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SaveUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().SaveUsuario(objUsuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().UpdateUsuario(objUsuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean DeleteUsuario(UsuarioBE objUsuario)
        {
            try
            {
                return new UsuarioDAL().DeleteUsuario(objUsuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
