using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class ReprogramacionBLL
    {

        public Boolean SaveReprogramacion(ReprogramacionBE objReprogramacion)
        {
            try
            {
                return new ReprogramacionDAL().SaveReprogramacion(objReprogramacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean UpdateReprogramacion(ReprogramacionBE objReprogramacion)
        {
            try
            {
                return new ReprogramacionDAL().UpdateReprogramacion(objReprogramacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReprogramacionBE> GetReprogramacion(ReprogramacionBE objReprogramacion)
        {
            try
            {
                return new ReprogramacionDAL().GetReprogramacion(objReprogramacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean DeleteReprogramacion(ReprogramacionBE objReprogramacion)
        {
            try
            {
                return new ReprogramacionDAL().DeleteReprogramacion(objReprogramacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReprogramacionBE> GetReprogramacionbyExpediente(ReprogramacionBE objReprogramacion)
        {
            try
            {
                return new ReprogramacionDAL().GetReprogramacionbyExpediente(objReprogramacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
