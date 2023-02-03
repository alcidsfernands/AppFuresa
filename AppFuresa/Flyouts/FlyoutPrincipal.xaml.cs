using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Flyouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPrincipal : FlyoutPage
    {
        public FlyoutPrincipal()
        {
            InitializeComponent();
         
                flyoutPage.listView.ItemSelected += OnItemSelected;


            }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                var item = e.SelectedItem as FlyoutPrincipalFlyoutMenuItem;
                if (item != null)
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    flyoutPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
}