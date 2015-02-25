using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VK.App.Windows.GUI;
using VK.API;
using VK.Logic;

namespace VK.App.Windows
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            API.App.SetPlatform(new Platform());

            AppLogic.Startup();

            Application.ApplicationExit += (o, args) => AppLogic.Exit();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
