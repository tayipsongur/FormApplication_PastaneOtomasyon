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
    public partial class Müşteriler : Form
    {
        public Müşteriler()
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
            Listele("Select * From Müsteriler");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            SqlCommand komut = new SqlCommand("insert into Müsteriler (MusteriAdSoyad,MusteriTelefon,SiparisNo) values(@MusteriAdSoyad,@MusteriTelefon,@SiparisNo)", baglanti);

            komut.Parameters.AddWithValue("@MusteriAdSoyad",txtMüsteriAdSoyad.Text);
            komut.Parameters.AddWithValue("@MusteriTelefon", mskdTelefon.Text);
            komut.Parameters.AddWithValue("@SiparisNo", comboSiparis.Text);
           
            komut.ExecuteNonQuery();

            baglanti.Close();
            Listele("Select * From Müsteriler");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Müsteriler set MusteriAdSoyad='" + txtMüsteriAdSoyad.Text.ToString() + "',MusteriTelefon='"+mskdTelefon.Text.ToString() +"' where MusteriNo='"+txtMüsteriNo.Text.ToString()+ "' ", baglanti);
            komut.ExecuteNonQuery();
            Listele("Select * From Müsteriler");
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            int secim = dataGridView1.SelectedCells[0].RowIndex;

            string MusteriNo = dataGridView1.Rows[secim].Cells[0].Value.ToString();
            string MusteriAdSoyad = dataGridView1.Rows[secim].Cells[1].Value.ToString();
            string MusteriTelefon = dataGridView1.Rows[secim].Cells[2].Value.ToString();
            string SiparisNo = dataGridView1.Rows[secim].Cells[3].Value.ToString();

            txtMüsteriNo.Text = MusteriNo;
            txtMüsteriAdSoyad.Text = MusteriAdSoyad;
            mskdTelefon.Text = MusteriTelefon;
            comboSiparis.Text = SiparisNo;

          

            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Delete  From Müsteriler where MusteriNo=@MusteriNo", baglanti);

            komut.Parameters.AddWithValue("@MusteriNo", txtMüsteriNo.Text);

            Listele("Select * From Müsteriler");

            komut.ExecuteNonQuery();
            baglanti.Close();
            
        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Müsteriler where MusteriAdSoyad like '%" + txtMüsteriAdSoyad.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void Müşteriler_Load(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From Sipariş", baglanti);

            SqlDataReader dr;
            dr = komut.ExecuteReader();

            while (dr.Read())
            {
                comboSiparis.Items.Add(dr["SiparisNo"]);
            }


            baglanti.Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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
