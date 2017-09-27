using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPX.Models;
using Xamarin.Forms;

namespace MPX.Views
{
    public partial class DesktopView : ContentPage
    {
        ListaPaginas Marcadores = new ListaPaginas();
        private Tab ActualTab = new Tab("https://www.google.es");

        //Cajas de las pestañas
        private ButtonList TabBoxes = new ButtonList();
        


        public TabList tabList = new TabList();
        private ListaPaginas Historial = new ListaPaginas();
        private ListaPaginas Tabs = new ListaPaginas();

        //private Xamarin.Forms.WebView webView;

        private int TabContador = 0;
        private int TabPosition = -1;


        private bool IsFavorite = false;
        private bool PopUp = false;


        public DesktopView()
        {
            InitializeComponent();
            //webView = tabList.GetTab(0);

            Tabs.SetPagina(ActualTab.Pagina);
            lista.ItemsSource = Historial.GetPagina();

            #region ButtonAdds
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            TabBoxes.AddButton();
            #endregion


            AddTab(null, null);

            #region TapGesture
            BtnBack.GestureRecognizers.Add(new TapGestureRecognizer(OnGoBack));
            BtnNext.GestureRecognizers.Add(new TapGestureRecognizer(OnGoForward));
            BtnFavorites.GestureRecognizers.Add(new TapGestureRecognizer(AddFavorite));
            BtnMenu.GestureRecognizers.Add(new TapGestureRecognizer(PopUpMore));
            BtnFavorite.GestureRecognizers.Add(new TapGestureRecognizer(OnGoFavorites));
            BtnHistorial.GestureRecognizers.Add(new TapGestureRecognizer(OnGoHistorial));
            LogoIcon.GestureRecognizers.Add(new TapGestureRecognizer(PopUpMore));
            PopUpTrigger.GestureRecognizers.Add(new TapGestureRecognizer(DisablePopUp));
            BtnAdd.GestureRecognizers.Add(new TapGestureRecognizer(AddTab));

            TabBoxes.GetTab(0).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab1));
            TabBoxes.GetTab(1).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab2));
            TabBoxes.GetTab(2).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab3));
            TabBoxes.GetTab(3).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab4));
            TabBoxes.GetTab(4).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab5));
            TabBoxes.GetTab(5).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab6));
            TabBoxes.GetTab(6).GestureRecognizers.Add(new TapGestureRecognizer(GoToTab7));
            


            tabList.GetTab(0).Navigating += WebView_OnNavigating;
            tabList.GetTab(0).Navigated += WebView_OnNavigated;


            #endregion
        }

        #region GoToTabs
        private void GoToTab1(View arg1, object arg2)
        {
            TabPosition = 0;
            GotoTab(TabPosition);
        }

        private void GoToTab2(View arg1, object arg2)
        {
            TabPosition = 1;
            GotoTab(TabPosition);
        }

        private void GoToTab3(View arg1, object arg2)
        {
            TabPosition = 2;
            GotoTab(TabPosition);
        }

        private void GoToTab4(View arg1, object arg2)
        {
            TabPosition = 3;
            GotoTab(TabPosition);
        }

        private void GoToTab5(View arg1, object arg2)
        {
            TabPosition = 4;
            GotoTab(TabPosition);
        }

        private void GoToTab6(View arg1, object arg2)
        {
            TabPosition = 5;
            GotoTab(TabPosition);
        }

        private void GoToTab7(View arg1, object arg2)
        {
            TabPosition = 6;
            GotoTab(TabPosition);
        }

        private void GotoTab(int position)
        {
            tabList.GetTab(position).IsVisible = true;
            TabBoxes.GetTab(TabPosition).BackgroundColor = Color.FromHex("#383838");

            for (int i = position - 1; i >= 0; i--)
            {
                TabBoxes.GetTab(i).BackgroundColor = Color.FromHex("#242424");
                tabList.GetTab(i).IsVisible = false;
            }

            for (int i = position + 1; i <= TabContador; i++)
            {
                TabBoxes.GetTab(i).BackgroundColor = Color.FromHex("#242424");
                tabList.GetTab(i).IsVisible = false;
            }

        }

        #endregion



        private void DisablePopUp(View arg1, object arg2)
        {
            if (PopUp)
            {
                PopUpMenu.TranslateTo(-500, 0, 200, Easing.Linear);
                PopUpTrigger.InputTransparent = true;
                BtnMenu.Source = ImageSource.FromFile("MenuIcon.png");
                PopUp = !PopUp;
            }
        }

        private void AddTab(View arg1, object arg2)
        {
            if (TabPosition >= 0)
                TabBoxes.GetTab(TabPosition).BackgroundColor = Color.FromHex("#242424");
            tabList.AddTab();
            TabPosition++;
            TabBoxes.GetTab(TabPosition).BackgroundColor = Color.FromHex("#383838");
            TabBoxes.GetLabel(TabContador).Text = ((Xamarin.Forms.UrlWebViewSource) tabList.GetTab(TabContador).Source)
                .Url;

            CarouselTabs.Children.Add(TabBoxes.GetTab(TabContador), TabContador, 0);

            grid.Children.Add(tabList.GetTab(TabContador), 0, 1);
            Grid.SetColumnSpan(tabList.GetTab(TabContador), 2);
            TabContador++;

            tabList.GetTab(TabPosition).Navigating += WebView_OnNavigating;
            tabList.GetTab(TabPosition).Navigated += WebView_OnNavigated;

            grid.RaiseChild(PopUpTrigger);
            grid.RaiseChild(PopUpMenu);
        }


        private void PopUpMore(View arg1, object arg2)
        {
            if (!PopUp)
            {
                BtnMenu.Source = ImageSource.FromFile("MenuActivatedIcon.png");
                LogoIcon.RotateTo(-360, 200, Easing.BounceOut);
                PopUpMenu.TranslateTo(0, 0, 200, Easing.Linear);
            }

            else
            {
                BtnMenu.Source = ImageSource.FromFile("MenuIcon.png");
                LogoIcon.RotateTo(360, 200, Easing.BounceOut);
                PopUpMenu.TranslateTo(-500, 0, 200, Easing.Linear);
            }

            PopUp = !PopUp;
            PopUpTrigger.InputTransparent = !PopUpTrigger.InputTransparent;

        }


        private async void AddFavorite(View arg1, object arg2)
        {
            if (!IsFavorite)
            {
                var confirmacion = await DisplayAlert("Marcadores", "Desea añadir esta página a marcadores? ", "AÑADIR", "CANCELAR");
                if (confirmacion)
                {
                    BtnFavorites.Source = ImageSource.FromFile("FavoriteActivatedIcon.png");
                    Marcadores.SetPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url });
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

        #region OnGo
        private void OnGoHistorial(View arg1, object arg2)
        {
            lista.ItemsSource = Historial.GetPagina();
        }

        private void OnGoFavorites(View arg1, object arg2)
        {
            lista.ItemsSource = Marcadores.GetPagina();
        }

        private void OnGoForward(View arg1, object arg2)
        {
            if (ActualTab.TabHistorial.GetPaginaSiguiente(false) != null)
            {
                BtnBack.Source = ImageSource.FromFile("BackActivatedIcon.png");
                ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = ActualTab.TabHistorial.GetPaginaSiguiente(true).link;
            }
        }

        private void OnGoBack(View arg1, object arg2)
        {
            BtnNext.Source = ImageSource.FromFile("NextActivatedIcon.png");
            ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = ActualTab.TabHistorial.GetPaginaAnterior().link;
        }
        #endregion

        private void OnEntryCompleted(object sender, EventArgs e)
        {

            if (VerifyURL(DirectionBar.Text))
            {
                if (DirectionBar.Text.StartsWith("http://") || DirectionBar.Text.StartsWith("https://"))
                    ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = DirectionBar.Text;

                else
                {
                    ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = "http://" + DirectionBar.Text;
                }
            }

            else
            {
                ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = "https://www.google.es/search?q=" + DirectionBar.Text;
            }

            BtnBack.Source = ImageSource.FromFile("BackActivatedIcon.png");


        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url = ((MPX.Models.Pagina)e.SelectedItem).link;
        }

        #region WebViewNavigation

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            LogoIcon.RotateTo(14400, 30000, Easing.Linear);
            ActualTab.ChangeLink(((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url);
            Historial.SetPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url });
            DirectionBar.Text = ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url;

            ActualTab.TabHistorial.SetNewPagina(new Pagina() { link = ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source).Url });

        
    }

        private async void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {

            string link =
                ((Xamarin.Forms.UrlWebViewSource)tabList.GetTab(TabPosition).Source)
                .Url;
            DirectionBar.Text = link;

            if (link.StartsWith("https://www."))
                link = link.Remove(0, 12);

            else if (link.StartsWith("http://www."))
                link = link.Remove(0, 11);

            else if (link.StartsWith("https://"))
                link = link.Remove(0, 8);

            else if (link.StartsWith("http://"))
                link = link.Remove(0, 7);

            else if (link.StartsWith("www."))
                link = link.Remove(0, 4);


            if (VerifyURL(link))
            {
                link = link.Remove(link.IndexOf('.'));
            }

            link = Char.ToUpper(link[0]) + link.Substring(1);

            if (link.Length > 25)
            {
                TabBoxes.GetLabel(TabPosition).Text = link.Remove(25);
            }

            else TabBoxes.GetLabel(TabPosition).Text = link;

            await LogoIcon.RotateTo(1440, 1500, Easing.BounceOut);


            if (IsFavorite)
            {
                IsFavorite = false;
                BtnFavorites.Source = ImageSource.FromFile("FavoriteIcon.png");
            }

        }



        #endregion


        bool VerifyURL(string link)
        {
            return link.Contains(".com") || link.Contains(".es") ||
                   link.Contains(".net") || link.Contains(".php") ||
                   link.Contains(".html");
        }

    }
}
