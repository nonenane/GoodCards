using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Project;

namespace FormationOfRevaluation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Project.FillSettings(args);

                Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                //Заполнение глобальных настроек
                //Осн. коннект
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                Config.hCntMainKassRealizz = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
                Config.hCntSecondKassRealizz = new Procedures(ConnectionSettings.GetServer("4"), ConnectionSettings.GetDatabase("4"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                //режим пользователя
                Config.StatusCode = Nwuram.Framework.Settings.User.UserSettings.User.StatusCode;

                //Logging.StartFirstLevel(1);
                //Logging.Comment("Вход в программу");
                //Logging.StopFirstLevel();

                Application.Run(new frmView());

                //Logging.StartFirstLevel(2);
                //Logging.Comment("Пользователь закрыл программу");
                //Logging.StopFirstLevel();                
            }            
        }

    }
}
