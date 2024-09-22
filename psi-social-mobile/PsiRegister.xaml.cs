using System.Text.Json;

namespace psi_social_mobile
{
    public partial class PsiRegister : ContentPage
    {
        public PsiRegister()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await GetHelp_REQUEST();
        }

        private async Task GetHelp_REQUEST()
        {
            var client = new HttpClient();

            try
            {

                var payload = new
                {
                    email = email.Text,
                    crp = crp.Text,
                    phone = phone.Text,
                    name = name.Text,
                    cpf = cpf.Text
                };


                var response = await client.PostAsync("http://10.0.2.2:3001/psicologo", new StringContent(JsonSerializer.Serialize(payload)));

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync("///Confirmacao");
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
