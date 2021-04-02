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
    public partial class arac_Teslim : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = oto_kiralama.accdb; Persist Security Info=True");
        DataTable dt = new DataTable();
        public arac_Teslim()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
        }
        void uygunaracdoldur()
        {
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select plaka from arac where durum='" + "Kirada" + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            OleDbCommand guncelle = new OleDbCommand("update arac set durum ='" + "Uygun" + "'where plaka='" + comboBox1.Text + "' ", baglanti);
            guncelle.ExecuteNonQuery();
            uygunaracdoldur();
            baglanti.Close();
            MessageBox.Show("Araç Kiralamaya Uygun Hale Getirilmiştir.");
            comboBox1.Text = "";
        }

        private void arac_Teslim_Load(object sender, EventArgs e)
        {
            uygunaracdoldur();
        }
    }
}
