using System.Text.Json;

namespace EspinosaAExamen
{
    public partial class JokePage : ContentPage
    {
        public JokePage()
        {
            InitializeComponent();
            LoadJoke();
        }

        private async void LoadJoke()
        {
            try
            {
                using var client = new HttpClient();
                var json = await client.GetStringAsync("https://official-joke-api.appspot.com/random_joke");
                var joke = JsonSerializer.Deserialize<Joke>(json);
                JokeLabel.Text = $"{joke.Setup}\n\n{joke.Punchline}";
            }
            catch
            {
                JokeLabel.Text = "Error al cargar el chiste.";
            }
        }

        private void OnRefreshJokeClicked(object sender, EventArgs e)
        {
            LoadJoke();
        }
    }

    public class Joke
    {
        public string Setup { get; set; }
        public string Punchline { get; set; }
    }
}
