using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrupoHidraulicoPage : ContentPage
    {

        S7Client ClientePLC_Puerta;
        byte[] ReceberDatos_GrupoHi = new byte[78];
  

        public GrupoHidraulicoPage()
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
                    if (ClientePLC_Puerta == null)
                    {
                        ClientePLC_Puerta = new S7Client();
                        int clieConected = ClientePLC_Puerta.ConnectTo("192.168.0.169", 0, 1);
                    }


                }
                catch (Exception)
                {

                    throw;
                }


                while (ClientePLC_Puerta.Connected)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        ClientePLC_Puerta.DBRead(14, 0, 78, ReceberDatos_GrupoHi);
                      
                        Temp_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 10).ToString() + " ºC";
                        Nivel_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 14).ToString() + " cm";
                        percent_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 18).ToString() + " %";
                        Nivel_minimo_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 46).ToString() + " cm";
                        Val_mini_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 38).ToString() + " cm";
                        Val_reset_aceite.Text = S7.GetRealAt(ReceberDatos_GrupoHi, 42).ToString() + " cm";
                        bool presNeumatica = S7.GetBitAt(ReceberDatos_GrupoHi, 76, 6);
                        bool presHidraulica = S7.GetBitAt(ReceberDatos_GrupoHi, 76, 7);
                        bool SeguridadArranque = S7.GetBitAt(ReceberDatos_GrupoHi, 61, 0);
                        bool AlarmaBombas = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 7);



                        bool ev1 = S7.GetBitAt(ReceberDatos_GrupoHi, 68, 1);
                        bool ev2 = S7.GetBitAt(ReceberDatos_GrupoHi, 68, 2);
                        bool ev3 = S7.GetBitAt(ReceberDatos_GrupoHi, 68, 3);
                        bool ev4 = S7.GetBitAt(ReceberDatos_GrupoHi, 68, 4);
                        bool ev5 = S7.GetBitAt(ReceberDatos_GrupoHi, 68, 5);

                        bool est1 = S7.GetBitAt(ReceberDatos_GrupoHi, 64, 1);
                        bool est2 = S7.GetBitAt(ReceberDatos_GrupoHi, 64, 2);
                        bool est3 = S7.GetBitAt(ReceberDatos_GrupoHi, 64, 3);
                        bool est4 = S7.GetBitAt(ReceberDatos_GrupoHi, 64, 4);
                        bool est5 = S7.GetBitAt(ReceberDatos_GrupoHi, 64, 5);

                        bool ter1 = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 0);
                        bool ter2 = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 1);
                        bool ter3 = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 2);
                        bool ter4 = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 3);
                        bool ter5 = S7.GetBitAt(ReceberDatos_GrupoHi, 71, 4);

                        if (!presNeumatica) Presion_Neumatica.BackgroundColor = Color.Green;
                        else Presion_Neumatica.BackgroundColor = Color.Red;

                        if (!presHidraulica) Presion_Hidraulica.BackgroundColor = Color.Green;
                        else Presion_Hidraulica.BackgroundColor = Color.Red;

                        if (SeguridadArranque) Seguridad_arranque.BackgroundColor = Color.Green;
                        else Seguridad_arranque.BackgroundColor = Color.Red;

                        if (!AlarmaBombas) Alarma_Bombas.BackgroundColor = Color.Green;
                        else Alarma_Bombas.BackgroundColor = Color.Red;

                        #region Estado Bombas

                        if (est1)
                        {
                            GRUPO1_Estado.BackgroundColor=Color.Green;
                            GRUPO1.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            GRUPO1.Source = ImageSource.FromFile("pump_off.png");
                            GRUPO1_Estado.BackgroundColor = Color.Red;
                        }
                        if (est2)
                        {
                            GRUPO2_Estado.BackgroundColor = Color.Green;
                            GRUPO2.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            GRUPO2.Source = ImageSource.FromFile("pump_off.png");
                            GRUPO2_Estado.BackgroundColor = Color.Red;
                        }
                        if (est3)
                        {
                            GRUPO3_Estado.BackgroundColor = Color.Green;
                            GRUPO3.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            GRUPO3.Source = ImageSource.FromFile("pump_off.png");
                            GRUPO3_Estado.BackgroundColor = Color.Red;
                        }

                        if (est4)
                        {
                            GRUPO4_Estado.BackgroundColor = Color.Green;
                            GRUPO4.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            GRUPO4.Source = ImageSource.FromFile("pump_off.png");
                            GRUPO4_Estado.BackgroundColor = Color.Red;
                        }
                        if (est5)
                        {
                            GRUPO5_Estado.BackgroundColor = Color.Green;
                            GRUPO5.Source = ImageSource.FromFile("pump_on1.png");
                        }
                        else
                        {
                            GRUPO5.Source = ImageSource.FromFile("pump_off.png");
                            GRUPO5_Estado.BackgroundColor = Color.Red;
                        }
                        #endregion 
                        #region TERMICO Bombas

                        if (ter1) TER1_Estado.BackgroundColor = Color.Red;
                        else TER1_Estado.BackgroundColor = Color.Green;

                        if (ter2) TER2_Estado.BackgroundColor = Color.Red;
                        else TER2_Estado.BackgroundColor = Color.Green;

                        if (ter3) TER3_Estado.BackgroundColor = Color.Red;
                        else TER3_Estado.BackgroundColor = Color.Green;

                        if (ter4) TER4_Estado.BackgroundColor = Color.Red;
                        else TER4_Estado.BackgroundColor = Color.Green;

                        if (ter5) TER5_Estado.BackgroundColor = Color.Red;
                        else TER5_Estado.BackgroundColor = Color.Green;
                        #endregion
                        #region ELectrovalvulas Bombas

                        if (ev1) EV1_Estado.BackgroundColor = Color.Green;
                        else EV1_Estado.BackgroundColor = Color.Red;

                        if (ev2) EV2_Estado.BackgroundColor = Color.Green;
                        else EV2_Estado.BackgroundColor = Color.Red;

                        if (ev3) EV3_Estado.BackgroundColor = Color.Green;
                        else EV3_Estado.BackgroundColor = Color.Red;

                        if (ev4) EV4_Estado.BackgroundColor = Color.Green;
                        else EV4_Estado.BackgroundColor = Color.Red;

                        if (ev5) EV5_Estado.BackgroundColor = Color.Green;
                        else EV5_Estado.BackgroundColor = Color.Red;
                        #endregion




                    });
                    await Task.Delay(2000);

                }

            });

        }
    }
}