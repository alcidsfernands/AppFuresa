using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Flyouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPrincipalFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutPrincipalFlyout()
        {
            InitializeComponent();

            BindingContext = new FlyoutPrincipalFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutPrincipalFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutPrincipalFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutPrincipalFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutPrincipalFlyoutMenuItem>(new[]
                {
                    new FlyoutPrincipalFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new FlyoutPrincipalFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new FlyoutPrincipalFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new FlyoutPrincipalFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new FlyoutPrincipalFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}