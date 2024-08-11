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

            // Pr�fen Sie, ob das ausgew�hlte Datum ein Montag oder Mittwoch ist
            bool isMondayOrWednesday = selectedDate.DayOfWeek == DayOfWeek.Monday || selectedDate.DayOfWeek == DayOfWeek.Wednesday;

            // �berpr�fen Sie, ob es ein Training f�r den ausgew�hlten Tag gibt
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
                    // Wenn angemeldet, f�gen Sie das Datum zur Dictionary hinzu oder aktualisieren Sie es
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
                    // Wenn abgemeldet, �berpr�fen Sie, ob die Begr�ndung nicht leer ist
                    if (string.IsNullOrEmpty(reasonEntry.Text))
                    {
                        DisplayAlert("Fehler", "Begr�ndung ist leer f�r das ausgew�hlte Datum.", "OK");
                        return; // Beenden Sie die Methode, wenn die Begr�ndung leer ist
                    }

                    // F�gen Sie das abgemeldete Datum zur Dictionary hinzu oder aktualisieren Sie es
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

                // Verstecke die Buttons, wenn "Abmelden" ausgew�hlt wurde
                if (!isSignedUp)
                {
                    HideButtons();
                }
            }
            else
            {
                DisplayAlert("Fehler", "Bitte w�hlen Sie ein Datum aus.", "OK");
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
                // L�schen Sie die vorherige Anmeldeinformation f�r das ausgew�hlte Datum
                signUpStatusByDate.Remove(selectedDate);

                // Zeigen Sie die RadioButton-Optionen und den Grund-Entry an
                signupRadioButton.IsVisible = true;
                unregisterRadioButton.IsVisible = true;
                reasonEntry.IsVisible = true;

                // Zur�cksetzen der RadioButton-Auswahl und des Grund-Entries
                signupRadioButton.IsChecked = false;
                unregisterRadioButton.IsChecked = false;
                reasonEntry.Text = "";

                // Zur�cksetzen der Statuslabel-Anzeige
                statusLabel.Text = "";
            }
            else
            {
                DisplayAlert("Fehler", "Bitte w�hlen Sie ein Datum aus.", "OK");
            }

            // Dynamisch die Sichtbarkeit der Buttons und des reasonEntry aktualisieren
            UpdateButtonAndReasonVisibility();

            // Rufen Sie UpdateTableView auf, um die TableView zu aktualisieren
            UpdateTableView();
        }

        private void UpdateButtonAndReasonVisibility()
        {
            bool isSignedUp = signUpStatusByDate.TryGetValue(selectedDate, out bool signUpStatus);

            // "�ndern"-Button sichtbar machen, wenn bereits angemeldet oder abgemeldet
            changeButton.IsVisible = isSignedUp;

            // "Absenden"-Button sichtbar machen, wenn ausgew�hltes Datum nicht bereits registriert ist
            submitButton.IsVisible = !signUpStatusByDate.ContainsKey(selectedDate);

            // reasonEntry sichtbar machen, wenn nichts ausgew�hlt wurde oder Abmelden ausgew�hlt wurde
            reasonEntry.IsVisible = !isSignedUp || unregisterRadioButton.IsChecked;

            // "Absenden"-Button deaktivieren, wenn Abmelden ausgew�hlt ist und keine Begr�ndung vorliegt
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

            // L�schen Sie vorhandene Zellen
            tableSection.Clear();

            // F�gen Sie neue Zellen basierend auf den angemeldeten und abgemeldeten Daten hinzu
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

