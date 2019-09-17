using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using doMain.INS.Entity;
using doMain.INS.Data;

namespace doMain.INS.Business
{
    public class EvaluacionBLL
    {
        public List<EvaluacionBE> GetEvaluacionbyIdExpediente(EvaluacionBE objEvaluacion)
        {
            try
            {
                return new EvaluacionDAL().GetEvaluacionbyIdExpediente(objEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Boolean SaveEvaluacion(EvaluacionBE objEvaluacion)
        {
            try
            {
                return new EvaluacionDAL().SaveEvaluacion(objEvaluacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
