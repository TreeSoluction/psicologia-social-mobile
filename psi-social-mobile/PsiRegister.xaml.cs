using psi_social_mobile.Domain;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace psi_social_mobile
{
    public partial class PsiRegister : ContentPage
    {
        public PsiRegister()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public bool ValidatedForm()
        {
            string name = NameEntry.Text?.Trim();
            string crp = CrpEntry.Text?.Trim();
            string email = EmailEntry.Text?.Trim();
            string phone = PhoneEntry.Text?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                DisplayAlert("Erro", "O nome completo é obrigatório.", "OK");
                return false;
            }

            if (!string.IsNullOrEmpty(crp) && !Regex.IsMatch(crp, @"^\d+$") && crp.Length == 7)
            {
                DisplayAlert("Erro", "O CRP deve conter apenas números e deve ter o tamanho de 7", "OK");
                return false;;
            }

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                DisplayAlert("Erro", "Insira um endereço de e-mail válido.", "OK");
                return false;;
            }

            if (string.IsNullOrEmpty(phone))
            {
                DisplayAlert("Erro", "O telefone deve seguir o formato (XX) XXXXX-XXXX.", "OK");
                return false;;
            }

            return true;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = new string(e.NewTextValue.Where(char.IsDigit).ToArray());
            var formattedInput = input;
            if (input.Length > 11)
            {
                 formattedInput = input.Substring(0, 11);
            }
            if (formattedInput != e.NewTextValue)
                ((Entry)sender).Text = formattedInput;
        }

        private async void Cadastrar_OnClicked(object? sender, EventArgs e)
        {
            if (ValidatedForm())
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://10.0.2.2:3001/psicologo";

                    var userData = new
                    {
                        nome = NameEntry.Text?.Trim(),
                        crp = CrpEntry.Text?.Trim(),
                        email = EmailEntry.Text?.Trim(),
                        telefone = PhoneEntry.Text?.Trim()
                    };

                    string json = JsonSerializer.Serialize(userData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");
                            await Navigation.PushAsync(new PosRegister());
                        }
                        else
                        {
                            await DisplayAlert("Erro", "Falha ao cadastrar. Tente novamente.", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Erro", $"Erro ao se conectar: {ex.Message}", "OK");
                    }
                }
            }
        }

        private void Button_OnClicked(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });
        }

        protected override bool OnBackButtonPressed()
        {
            Task.Run(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });

            return true;
        }
    }
}
