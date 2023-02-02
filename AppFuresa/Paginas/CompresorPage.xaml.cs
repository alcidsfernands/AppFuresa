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
    public partial class CompresorPage : ContentPage
    {

        S7Client ClientPLC_CucharaProgelta;
        byte[] ReceberDatosCompresor1 = new byte[42];
        byte[] ReceberDatosCompresor2 = new byte[42];
        bool EstadoNitro;
        public CompresorPage()
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
                        ClientPLC_CucharaProgelta.DBRead(51, 0, 42, ReceberDatosCompresor1);

                        List<string> Alarm = new List<string>();

                        Presion_C1.Text = S7.GetRealAt(ReceberDatosCompresor1, 0).ToString() + " Bar";
                        Temp_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 4).ToString() + " ºC";
                        corriente_C1_A.Text = S7.GetRealAt(ReceberDatosCompresor1, 10).ToString() + " A";
                        corriente_C1_B.Text = S7.GetRealAt(ReceberDatosCompresor1, 14).ToString() + " A";
                        corriente_C1_C.Text = S7.GetRealAt(ReceberDatosCompresor1, 18).ToString() + " A";
                        Total_Horas_Funcion_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 6).ToString() + " H";
                        Tension_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 34).ToString() + " V";
                        int estado = S7.GetIntAt(ReceberDatosCompresor1, 36);
                        Total_Horas_Carga_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 8).ToString() + " H";
                        Tiemp_vida_aceite_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 22).ToString() + " H";
                        Tiemp_vida_grasa_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 30).ToString() + " H";
                        Tiemp_vida_separad_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 24).ToString() + " H";
                        Tiemp_vida_aire_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 26).ToString() + " H";
                        Tiemp_vida_lubri_C1.Text = S7.GetIntAt(ReceberDatosCompresor1, 28).ToString() + " H";

                        string ESTADO_1 = "";


                        Presion_C2.Text = S7.GetRealAt(ReceberDatosCompresor1, 0).ToString() + " Bar";
                        Temp_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 4).ToString() + " ºC";
                        corriente_C2_A.Text = S7.GetRealAt(ReceberDatosCompresor1, 10).ToString() + " A";
                        corriente_C2_B.Text = S7.GetRealAt(ReceberDatosCompresor1, 14).ToString() + " A";
                        corriente_C2_C.Text = S7.GetRealAt(ReceberDatosCompresor1, 18).ToString() + " A";
                        Total_Horas_Funcion_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 6).ToString() + " H";
                        Tension_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 34).ToString() + " V";
                        int estado_2 = S7.GetIntAt(ReceberDatosCompresor1, 36);
                        Total_Horas_Carga_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 8).ToString() + " H";
                        Tiemp_vida_aceite_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 22).ToString() + " H";
                        Tiemp_vida_grasa_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 30).ToString() + " H";
                        Tiemp_vida_separad_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 24).ToString() + " H";
                        Tiemp_vida_aire_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 26).ToString() + " H";
                        Tiemp_vida_lubri_C2.Text = S7.GetIntAt(ReceberDatosCompresor1, 28).ToString() + " H";

                        string ESTADO_2 = "";



                        EstadoNitro = S7.GetBitAt(ReceberDatosCompresor1, 36, 0);
                        switch (estado_2)
                        {

                            case 1: ESTADO_2 = "ALARMA FALLO";break;
                            case 2: ESTADO_2 = "PARO EMERGENCIA PULSADA"; break;
                            case 3: ESTADO_2 = "RETARDO AL PARO";break;
                            case 4: ESTADO_2 = "INICIO VSD";break;
                            case 5: ESTADO_2 = "ARRANQUE ESTRELLA"; break;
                            case 6: ESTADO_2 = "ARRANQUE TRIANGULO";break;
                            case 7: ESTADO_2 = "DESCARGA AUTOMÁTICA";break;
                            case 8: ESTADO_2 = "CARGA AUTOMÁTICA";break;
                            case 9: ESTADO_2 = "EN ESPERA";break;
                            case 10: ESTADO_2 = "PARO NORMAL";break;
                               
                            


                            default: ESTADO_2 = "EN MARCHA";
                                break;
                        }
                        Estado_C1.Text = ESTADO_1;




                    });
                    await Task.Delay(2000);

                }

            });

        }
    }
}