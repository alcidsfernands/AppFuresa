using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFuresa.Modelos;
using System.IO;

namespace AppFuresa
{
    public partial class App : Application
    {
        static SQLiteHelper db;
        public App()
        {
            InitializeComponent();

            MainPage = new Paginas.cctvPage();
        }


        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Usuario.db2"));  //si no tiene una base de datos crea una en la carpeta de la app
                }
                return db;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
