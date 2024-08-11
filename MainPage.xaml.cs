// MainPage.xaml.cs
namespace Football
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Weitere vorhandene Methoden und Code...

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            // Hier können Sie zusätzliche Logik für die Abmeldung hinzufügen, falls erforderlich.

            // Navigieren Sie zurück zur Login-Seite
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}
