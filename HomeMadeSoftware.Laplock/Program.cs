using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeMadeSoftware.Laplock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string mutexName = Assembly.GetExecutingAssembly().GetName().Name;
            Mutex singleInstanceMutex = null;

            // Try and open a mutex already registered by a previous instance
            try
            {
                singleInstanceMutex = Mutex.OpenExisting(mutexName);
            }
            catch { }
            
            // another instance already running 
            if (singleInstanceMutex != null)
            {
                singleInstanceMutex.Close();
                return;
            }

            // Register a new mutex
            singleInstanceMutex = new Mutex(false, mutexName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
