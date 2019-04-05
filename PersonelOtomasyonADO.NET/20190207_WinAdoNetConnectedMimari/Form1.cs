using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;// Sql server bağlantısı ve classlarının bulunduğu ktüphane
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20190207_WinAdoNetConnectedMimari
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=.;Database=SuskunlarDB;User=sa;pwd=1");
        //baglanti.ConnectionString = "Server=.;Database=SuskunlarDB;User=sa;Pwd=1";

        void firstRunSettings()
        {
            cmbCinsiyet.DropDownStyle = cmbMedeniHal.DropDownStyle = ComboBoxStyle.DropDownList;
            fillCombos();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            firstRunSettings();
            getPersoneller();
        }

        private void getPersoneller()
        {
            lstPersoneller.Items.Clear();
            // veri tabanımızı oluşturduk ve artık biz bir komut(sorgu) lazım (personelleri listeleyecek
            SqlCommand cmd = new SqlCommand("Select * from Personeller", baglanti);
            baglanti.Open();//bağlantıyı açtık
            // Artık okumam lazım
            SqlDataReader okuyucu = cmd.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem li = new ListViewItem();

                li.Tag = Convert.ToInt32(okuyucu["Id"].ToString());

                li.Text = okuyucu["Id"].ToString();
                li.SubItems.Add(okuyucu[1].ToString());//Adi
                li.SubItems.Add(okuyucu[2].ToString());//Soyadi
                li.SubItems.Add(okuyucu["Yas"].ToString());
                li.SubItems.Add(okuyucu["Meslek"].ToString());
                li.SubItems.Add(okuyucu["Memleket"].ToString());
                li.SubItems.Add(Convert.ToBoolean(okuyucu["Cinsiyet"].ToString()) == true ? "Kadın" : "Erkek");
                li.SubItems.Add(okuyucu["KanGrubu"].ToString());
                li.SubItems.Add(okuyucu["GozRengi"].ToString());
                li.SubItems.Add(okuyucu["Telefon"].ToString());
                li.SubItems.Add(okuyucu["TCKN"].ToString());
                li.SubItems.Add(okuyucu["Boy"].ToString());
                li.SubItems.Add(okuyucu["Kilo"].ToString());
                li.SubItems.Add(okuyucu["Maas"].ToString());
                li.SubItems.Add(Convert.ToBoolean(okuyucu["MedeniHal"].ToString()) == true ? "Evli" : "Bekar");
                li.SubItems.Add(okuyucu["DogumTarihi"].ToString());
                lstPersoneller.Items.Add(li);
            }
            baglanti.Close();// Bağlantıyı açık bırakmamalıyım
        }
        private void getPersoneller(string topkomut, string orderbyKomut)
        {
            lstPersoneller.Items.Clear();
            SqlCommand cmd = new SqlCommand($"Select {topkomut} * from Personeller order by {orderbyKomut}", baglanti);
            baglanti.Open();
            
            SqlDataReader okuyucu = cmd.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem li = new ListViewItem();

                li.Tag = Convert.ToInt32(okuyucu["Id"].ToString());

                li.Text = okuyucu["Id"].ToString();
                li.SubItems.Add(okuyucu[1].ToString());//Adi
                li.SubItems.Add(okuyucu[2].ToString());//Soyadi
                li.SubItems.Add(okuyucu["Yas"].ToString());
                li.SubItems.Add(okuyucu["Meslek"].ToString());
                li.SubItems.Add(okuyucu["Memleket"].ToString());
                li.SubItems.Add(Convert.ToBoolean(okuyucu["Cinsiyet"].ToString()) == true ? "Kadın" : "Erkek");
                li.SubItems.Add(okuyucu["KanGrubu"].ToString());
                li.SubItems.Add(okuyucu["GozRengi"].ToString());
                li.SubItems.Add(okuyucu["Telefon"].ToString());
                li.SubItems.Add(okuyucu["TCKN"].ToString());
                li.SubItems.Add(okuyucu["Boy"].ToString());
                li.SubItems.Add(okuyucu["Kilo"].ToString());
                li.SubItems.Add(okuyucu["Maas"].ToString());
                li.SubItems.Add(Convert.ToBoolean(okuyucu["MedeniHal"].ToString()) == true ? "Evli" : "Bekar");
                li.SubItems.Add(okuyucu["DogumTarihi"].ToString());
                lstPersoneller.Items.Add(li);
            }
            baglanti.Close();// Bağlantıyı açık bırakmamalıyım
        }
        void fillCombos()
        {
            cmbTop.Items.Clear();
            cmbTop.Items.Add("Hepsi");
            cmbTop.Items.Add(10);
            cmbTop.Items.Add(25);
            cmbTop.Items.Add(50);
            cmbTop.Items.Add(100);

            cmbSirala.Items.Clear();
            cmbSirala.Items.Add("Default");
            cmbSirala.Items.Add("Ada Göre");
            cmbSirala.Items.Add("Soyada Göre");
            cmbSirala.Items.Add("Yaşa Göre");
            cmbSirala.Items.Add("Boya Göre");
            cmbSirala.Items.Add("Maaşa Göre");

            cmbCinsiyet.Items.Clear();
            cmbMedeniHal.Items.Clear();

            cmbCinsiyet.Items.Add("Erkek");
            cmbCinsiyet.Items.Add("Kadın");

            cmbMedeniHal.Items.Add("Bekar");
            cmbMedeniHal.Items.Add("Evli");

            txtAdi.Text = txtSoyadi.Text = txtMemleket.Text = txtMeslek.Text = txtKanGrubu.Text = txtGozRengi.Text = mtxtTelefon.Text = mtxtTCKN.Text = txtBoy.Text = txtKilo.Text = "";

            numMaas.Value = 0;
            numYas.Value = 18;

            cmbSirala.SelectedIndex = cmbTop.SelectedIndex =
                cmbMedeniHal.SelectedIndex = cmbCinsiyet.SelectedIndex = 0;
        }

        bool checkControlsValues()
        {
            if (string.IsNullOrEmpty(txtAdi.Text))
            {
                MessageBox.Show("Lütfen personel adını girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (string.IsNullOrEmpty(txtSoyadi.Text))
            {
                MessageBox.Show("Lütfen personel soyadını girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (string.IsNullOrEmpty(txtMeslek.Text))
            {
                MessageBox.Show("Lütfen personel mesleğini girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (string.IsNullOrEmpty(txtMemleket.Text))
            {
                MessageBox.Show("Lütfen personel memleket bilgisini girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (string.IsNullOrEmpty(txtKanGrubu.Text))
            {
                MessageBox.Show("Lütfen personel kan grubu bilgisini girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (string.IsNullOrEmpty(txtGozRengi.Text))
            {
                MessageBox.Show("Lütfen personel göz rengi bilgisini girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!mtxtTCKN.MaskFull)
            {
                MessageBox.Show("Lütfen personel TCKN bilgisini girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!mtxtTelefon.MaskFull)
            {
                MessageBox.Show("Lütfen personel telefon numarasını girin.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (!checkControlsValues())
                return;


            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            //komut.CommandText = $"insert into Personeller(Adi,Soyadi,Yas) values('{txtAdi.Text}', '{txtSoyadi.Text}','{numYas.Value}')";// Sql Injection 

            //insert into Personeller(Adi,Soyadi,Yas) values('', '', '')
            //Tuncay','Kurt','25') drop table BankaHesaplari --
            komut.CommandText = "insert into Personeller(Adi, Soyadi,Yas, TCKN, KanGrubu, Cinsiyet, Meslek, Telefon, Boy, Kilo, Maas, MedeniHal, Memleket, GozRengi, DogumTarihi) values(@ad, @soyad, @yas, @tc, @kangr, @cins, @meslek, @tel, @boy, @kilo, @maas, @medeniHal, @memleket, @goz, @dt)";
            komut.Parameters.AddWithValue("@ad", txtAdi.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyadi.Text);
            komut.Parameters.AddWithValue("@yas", numYas.Value);
            komut.Parameters.AddWithValue("@tc", mtxtTCKN.Text);
            komut.Parameters.AddWithValue("@kangr", txtKanGrubu.Text);
            komut.Parameters.AddWithValue("@cins", cmbCinsiyet.SelectedIndex);
            komut.Parameters.AddWithValue("@meslek", txtMeslek.Text);
            komut.Parameters.AddWithValue("@tel", mtxtTelefon.Text);
            komut.Parameters.AddWithValue("@boy", txtBoy.Text);
            komut.Parameters.AddWithValue("@kilo", txtKilo.Text);
            komut.Parameters.AddWithValue("@maas", numMaas.Value);
            komut.Parameters.AddWithValue("@medeniHal", cmbMedeniHal.SelectedIndex);
            komut.Parameters.AddWithValue("@memleket", txtMemleket.Text);
            komut.Parameters.AddWithValue("@goz", txtGozRengi.Text);
            komut.Parameters.AddWithValue("@dt", dtpDogum.Value);

            baglanti.Open();
            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();
            if (sonuc > 0)
            {
                MessageBox.Show("Kayıt Eklendi.");
                getPersoneller();
            }
            else
                MessageBox.Show("Personel Eklenirken Hata Oluştu", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void Kisi_Sil(object sender, EventArgs e)
        {
            if (lstPersoneller.SelectedItems[0] == null)
                return;

            ListViewItem seciliSatir = lstPersoneller.SelectedItems[0];

            string adSoyad = seciliSatir.SubItems[1].Text + " " + seciliSatir.SubItems[2].Text;
            //int id=int.Parse(seciliSatir.Text);
            int id = (int)seciliSatir.Tag;

            DialogResult silme = MessageBox.Show($"{adSoyad} adlı personeli silmek istediğinizden emin misiniz?", "Kişi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (silme == DialogResult.No)
            {
                MessageBox.Show("İşlem iptal edildi.");
                return;
            }

            SqlCommand silmeKomutu = new SqlCommand();
            silmeKomutu.Connection = baglanti;
            silmeKomutu.CommandText = "Delete Personeller where Id=@id";
            silmeKomutu.Parameters.AddWithValue("@id", id);
            baglanti.Open();
            int sonuc = silmeKomutu.ExecuteNonQuery();
            baglanti.Close();
            if (sonuc > 0)
                MessageBox.Show($"{adSoyad} adlı personel listeden silindi.");
            else
                MessageBox.Show("Kayıt silinirken bir hata oluştu.");

            getPersoneller();

        }

        private void Duzenle_Click(object sender, EventArgs e)
        {
            if (lstPersoneller.SelectedItems[0] == null)
                return;

            ListViewItem secili = lstPersoneller.SelectedItems[0];

            txtAdi.Text = secili.SubItems[1].Text;
            txtSoyadi.Text = secili.SubItems[2].Text;
            numYas.Value = Convert.ToDecimal(secili.SubItems[3].Text);
            txtMeslek.Text = secili.SubItems[4].Text;
            txtMemleket.Text = secili.SubItems[5].Text;
            cmbCinsiyet.SelectedIndex = secili.SubItems[6].Text == "Kadın" ? 1 : 0;
            txtKanGrubu.Text = secili.SubItems[7].Text;
            txtGozRengi.Text = secili.SubItems[8].Text;
            mtxtTelefon.Text = secili.SubItems[9].Text;
            mtxtTCKN.Text = secili.SubItems[10].Text;
            txtBoy.Text = secili.SubItems[11].Text;
            txtKilo.Text = secili.SubItems[12].Text;
            numMaas.Value = Convert.ToDecimal(secili.SubItems[13].Text);
            cmbMedeniHal.SelectedIndex = secili.SubItems[14].Text == "Evli" ? 1 : 0;
            dtpDogum.Value = Convert.ToDateTime(secili.SubItems[15].Text);

            txtAdi.Tag = secili.Tag;
            btnEkle.Enabled = false;

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "Update Personeller set Adi=@ad, Soyadi=@soyad,Yas=@yas, TCKN=@tc, KanGrubu=@kangr, Cinsiyet=@cins, Meslek=@meslek, Telefon=@tel, Boy=@boy, Kilo=@kilo, Maas=@maas, MedeniHal=@medeniHal, Memleket=@memleket, GozRengi=@goz, DogumTarihi=@dt where Id=@id";
            cmd.Parameters.AddWithValue("@ad", txtAdi.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyadi.Text);
            cmd.Parameters.AddWithValue("@yas", numYas.Value);
            cmd.Parameters.AddWithValue("@tc", mtxtTCKN.Text);
            cmd.Parameters.AddWithValue("@kangr", txtKanGrubu.Text);
            cmd.Parameters.AddWithValue("@cins", cmbCinsiyet.SelectedIndex);
            cmd.Parameters.AddWithValue("@meslek", txtMeslek.Text);
            cmd.Parameters.AddWithValue("@tel", mtxtTelefon.Text);
            cmd.Parameters.AddWithValue("@boy", txtBoy.Text);
            cmd.Parameters.AddWithValue("@kilo", txtKilo.Text);
            cmd.Parameters.AddWithValue("@maas", numMaas.Value);
            cmd.Parameters.AddWithValue("@medeniHal", cmbMedeniHal.SelectedIndex);
            cmd.Parameters.AddWithValue("@memleket", txtMemleket.Text);
            cmd.Parameters.AddWithValue("@goz", txtGozRengi.Text);
            cmd.Parameters.AddWithValue("@dt", dtpDogum.Value);
            cmd.Parameters.AddWithValue("@id", txtAdi.Tag);

            baglanti.Open();
            int s = cmd.ExecuteNonQuery();
            baglanti.Close();

            if (s > 0)
                MessageBox.Show("Kayıt başarıyla güncellendi.");
            else
                MessageBox.Show("Güncelleme gerçekleştirilemedi.");
            btnGuncelle.Enabled = false;
            btnEkle.Enabled = true;
            fillCombos();
            getPersoneller();
        }

        private void btnFiltre_Click(object sender, EventArgs e)
        {
            string topCmd = "";
            switch (cmbTop.SelectedIndex)
            {
                case 1:
                    topCmd = "Top 10";
                    break;
                case 2:
                    topCmd = "Top 25";
                    break;
                case 3:
                    topCmd = "Top 50";
                    break;
                case 4:
                    topCmd = "Top 100";
                    break;
                default:
                    topCmd = "";
                    break;
            }

            string orderbyCmd = "";
            switch (cmbSirala.SelectedIndex)
            {
                case 1:
                    orderbyCmd = "Adi";
                    break;
                case 2:
                    orderbyCmd = "Soyadi";
                    break;
                case 3:
                    orderbyCmd = "Yas";
                    break;
                case 4:
                    orderbyCmd = "Boy";
                    break;
                case 5:
                    orderbyCmd = "Maas";
                    break;
                default:
                    orderbyCmd = "";
                    break;
                
            }

            getPersoneller(topCmd, orderbyCmd);
        }
    }
}
