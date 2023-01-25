using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using AppFuresa.Modelos;


namespace AppFuresa.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        Modelos.user Usuario;
        public Login()
        {
            InitializeComponent();
        }

        private async void btn_Login_Clicked(object sender, EventArgs e)
        {
            try
            {

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=tcp:192.168.0.202,1433\SQLEXPRESS;Initial Catalog=PDFurescreen;User ID=FURESCREEN;Password=Furesa11@;";
                cnn = new SqlConnection(connetionString);
                cnn.Close();    
                cnn.Open();
                String queryCommand = $"select * from dbo.USUARIO where Username like '{UserId.Text}' AND Contrasena like '{UserPassword.Text}' ";
                SqlCommand command = new SqlCommand(queryCommand, cnn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                DisplayAlert("Aviso", "Aceso permitido", "OK");

                                if (Guardar_user.IsChecked)
                                {
                                    List<user> DatosLista = await BuscarUserAsync();            //buscar datos en la tabla user

                                    if (DatosLista != null)                   //verificar se a tabla hay datos
                                    {
                                        if (DatosLista.Count > 0)
                                        {
                                            foreach (var item in DatosLista)   //verificar en cada linha
                                            {
                                                if (item.Username != UserId.Text)                //Comparar se ya tiene el miimo user
                                                {
                                                    Usuario = new user
                                                    {
                                                        Username = UserId.Text,
                                                        Password = UserPassword.Text

                                                    };
                                                    await App.SQLiteDB.guardaUserAsync(Usuario);                //guardar nuevo user 
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Usuario = new user
                                            {
                                                Username = UserId.Text,
                                                Password = UserPassword.Text

                                            };
                                            await App.SQLiteDB.guardaUserAsync(Usuario);                //guardar nuevo user 
                                        }

                                    }


                                }
                                await Navigation.PushModalAsync(new Flyouts.FlyoutPrincipal());
                                UserId.Text = string.Empty;
                                UserPassword.Text = string.Empty;
                            }
                            else
                                await DisplayAlert("ERROR", "Usuario o contraseña no valida", "OK");

                        }
                    }
                    else
                        await DisplayAlert("ERROR", "Usuario o contraseña no valida", "OK");
                    reader.Close();

                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                List<user> DatosLista = await BuscarUserAsync();

                if (DatosLista != null)
                {
                    foreach (var item in DatosLista)
                    {
                        UserId.Text = item.Username;
                        UserPassword.Text = item.Password;
                    }
                }


                base.OnAppearing();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<Modelos.user>> BuscarUserAsync()
        {
            List<Modelos.user> DatosLista = await App.SQLiteDB.getUserAsync<Modelos.user>();
            return DatosLista;
        }


    }
}