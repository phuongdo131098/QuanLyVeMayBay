using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class ChuyenBay
    {
        private string maCB;
        public string MaCB
        {
            get { return maCB; }
            set { maCB = value; }
        }

        private string sanBayDi;
        public string SanBayDi
        {
            get { return sanBayDi; }
            set { sanBayDi = value; }
        }
        private string sanBayDen;
        public string SanBayDen
        {
            get { return sanBayDen; }
            set { sanBayDen = value; }
        }
        private string maHHK;

        public string MaHHK
        {
            get { return maHHK; }
            set { maHHK = value; }
        }
        private int thoiGianBay;

        public int ThoiGianBay
        {
            get { return thoiGianBay; }
            set { thoiGianBay = value; }
        }
        private int soGheHang1;

        public int SoGheHang1
        {
            get { return soGheHang1; }
            set { soGheHang1 = value; }
        }
        private int soGheHang2;

        public int SoGheHang2
        {
            get { return soGheHang2; }
            set { soGheHang2 = value; }
        }
        private int giaVe;

        public int GiaVe
        {
            get { return giaVe; }
            set { giaVe = value; }
        }
    }
}
