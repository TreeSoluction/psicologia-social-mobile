using psi_social_mobile.Domain;
using System.Text;
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
                    telefone = phone.Text,
                    nome = name.Text,
                    cpf = cpf.Text
                };

                var json = JsonSerializer.Serialize(payload);
                var response = await client.PostAsync("http://10.0.2.2:3001/psicologo", new StringContent(json, Encoding.UTF8, "application/json"));
                var responseBody = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync("///Confirmacao");
                }
                else
                {
                    var errorRequest = JsonSerializer.Deserialize<ErrorMessage>(await response.Content.ReadAsStringAsync());
                    var errors = new List<string>();

                    foreach (var error in errorRequest.message)
                    {
                        if (error.StartsWith("crp"))
                        {
                            errors.Add(PsiRegisterErros.CRP_ERROR);
                        }
                        if (error.StartsWith("cpf"))
                        {
                            errors.Add(PsiRegisterErros.CPF_ERROR);
                        }
                        if (error.StartsWith("nome"))
                        {
                            errors.Add(PsiRegisterErros.NOME_ERROR);
                        }
                        if (error.StartsWith("email"))
                        {
                            errors.Add(PsiRegisterErros.EMAIL_ERROR);
                        }
                    }

                    var errorMessage = String.Join("\n", errors);

                    await DisplayAlert("Erro", $"{errorMessage}", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", $"{e}", "OK");
            }
        }
    }
}
