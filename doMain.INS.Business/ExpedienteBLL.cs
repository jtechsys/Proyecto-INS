using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class ExpedienteBLL
    {
        public List<ExpedienteBE> ListExpediente(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().ListExpediente(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpedienteBE> ListExpedienteRM(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().ListExpedienteRM(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpedienteBE> ListExpedienteFisicoQuimico(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().ListExpedienteFisicoQuimico(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpedienteBE> ExportExpediente(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().ExportExpediente(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ExpedienteBE> GetExpediente(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().GetExpediente(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean UpdateAnulacionCotizacion(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateAnulacionCotizacion(objExpediente);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SaveCotizacion(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().SaveCotizacion(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateCotizacion(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateCotizacion(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateCotizacionRM(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateCotizacionRM(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateEstadoExpediente(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateEstadoExpediente(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateExpedienteCustodia(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateExpedienteCustodia(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateExpedienteContraMuestra(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateExpedienteContraMuestra(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateExpedienteFacturacion(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().UpdateExpedienteFacturacion(objExpediente);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpedienteBE> GetExpedienteRecepcionMuestraId(ExpedienteBE objExpediente)
        {
            try
            {
                return new ExpedienteDAL().GetExpedienteRecepcionMuestraId(objExpediente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
