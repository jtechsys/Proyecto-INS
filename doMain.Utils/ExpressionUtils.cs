using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public class ExpressionUtils
    {

        public  static Expression GetIgnoreExpression<TEntityType>(PropertyInfo prop)
        {
            ParameterExpression arg = Expression.Parameter(typeof(TEntityType), "x");
            MemberExpression property = Expression.Property(arg, prop.Name);
            var exp = Expression.Lambda(property, new ParameterExpression[] { arg });
            return exp;
        }

    }
}
