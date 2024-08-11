using Microsoft.Maui.Controls;

namespace Football
{
    public partial class FAQPage : ContentPage
    {
        public FAQPage()
        {
            InitializeComponent();
        }

        private void OnQuestionSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedQuestion = QuestionPicker.SelectedItem?.ToString();

            if (!string.IsNullOrWhiteSpace(selectedQuestion))
            {
                switch (selectedQuestion)
                {
                    case "Was ist FootballTech?":
                        SelectedAnswer.Text = "FootballTech ist ein innovatives Unternehmen im Bereich Fussballtechnologie. Wir streben danach, den Fussball f�r Vereine zug�nglicher und effizienter zu gestalten.";
                        break;
                    case "Wer ist die Zielgruppe von FootballTech?":
                        SelectedAnswer.Text = "Unsere Zielgruppe umfasst Fussballvereine jeder Gr�sse. Unser Ziel ist es, durch benutzerfreundliche L�sungen die Effizienz im Training und Management zu steigern.";
                        break;
                    case "Wie kann ich die FootballTech-App nutzen?":
                        SelectedAnswer.Text = "Die Nutzung der FootballTech-App erfolgt durch einfache An- und Abmeldung von Spielern sowie durch die Organisation von Trainingseinheiten. Die App ist darauf ausgerichtet, die Trainingsorganisation zu vereinfachen.";
                        break;
                    case "Gibt es eine Support-Option?":
                        SelectedAnswer.Text = "Ja, wir bieten Support f�r unsere Nutzer an. Bei Fragen oder Problemen kannst du uns �ber die angegebenen Kontaktdaten erreichen.";
                        break;
                    case "Wie melde ich mich f�r ein Training an?":
                        SelectedAnswer.Text = "W�hlen Sie das gew�nschte Datum aus dem Kalender aus und klicken Sie auf 'Anmelden'. Der Spieler wird dann f�r das ausgew�hlte Training angemeldet.";
                        break;
                    case "Ist eine Anmeldung erforderlich?":
                        SelectedAnswer.Text = "Ja, um die Teilnahme am Training ist eine Anmeldung erforderlich. Dies erm�glicht die Verfolgung der angemeldeten Spieler.";
                        break;
                    default:
                        SelectedAnswer.Text = "Frage nicht gefunden";
                        break;
                }
            }
        }
    }
}
