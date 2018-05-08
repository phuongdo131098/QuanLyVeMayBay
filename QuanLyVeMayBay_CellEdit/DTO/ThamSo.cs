using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class ThamSo
    {
        private string maTS;

        public string MaTS
        {
            get { return maTS; }
            set { maTS = value; }
        }
        private string tenTS;

        public string TenTS
        {
            get { return tenTS; }
            set { tenTS = value; }
        }
        private int giaTri;

        public int GiaTri
        {
            get { return giaTri; }
            set { giaTri = value; }
        }
        private DateTime ngayApDung;

        public DateTime NgayApDung
        {
            get { return ngayApDung; }
            set { ngayApDung = value; }
        }
        public ThamSo(string maTS, string tenTS, int giaTri, DateTime ngayApDung)
        {
            this.maTS = maTS;
            this.tenTS = tenTS;
            this.giaTri = giaTri;
            this.ngayApDung = ngayApDung;
        }
    }
}
