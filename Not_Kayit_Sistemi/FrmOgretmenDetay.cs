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

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }
        public void dersdurum()
        {
            label1gecensayısı.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == true).ToString();
            label1kalansayısı.Text = dbNotKayitDataSet.TBLDERS.Count(x => x.DURUM == false).ToString();
        }
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dbNotKayitDataSet.TBLDERS". Sie können sie bei Bedarf verschieben oder entfernen.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
           
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=KOBOOM\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True");
        public void listele()
        {
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
            dersdurum();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBLDERS (ogrnumara,ograd,soyad,ogrs1,ogrs2,ogrs3) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            baglanti.Open();
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", mskAd.Text);
            komut.Parameters.AddWithValue("@p3", mskSoyad.Text);
            komut.Parameters.AddWithValue("@p4", mskSinav1.Text);
            komut.Parameters.AddWithValue("@p5", mskSinav2.Text);
            komut.Parameters.AddWithValue("@p6", mskSinav3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme Yapıldı","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mskNumara.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            mskAd.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            mskSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            mskSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            mskSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            mskSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            labelOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            string gecen = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            if(bool.Parse(gecen)==true)
            {
                labelGecen.Text = "GEÇTİ";
            }
            else
            {
                labelGecen.Text = "KALDI";
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            double s1, s2, s3, ortalama;
            string durum;

            s1 = Convert.ToDouble(mskSinav1.Text);
            s2 = Convert.ToDouble(mskSinav2.Text);
            s3 = Convert.ToDouble(mskSinav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            labelOrtalama.Text = ortalama.ToString();
            if(ortalama>=50)
            {
                durum = "true";
            }
            else
            {
                durum = "false";
            
            }
            

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tblders set ogrs1=@p1,ogrs2=@p2,ogrs3=@p3,ortalama=@p4,durum=@p5 where ogrnumara=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", mskSinav1.Text);
            komut.Parameters.AddWithValue("@p2", mskSinav2.Text);
            komut.Parameters.AddWithValue("@p3", mskSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(labelOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara.Text);
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            mskNumara.Text = "";
            mskAd.Text = "";
            mskSoyad.Text = "";
            mskSinav1.Text = "";
            mskSinav2.Text = "";
            mskSinav3.Text = "";
        }
    }
}
