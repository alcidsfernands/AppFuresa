using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sharp7;
using Xamarin.Essentials;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PuertasPage : ContentPage
    {
        const string URL_CamPuertaEntrada = "rtsp://admin:Furesa11@192.168.0.166:554/live1.sdp";
        const string URL_CamPuertaCascula = "rtsp://admin:Furesa11@192.168.0.165:554/live1.sdp";
        readonly LibVLC _libvlc;
        S7Client ClientPLC_PuertaEntrada;
        S7Client ClientPLC_PuertaBAscula;
        int clieConected;
        byte[] Receber_Puerta_Entrada = new byte[1];
        byte[] Enviar_Puerta_Entrada = new byte[1];
        byte[] EnviarPuertaBascula = new byte[1];
   
        

        public PuertasPage()
        {
            InitializeComponent();
            Core.Initialize();
            _libvlc = new LibVLC();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            CamPuertaPrincipal.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(URL_CamPuertaEntrada)))
                CamPuertaPrincipal.MediaPlayer.Play(media);

            CamPuertaBascula.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(URL_CamPuertaCascula)))
                CamPuertaBascula.MediaPlayer.Play(media);


            await Task.Run(async () =>
            {
                try
                {
                    if (ClientPLC_PuertaEntrada == null)
                    {
                        ClientPLC_PuertaEntrada = new S7Client();
                        ClientPLC_PuertaEntrada.Disconnect();
                        clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                        
                    }

                     

                }
                catch (Exception)
                {

                    throw;
                }


                while (ClientPLC_PuertaEntrada.Connected)
                {
                    btn_conectarEntrada.BackgroundColor = Color.Red;
                    btn_conectarEntrada.Text = "DESCONECTAR";

                   
                    MainThread.BeginInvokeOnMainThread(() =>     // Hilo principal ejecutando todo rato la peticion de datos
                    {
                        ClientPLC_PuertaEntrada.DBRead(40, 0, 1, Receber_Puerta_Entrada);
                        bool Aberta = S7.GetBitAt(Receber_Puerta_Entrada, 0, 0);
                        bool Cerrada = S7.GetBitAt(Receber_Puerta_Entrada, 0, 1);
                        bool abriendose = S7.GetBitAt(Receber_Puerta_Entrada, 0, 2);
                        bool cerrandose = S7.GetBitAt(Receber_Puerta_Entrada, 0, 3);
                        bool SensorOptico = S7.GetBitAt(Receber_Puerta_Entrada, 0, 6);
                        if (Aberta)
                        {
                            Led_PuertaEntradaAbierta.BackgroundColor = Color.Gold;
                        }
                        else Led_PuertaEntradaAbierta.BackgroundColor = Color.Gray;
                        if (Cerrada)
                        {
                            Led_PuertaCerrada.BackgroundColor = Color.Gold;
                        }
                        else Led_PuertaCerrada.BackgroundColor = Color.Gray;
                        if (abriendose)
                        {
                            Led_PuertaEntradaAbriendose.BackgroundColor = Color.Gold;
                        }
                        else Led_PuertaEntradaAbriendose.BackgroundColor = Color.Gray;
                        if (cerrandose)
                        {
                            Led_PuertaCerrandose.BackgroundColor = Color.Gold;
                        }
                        else Led_PuertaCerrandose.BackgroundColor = Color.Gray;
                        if (SensorOptico)
                        {
                            Led_PuertaSensor.BackgroundColor = Color.Gold;
                        }
                        else Led_PuertaSensor.BackgroundColor = Color.Gray;


                    });
                    await Task.Delay(2000);

                }

            });


        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        
        private void btn_CerrarPuertaEntrada_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ClientPLC_PuertaEntrada == null)
                {
                    ClientPLC_PuertaEntrada = new S7Client();
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                else
                {
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                if (ClientPLC_PuertaEntrada.Connected)
                {
                    btn_conectarEntrada.BackgroundColor = Color.Red;
                    btn_conectarEntrada.Text = "DESCONECTAR";
                    S7.SetBitAt(ref Enviar_Puerta_Entrada, 0, 0, false);
                    S7.SetBitAt(ref Enviar_Puerta_Entrada, 0, 1, true);
                    ClientPLC_PuertaEntrada.DBWrite(8, 0, 1, Enviar_Puerta_Entrada);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void btn_AbrirPuertaEntrada_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ClientPLC_PuertaEntrada == null)
                {
                    ClientPLC_PuertaEntrada = new S7Client();
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                else
                {
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                if (ClientPLC_PuertaEntrada.Connected)
                {
                    S7.SetBitAt(ref Enviar_Puerta_Entrada, 0, 0, true);
                    S7.SetBitAt(ref Enviar_Puerta_Entrada, 0, 1, false);
                    ClientPLC_PuertaEntrada.DBWrite(8, 0, 1, Enviar_Puerta_Entrada);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void btn_conectarEntrada_Clicked(object sender, EventArgs e)
        {
            if (btn_conectarEntrada.Text== "CONECTAR")
            {
                if (ClientPLC_PuertaEntrada == null)
                {
                    ClientPLC_PuertaEntrada = new S7Client();
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                else
                {
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.169", 0, 1);
                }
                if (ClientPLC_PuertaEntrada.Connected == false)
                {
                    btn_conectarEntrada.BackgroundColor = Color.Green;
                    btn_conectarEntrada.Text = "CONECTAR";
                    DisplayAlert("ERRO DE CONECCION", "Certifique que manten una coneccion WI-FI o VPN con la  Fabrica ", "OK");
                }
                else
                {
                    btn_conectarEntrada.BackgroundColor = Color.Red;
                    btn_conectarEntrada.Text = "DESCONECTAR";

                }

            }
            else if (btn_conectarEntrada.Text == "DESCONECTAR")
            {
  
                    ClientPLC_PuertaEntrada.Disconnect();
                    btn_conectarEntrada.BackgroundColor = Color.Green;
                    btn_conectarEntrada.Text = "CONECTAR";
                if (ClientPLC_PuertaEntrada.Connected) DisplayAlert("ERRO DE DESCONECCION", "Certifique que manten una coneccion WI-FI o VPN con la  Fabrica ", "OK");

            }
        }
 
        private void btn_conectarBascula_Clicked(object sender, EventArgs e)
        {
            if (btn_conectarBascula.Text == "CONECTAR")
            {
                if (ClientPLC_PuertaBAscula == null)
                {
                    ClientPLC_PuertaBAscula = new S7Client();
                    ClientPLC_PuertaBAscula.Disconnect();
                    clieConected = ClientPLC_PuertaBAscula.ConnectTo("192.168.0.142", 0, 1);
                }
                else
                {
                    ClientPLC_PuertaBAscula.Disconnect();
                    clieConected = ClientPLC_PuertaBAscula.ConnectTo("192.168.0.142", 0, 1);
                }
                if (ClientPLC_PuertaBAscula.Connected == false)
                {
                    btn_conectarBascula.BackgroundColor = Color.Green;
                    btn_conectarBascula.Text = "CONECTAR";
                    DisplayAlert("ERRO DE CONECCION", "Certifique que manten una coneccion WI-FI o VPN con la  Fabrica ", "OK");
                }
                else
                {
                    btn_conectarBascula.BackgroundColor = Color.Red;
                    btn_conectarBascula.Text = "DESCONECTAR";

                }

            }
            else if (btn_conectarBascula.Text == "DESCONECTAR")
                {
                    
                        ClientPLC_PuertaBAscula.Disconnect();
                        btn_conectarBascula.BackgroundColor = Color.Green;
                        btn_conectarBascula.Text = "CONECTAR";
                    if (ClientPLC_PuertaBAscula.Connected) DisplayAlert("ERRO DE DESCONECCION", "Certifique que manten una coneccion WI-FI o VPN con la  Fabrica ", "OK");

                }

        }
        private void btn_AbrirPuertaBascula_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ClientPLC_PuertaEntrada == null)
                {
                    ClientPLC_PuertaEntrada = new S7Client();
                    ClientPLC_PuertaEntrada.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.142", 0, 1);
                }
                else
                {
                    ClientPLC_PuertaBAscula.Disconnect();
                    clieConected = ClientPLC_PuertaEntrada.ConnectTo("192.168.0.142", 0, 1);
                }
                if (ClientPLC_PuertaBAscula.Connected)
                {
                    S7.SetBitAt(ref EnviarPuertaBascula, 0, 1, true);
                    ClientPLC_PuertaEntrada.DBWrite(4, 0, 1, EnviarPuertaBascula);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}