using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pagina_Inicial : ContentPage
    {
        public Pagina_Inicial()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);  //ocultar la barra de navegacion en la pagina inicial
        }

        protected async override void OnAppearing()
        {
            try
            {
                await Task.Delay(2000);
                await Navigation.PushModalAsync(new NavigationPage(new Login()));
                base.OnAppearing();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}