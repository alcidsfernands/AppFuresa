using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using AppFuresa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.ComponentModel.Bindable(true)]
    public partial class cctvPage : ContentPage
    {

        #region url camera
        //const string VIDEO_URL0 = "rtsp://admin:Furesa11@192.168.0.161:554/live1.sdp";
        //const string VIDEO_URL1 = "rtsp://admin:Furesa11@192.168.0.166:554/live1.sdp";
        //const string VIDEO_URL2 = "rtsp://admin:Furesa11@192.168.0.165:554/live1.sdp";
        //const string VIDEO_URL3 = "rtsp://admin:Furesa11@192.168.0.180:554/live1.sdp";
        //const string VIDEO_URL4 = "rtsp://admin:Furesa11@192.168.0.211:554/live1.sdp";
        //const string VIDEO_URL5 = "rtsp://admin:Furesa11@192.168.0.212:554/live1.sdp";
        //const string VIDEO_URL6 = "rtsp://admin:Furesa11@192.168.0.213:554/live1.sdp";
        //const string VIDEO_URL7 = "rtsp://admin:Furesa11@192.168.0.214:554/live1.sdp";
        //const string VIDEO_URL8 = "rtsp://admin:Furesa11@192.168.0.215:554/live1.sdp";
        //const string VIDEO_URL9 = "rtsp://admin:Furesa11@192.168.0.216:554/live1.sdp";
        //const string VIDEO_URL10 = "rtsp://admin:Furesa11@192.168.0.217:554/live1.sdp";
        //const string VIDEO_URL11 = "rtsp://admin:Furesa11@192.168.0.218:554/live1.sdp";
        //const string VIDEO_URL12 = "rtsp://admin:Furesa11@192.168.0.219:554/live1.sdp";
        //const string VIDEO_URL13 = "rtsp://admin:Furesa11@192.168.0.220:554/live1.sdp";
        //const string VIDEO_URL14 = "rtsp://admin:Furesa11@192.168.1.169:554/live1.sdp";
        //const string VIDEO_URL15 = "rtsp://admin:Furesa11@192.168.1.170:554/live1.sdp";
        //const string VIDEO_URL16 = "rtsp://admin:Furesa11@192.168.1.172:554/live1.sdp";
        //const string VIDEO_URL17 = "rtsp://admin:Furesa11@192.168.1.173:554/live1.sdp";
        //const string VIDEO_URL18 = "rtsp://admin:Furesa11@192.168.1.228:554/live1.sdp";
        #endregion

        readonly LibVLC _libvlc;
        List<Servidores> DatosLista = null;
        List<listaServidores> list = new List<listaServidores>();

        public cctvPage()
        {
            InitializeComponent();
            
            Core.Initialize();
            _libvlc = new LibVLC();
            VideoView0.MediaPlayer = new MediaPlayer(_libvlc);

        }

        protected override async void OnAppearing()
        {
            

            try
            {

                DatosLista = await App.SQLiteDB.getServerAsync<Servidores>();

                if (DatosLista != null)
                {
                    foreach (var item in DatosLista)
                    {
                        

                        string name= $"Button_{item.Name}";                      
                            list.Add(new listaServidores
                        {
                            Nome = item.Name,
                            IP = item.IP,
                            Tipo = item.Tipo,

                        }) ;


                    }
                }
               ListServerView.ItemsSource = list;

                #region coment


                //VideoView0.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL0)))
                //    VideoView0.MediaPlayer.Play(media);
                //VideoView1.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL1)))
                //    VideoView1.MediaPlayer.Play(media);
                //VideoView2.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL2)))
                //    VideoView2.MediaPlayer.Play(media);
                //VideoView3.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL3)))
                //    VideoView3.MediaPlayer.Play(media);

                //VideoView4.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL4)))
                //    VideoView4.MediaPlayer.Play(media);

                //VideoView5.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL5)))
                //    VideoView5.MediaPlayer.Play(media);


                //VideoView6.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL6)))
                //    VideoView6.MediaPlayer.Play(media);
                //VideoView7.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL7)))
                //    VideoView7.MediaPlayer.Play(media);
                //VideoView8.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL8)))
                //    VideoView8.MediaPlayer.Play(media);
                //VideoView9.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL9)))
                //    VideoView9.MediaPlayer.Play(media);

                //VideoView10.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL10)))
                //    VideoView10.MediaPlayer.Play(media);

                //VideoView11.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL11)))
                //    VideoView11.MediaPlayer.Play(media);


                //VideoView12.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL12)))
                //    VideoView12.MediaPlayer.Play(media);
                //VideoView13.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL13)))
                //    VideoView13.MediaPlayer.Play(media);
                //VideoView14.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL14)))
                //    VideoView14.MediaPlayer.Play(media);
                //VideoView15.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL15)))
                //    VideoView15.MediaPlayer.Play(media);

                //VideoView16.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL16)))
                //    VideoView16.MediaPlayer.Play(media);

                //VideoView17.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL17)))
                //    VideoView17.MediaPlayer.Play(media);
                //VideoView18.MediaPlayer = new MediaPlayer(_libvlc);
                //using (var media = new Media(_libvlc, new Uri(VIDEO_URL18)))
                //    VideoView18.MediaPlayer.Play(media);
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            
            base.OnAppearing();

        }
 

  

        private async void btn_Clicked(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
           // btn.BackgroundColor = Color.Gray;
            string VIDEO_URL0 = "";
            string VIDEO_URL1 = "";
            string btn_text=btn.Text;
            foreach (var item in DatosLista)
            {
                if (item.Name == btn_text)
                {
                    VIDEO_URL0 = "rtsp://" + item.Username + ":" + item.Password + "@" + item.IP + ":554/live1.sdp";
                    VIDEO_URL1 = "http://"+item.IP;
                    using (var media = new Media(_libvlc, new Uri(VIDEO_URL0)))            
                        VideoView0.MediaPlayer.Play(media);
                      //await Browser.OpenAsync(new Uri(VIDEO_URL1));
                    // btn.BackgroundColor = Color.Gold;
                    //Device.OpenUri(new Uri("http://example.com"))
                    break;
                }  
            }
 
            

        }
    }

    public class listaServidores
    {
        public string Nome { get; set; }
        public string IP { get; set; }
        public string Tipo { get; set; }
        public Button btn_cam { get; set; }    

    }

}