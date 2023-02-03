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
        public TabPrincipal()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}