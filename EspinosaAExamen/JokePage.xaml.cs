using System.Text.Json;

namespace EspinosaAExamen
{
    public partial class JokePage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public JokePage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadJoke();
        }

        private async void LoadJoke()
        {
            try
            {
                // aqui se mjuesta el cargado mientras se carga el chiste
                JokeLabel.Text = "Cargando chiste...";
                RefreshButton.IsEnabled = false;

                var json = await _httpClient.GetStringAsync("https://official-joke-api.appspot.com/random_joke");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var joke = JsonSerializer.Deserialize<Joke>(json, options);

                if (joke != null)
                {
                    JokeLabel.Text = $"{joke.Setup}\n\n{joke.Punchline}";
                }
                else
                {
                    JokeLabel.Text = "No se pudo cargar el chiste.";
                }
            }
            catch (Exception ex)
            {
                JokeLabel.Text = $"Error al cargar el chiste: {ex.Message}";
            }
            finally
            {
                RefreshButton.IsEnabled = true;
            }
        }

        private void OnRefreshJokeClicked(object sender, EventArgs e)
        {
            LoadJoke();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _httpClient?.Dispose();
        }
    }

    public class Joke
    {
        public string Setup { get; set; } = string.Empty;
        public string Punchline { get; set; } = string.Empty;
    }
}