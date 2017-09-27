
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPX.Models
{
    public class Tab : BindableObject
    {
        private Pagina _pagina = new Pagina();
        private ListaPaginas _tabHistorial = new ListaPaginas();

        public Pagina Pagina { get { return _pagina; } set { _pagina = value; } }
        public ListaPaginas TabHistorial { get { return _tabHistorial;} set { _tabHistorial = value; } }

        public Tab(string link)
        {
            ChangeLink(link);
        }

        public void ChangeLink(string Link)
        {
            Pagina.link = Link;
            OnPropertyChanged("Pagina");
        }
    }
}
