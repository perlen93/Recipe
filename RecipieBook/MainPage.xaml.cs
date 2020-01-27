using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace RecipieBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(HomeView));
        }

        public async void MenuSelected(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = NavView.SelectedItem as NavigationViewItem;
            switch (item.Tag)
            {
                case "Home":
                    ContentFrame.Navigate(typeof(HomeView));
                    NavView.Header = "Home";
                    break;
                
                case "Saved":
                    ContentFrame.Navigate(typeof(SavedRecipiesView));
                    NavView.Header = "Saved recipies";
                    break;

                case "Chocolate":
                    NavView.Header = "Chocolate";
                    var reqUri = GetRequestUri("chocolate");
                    var recipieURL = await GetRandomRecipieURLAsync(reqUri);
                    ContentFrame.Navigate(typeof(ChocolateView), recipieURL);
                    break;

                case "Cinnamon":
                    NavView.Header = "Cinnamon";
                    var reqUri2 = GetRequestUri("cinnamon");
                    var recipieURL2 = await GetRandomRecipieURLAsync(reqUri2);                    
                    ContentFrame.Navigate(typeof(CinnamonView), recipieURL2);                   
                    break;

                case "RndFood":
                    NavView.Header = "Random Food";
                    var reqUri1 = GetRequestUri("");
                    var recipieURL1 = await GetRandomRecipieURLAsync(reqUri1);
                    ContentFrame.Navigate(typeof(RndFoodView), recipieURL1);                   
                    break;
            }
        }

        public Uri GetRequestUri(string typeOfingredient)
        {
            string uri = "";
            if (String.IsNullOrEmpty(typeOfingredient))
            {
                uri = "http://www.recipepuppy.com/api/";
            }
            else
            {
                uri = "http://www.recipepuppy.com/api/?i=" + typeOfingredient;
            }
            Uri requestUri = new Uri(uri);

            return requestUri;
        }

        public async Task<Uri> GetRandomRecipieURLAsync(Uri requestUri)
        {
            try
            {
                var rootObj = await DeserializeRecipieAsync(requestUri);

                if (rootObj != null)
                {
                    Random rnd = new Random();
                    int randomIndex = rnd.Next(rootObj.Results.Count);

                    var randomItem = rootObj.Results[randomIndex].Href;
                    var randomRecepieURL = new Uri(randomItem);
                    return randomRecepieURL;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                string message = "No internetconnection or response from API. Contact app owner for further assistance.";
                var messageDialog = new MessageDialog(message);
                await messageDialog.ShowAsync();
                return null;
            }
        }

        public async Task<RootObject> DeserializeRecipieAsync(Uri requestUri)
        {
            string httpResponseBody = await GetResponseBodyAsync(requestUri);

            if (!String.IsNullOrEmpty(httpResponseBody))
            {
                var rootObj = JsonConvert.DeserializeObject<RootObject>(httpResponseBody);

                RootObject root = new RootObject
                {
                    Title = rootObj.Title,
                    Version = rootObj.Version,
                    Href = rootObj.Href,
                    Results = rootObj.Results
                };
                return root;
            }
            return null;
        }

        public async Task<string> GetResponseBodyAsync(Uri requestUri)
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            var headers = httpClient.DefaultRequestHeaders;
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            return httpResponseBody;
        }


    }
}
