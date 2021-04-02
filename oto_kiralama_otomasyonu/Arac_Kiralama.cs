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
    public partial class Arac_Kiralama : Form
    {
        public Arac_Kiralama()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = oto_kiralama.accdb; Persist Security Info=True");
        DataTable dt = new DataTable();
        private void Arac_Kiralama_Load(object sender, EventArgs e)
        {
            musteridoldur();
            uygunaracdoldur();
            doldur();
        }
        void doldur()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dt.Clear();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from kiralama", baglanti);
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[3].Width = 77;
                dataGridView1.Columns[4].Width = 76;
                dataGridView1.Columns[5].Width = 76;
             
                baglanti.Close();
             
                comboBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
          
                comboBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
        void musteridoldur()
        {
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select tc from musteri",baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["tc"].ToString());
            }
            baglanti.Close();
        }
        void uygunaracdoldur()
        {
            comboBox2.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select plaka from arac where durum='"+ "Uygun" + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }
       

        private void button5_Click(object sender, EventArgs e)
        {
            menu menu = new menu();
            menu.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = dateTimePicker2.Value.ToShortDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("insert into kiralama(tc,plaka,alis_tarihi,veris_tarihi,ucret) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglanti);
                //
                // 
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                komut.ExecuteNonQuery();
             
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    OleDbCommand guncelle = new OleDbCommand("update arac set durum ='"+"Kirada"+"'where plaka='"+comboBox2.Text+"' ",baglanti);
                    guncelle.ExecuteNonQuery();
                    uygunaracdoldur();
                }
                catch (Exception)
                {

                    throw;
                }
                MessageBox.Show("Ekleme İşleminiz Başarılı");
                baglanti.Close();

              doldur();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
            {
                try
                {

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    OleDbCommand sil = new OleDbCommand("delete from kiralama where plaka='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'", baglanti);
                    sil.ExecuteNonQuery();
                    uygunaracdoldur();
                    musteridoldur();
                    MessageBox.Show("Silme İşleminiz Başarılı");
                    doldur();
                    baglanti.Close();

                }
                catch
                {

                    ;
                }
            }
    }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            dt.Clear();

            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From kiralama where tc like'%" + textBox7.Text + "%'or plaka like '%" + textBox7.Text + "%'or alis_tarihi like '%" + textBox7.Text + "%'or veris_tarihi like '%" + textBox7.Text + "%'or ucret like '%" + textBox7.Text + "%'", baglanti);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 77;
            dataGridView1.Columns[4].Width = 76;
            dataGridView1.Columns[5].Width = 76;

            baglanti.Close();

            comboBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
    
            comboBox1.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("update kiralama set tc='"+comboBox1.Text+"',plaka='"+comboBox2.Text+"',alis_tarihi='"+textBox3.Text+"',veris_tarihi='"+textBox4.Text+"',ucret='"+textBox5.Text+ "' where plaka='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'",baglanti);
            // 
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            doldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Parse(textBox4.Text);
                DateTime dt2 = DateTime.Parse(textBox3.Text);
                TimeSpan fark = dt - dt2;
                textBox5.Text = (fark.TotalDays * 150).ToString();

            }
            catch
            { 

                ;
            }
        }
    }
}