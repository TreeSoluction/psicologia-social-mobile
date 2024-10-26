namespace psi_social_mobile;

public partial class PosRegister : ContentPage
{
    public PosRegister()
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
}