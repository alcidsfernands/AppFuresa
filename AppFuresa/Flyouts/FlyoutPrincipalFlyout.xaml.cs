using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFuresa.Modelos;
using SQLite;
//using AppFuresa.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using AppFuresa.Paginas;


namespace AppFuresa.Flyouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPrincipalFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutPrincipalFlyout()
        {
            InitializeComponent();

        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            object lis = listView.ItemsSource;


            if (listView.SelectedItem == flayoutIteM.GetValue(3))
            {
                try
                {
                    SQLiteAsyncConnection db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Usuario.db2"));  //busca en este ruta
                    int del = await App.SQLiteDB.BorrarUserAll();   // metodo para borrar la tabla de user
                    db.CreateTableAsync<user>().Wait();  // depues de borrar la tabla, se crea una nueva tabla con mismo nombre

                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                catch (Exception)
                {

                    throw;
                }
            }



        }
    }
}
