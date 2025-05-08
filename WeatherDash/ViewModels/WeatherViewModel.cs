using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WeatherDash.Command;
using WeatherDash.Services;


namespace WeatherDash.ViewModels
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        private string _city;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        private string _temperature;
        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string _icon;
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SearchCommand { get; }

        private readonly WeatherService _weatherService = new WeatherService();

        public WeatherViewModel()
        {
            SearchCommand = new RelayCommand(async _ =>
            {
                var weather = await _weatherService.GetWeatherAsync(City);

                if (weather != null)
                {
                    Temperature = weather.Main.Temp.ToString("0.#");
                    Description = weather.Weather[0].Description;
                    Icon = $"http://openweathermap.org/img/wn/{weather.Weather[0].Icon}@2x.png";
                    Debug.WriteLine("Icon URL: " + Icon);

                }
            });
        }


    }
}
