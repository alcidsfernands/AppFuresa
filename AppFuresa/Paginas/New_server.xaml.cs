using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFuresa.Modelos;
using SQLite;
using System.IO;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class New_server : ContentPage
    {
        public New_server()
        {
            InitializeComponent();
        }


        private async void guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var Server = new Servidores()
                {
                  Name = Nome_Servidor.Text,
                  IP=Ip.Text,
                  Username=User.Text,
                  Password=Password.Text,  
                  Tipo=Tipo.SelectedItem.ToString()    

                };

               await App.SQLiteDB.guardaServerAsync(Server);
                await DisplayAlert("Aviso", "Servidor guardado", "OK");
                Server = null;
              
                
            }
            catch (Exception)
            {
                await DisplayAlert("Aviso", "Error al guardar", "OK");
                throw;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Flyouts.FlyoutPrincipal());

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {

                int del = await App.SQLiteDB.BorrarServerAll();   // metodo para borrar la tabla de user
                SQLiteAsyncConnection db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Usuario.db2"));  //busca en este ruta
                db.CreateTableAsync<Servidores>().Wait();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}