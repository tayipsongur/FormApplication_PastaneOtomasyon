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
    public partial class KullaniciGiris : Form
    {
        public KullaniciGiris()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-AIKEIHG\SA;Initial Catalog=Pastane;Integrated Security=True");
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("insert into KullaniciGiris (KullaniciAd,KullaniciSifre,Email,Telefon) values(@KullaniciAd,@KullaniciSifre,@Email,@Telefon)", baglanti);

            komut.Parameters.AddWithValue("@KullaniciAd", txtKulAdi.Text);
            komut.Parameters.AddWithValue("@KullaniciSifre", txtKulSifre.Text);
            komut.Parameters.AddWithValue("@Email", txtMail.Text);
            komut.Parameters.AddWithValue("@Telefon", mskdTelefon.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();

        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From KullaniciGiris where KullaniciAd=@KullaniciAd and KullaniciSifre=@KullaniciSifre", baglanti);

            

            komut.Parameters.AddWithValue("@KullaniciAd", txtkullaniciad.Text);
            komut.Parameters.AddWithValue("@KullaniciSifre", txtkullanicisifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı","BAŞARILI",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Form1 anasayfagit = new Form1();
                anasayfagit.Show();

                this.Hide();
             
               
            }
            else
            {
                MessageBox.Show("KULLANICI ADI VEYA ŞİFRE HATALI","HATA",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            
        }

        private void KullaniciGiris_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void KullaniciGiris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btnGirisYap.PerformClick();
            }
        }
    }
}
