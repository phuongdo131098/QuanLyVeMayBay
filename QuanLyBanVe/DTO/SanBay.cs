using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class SanBay
    {
        private string maSB;

        public string MaSB
        {
            get { return maSB; }
            set { maSB = value; }
        }
        private string tenSB;

        public string TenSB1
        {
            get { return tenSB; }
            set { tenSB = value; }
        }
        public SanBay(string maSB, string tenSB)
        {
            this.maSB = maSB;
            this.tenSB = tenSB;
        }
    }
}
