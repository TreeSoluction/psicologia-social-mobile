using psi_social_mobile.Domain;
using System.Text.Json;

namespace psi_social_mobile
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnHelperButtonClicked(object sender, EventArgs e)
        {
            await GetHelp_REQUEST();
        }
        private async void OnNumeroButtonClicked(object sender, EventArgs e)
        {
            var phoneNumber = numero.Text; // Replace with your dynamic number if needed
            var phoneUrl = $"tel:{phoneNumber}";

            try
            {
                await Launcher.OpenAsync(phoneUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to open dialer: {ex.Message}");
            }
        }

        private async Task GetHelp_REQUEST()
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync("http://10.0.2.2:3001/consulta");
                if (response.IsSuccessStatusCode)
                {
                    var consultaResponse = JsonSerializer.Deserialize<ConsultaResponse>(await response.Content.ReadAsStringAsync());

                    if (consultaResponse != null)
                    {
                        nome.Text = consultaResponse.nome;
                        numero.Text = consultaResponse.telefone;
                        frase.Text = consultaResponse.crp;
                    }
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", $"{e}", "OK");
            }
        }
    }
}
