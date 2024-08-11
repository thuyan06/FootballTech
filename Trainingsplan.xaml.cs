using System.Collections.ObjectModel;

namespace Football
{
    public partial class Trainingsplan : ContentPage
    {
        public ViewModel ViewModel { get; set; }

        public Trainingsplan()
        {
            InitializeComponent();

            // Erstelle eine Instanz deines ViewModels
            ViewModel = new ViewModel();

            // Setze das ViewModel als BindingContext für die Seite
            BindingContext = ViewModel;
        }
    }

    public class ViewModel
    {
        public ObservableCollection<TrainingDay> TrainingDays { get; set; }

        public ViewModel()
        {
            TrainingDays = new ObservableCollection<TrainingDay>
            {
                new TrainingDay
                {
                    Day = "Montag",
                    Activity = "Technik und Ballkontrolle",
                    WarmUp = "Aufwärmen (15 Minuten):",
                    WarmUpDetails = "Leichtes Laufen, Dehnen\nKoordinationsübungen",
                    Technique = "Techniktraining (30 Minuten):",
                    TechniqueDetails = "Passübungen im Paar oder in kleinen Gruppen\nDribbling durch Slalomstangen\nBallführung und schnelle Richtungswechsel",
                    SmallGames = "Kleine Spielformen (20 Minuten):",
                    SmallGamesDetails = "4 gegen 4 oder 5 gegen 5 auf engem Raum\nFokus auf schnelle Ballzirkulation und Bewegung ohne Ball",
                    FinalTraining = "Abschlusstraining (15 Minuten):",
                    FinalTrainingDetails = "Torschussübungen\nPräzise Flanken und Kopfballabschlüsse"
                },
                new TrainingDay
                {
                    Day = "Mittwoch",
                    Activity = "Taktik und Spielformen",
                   
                    WarmUp = "Aufwärmen (15 Minuten):",
                    WarmUpDetails = "Leichtes Laufen, Dehnen\nPassen im Zirkel",
                    Technique = "Taktiktraining (30 Minuten):",
                    TechniqueDetails = "Positionsspiel: Aufbau aus der Defensive\nPressing und Balleroberung",
                    SmallGames = "Spielformen (20 Minuten):",
                    SmallGamesDetails = "7 gegen 7 oder 8 gegen 8\nUmsetzung der taktischen Vorgaben im Spiel",
                    FinalTraining = "Abschlusstraining (15 Minuten):",
                    FinalTrainingDetails = "Standardsituationen: Freistöße und Eckbälle\nTorschussübungen aus unterschiedlichen Spielsituationen"
                }
            };
        }
    }

    public class TrainingDay
    {
        public string Day { get; set; }
        public string Activity { get; set; }
        public string WarmUp { get; set; }
        public string WarmUpDetails { get; set; }
        public string Technique { get; set; }
        public string TechniqueDetails { get; set; }
        public string SmallGames { get; set; }
        public string SmallGamesDetails { get; set; }
        public string FinalTraining { get; set; }
        public string FinalTrainingDetails { get; set; }
    }
}
