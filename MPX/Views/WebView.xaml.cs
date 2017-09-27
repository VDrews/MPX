using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPX.Models;
using MPX.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MPX
{

    public partial class WebView : ContentPage
    {
        ListaPaginas Marcadores = new ListaPaginas();
        private Tab ActualTab = new Tab("https://www.google.es");
        private ListaPaginas Historial = new ListaPaginas();
        private ListaPaginas Tabs = new ListaPaginas();
        private bool IsFavorite = false;

        public WebView()
        {
            InitializeComponent();

            historial.ItemsSource = Historial.GetPagina();
            marcadores.ItemsSource = Marcadores.GetPagina();
            tabs.ItemsSource = Tabs.GetPagina();

            BtnBack.GestureRecognizers.Add(new TapGestureRecognizer(OnGoBack));
            BtnNext.GestureRecognizers.Add(new TapGestureRecognizer(OnGoForward));
            BtnFavorites.GestureRecognizers.Add(new TapGestureRecognizer(OnGoFavorites));
            BtnMore.GestureRecognizers.Add(new TapGestureRecognizer(PopUpMore));

        }


        private void PopUpMore(View arg1, object arg2)
        {
            PopUpMenu.IsVisible = !PopUpMenu.IsVisible;
        }


        private async void OnGoFavorites(View arg1, object arg2)
        {
            if (!IsFavorite)
            {
                var confirmacion = await DisplayAlert("Marcadores", "Desea añadir esta página a marcadores? ", "AÑADIR", "CANCELAR");
                if (confirmacion)
                {
                    BtnFavorites.Source = ImageSource.FromFile("FavoriteActivatedIcon.png");
                    Marcadores.SetPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url });
                    IsFavorite = !IsFavorite;
                }
                
            }

            else
            {
                BtnFavorites.Source = ImageSource.FromFile("FavoriteIcon.png");
                IsFavorite = !IsFavorite;
                Marcadores.DeleteLast();
            }



        }

        private void OnGoForward(View arg1, object arg2)
        {
            if (ActualTab.TabHistorial.GetPaginaSiguiente(false) != null)
            {
                BtnBack.Source = ImageSource.FromFile("BackActivatedIcon.png");
                ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url = ActualTab.TabHistorial.GetPaginaSiguiente(true).link;
            }
        }

        private void OnGoBack(View arg1, object arg2)
        {
            BtnNext.Source = ImageSource.FromFile("NextActivatedIcon.png");
            ((Xamarin.Forms.UrlWebViewSource) webView.Source).Url = ActualTab.TabHistorial.GetPaginaAnterior().link;
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {

           if (VerifyURL(DirectionBar.Text))
            {
                if (((Xamarin.Forms.UrlWebViewSource)webView.Source).Url.ToString().StartsWith("http://"))
                    ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url = DirectionBar.Text;
                else
                {
                    ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url = "http://" + DirectionBar.Text;
                    BtnBack.Source = ImageSource.FromFile("BackActivatedIcon.png");
                }
            }

           else
            {
                ((Xamarin.Forms.UrlWebViewSource) webView.Source).Url = "https://www.google.es/search?q=" + DirectionBar.Text;
            }


        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.UrlWebViewSource) webView.Source).Url = ((MPX.Models.Pagina) e.SelectedItem).link;
        }

        #region WebViewNavigation
        private async void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            PopUpMenu.IsVisible = false;
            ProgressBar.IsVisible = true;
            await ProgressBar.ProgressTo(.2, 400, Easing.Linear);
        }

        private async void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            //ActualTab.ChangeLink(((Xamarin.Forms.UrlWebViewSource)webView.Source).Url);
            Historial.SetPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url });
            DirectionBar.Text = ((Xamarin.Forms.UrlWebViewSource) webView.Source).Url;
            await ProgressBar.ProgressTo(1, 400, Easing.Linear);
            await Task.Delay(1000);
            ProgressBar.IsVisible = false;
            ProgressBar.Progress = 0;
            ActualTab.TabHistorial.SetNewPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)webView.Source).Url });

            if (IsFavorite)
            {
                IsFavorite = false;
                BtnFavorites.Source = ImageSource.FromFile("FavoriteIcon.png");
            }
        }

        #endregion

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        bool VerifyURL(string link)
        {
            return link.EndsWith(".com") || link.EndsWith(".com/") || link.EndsWith(".es") || link.EndsWith(".es/") ||
                   link.EndsWith(".net") || link.EndsWith(".net/") || link.EndsWith(".com") || link.EndsWith(".php") ||
                   link.EndsWith(".php/") || link.EndsWith(".html") || link.EndsWith(".html/");
        }

    }
}
