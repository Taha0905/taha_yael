using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace taha_yael.page
{
    public class Flags
    {
        public string png { get; set; }
        public string countryName { get; set; }
    }

    public partial class Quizz : Window
    {
        private List<Flags> flagsList;
        private Random random = new Random();
        private Flags currentFlag;

        public Quizz()
        {
            InitializeComponent();
            ChargerDrapeaux();
            
            //AfficherDrapeauAleatoire();
        }

        private async void ChargerDrapeaux()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/all");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Root> countries = JsonConvert.DeserializeObject<List<Root>>(jsonResponse);

                        // Extrait la liste de drapeaux
                        flagsList = countries.ConvertAll(c => new Flags { png = c.flags.png, countryName = c.name.common });
                        currentFlag = flagsList[random.Next(flagsList.Count)];
                        BitmapImage flagImage = new BitmapImage(new Uri(currentFlag.png));
                        imageFlag.Source = flagImage;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des drapeaux :");
            }
        }

        private void AfficherDrapeauAleatoire()
        {
            if (flagsList != null)
            {
                currentFlag = flagsList[random.Next(flagsList.Count)];
                BitmapImage flagImage = new BitmapImage(new Uri(currentFlag.png));
                imageFlag.Source = flagImage;
            }
            else
            {
                MessageBox.Show("Aucun drapeau disponible.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFlag != null)
            {
                // Convertissez le nom du pays en minuscules pour une comparaison insensible à la casse
                string paysAttendu = currentFlag.countryName.ToLower();
                string reponseUtilisateur = textBoxAnswer.Text.ToLower();

                // Vérifiez la réponse
                if (reponseUtilisateur.Equals(paysAttendu))
                {
                    messageTextBlock.Text = "Bonne réponse !";
                }
                else
                {
                    // Affichez le message d'erreur avec la bonne réponse
                    messageTextBlock.Text = $"Mauvaise réponse. Essayez à nouveau. La bonne réponse était : {paysAttendu}";
                }

                // Affichez le drapeau suivant
                AfficherDrapeauAleatoire();
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Fermez votre fenêtre ici
            this.Close();
        }
      

    }
}
