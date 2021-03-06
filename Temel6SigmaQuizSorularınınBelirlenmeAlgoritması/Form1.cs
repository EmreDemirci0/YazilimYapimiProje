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
using System.Data.Sql;
using Bunifu.Framework.UI;

namespace Temel6SigmaQuizSorularınınBelirlenmeAlgoritması
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
       

        public static string epostaStringi;
        private void btn_kayıtOl_Click(object sender, EventArgs e)
        {
            frm_kayitOl formKayitOl = new frm_kayitOl();
            formKayitOl.Show();
            

        }
        private void btn_girisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut;
            SqlDataReader dr;
            epostaStringi = txt_girisEposta.Text;

            if (radioButton1.Checked)
            {
                komut = new SqlCommand($"select * from tblOgrenci where email='{txt_girisEposta.Text}' and sifre='{ txt_girisSifre.Text}'", sqlBaglantisi.baglanti());
                dr = komut.ExecuteReader();
                if (dr.Read())//ogrenci
                {
                    MessageBox.Show("Giriş Başarılı: "+radioButton1.Text);
                    frm_ogrPanel frm_OgrPanel = new frm_ogrPanel();
                    frm_OgrPanel.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız.Email yada şifre hatalıdır.");
                }
                dr.Close();
            }
            if (radioButton2.Checked)//admin
            {
                komut = new SqlCommand($"select * from tblAdmin where email='{txt_girisEposta.Text}' and sifre='{ txt_girisSifre.Text}'", sqlBaglantisi.baglanti());
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş Başarılı:" + radioButton2.Text);
                    frm_AdminPanel frm_AdminPanel = new frm_AdminPanel();
                    frm_AdminPanel.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız.Email yada şifre hatalıdır.");

                }
                dr.Close();
            }
            if (radioButton3.Checked)//sorumlu
            {
                komut = new SqlCommand($"select * from tblSinavSorumlusu where email='{txt_girisEposta.Text}' and sifre='{ txt_girisSifre.Text}'", sqlBaglantisi.baglanti());
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş Başarılı:" + radioButton3.Text);
                    frm_Sorumlu frmSorumlu = new frm_Sorumlu();
                    frmSorumlu.Show();
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız.Email yada şifre hatalıdır.");

                }

                dr.Close();
               
            }

            sqlBaglantisi.baglanti().Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
          
            if (sqlBaglantisi.baglanti().State == ConnectionState.Closed)
                sqlBaglantisi.baglanti().Open();
            if (sqlBaglantisi.baglanti().State==ConnectionState.Open)
            {
                sqlBaglantisi.baglanti().Close();
            }
            radioButton1.Checked = true;
            txt_girisEposta.Focus();
           
        }

        private void btn_sifreUnuttum_Click(object sender, EventArgs e)
        {
            FrmSifremiUnuttum frmSifremiUnuttum = new FrmSifremiUnuttum();
            frmSifremiUnuttum.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btn_kayıtOl.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            btn_kayıtOl.Visible = false;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btn_kayıtOl.Visible = true;
        }

        private void picture_close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void picture_max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void pictureBox_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        int TogMove;
        int MValX;
        int MValY;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove==1)
            {
                this.SetDesktopLocation(MousePosition.X -MValX,MousePosition.Y-MValY);
            }
        }
    }
}
