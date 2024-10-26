using System.Text.Json;
using System.Text.Json.Serialization;

namespace psi_social_mobile;

public partial class DisponibilityChange : ContentPage
{
    public DisponibilityChange()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            await Shell.Current.GoToAsync("///MainPage");
        });
    }

    private async void Button_Deactive(object? sender, EventArgs e)
    {
        var client = new HttpClient();

        string email = EmailEntry.Text?.Trim();

        string url = $"http://10.0.2.2:3001/psicologo/{email}/deactive";

        var result = await client.PutAsync(url, null);
        if (result.IsSuccessStatusCode)
        {
            DisplayAlert("Operacao realizada", "Disponibilidade Desativada!", "OK");
        }
    }

    private async void Button_Active(object? sender, EventArgs e)
    {
        var client = new HttpClient();

        string email = EmailEntry.Text?.Trim();

        string url = $"http://10.0.2.2:3001/psicologo/{email}/active";

        var result = await client.PutAsync(url, null);

        // TODO UPDATE BUTTON STATUS BASED OF RESULT
        if (result.IsSuccessStatusCode)
        {
            DisplayAlert("Operacao realizada", "Disponibilidade Desativada!", "OK");
        }
    }
}
