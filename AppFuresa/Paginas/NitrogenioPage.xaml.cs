using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sharp7;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NitrogenioPage : ContentPage
    {
        S7Client ClientPLC_CucharaProgelta;
        byte[] ReceberDatos_Nitrogenio = new byte[37];
        bool EstadoNitro;
        public NitrogenioPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
       
            await Task.Run(async () =>
            {
                try
                {
                    if (ClientPLC_CucharaProgelta == null)
                    {
                        ClientPLC_CucharaProgelta = new S7Client();
                        int clieConected = ClientPLC_CucharaProgelta.ConnectTo("192.168.0.127", 0, 1);
                    }


                }
                catch (Exception)
                {

                    throw;
                }


                while (ClientPLC_CucharaProgelta.Connected)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        ClientPLC_CucharaProgelta.DBRead(42, 0, 37, ReceberDatos_Nitrogenio);

                        List<string> Alarm = new List<string>();

                        caudalN.Text = (S7.GetRealAt(ReceberDatos_Nitrogenio, 0)/10.0).ToString() + " l/min";
                        presionN.Text = (S7.GetRealAt(ReceberDatos_Nitrogenio, 4)/100.0).ToString() + " Bar";
                        purezaN.Text = S7.GetRealAt(ReceberDatos_Nitrogenio, 8).ToString() + " %";


                        EstadoNitro = S7.GetBitAt(ReceberDatos_Nitrogenio, 36, 0);
                        if (EstadoNitro)
                        {
                            NitroImage.Source = "nitrogenio_on1.png";
                            
                            estadoN.Text = "En Marcha";
                          
                        }
                        else
                        {
                            estadoN.Text = "Parado";
                            NitroImage.Source = "nitrogenio_off.png";
                        }




                    });
                    await Task.Delay(2000);

                }

            });
          
        }

    }
}