//using WeatherTraining.Models;
//using WeatherTraining.Services;

namespace WeatherTraining
{

    public partial class MainPage : ContentPage
    {
        //private readonly WeatherService _weatherService;

        private readonly Dictionary<string, (double Latitude, double Longitude)> _locations = new()
    {
        { "Detroit, MI", (42.3314, -83.0458) },
        { "Chicago, IL", (41.8781, -87.6298) },
        { "New York City, NY", (40.7128, -74.0060) },
        { "Los Angeles, CA", (34.0522, -118.2437) },
        { "Dallas, TX", (32.7767, -96.7970) },
        { "Miami, FL", (25.7617, -80.1918) },
        { "Seattle, WA", (47.6062, -122.3321) },
        { "Denver, CO", (39.7392, -104.9903) },
        { "Boston, MA", (42.3601, -71.0589) }
    };

        private void LocationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedLocation = LocationPicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedLocation))
                return;

            if (selectedLocation == "Custom Location")
            {
                LatitudeEntry.IsEnabled = true;
                LongitudeEntry.IsEnabled = true;

                LatitudeEntry.Text = "";
                LongitudeEntry.Text = "";

                return;
            }

            if (_locations.TryGetValue(
                selectedLocation,
                out var coordinates))
            {
                LatitudeEntry.Text = coordinates.Latitude.ToString();
                LongitudeEntry.Text = coordinates.Longitude.ToString();

                LatitudeEntry.IsEnabled = false;
                LongitudeEntry.IsEnabled = false;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            //_weatherService = new WeatherService();
        }
        //Challenge code here
        private async void GetWeatherButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(LatitudeEntry.Text, out double latitude))
                {
                    await DisplayAlert(
                        "Invalid Latitude",
                        "Please enter a valid latitude.",
                        "OK");

                    return;
                }

                if (!double.TryParse(LongitudeEntry.Text, out double longitude))
                {
                    await DisplayAlert(
                        "Invalid Longitude",
                        "Please enter a valid longitude.",
                        "OK");

                    return;
                }

                if (latitude < -90 || latitude > 90)
                {
                    await DisplayAlert(
                        "Invalid Latitude",
                        "Latitude must be between -90 and 90.",
                        "OK");

                    return;
                }

                if (longitude < -180 || longitude > 180)
                {
                    await DisplayAlert(
                        "Invalid Longitude",
                        "Longitude must be between -180 and 180.",
                        "OK");

                    return;
                }

                //WeatherData method here

                //Challenge code here
            }
            catch (HttpRequestException)
            {
                await DisplayAlert(
                    "Connection Error",
                    "Unable to connect to the weather service. Please check your internet connection and try again.",
                    "OK");
            }
            catch (Exception)
            {
                await DisplayAlert(
                    "Error",
                    "Something went wrong while retrieving the weather data. Please try again.",
                    "OK");
            }
        }
    }
}
