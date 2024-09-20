using System.Text;
using System.Text.Json;

namespace psi_social_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Shell.Current.GoToAsync("///HomePage").GetAwaiter();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await TryLogin_REQUEST();
        }

        private async Task TryLogin_REQUEST()
        {
            var client = new HttpClient();

            var emailValue = email.Text;
            var cpfValue = cpf.Text;

            var registerObject = new
            {
                email = emailValue,
                cpf = cpfValue
            };

            if (emailValue == "" || emailValue == null || cpfValue == "" || cpfValue == null)
            {
                await DisplayAlert("Erro", "Necessario preencher todos os campos", "OK");
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(registerObject), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("http://10.0.2.2:3001/paciente/fast", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync("///HomePage");
                }
                else
                {
                    await DisplayAlert("Erro", $"{response.Content.ToString()}", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", $"{e}", "OK");
            }
        }
    }
}
