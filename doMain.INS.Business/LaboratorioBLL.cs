using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class LaboratorioBLL
    {

        public List<LaboratorioBE> List(LaboratorioBE objLaboratorio)
        {
            try
            {
                return new LaboratorioDAL().List(objLaboratorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<LaboratorioBE> GetLaboratorio(LaboratorioBE objLaboratorio)
        {
            try
            {
                return new LaboratorioDAL().GetLaboratorio(objLaboratorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean SaveLaboratorio(LaboratorioBE objLaboratorio)
        {
            try
            {
                return new LaboratorioDAL().SaveLaboratorio(objLaboratorio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateLaboratorio(LaboratorioBE objLaboratorio)
        {
            try
            {
                return new LaboratorioDAL().UpdateLaboratorio(objLaboratorio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean DeleteLaboratorio(LaboratorioBE objLaboratorio)
        {
            try
            {
                return new LaboratorioDAL().DeleteLaboratorio(objLaboratorio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
