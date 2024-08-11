namespace Football
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // L�schen vorhandener Fehlermeldungen
            usernameErrorLabel.Text = "";
            passwordErrorLabel.Text = "";

            // �berpr�fen, ob Benutzername und Passwort nicht leer sind
            if (string.IsNullOrWhiteSpace(username))
            {
                usernameErrorLabel.Text = "Benutzername darf nicht leer sein.";
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                passwordErrorLabel.Text = "Passwort darf nicht leer sein.";
                return;
            }

            // Beispiel f�r eine einfache Anmeldepr�fung (ersetzen Sie dies durch eine sicherere Implementierung)
            if (IsValidLogin(username, password))
            {
                // Erfolgreiche Anmeldung
                // Hier zur n�chsten Seite navigieren und den Navigation Stack leeren
                AppShell appShell = new AppShell();
                Application.Current.MainPage = appShell;

                // Optional: Wenn Sie den Navigation Stack leeren m�chten
                appShell.Navigation.PopToRootAsync();
            }
            else
            {
                // Fehlerhafte Anmeldung
                usernameErrorLabel.Text = "Ung�ltige Anmeldeinformationen";
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            // Beispiel f�r eine einfache Anmeldepr�fung (ersetzen Sie dies durch eine sicherere Implementierung)
            return username == "FCB_01" && password == "Alex2006.";
        }
    }
}
