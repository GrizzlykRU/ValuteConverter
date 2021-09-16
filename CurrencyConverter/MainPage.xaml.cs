using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace CurrencyConverter
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Root Quotes = new Root();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            ApplicationView.PreferredLaunchViewSize = new Size(640, 360);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            Quotes = GetRates();

        }

        private Root GetRates()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.cbr-xml-daily.ru/daily_json.js");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Root>(result);
            }
        }

        private void ChangeCurrency(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] CurrencyData = new string[2];
            CurrencyData[0] = button.Name;
            if (button.Name=="FirstCurrencyButton")
                CurrencyData[1] = FirstCurrencyName.Text;
            else
                CurrencyData[1] = SecondCurrencyName.Text;
            Frame.Navigate(typeof(CurrencyList), CurrencyData);
        }

        private void SwapCurrency(object sender, RoutedEventArgs e)
        {
            string tmp = FirstCurrencyName.Text;
            FirstCurrencyName.Text = SecondCurrencyName.Text;
            SecondCurrencyName.Text = tmp;
            Calculate();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!e.Parameter.Equals(""))
            {
                string[] CurrencyData = (string[])e.Parameter;
                if (CurrencyData[0] == "FirstCurrencyButton")
                    FirstCurrencyName.Text = CurrencyData[1];
                else
                    SecondCurrencyName.Text = CurrencyData[1];
                Calculate();
            }
        }
        private void Calculate()
        {
            var valute1 = typeof(Valute).GetProperty(FirstCurrencyName.Text).GetValue(Quotes.Valute) as ValuteInfo;
            var valute2 = typeof(Valute).GetProperty(SecondCurrencyName.Text).GetValue(Quotes.Valute) as ValuteInfo;
            double rub1 = valute1.Value / (double)valute1.Nominal,
                   rub2 = valute2.Value / (double)valute2.Nominal,
                   result = 0;
            if (FirstCurrencyValue.Text != "")
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(FirstCurrencyValue.Text) * (rub1 / rub2), 2);

                }
                catch
                {
                    MessageDialog dialog = new MessageDialog("Вычислительная ошибка (возможн введено слишком большое число)");
                    dialog.ShowAsync();
                }
            }
            SecondCurrencyValue.Text = result.ToString();

        }

        private void FirstCurrencyValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstCurrencyValue.Text = Regex.Replace(FirstCurrencyValue.Text, @"[^0-9+-,]+", "");
            Calculate();
        }

    }
}
