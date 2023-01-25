using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sharp7;

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
        byte[] Enviar_Puerta_Entrada2 = new byte[1];

        public PuertasPage()
        {
            InitializeComponent();
            Core.Initialize();
            _libvlc = new LibVLC();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CamPuertaPrincipal.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(URL_CamPuertaEntrada)))
                CamPuertaPrincipal.MediaPlayer.Play(media);

            CamPuertaBascula.MediaPlayer = new MediaPlayer(_libvlc);
            using (var media = new Media(_libvlc, new Uri(URL_CamPuertaCascula)))
                CamPuertaBascula.MediaPlayer.Play(media);


        }

        private void btn_AbrirPuertaBascula_Clicked(object sender, EventArgs e)
        {
            
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
    }
}