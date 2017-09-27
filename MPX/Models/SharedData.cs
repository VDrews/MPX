using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPX.Models;

namespace MPX
{
    public class SharedData
    {

        private static SharedData oInstance;

        private ListaPaginas tabList;

        protected SharedData()
        {
        }

        public static SharedData Instance()
        {

            if (oInstance == null)
                oInstance = new SharedData();

            return oInstance;

        }


        public ListaPaginas TabList
        {
            get
            {
                if (tabList == null)
                    tabList = new ListaPaginas();
                return tabList;
            }
        }

    }
}
