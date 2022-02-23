using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pastane_Otomasyon
{
    public partial class Saticilar : Form
    {
        public Saticilar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AIKEIHG\SA;Initial Catalog=Pastane;Integrated Security=True");



        public void Listele(string baglan)
        {

            SqlDataAdapter dr = new SqlDataAdapter(baglan, baglanti);
            DataSet doldur = new DataSet();
            dr.Fill(doldur);
            dataGridView1.DataSource = doldur.Tables[0];



        }

        private void btnListele_Click(object sender, EventArgs e)
        {

            Listele("Select * From Satıcı");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            SqlCommand komut = new SqlCommand("insert into Satıcı (SaticiAdSoyad,SaticiAdres,Saticiİl,Saticiİlce) values(@SaticiAdSoyad,@SaticiAdres,@Saticiİl,@Saticiİlce)",baglanti);

            komut.Parameters.AddWithValue("@SaticiAdSoyad", txtSaticiAdSoyad.Text);
            komut.Parameters.AddWithValue("@SaticiAdres", richTextBox1.Text);
            komut.Parameters.AddWithValue("@Saticiİl", txtİl.Text);
            komut.Parameters.AddWithValue("@Saticiİlce", txtİlce.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();
            Listele("Select * From Satıcı");


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Satıcı set   SaticiAdSoyad='"+txtSaticiAdSoyad.Text.ToString()+"', SaticiAdres='" + richTextBox1.Text.ToString() + "',Saticiİl='" + txtİl.Text.ToString() + "',Saticiİlce='" + txtİlce.Text.ToString() + "'where SaticiNo= '"+txtSaticiNo.Text.ToString()+"' ",baglanti);
            komut.ExecuteNonQuery();
            Listele("Select * From Satıcı");
            baglanti.Close();
           
        }

       
        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from Satıcı where SaticiNo=@SaticiNo",baglanti);
            komut.Parameters.AddWithValue("@SaticiNo", txtSaticiNo.Text);

            komut.ExecuteNonQuery();
            Listele("Select * From Satıcı");
            baglanti.Close();
          



        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;

            string SaticiNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string SaticiAdSoyad = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string SaticiAdres = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string Saticiİl = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string Saticiİlce = dataGridView1.Rows[secim].Cells[4].Value.ToString();



            txtSaticiNo.Text = SaticiNo;
            txtSaticiAdSoyad.Text = SaticiAdSoyad;
            richTextBox1.Text = SaticiAdres;
            txtİl.Text = Saticiİl;
            txtİlce.Text = Saticiİlce;

        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Satıcı where SaticiAdSoyad like '%" + txtSaticiAdSoyad.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            Form1 geridön = new Form1();

            geridön.Show();

            this.Hide();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
