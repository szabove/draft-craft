using DraftCraft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DraftCraft.ViewModels
{
    public class ProizvodIndexViewModel
    {
        public IPagedList<Proizvod> Proizvodi { get; set; }
        public int PageItems { get; set; }
        public Dictionary<int,string> PageItemNumber { get; set; }
        public string Search { get; set; }
        public IEnumerable<KategorijeIbrojac> KatSbrojacem { get; set; }
        public string Kategorija { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string,string> Sorts { get; set; }

        public IEnumerable<SelectListItem> KatFilter
        {
            get
            {
                var sveKat = KatSbrojacem.Select(cc => new SelectListItem
                {
                    Value = cc.NazivKategorije,
                    Text = cc.KatScount
                });
                return sveKat;
            }
        }



    }

    public class KategorijeIbrojac
    {
        public int ProizvodiCount { get; set; }
        public string NazivKategorije { get; set; }
        public string KatScount
        {
            get
            {
                return NazivKategorije + " (" + ProizvodiCount.ToString() + ")";
            }
        }

    }
}