using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace oto_kiralama_otomasyonu
{
    public partial class girisform : Form
    {
        public girisform()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=oto_kiralama.accdb;Persist Security Info=True");
            //access bağlantıyı sağladık
            OleDbCommand komut = new OleDbCommand("select * from musteri where tc='" + textBox5.Text + "' and sifre ='" + textBox1.Text + "'", baglanti);
            //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
            // çekmesini istedik
            baglanti.Open();//bağlantıyı açdık

            OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
            if (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
            {
                MessageBox.Show("Giriş Başarılı !");//giriş başarılı diye uyari verir
                baglanti.Close();//bağlantıyı kapar
                musteri menu = new musteri();//yeni bir menü sayfası oluşturur
                menu.Show();//menü sayfasını açar
                this.Hide();////mevcut sayfayı gizler

            }
            else
            {
                MessageBox.Show("Kullanıcı Adınız Yada Şifreniz Yanlış Yazılmıştır");//hayır veri okuyamadıysa uyarı verir
                textBox1.Text = "";
                textBox2.Text = "";
                //verileri temizler
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=oto_kiralama.accdb;Persist Security Info=True");
            //access bağlantıyı sağladık
            OleDbCommand komut = new OleDbCommand("select * from admin where k_adi='" + textBox3.Text + "' and sifre ='" + textBox2.Text + "'", baglanti);
            //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
            // çekmesini istedik
            baglanti.Open();//bağlantıyı açdık

            OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
            if (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
            {
                MessageBox.Show("Giriş Başarılı !");//giriş başarılı diye uyari verir
                baglanti.Close();//bağlantıyı kapar
                menu menu = new menu();//yeni bir menü sayfası oluşturur
                menu.Show();//menü sayfasını açar
                this.Hide();////mevcut sayfayı gizler

            }
            else
            {
                MessageBox.Show("Kullanıcı Adınız Yada Şifreniz Yanlış Yazılmıştır");//hayır veri okuyamadıysa uyarı verir
                textBox1.Text = "";
                textBox2.Text = "";
                //verileri temizler
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            musteri_ol mstr = new musteri_ol();
            mstr.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremi_unuttum sfr = new sifremi_unuttum();
            sfr.Show();
            this.Hide();
        }
    }
}
