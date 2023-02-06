using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BombasPage : ContentPage
    {
        public S7Client ClientPLC;
        bool pageIsActive = false;
        int clieConected;
        byte[] ReceberDatos_Bombas = new byte[28];
        public BombasPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            await Task.Run(async () =>
            {
                try
                {
                    if (ClientPLC == null)
                    {
                        ClientPLC = new S7Client();
                        clieConected = ClientPLC.ConnectTo("192.168.0.129", 0, 1);
                    }


                }
                catch (Exception)
                {

                    throw;
                }


                while (ClientPLC.Connected)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        ClientPLC.DBRead(6, 0, 28, ReceberDatos_Bombas);

                        List<string> Alarm = new List<string>();

                        tempB2.Text = S7.GetRealAt(ReceberDatos_Bombas, 22).ToString()+" ºC";
                        horasB1.Text = S7.GetDWordAt(ReceberDatos_Bombas, 8).ToString() + " H";
                        tempB1.Text = tempB2.Text;
                        horasB2.Text = S7.GetDWordAt(ReceberDatos_Bombas, 16).ToString() + " H";


                        bool Seleccion_B1 = S7.GetBitAt(ReceberDatos_Bombas, 0, 0);
                        bool Seleccion_B2 = S7.GetBitAt(ReceberDatos_Bombas, 0, 1);
                        bool PresenciaTension = S7.GetBitAt(ReceberDatos_Bombas, 0, 2);
                        bool FlujostatoAgua = S7.GetBitAt(ReceberDatos_Bombas, 0, 3);
                        bool Proteccion_B1 = S7.GetBitAt(ReceberDatos_Bombas, 0, 4);
                        bool Proteccion_B2 = S7.GetBitAt(ReceberDatos_Bombas, 0, 5);
                        bool En_marcha_B1 = S7.GetBitAt(ReceberDatos_Bombas, 0, 6);
                        bool En_marcha_B2 = S7.GetBitAt(ReceberDatos_Bombas, 0, 7);
                        bool alarma_General_B1 = S7.GetBitAt(ReceberDatos_Bombas, 1, 0);
                        bool alarma_General_B2 = S7.GetBitAt(ReceberDatos_Bombas, 1, 1);
                        bool alarma_PresenciaTension = S7.GetBitAt(ReceberDatos_Bombas, 1, 2);
                        bool alarma_PresenciaAgua = S7.GetBitAt(ReceberDatos_Bombas, 1, 3);
                        bool alarma_Temperatura = S7.GetBitAt(ReceberDatos_Bombas, 1, 4);
                        bool RoturaHilo_Analogica = S7.GetBitAt(ReceberDatos_Bombas, 1, 5);
                        bool En_Estrella_B1 = S7.GetBitAt(ReceberDatos_Bombas, 1, 6);
                        bool En_Estrella_B2 = S7.GetBitAt(ReceberDatos_Bombas, 1, 7);



                        if (FlujostatoAgua)
                        {
                            FluxoB1.BackgroundColor = Color.Green;
                            FluxoB2.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            FluxoB1.BackgroundColor = Color.Red;
                            FluxoB2.BackgroundColor = Color.Red;
                        }
                        if (PresenciaTension)
                        {
                            tensionB1.BackgroundColor = Color.Green;
                            tensionB2.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            tensionB1.BackgroundColor = Color.Red;
                            tensionB2.BackgroundColor = Color.Green;
                        }

                        if (Proteccion_B1)
                        {
                            protecionB1.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            protecionB1.BackgroundColor = Color.Red;
                            Alarm.Add("Bomba1 sin proteción\n");
                        }
                        if (En_marcha_B1)
                        {
                            EstadoB1.Text = "En Marcha";
                            b1.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            b1.Source = ImageSource.FromFile("pump_off.png");
                            EstadoB1.Text = "Parado";
                        }

                        if (alarma_General_B2)
                        {
                            protecionB2.BackgroundColor = Color.Green;

                        }
                        else
                        {
                            protecionB2.BackgroundColor = Color.Red;
                            Alarm.Add("Bomba2 sin proteción\n");
                        }
                        if (En_marcha_B2)
                        {
                            EstadoB2.Text = "En Marcha";
                            // Image im = new Image();
                            b2.Source = ImageSource.FromFile("pump_on1.png");

                        }
                        else
                        {
                            b2.Source = ImageSource.FromFile("pump_off.png");
                            EstadoB2.Text = "Parado";
                        }

                        if (!alarma_PresenciaTension)
                        {
                            Alarm.Add("Bomba 1 y 2 sin Tension \n");

                        }
                        if (alarma_PresenciaAgua)
                        {
                            Alarm.Add("Tubería sin agua\n");

                        }

                        if (alarma_Temperatura)
                        {
                            Alarm.Add("Temperatura fuera de rango\n");

                        }
                        if (RoturaHilo_Analogica) Alarm.Add("Rotura do Hilo\n");
                        if (En_Estrella_B1) Alarm.Add("Bomba 1 en estrella\n");
                        if (En_Estrella_B2) Alarm.Add("Bomba 2 en estrellao\n");
                        if (alarma_General_B1) Alarm.Add("Alarma General BOMBA1\n");
                        if (alarma_General_B2) Alarm.Add("Alarma General BOMBA2\n");
                        if (alarma_Temperatura) Alarm.Add("Temperatura de la Agua está Alta\n");
                        if (alarma_PresenciaAgua) Alarm.Add("Falta de Agua\n");
                      //  if (alarma_PresenciaTension) Alarm.Add("Bombas Sin Tension\n");
                        



                        MylistView.ItemsSource = Alarm;






                    });
                    await Task.Delay(2000);

                }

            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
        }
    }
}