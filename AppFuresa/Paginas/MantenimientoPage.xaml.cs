using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MantenimientoPage : ContentPage
    {
        public MantenimientoPage()
        {
            InitializeComponent();
        }
        async void Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new QrCode());
            var nada = QrCode.Instance;
            

        }
        public void buscaQR(string buscar)
        {
            string vd = buscar;
            ResultQrCode.Text = buscar;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            try
            {
                List<Mylist> list = new List<Mylist>();
                string connetionString;
                String queryCommand = "";
                SqlConnection cnn;
                connetionString = @"Data Source=tcp:192.168.0.202,1433\SQLEXPRESS;Initial Catalog=PDStorage;User ID=FURESCREEN;Password=Furesa11-;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                if (buscarbar.Text == string.Empty)
                {
                    queryCommand = "SELECT TOP (1000) [FechaRegistroAreneria],[Seccion],[Maquina],[Grupo],[EstadoMaquina],[Tarea],[Operario] FROM[PDStorage].[Mantenimiento].[RegistroAreneria]";
                }
                else
                {
                    queryCommand = $"SELECT [FechaRegistroAreneria],[Seccion],[Maquina],[Grupo],[EstadoMaquina],[Tarea],[Operario] FROM[PDStorage].[Mantenimiento].[RegistroAreneria] where [Maquina] like '{buscarbar.Text}'";

                }
                SqlCommand command = new SqlCommand(queryCommand, cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        list.Add(new Mylist
                        {
                            Data = reader["FechaRegistroAreneria"].ToString(),
                            Seccion = reader["Seccion"].ToString(),
                            Maquina = reader["Maquina"].ToString(),
                            Grupo = reader["Grupo"].ToString(),
                            Estado = reader["EstadoMaquina"].ToString(),
                            Tarea = reader["Tarea"].ToString(),
                            Operador = reader["Operario"].ToString(),
                        }
                        );
                    }
                    reader.Close();
                    MylistView.ItemsSource = list;

                }



            }
            catch (Exception ex)
            {


                throw;
            }

        }


    }
    public class Mylist
    {
        public string Data { get; set; }
        public string Seccion { get; set; }
        public string Maquina { get; set; }
        public string Grupo { get; set; }
        public string Estado { get; set; }
        public string Tarea { get; set; }
        public string Operador { get; set; }

    }

}