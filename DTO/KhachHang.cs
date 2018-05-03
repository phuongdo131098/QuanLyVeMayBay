using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class KhachHang
    {
        private string maKH;

        public string MaKH
        {
            get { return maKH; }
            set { maKH = value; }
        }

        private string hoTen;

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }
        private string cmnd;

        private int tuoi;

        public int Tuoi
        {
            get { return tuoi; }
            set { tuoi = value; }
        }
        private bool gioiTinh;

        public bool GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }
        public string Cmnd
        {
            get { return cmnd; }
            set { cmnd = value; }
        }
        private string diaChi;

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }
        private string sdt;

        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }

        public KhachHang(string maKH, string hoTen, int tuoi, string cmnd, string diaChi, string sdt)
        {
            this.maKH = maKH;
            this.hoTen = hoTen;
            this.tuoi = tuoi;
            this.cmnd = cmnd;
            this.diaChi = diaChi;
            this.sdt = sdt;
        }
    }
}
