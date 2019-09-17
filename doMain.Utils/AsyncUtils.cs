using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace doMain.Utils
{
    /// <summary>
    /// Asincrono Utilitario
    /// </summary>
    public class AsyncUtils
    {

        /// <summary>
        /// Ejecuta un metodo de forma asincrona
        /// </summary>  
        /// <param name="method">Nombre del Metodo</param>
        public static void RunAsyncMethod(Action method)
        {
                
            Task task = new Task(method);
            task.Start();

        }


        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        public static T RunSync<T>(Func<Task<T>> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew<Task<T>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap<T>().GetAwaiter().GetResult();
        }
    }
}
