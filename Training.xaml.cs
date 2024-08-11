using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Football
{
    public partial class Training : ContentPage
    {
        private Dictionary<DateTime, bool> signUpStatusByDate = new Dictionary<DateTime, bool>();
        private DateTime selectedDate = DateTime.MinValue;

        public Training()
        {
            InitializeComponent();
        }

        private void InitializeTableView()
        {
            var tableSection = new TableSection("An- und Abmeldungen");
            trainingTableView.Root.Add(tableSection);
        }
        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            selectedDate = e.NewDate;

            // Prüfen Sie, ob das ausgewählte Datum ein Montag oder Mittwoch ist
            bool isMondayOrWednesday = selectedDate.DayOfWeek == DayOfWeek.Monday || selectedDate.DayOfWeek == DayOfWeek.Wednesday;

            // Überprüfen Sie, ob es ein Training für den ausgewählten Tag gibt
            if (isMondayOrWednesday)
            {
                reasonEntry.IsVisible = true;

                // Aktualisieren Sie die Sichtbarkeit der Anmeldeoptionen basierend auf dem Wochentag
                reasonEntry.IsVisible = true;
                labelBegrundung.IsVisible = true; // Zeigen Sie das Label nur an, wenn es ein Training gibt
                signupRadioButton.IsVisible = true;
                unregisterRadioButton.IsVisible = true;

                if (signUpStatusByDate.TryGetValue(selectedDate, out bool isSignedUp))
                {
                    signupRadioButton.IsChecked = isSignedUp;
                    unregisterRadioButton.IsChecked = !isSignedUp;
                    UpdateLayout(isSignedUp);
                }
                else
                {
                    signupRadioButton.IsChecked = true;
                    unregisterRadioButton.IsChecked = false;
                    ShowButtons();
                }

                UpdateStatusLabel(isSignedUp);

                // Dynamisch die Sichtbarkeit der Buttons und des reasonEntry aktualisieren
                UpdateButtonAndReasonVisibility();
            }
            else
            {
                // Wenn kein Training an diesem Tag, verstecken Sie die Buttons und den Grund
                signupRadioButton.IsVisible = false;
                unregisterRadioButton.IsVisible = false;
                reasonEntry.IsVisible = false;
                submitButton.IsVisible = false;
                changeButton.IsVisible = false;
                labelBegrundung.IsVisible = false; // Verstecken Sie das Label, wenn kein Training geplant ist

                statusLabel.Text = "Kein Training";
            }
        }



        private void OnSubmitClicked(object sender, EventArgs e)
        {
            bool isSignedUp = signupRadioButton.IsChecked;

            if (selectedDate != DateTime.MinValue)
            {
                if (isSignedUp)
                {
                    // Wenn angemeldet, fügen Sie das Datum zur Dictionary hinzu oder aktualisieren Sie es
                    if (signUpStatusByDate.ContainsKey(selectedDate))
                    {
                        signUpStatusByDate[selectedDate] = true;
                    }
                    else
                    {
                        signUpStatusByDate.Add(selectedDate, true);
                    }
                }
                else
                {
                    // Wenn abgemeldet, überprüfen Sie, ob die Begründung nicht leer ist
                    if (string.IsNullOrEmpty(reasonEntry.Text))
                    {
                        DisplayAlert("Fehler", "Begründung ist leer für das ausgewählte Datum.", "OK");
                        return; // Beenden Sie die Methode, wenn die Begründung leer ist
                    }

                    // Fügen Sie das abgemeldete Datum zur Dictionary hinzu oder aktualisieren Sie es
                    if (signUpStatusByDate.ContainsKey(selectedDate))
                    {
                        signUpStatusByDate[selectedDate] = false;
                    }
                    else
                    {
                        signUpStatusByDate.Add(selectedDate, false);
                    }
                }

                UpdateStatusLabel(isSignedUp);

                // Verstecke die Buttons, wenn "Abmelden" ausgewählt wurde
                if (!isSignedUp)
                {
                    HideButtons();
                }
            }
            else
            {
                DisplayAlert("Fehler", "Bitte wählen Sie ein Datum aus.", "OK");
            }

            // Dynamisch die Sichtbarkeit der Buttons aktualisieren
            UpdateButtonAndReasonVisibility();

            // Rufen Sie UpdateTableView auf, um die TableView zu aktualisieren
            UpdateTableView();
        }


        private void OnChangeClicked(object sender, EventArgs e)
        {
            if (selectedDate != DateTime.MinValue)
            {
                // Löschen Sie die vorherige Anmeldeinformation für das ausgewählte Datum
                signUpStatusByDate.Remove(selectedDate);

                // Zeigen Sie die RadioButton-Optionen und den Grund-Entry an
                signupRadioButton.IsVisible = true;
                unregisterRadioButton.IsVisible = true;
                reasonEntry.IsVisible = true;

                // Zurücksetzen der RadioButton-Auswahl und des Grund-Entries
                signupRadioButton.IsChecked = false;
                unregisterRadioButton.IsChecked = false;
                reasonEntry.Text = "";

                // Zurücksetzen der Statuslabel-Anzeige
                statusLabel.Text = "";
            }
            else
            {
                DisplayAlert("Fehler", "Bitte wählen Sie ein Datum aus.", "OK");
            }

            // Dynamisch die Sichtbarkeit der Buttons und des reasonEntry aktualisieren
            UpdateButtonAndReasonVisibility();

            // Rufen Sie UpdateTableView auf, um die TableView zu aktualisieren
            UpdateTableView();
        }

        private void UpdateButtonAndReasonVisibility()
        {
            bool isSignedUp = signUpStatusByDate.TryGetValue(selectedDate, out bool signUpStatus);

            // "Ändern"-Button sichtbar machen, wenn bereits angemeldet oder abgemeldet
            changeButton.IsVisible = isSignedUp;

            // "Absenden"-Button sichtbar machen, wenn ausgewähltes Datum nicht bereits registriert ist
            submitButton.IsVisible = !signUpStatusByDate.ContainsKey(selectedDate);

            // reasonEntry sichtbar machen, wenn nichts ausgewählt wurde oder Abmelden ausgewählt wurde
            reasonEntry.IsVisible = !isSignedUp || unregisterRadioButton.IsChecked;

            // "Absenden"-Button deaktivieren, wenn Abmelden ausgewählt ist und keine Begründung vorliegt
            submitButton.IsEnabled = !(unregisterRadioButton.IsChecked && string.IsNullOrEmpty(reasonEntry.Text));
        }

        private void UpdateStatusLabel(bool isSignedUp)
        {
            statusLabel.Text = selectedDate != DateTime.MinValue && signUpStatusByDate.TryGetValue(selectedDate, out bool signUpStatus)
                ? (signUpStatus ? "Angemeldet" : "Abgemeldet")
                : "";

            UpdateLayout(isSignedUp);
        }

        private void UpdateLayout(bool isSignedUp)
        {
            if (isSignedUp)
            {
                HideButtons();
            }
            else
            {
                // Nur die Buttons zeigen, wenn das Datum angemeldet ist
                if (!signUpStatusByDate.ContainsKey(selectedDate) || signUpStatusByDate[selectedDate])
                {
                    ShowButtons();
                }
                else
                {
                    HideButtons();
                }
            }
        }

        private void ShowButtons()
        {
            signupRadioButton.IsVisible = true;
            unregisterRadioButton.IsVisible = true;
        }

        private void HideButtons()
        {
            signupRadioButton.IsVisible = false;
            unregisterRadioButton.IsVisible = false;
        }

        private void UpdateTableView()
        {
            // Finden Sie die vorhandene TableSection
            var tableSection = (TableSection)trainingTableView.Root[0];

            // Löschen Sie vorhandene Zellen
            tableSection.Clear();

            // Fügen Sie neue Zellen basierend auf den angemeldeten und abgemeldeten Daten hinzu
            foreach (var kvp in signUpStatusByDate)
            {
                var cell = new TextCell
                {
                    Text = $"{kvp.Key.ToShortDateString()}: {(kvp.Value ? "Angemeldet" : "Abgemeldet")}",
                    TextColor = Colors.White
                };

                tableSection.Add(cell);
            }
        }
    }
}

