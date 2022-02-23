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
    public partial class Sipariş : Form
    {
        public Sipariş()
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
            Listele("Select * From Sipariş");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

          


            baglanti.Open();


            SqlCommand komut = new SqlCommand("insert into Sipariş (SiparisAdi,SiparisAdres,SiparisAdet,SiparisFiyat,UrunNo,Tutar) values(@SiparisAdi,@SiparisAdres,@SiparisAdet,@SiparisFiyat,@UrunNo,@Tutar)", baglanti);

            komut.Parameters.AddWithValue("@SiparisAdi", txtSiparisAd.Text);
            komut.Parameters.AddWithValue("@SiparisAdres", richSiparişAdres.Text);
            komut.Parameters.AddWithValue("@SiparisAdet", txtSiparisAdet.Text);
            komut.Parameters.AddWithValue("@SiparisFiyat", txtSiparisFiyat.Text);
            komut.Parameters.AddWithValue("@UrunNo", comboUrunNo.Text);
            komut.Parameters.AddWithValue("@Tutar", txtTutar.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();
            Listele("Select * From Sipariş");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Sipariş set SiparisAdi='" + txtSiparisAd.Text.ToString()+"',SiparisAdres='"+richSiparişAdres.Text+"',SiparisAdet='"+txtSiparisAdet.Text.ToString()+"',SiparisFiyat='"+txtSiparisFiyat.Text.ToString()+"' where SiparisNo='"+txtSiparisNo.Text.ToString()+"' ", baglanti);
            komut.ExecuteNonQuery();
           
            baglanti.Close();
            Listele("Select * From Sipariş");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Delete from Sipariş where SiparisNo=@SiparisNo", baglanti);
            komut.Parameters.AddWithValue("@SiparisNo", txtSiparisNo.Text);

            komut.ExecuteNonQuery();
            Listele("Select * From Sipariş");

            txtSiparisNo.Clear();
            baglanti.Close();
        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Sipariş where SiparisAdi like '%" + txtSiparisAd.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

     

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;

            string SiparisNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string SiparisAdi = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string SiparisAdres = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string SiparisAdet = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string SiparisFiyat = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            string UrunNo = dataGridView1.Rows[secim].Cells[5].Value.ToString();

            txtSiparisNo.Text = SiparisNo;
            txtSiparisAd.Text = SiparisAdi;
            richSiparişAdres.Text = SiparisAdres;
            txtSiparisAdet.Text = SiparisAdet;
            txtSiparisFiyat.Text = SiparisFiyat;
            comboUrunNo.Text = UrunNo;


            baglanti.Open();

             int adet = Convert.ToInt32(txtSiparisAdet.Text);
             decimal fiyat = Convert.ToDecimal(txtSiparisFiyat.Text);

             decimal toplamfiyat = adet * fiyat;

            txtTutar.Text = toplamfiyat.ToString();

            SqlCommand komut = new SqlCommand("Update Sipariş set Tutar='"+txtTutar.Text.ToString()+"' where SiparisNo='"+txtSiparisNo.Text.ToString()+"'", baglanti);

         
            komut.Parameters.AddWithValue("@Tutar", txtTutar.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();

            Listele("Select * From Sipariş");

        }

        private void Sipariş_Load(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Urunler", baglanti);

            SqlDataReader dr;
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboUrunNo.Items.Add(dr["UrunNo"]);
            }

         
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

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

