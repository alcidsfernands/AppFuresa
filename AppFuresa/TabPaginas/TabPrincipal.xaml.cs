using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.TabPaginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPrincipal : TabbedPage
    {
      // private Paginas.Login log=new Paginas.Login();
        public TabPrincipal()
        {
            try
            {
                InitializeComponent();
                string test = Paginas.Pagina_Inicial.LOGIN.nivelUser;
                switch (test)
                {
                    case "1":scada.IsEnabled = true; scada.IsVisible = true;
                        cctv.IsEnabled = true;cctv.IsVisible = true;
                        mantenimiento.IsEnabled = true;mantenimiento.IsVisible = true;
                        puerta.IsEnabled = true; puerta.IsVisible = true;
                        break;

                    case "2":
                        scada.IsEnabled = true; scada.IsVisible = true;
                        cctv.IsEnabled = false; cctv.IsVisible = false;
                        mantenimiento.IsEnabled = true; mantenimiento.IsVisible = true;
                        puerta.IsEnabled = true; puerta.IsVisible = true;
                        break;

                    default:

                        scada.IsEnabled = false; scada.IsVisible = false;
                        cctv.IsEnabled = false; cctv.IsVisible = false;
                        mantenimiento.IsEnabled = false; mantenimiento.IsVisible = false;
                        puerta.IsEnabled = true; puerta.IsVisible = true;
                        
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}