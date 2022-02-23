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
    public partial class Ürünler : Form
    {
        public Ürünler()
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
            Listele("Select * From Urunler");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            SqlCommand komut = new SqlCommand("insert into Urunler (UrunAdi,UrunFiyat,KullanimTarihi,UretimTarihi,SaticiNo) values(@UrunAdi,@UrunFiyat,@KullanimTarihi,@UretimTarihi,@SaticiNo)", baglanti);

            komut.Parameters.AddWithValue("@UrunAdi", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@UrunFiyat", txtUrunFiyat.Text);
            komut.Parameters.AddWithValue("@KullanimTarihi", dateKullanım.Text);
            komut.Parameters.AddWithValue("@UretimTarihi", dateUretim.Text);
            komut.Parameters.AddWithValue("@SaticiNo", comboSaticiNo.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();
            Listele("Select * From Urunler");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Urunler set UrunFiyat='"+txtUrunFiyat.Text.ToString()+"'where UrunNo='"+txtUrunNo.Text.ToString()+"' ",baglanti);
            komut.ExecuteNonQuery();
            Listele("Select * From Urunler");
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;

            string UrunNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string UrunAdi = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string UrunFiyat = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string KullanimTarih = dataGridView1.Rows[secim].Cells[3].Value.ToString();
            string UretimTarih = dataGridView1.Rows[secim].Cells[4].Value.ToString();
            string SaticiNo = dataGridView1.Rows[secim].Cells[5].Value.ToString();

            txtUrunNo.Text = UrunNo;
            txtUrunAdi.Text = UrunAdi;
            txtUrunFiyat.Text = UrunFiyat;
            dateKullanım.Text = KullanimTarih;
            dateUretim.Text = UretimTarih;
            comboSaticiNo.Text = SaticiNo;


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Delete from Urunler where UrunNo=@UrunNo",baglanti);
            komut.Parameters.AddWithValue("@UrunNo", txtUrunNo.Text);

            komut.ExecuteNonQuery();
            Listele("Select * From Urunler");
            baglanti.Close();

        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Urunler where UrunNo like '%" + txtUrunNo.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void Ürünler_Load(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Satıcı", baglanti);

            SqlDataReader dr;
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboSaticiNo.Items.Add(dr["SaticiNo"]);
            }


            baglanti.Close();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            Form1 geridön = new Form1();

            geridön.Show();
            this.Hide();
        }
    }
}
