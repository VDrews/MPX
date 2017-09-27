using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.ComponentModel;

namespace MPX.Models
{
    [Table("Paginas")]
    public class Pagina : BindableObject
    {
        private String _link;

        [PrimaryKey]
        public String link {
            get { return _link; }
            set
            {
                _link = value;
                OnPropertyChanged(nameof(link));
            } }

    }

}
