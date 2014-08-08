using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_Encode
{
    public partial class frmMain : Form
    {
        DataEncode dataEncoding = new DataEncode();
        EncodeHelper qrHelper = new EncodeHelper();
        public frmMain()
        {
            InitializeComponent();
        }

        //This is an extremely absurd function name but it does do magic
        public void doMagic()
        {

        }
    }
}
