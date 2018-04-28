using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class ChiTietCB
    {
        private string maCB;

        public string MaCB
        {
            get { return maCB; }
            set { maCB = value; }
        }
        private string maSBTG;

        public string MaSBTG
        {
            get { return maSBTG; }
            set { maSBTG = value; }
        }
        private int thoiGianDung;

        public int ThoiGianDung
        {
            get { return thoiGianDung; }
            set { thoiGianDung = value; }
        }
        private string ghiChu;

        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }

        public ChiTietCB(string maCB, string maSBTG, int thoiGianDung, string ghiChu)
        {
            this.maCB = maCB;
            this.maSBTG = maSBTG;
            this.thoiGianDung = thoiGianDung;
            this.ghiChu = ghiChu;
        }
    }
}
