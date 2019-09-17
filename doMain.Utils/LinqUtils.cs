using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace doMain.Utils
{
    public sealed class LinqUtils
    {
        public static string ConvertLambdaToString(List<LambdaExpression> expressions)
        {
            return string.Join("|", expressions.Select(x => x.Body.ToString()).ToArray());           
        }

        public static object[] ConvertAnonymousType(object value)
        {
            // TODO: Validation that it's really an anonymous type
            Type type = value.GetType();
            var genericType = type.GetGenericTypeDefinition();
            var parameterTypes = genericType.GetConstructors()[0]
                                            .GetParameters()
                                            .Select(p => p.ParameterType)
                                            .ToList();
            var propertyNames = genericType.GetProperties()
                                           .OrderBy(p => parameterTypes.IndexOf(p.PropertyType))
                                           .Select(p => p.Name);

            return propertyNames.Select(name => type.GetProperty(name)
                                                    .GetValue(value, null))
                                .ToArray();

        }


        public static void CompareList(List<Guid> source ,List<Guid> updates,out List<Guid> news , out List<Guid> deletes)
        {
            //lista de los nuevos ids
            var documentoIdsExistentes = new HashSet<Guid>(source.Select(s => s));
            ////1 2 3 
            var documentoIdsNuevos = new HashSet<Guid>(updates.Select(s => s));
            //ids q no existen en la lista de la db.
            var documentosNuevos = updates.Where(m => !documentoIdsExistentes.Contains(m));

            news = updates.Where(m => !documentoIdsExistentes.Contains(m)).ToList();
            deletes = source.Where(m => !documentoIdsNuevos.Contains(m)).ToList();
                  
        }

    }
}
