using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pastane_Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            Müşteriler musterigit = new Müşteriler();
            musterigit.Show();
            this.Hide();
           
        }

        private void btnSatici_Click(object sender, EventArgs e)
        {
            Saticilar saticigit = new Saticilar();
            saticigit.Show();
            this.Hide();
          
        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            Ürünler ürüngit = new Ürünler();

            ürüngit.Show();
            this.Hide();
      
        }

        private void btnSiparisler_Click(object sender, EventArgs e)
        {
            Sipariş siparisgit = new Sipariş();
            siparisgit.Show();
            this.Hide();
           
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
