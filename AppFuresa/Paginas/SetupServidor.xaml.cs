using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFuresa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupServidor : ContentPage
    {
  
        public SetupServidor()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new New_server());
        }



        private void servidors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected async override void OnAppearing()
        {
            try
            {
               

                List<Servidores> DatosLista = await App.SQLiteDB.getServerAsync<Servidores>();
                List<listaServidores> list = new List<listaServidores>();
                if (DatosLista != null)
                {
                    foreach (var item in DatosLista)
                    {
                        list.Add(new listaServidores
                        {
                            Nome =item.Name,
                            IP=item.IP,
                            Tipo=item.Tipo
                         });    
           
                        
                    }
                }
                ListServerView.ItemsSource = list;


                base.OnAppearing();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public class listaServidores
        {
            public string Nome { get; set; }
            public string IP { get; set; }
            public string Tipo { get; set; }
           // public bool estado { get; set; }    

        }


    }
}