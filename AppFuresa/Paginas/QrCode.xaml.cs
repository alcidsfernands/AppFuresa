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
    public partial class QrCode : ContentPage
    {
        public string valor;
        private static QrCode instance = new QrCode();
        public QrCode()
        {
            InitializeComponent();
            zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() =>
            {
                lblResult.Text = result.Text;
                 valor = result.Text; 
                MantenimientoPage md = new MantenimientoPage();
                md.buscaQR(result.Text);

            });
        }

        public static QrCode Instance
        {
            get
            {
                if (instance == null)   
                    instance = new QrCode();


                return instance;
            }

            
        }    
        public string Result() 
        {
            return lblResult.Text;
        }  

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }
        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}