using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPX.Models
{
    public class TabList : BindableObject
    {
        private ObservableCollection<Xamarin.Forms.WebView> lista = new ObservableCollection<Xamarin.Forms.WebView>();
        private ListaPaginas TabHistorial = new ListaPaginas();
        

        public TabList()
        {
            lista.Add(new Xamarin.Forms.WebView() {Source = "https://www.google.es/"});
            TabHistorial.SetNewPagina(new Pagina() {link = "https://www.google.es/"});
            OnPropertyChanged("lista");
        }

        public void AddTab()
        {
            lista.Add(new Xamarin.Forms.WebView() {Source = "https://www.google.es"});
            OnPropertyChanged("lista");
        }


        public Xamarin.Forms.WebView GetTab(int index)
        {
            return lista[index];
        }

        public void Clear()
        {
            lista.Clear();
            OnPropertyChanged("lista");
        }

        public void Delete(Xamarin.Forms.WebView param)
        {
            lista.Remove(param);
            OnPropertyChanged("lista");
        }

        public ObservableCollection<Xamarin.Forms.WebView> GetTabs()
        {
            return lista;
        }
    }
}
