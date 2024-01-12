using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Windows.Controls;
using taha_yael.page;


namespace taha_yael
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string countryName = CountryNameTextBox.Text.ToLower();
            string apiUrl = $"https://restcountries.com/v3.1/name/{countryName}";


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonResponse = await client.GetStringAsync(apiUrl);
                    List<Root> countriesInfo = JsonConvert.DeserializeObject<List<Root>>(jsonResponse);

                    if (countriesInfo.Count > 0)
                    {
                        Root countryInfo = countriesInfo[0];
                        UpdateUI(countryInfo);
                    }
                    else
                    {
                        MessageBox.Show("Aucun résultat trouvé pour ce pays.", "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veuillez entrer des caractères valides");
                }
            }
        }

        private void UpdateUI(Root countryInfo)
        {
            NameTextBlock.Text = $"Nom : {countryInfo.name.common}";
            CapitalTextBlock.Text = $"Capitale : {string.Join(", ", countryInfo.capital)}";
            PopulationTextBlock.Text = $"Population : {countryInfo.population}";
            RegionTextBlock.Text = $"Région : {countryInfo.region}";
            codetelephonique.Text = $"Code Téléphonique : {countryInfo.idd.root}";
            // Vérifiez si la propriété government existe
            if (countryInfo.government != null)
            {
                gouvernement.Text = $"Gouvernement : {countryInfo.government}";
            }
            else
            {
                gouvernement.Text = "Information gouvernementale non disponible";
            }
            timezone.Text = $"Fuseau Horaire : {string.Join(", ", countryInfo.timezones)}";
            demonym.Text = $"Démonyme : {countryInfo.demonyms.eng.f}";
            domaineinternet.Text = $"Domaine Internet : {string.Join(", ", countryInfo.tld)}";
            borders.Text = $"Frontières : {string.Join(", ", countryInfo.borders)}";


            // Nouvelles propriétés
            currency.Text = $"Monnaie : {countryInfo.currency}";
            superficie.Text = $"Superficie : {countryInfo.area} km²";
          


            // Afficher le drapeau
            BitmapImage flagImage = new BitmapImage(new Uri(countryInfo.flags.png));
            FlagImage.Source = flagImage;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Quizz quizzWindow = new Quizz();
            quizzWindow.Show();
        }

    }

    public class Root
    {
        public Name name { get; set; }
        public List<string> capital { get; set; }
        public int population { get; set; }
        public string region { get; set; }
        public Flags flags { get; set; }
        public string currency{ get; set; }
        public double? area { get; set; }
        public List<double> latlng { get; set; }
        public Idd idd { get; set; }
        public Translations translations { get; set; }
        public List<string> timezones { get; set; }
        public List<string> borders { get; set; }
        public Demonyms demonyms { get; set; }
        
        public Gini gini { get; set; }
        public Car car { get; set; }
        public List<string> continents { get; set; }
        public CoatOfArms coatOfArms { get; set; }
        public CapitalInfo capitalInfo { get; set; }
        public PostalCode postalCode { get; set; }
        public string government { get; set; }  // Ajoutez cette ligne pour la propriété manquante
        public List<string> tld { get; set; }   // Ajoutez cette ligne pour la propriété manquante
    }

    public class Name
    {
        public string common { get; set; }
    }

    public class Flags
    {
        public string png { get; set; }
    }
}



// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Ara
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Bre
{
    public string official { get; set; }
    public string common { get; set; }
}

public class CapitalInfo
{
    public List<double> latlng { get; set; }
}

public class Car
{
    public List<string> signs { get; set; }
    public string side { get; set; }
}

public class Ces
{
    public string official { get; set; }
    public string common { get; set; }
}

public class CoatOfArms
{
    public string png { get; set; }
    public string svg { get; set; }
}

public class Currencies
{
    public EUR EUR { get; set; }
}

public class Cym
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Demonyms
{
    public Eng eng { get; set; }
    public Fra fra { get; set; }
}

public class Deu
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Eng
{
    public string f { get; set; }
    public string m { get; set; }
}

public class Est
{
    public string official { get; set; }
    public string common { get; set; }
}

public class EUR
{
    public string name { get; set; }
    public string symbol { get; set; }
}

public class Fin
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Flags
{
    public string png { get; set; }
    public string svg { get; set; }
    public string alt { get; set; }
}

public class Fra
{
    public string official { get; set; }
    public string common { get; set; }
    public string f { get; set; }
    public string m { get; set; }
}

public class Gini
{
    [JsonProperty("2018")]
    public double _2018 { get; set; }
}

public class Hrv
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Hun
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Idd
{
    public string root { get; set; }
    public List<string> suffixes { get; set; }
}

public class Ita
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Jpn
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Kor
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Languages
{
    public string fra { get; set; }
}

public class Maps
{
    public string googleMaps { get; set; }
    public string openStreetMaps { get; set; }
}

public class Name
{
    public string common { get; set; }
    public string official { get; set; }
    public NativeName nativeName { get; set; }
}

public class NativeName
{
    public Fra fra { get; set; }
}

public class Nld
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Per
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Pol
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Por
{
    public string official { get; set; }
    public string common { get; set; }
}

public class PostalCode
{
    public string format { get; set; }
    public string regex { get; set; }
}

public class Root
{
    public Name name { get; set; }
    public List<string> tld { get; set; }
    public string cca2 { get; set; }
    public string ccn3 { get; set; }
    public string cca3 { get; set; }
    public string cioc { get; set; }
    public bool independent { get; set; }
    public string status { get; set; }
    public bool unMember { get; set; }
    public Currencies currencies { get; set; }
    public Idd idd { get; set; }
    public List<string> capital { get; set; }
    public List<string> altSpellings { get; set; }
    public string region { get; set; }
    public string subregion { get; set; }
    public Languages languages { get; set; }
    public Translations translations { get; set; }
    public List<double> latlng { get; set; }
    public bool landlocked { get; set; }
    public List<string> borders { get; set; }
    public double area { get; set; }
    public Demonyms demonyms { get; set; }
    public string flag { get; set; }
    public Maps maps { get; set; }
    public int population { get; set; }
    public Gini gini { get; set; }
    public string fifa { get; set; }
    public Car car { get; set; }
    public List<string> timezones { get; set; }
    public List<string> continents { get; set; }
    public Flags flags { get; set; }
    public CoatOfArms coatOfArms { get; set; }
    public string startOfWeek { get; set; }
    public CapitalInfo capitalInfo { get; set; }
    public PostalCode postalCode { get; set; }
}

public class Rus
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Slk
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Spa
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Srp
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Swe
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Translations
{
    public Ara ara { get; set; }
    public Bre bre { get; set; }
    public Ces ces { get; set; }
    public Cym cym { get; set; }
    public Deu deu { get; set; }
    public Est est { get; set; }
    public Fin fin { get; set; }
    public Fra fra { get; set; }
    public Hrv hrv { get; set; }
    public Hun hun { get; set; }
    public Ita ita { get; set; }
    public Jpn jpn { get; set; }
    public Kor kor { get; set; }
    public Nld nld { get; set; }
    public Per per { get; set; }
    public Pol pol { get; set; }
    public Por por { get; set; }
    public Rus rus { get; set; }
    public Slk slk { get; set; }
    public Spa spa { get; set; }
    public Srp srp { get; set; }
    public Swe swe { get; set; }
    public Tur tur { get; set; }
    public Urd urd { get; set; }
    public Zho zho { get; set; }
}

public class Tur
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Urd
{
    public string official { get; set; }
    public string common { get; set; }
}

public class Zho
{
    public string official { get; set; }
    public string common { get; set; }
}



