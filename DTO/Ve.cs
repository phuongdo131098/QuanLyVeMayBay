using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Ve
    {
        private string maVe;

        public string MaVe
        {
            get { return maVe; }
            set { maVe = value; }
        }
        private string maCB;

        public string MaCB
        {
            get { return maCB; }
            set { maCB = value; }
        }
        private string maHHK;

        public string MaHHK
        {
            get { return maHHK; }
            set { maHHK = value; }
        }
        private string maHV;

        public string MaHV
        {
            get { return maHV; }
            set { maHV = value; }
        }
        private int giaTien;

        public int GiaTien
        {
            get { return giaTien; }
            set { giaTien = value; }
        }
        private DateTime thoiGianBay;

        public DateTime ThoiGianBay
        {
            get { return thoiGianBay; }
            set { thoiGianBay = value; }
        }
        private string maTT;

        public string MaTT
        {
            get { return maTT; }
            set { maTT = value; }
        }
    }
}
