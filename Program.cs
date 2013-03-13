using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LightFX;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Shell;
using System.IO;

namespace AlienFXWinTheme
{
    class Program :  ISingleInstanceApp
    {

        private static string Unique = "2B2551F8-0847-4DD3-B942-F800DEF8B841";
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(){
            //if (SingleInstance<Program>.InitializeAsFirstInstance(Unique))
            //{
            

            try
            {
                Directory.SetCurrentDirectory(Application.StartupPath);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            //}
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // handle command line arguments of second instance
            // ...

            return true;
        }
    }
}
