using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPX.Models;
using Xamarin.Forms;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MPX
{
    public class ListaPaginas : BindableObject
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        private ObservableCollection<Pagina> lista { get; set; }
        private int Puntero = 0;

        public ListaPaginas()
        {
            database =
                DependencyService.Get<IDatabaseConnection>().
                    DbConnection();
            database.CreateTable<Pagina>();
            this.lista =
                new ObservableCollection<Pagina>(database.Table<Pagina>());
            // If the table is empty, initialize the collection
            if (!database.Table<Pagina>().Any())
            {
                SetPagina(new Pagina() {link = "http://www.google.es/"});
            }

        }

        public void SetPagina(Pagina pagina)
        {
            lista.Add(pagina);
            Puntero++;
            OnPropertyChanged("lista");
        }

        public void SetNewPagina(Pagina pagina)
        {

            for (int i = ++Puntero; i < lista.Count; i++)
                lista.RemoveAt(i);
            lista.Add(pagina);
        }

        public Pagina GetPaginaAnterior()
        {
            Puntero-=2;
            return lista[Puntero];
        }

        public Pagina GetPaginaSiguiente(bool set)
        {
            Puntero++;
            if (Puntero < lista.Count)
            {
                if (set)
                {
                    return lista[Puntero];
                }

                else return lista[Puntero];

            }
            else return null;
        }

        public void Clear()
        {
            lista.Clear();
            OnPropertyChanged("lista");
        }

        public void Delete(Pagina param)
        {
            lista.Remove(param);
            OnPropertyChanged("lista");
        }

        public void DeleteLast()
        {
            lista.RemoveAt(lista.Count - 1);
        }

        public ObservableCollection<Pagina> GetPagina()
        {
            return lista;
        }
    }
}
