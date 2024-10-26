using System.Text;
using System.Text.Json;

namespace psi_social_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Task.Run(async () =>
            {
                await Shell.Current.GoToAsync("///Disponibility");
            });
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await Shell.Current.GoToAsync("///PsiRegister");
            });
        }
    }
}
