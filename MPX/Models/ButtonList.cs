using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPX.Models
{
    public class ButtonList
    {
        private ObservableCollection<StackLayout> lista = new ObservableCollection<StackLayout>();
        private List<Label> listaLabels = new List<Label>();

        public Image BtnClose = new Image()
        {
            Source = ImageSource.FromFile("CloseTabIcon.png"),
            WidthRequest = 20,
            HeightRequest = 20,
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            VerticalOptions = LayoutOptions.Center,
            Margin = 6
        };

        public ButtonList()
        {
           
        }

        public void AddButton()
        {

            listaLabels.Add(new Label() { TextColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.CenterAndExpand, FontSize = 12});
            lista.Add(new StackLayout()
            {
                HeightRequest = 35,
                BackgroundColor = Color.FromHex("#242424"),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    listaLabels[listaLabels.Count - 1],
                    BtnClose
                }
            });
            //lista.Add(new Button() {BackgroundColor = Color.FromHex("#242424"), TextColor = Color.White, VerticalOptions = LayoutOptions.EndAndExpand, BorderWidth = 0});

        }



        public StackLayout GetTab(int index)
        {
            return lista[index];
        }

        public Label GetLabel(int index)
        {
            return listaLabels[index];
        }
    }
}
