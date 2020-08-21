/*
    * Yer istasyonu izleme sistemi
    * Sakarya Uygulamalı Bilimler Üniversitesi
    * Tetra Elektromobil Kulübü
    * 2020 TÜBİTAK EFFICIENCY CHALLENGE
    
    * Hazırlayan
    * Mehmet Selçuk CANDAN
    * mehmetselcukcandan@icloud.com
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
/* Projede kullanılan ek kütüphanelerin tanımlamaları */
using OfficeOpenXml; // Excel Log kısmında kullanılan kütüphane
using System.IO; // Port kullanımı için gerekli olan kütüphane
using System.IO.Ports;
using System.Diagnostics;
/* Ek kütüphane tanımlamaları tamamlandı*/

namespace SubuTetraTelemetri
{
    public partial class Form1 : Form
    {
        /* Değişken tanımlamaları */
        SerialPort port = new SerialPort(); // Okuma yapılacak port'un tanımlanması (https://docs.microsoft.com/en-us/dotnet/api/system.io.ports.serialport?view=dotnet-plat-ext-3.1) konfirüsayonu daha sonra yapılıyor -> PortKonf()
        string okunan; // Seri porta yazılan bilgileri okuyup kaydedilen bilgi -> VeriOku()
        int intVeri = 0;
        /* Excel loglaması için gerekli olan değişken tanımlamaları  -> https://github.com/EPPlusSoftware/EPPlus */
        ExcelPackage package; // kütüphane için gerekli değişkenler -> ExcelSetup()
        ExcelWorksheet worksheet; // kütüphane için gerekli değişkenler
        SaveFileDialog dosyaYolu; // Log işlemi uygulanan excel dosyasının kaydedilmesi için gerekli değişken
        int satirSayisi = 1; // Excelde verinin kaydedileceği satır numarasını tutan değişken 
        /* Excel loglaması için gerekli değişken tanımlamaları tamamlandı */
        /* Değişken tanımlamaları tamamlandı */
        // Bilgisayardaki aktif COM portlarını alan ve ekrandaki ComboBox'a ekleyen fonksiyon
        private void PortlariListele()
        {
            string[] ports = SerialPort.GetPortNames();
            portListCombo.Items.AddRange(ports);
            baudrateTextBox.Text = "115200"; // XBEE cihazları 115200 baudrate ile çalıştığı için otomatik baudrate ataması
        }
        // Okuma yapılacak olan seri port'un konfigurasyonu işlemleri
        private void PortKonf()
        {
            port.PortName = portListCombo.SelectedItem.ToString(); // Seçilen seri portun port adı olarak alınması
            port.BaudRate = Convert.ToInt32(baudrateTextBox.Text); // Girilen baudrate değerinin baudrate değeri olarak atanması
            port.StopBits = StopBits.One; // 1 stop biti
            port.Parity = Parity.None; // Parity NONE
            port.DataBits = 8; // 8 bitlik okuma
            port.Open(); // portun aktif edilmesi
        }
        // Seri porttan okunan verinin ayrılması ve işlenmesi
        private void VeriOku(byte[] okunanHex)
        {
            okunan = BitConverter.ToString(okunanHex); // Okunan verinin byte türünden string türüne çevirilmesi
            string[] atanacakVeriler = okunan.Split("-"); // Okunan veriler arasında "-" işareti bulunuyor verileri ayırmak için "-" işaretine göre split ediyoruz ve dizide tutuyoruz
            if (atanacakVeriler.Length == 25 || atanacakVeriler.Length == 28) // Paket 1-2-4 uzunluğu 25, Paket 3 uzunluğu 28
            {
                if (atanacakVeriler[16] == "01") // Split edilmiş verinin 16 indis numaralı öğesinde paket 1'e atadığımız ID var ID kontrol edilerek gerekli pil ve sıcaklık değerleri alınıyor
                {
                    // pil 1 - pil 7
                    pil1label.Text = PilDegerHesabi(atanacakVeriler[17]).ToString();
                    pil2label.Text = PilDegerHesabi(atanacakVeriler[18]).ToString();
                    pil3label.Text = PilDegerHesabi(atanacakVeriler[19]).ToString();
                    pil4label.Text = PilDegerHesabi(atanacakVeriler[20]).ToString();
                    pil5label.Text = PilDegerHesabi(atanacakVeriler[21]).ToString();
                    pil6label.Text = PilDegerHesabi(atanacakVeriler[22]).ToString();
                    hizLabel.Text = Convert.ToInt32(atanacakVeriler[23], 16).ToString();
                }
                if (atanacakVeriler[16] == "02") // Split edilmiş verinin 16 indis numaralı öğesinde paket 1'e atadığımız ID var ID kontrol edilerek gerekli pil ve sıcaklık değerleri alınıyor
                {
                    // pil 8 - pil 14
                    pil8label.Text = PilDegerHesabi(atanacakVeriler[17]).ToString();
                    pil9label.Text = PilDegerHesabi(atanacakVeriler[18]).ToString();
                    pil10label.Text = PilDegerHesabi(atanacakVeriler[19]).ToString();
                    pil11label.Text = PilDegerHesabi(atanacakVeriler[20]).ToString();
                    pil12label.Text = PilDegerHesabi(atanacakVeriler[21]).ToString();
                    pil13label.Text = PilDegerHesabi(atanacakVeriler[22]).ToString();
                    hizLabel.Text = Convert.ToInt32(atanacakVeriler[23], 16).ToString();

                }
                if (atanacakVeriler[16] == "03") // Split edilmiş verinin 16 indis numaralı öğesinde paket 1'e atadığımız ID var ID kontrol edilerek gerekli pil ve sıcaklık değerleri alınıyor
                {
                    // pil 14 - pil 20
                    pil15label.Text = PilDegerHesabi(atanacakVeriler[17]).ToString();
                    pil16label.Text = PilDegerHesabi(atanacakVeriler[18]).ToString();
                    pil17label.Text = PilDegerHesabi(atanacakVeriler[19]).ToString();
                    pil18label.Text = PilDegerHesabi(atanacakVeriler[20]).ToString();
                    pil19label.Text = PilDegerHesabi(atanacakVeriler[21]).ToString();
                    pil20label.Text = PilDegerHesabi(atanacakVeriler[22]).ToString();
                    hizLabel.Text = Convert.ToInt32(atanacakVeriler[23], 16).ToString();
                }
                if (atanacakVeriler[16] == "04") // Split edilmiş verinin 16 indis numaralı öğesinde paket 1'e atadığımız ID var ID kontrol edilerek gerekli pil ve sıcaklık değerleri alınıyor
                {
                    // hız - sıcaklık 1 - sıcaklık 5 - motor gerilimi
                    sicaklik1label.Text = Convert.ToInt32(atanacakVeriler[17], 16).ToString();
                    sicaklik2label.Text = Convert.ToInt32(atanacakVeriler[18], 16).ToString();
                    sicaklik3label.Text = Convert.ToInt32(atanacakVeriler[19], 16).ToString();
                    sicaklik4label.Text = Convert.ToInt32(atanacakVeriler[20], 16).ToString();
                    sicaklik5label.Text = Convert.ToInt32(atanacakVeriler[21], 16).ToString();
                    hizLabel.Text = Convert.ToInt32(atanacakVeriler[22], 16).ToString();
                    motorGerilimiLabel.Text = Convert.ToInt32(atanacakVeriler[23], 16).ToString();
                }
                if (atanacakVeriler[16] == "05") // Split edilmis verinin 16 indis numarali ogesinde paket 1'e atadigimiz ID var ID kontrol gerekli batarya ve motor verilerinin degerini alir
                {
                    pil7label.Text = PilDegerHesabi(atanacakVeriler[17]).ToString();
                    pil14label.Text = PilDegerHesabi(atanacakVeriler[18]).ToString();
                    motorSicakligiDataLabel.Text = Convert.ToInt32(atanacakVeriler[19], 16).ToString();
                    hizLabel.Text = Convert.ToInt32(atanacakVeriler[20], 16).ToString();
                    alinanYolLabel.Text = Convert.ToInt32(atanacakVeriler[21], 16).ToString();
                    socDataLabel.Text = Convert.ToInt32(atanacakVeriler[22], 16).ToString();
                    sohDataLabel.Text = Convert.ToInt32(atanacakVeriler[23], 16).ToString();
                }
                if (excelKaydiCheck.Checked == true) // Kullanıcı excel kaydı yapmayı seçmişse
                {
                    satirSayisi++; // Excel loglamasında 1 satır aşağı geçmesi gerekiyor üstüste binme olmaması için
                    ExcelLog(satirSayisi);
                }
            }
            intVeri++;
            if (intVeri == 20)
            {
                VeriGonder();
                intVeri = 0;
            }
        }
        // String olarak okunan hexadecimal değerin decimale çevirilmesi ve arkasıdan 50'ye bölünerek gerçek değerin elde edilmesi
        private double PilDegerHesabi(string okunanDeger)
        {
            int decimalDeger = Convert.ToInt32(okunanDeger, 16); // hexadecimal -> decimal çevirim
            double deger = decimalDeger / 50.0;
            return deger;
        }
        public Form1()
        {
            InitializeComponent();
        }
        // Bağlantı işleminin başlanması için gereken butona tıklandığında
        private void portBaglanButton_Click(object sender, EventArgs e)
        {
            portBaglanButton.Enabled = false; // Aynı butona tekrar basılmasın diye butonu disable ediyoruz
            portDisconnectButton.Enabled = true; // Disconnect butonunun aktif edilmesi
            PortKonf(); // Port konfigürasyonunu gerçekleyecek fonksiyonun çağırılması
            timer1.Enabled = true; // Programın bağlantı açık olduğu sürece sürekli çalışmasını sağlayacak timer'ın aktif edilmesi
            if (excelKaydiCheck.Checked == true) // Kullanıcı eğer Excel loglamasını aktif ettiyse
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                package = new ExcelPackage();
                package.Workbook.Worksheets.Add("Worksheet1");
                worksheet = package.Workbook.Worksheets.FirstOrDefault();
                ExcelSetup(); // Setup fonksiyonunun çağırılması
            }
        }
        // Program açıldığında gerçeklenecek olan eylemler
        private void Form1_Load(object sender, EventArgs e)
        {
            PortlariListele(); // Aktif COM portlarını listele
            VeriGonder();
        }
        // 200 ms'de bir gerçekleşecek olan veri okuma ve ayıklama işlemleri için gerekli fonksiyonların çağırılması
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (port.IsOpen == true) // Port bağlantısı yapıldıysa
            {
                baglantiDurumuLabel.Text = "Bağlantı Aktif";
                baglantiDurumuLabel.ForeColor = Color.Green;
            }
            /* Seri porta yazılan verilerin okunması */
            int length = port.BytesToRead;
            byte[] buf = new byte[length];
            port.Read(buf, 0, length);
            /* Okuma tamamlandı */
            VeriOku(buf); // Okunan verinin ayıklanması için gerekli fonksiyon
        }
        // Bağlantı kesme butonuna tıklandığında
        private void portDisconnectButton_Click(object sender, EventArgs e)
        {
            port.Close(); // Port bağlantısının kapatılması
            timer1.Enabled = false; // Timer'ın kapatılması
            portBaglanButton.Enabled = true; // Bağlanma butonunun aktif edilmesi
            portDisconnectButton.Enabled = false; // Bağlantı kesme butonunun deaktif edilmesi
            if (excelKaydiCheck.Checked == true) // Log kaydı yapılan excel dosyasının kaydedilmesi
            {
                Stream stream = dosyaYolu.OpenFile();
                package.SaveAs(stream);
                stream.Close();
            }
            baglantiDurumuLabel.Text = "Bağlantı Kapalı"; // Ekrana bağlantının kapalı olduğunun bildirilmesi
            baglantiDurumuLabel.ForeColor = Color.Red;
        }
        // Excel loglaması seçeneği seçilirse hangi sütunda hangi verinin kaydedileceğini belirten fonksiyon
        private void ExcelSetup()
        {
            /*
                1,1 -> 1. Satır 1. Sütun 
                2,1 -> 2. Satır 1. Sütun
                2,2 -> 2. Satır 2. Sütun
            */
            worksheet.Cells[1, 1].Value = "Tarih";
            worksheet.Cells[1, 2].Value = "Pil 1";
            worksheet.Cells[1, 3].Value = "Pil 2";
            worksheet.Cells[1, 4].Value = "Pil 3";
            worksheet.Cells[1, 5].Value = "Pil 4";
            worksheet.Cells[1, 6].Value = "Pil 5";
            worksheet.Cells[1, 7].Value = "Pil 6";
            worksheet.Cells[1, 8].Value = "Pil 7";
            worksheet.Cells[1, 9].Value = "Pil 8";
            worksheet.Cells[1, 10].Value = "Pil 9";
            worksheet.Cells[1, 11].Value = "Pil 10";
            worksheet.Cells[1, 12].Value = "Pil 11";
            worksheet.Cells[1, 13].Value = "Pil 12";
            worksheet.Cells[1, 14].Value = "Pil 13";
            worksheet.Cells[1, 15].Value = "Pil 14";
            worksheet.Cells[1, 16].Value = "Pil 15";
            worksheet.Cells[1, 17].Value = "Pil 16";
            worksheet.Cells[1, 18].Value = "Pil 17";
            worksheet.Cells[1, 19].Value = "Pil 18";
            worksheet.Cells[1, 20].Value = "Pil 19";
            worksheet.Cells[1, 21].Value = "Pil 20";
            worksheet.Cells[1, 22].Value = "Sensor 1";
            worksheet.Cells[1, 23].Value = "Sensor 2";
            worksheet.Cells[1, 24].Value = "Sensor 3";
            worksheet.Cells[1, 25].Value = "Sensor 4";
            worksheet.Cells[1, 26].Value = "Sensor 5";
            worksheet.Cells[1, 27].Value = "Hız";
            worksheet.Cells[1, 28].Value = "Alınan Yol";
            worksheet.Cells[1, 29].Value = "SoC";
            worksheet.Cells[1, 30].Value = "SoH";
            worksheet.Cells[1, 31].Value = "Motor Gerilimi";
            worksheet.Cells[1, 32].Value = "Motor Sicakligi";
        }
        // Excel log yapılacak dosyanın seçilmesi
        private void excelDosyaButton_Click(object sender, EventArgs e)
        {
            dosyaYolu = new SaveFileDialog();
            if (dosyaYolu.ShowDialog() == DialogResult.OK)
            {
                excelDosyaYoluTextBox.Text = dosyaYolu.FileName;
            }
        }
        // Excel Loglaması yapan fonksiyon
        private void ExcelLog(int satirSayisi)
        {
            worksheet.Cells[satirSayisi, 1].Value = DateTime.Now.ToString(); // n. satır 1. sütuna anlık tarih ve saat atanması
            worksheet.Cells[satirSayisi, 2].Value = pil1label.Text;
            worksheet.Cells[satirSayisi, 3].Value = pil2label.Text;
            worksheet.Cells[satirSayisi, 4].Value = pil3label.Text;
            worksheet.Cells[satirSayisi, 5].Value = pil4label.Text;
            worksheet.Cells[satirSayisi, 6].Value = pil5label.Text;
            worksheet.Cells[satirSayisi, 7].Value = pil6label.Text;
            worksheet.Cells[satirSayisi, 8].Value = pil7label.Text;
            worksheet.Cells[satirSayisi, 9].Value = pil8label.Text;
            worksheet.Cells[satirSayisi, 10].Value = pil9label.Text;
            worksheet.Cells[satirSayisi, 11].Value = pil10label.Text;
            worksheet.Cells[satirSayisi, 12].Value = pil11label.Text;
            worksheet.Cells[satirSayisi, 13].Value = pil12label.Text;
            worksheet.Cells[satirSayisi, 14].Value = pil13label.Text;
            worksheet.Cells[satirSayisi, 15].Value = pil14label.Text;
            worksheet.Cells[satirSayisi, 16].Value = pil15label.Text;
            worksheet.Cells[satirSayisi, 17].Value = pil16label.Text;
            worksheet.Cells[satirSayisi, 18].Value = pil17label.Text;
            worksheet.Cells[satirSayisi, 19].Value = pil18label.Text;
            worksheet.Cells[satirSayisi, 20].Value = pil19label.Text;
            worksheet.Cells[satirSayisi, 21].Value = pil20label.Text;
            worksheet.Cells[satirSayisi, 22].Value = sicaklik1label.Text;
            worksheet.Cells[satirSayisi, 23].Value = sicaklik2label.Text;
            worksheet.Cells[satirSayisi, 24].Value = sicaklik3label.Text;
            worksheet.Cells[satirSayisi, 25].Value = sicaklik4label.Text;
            worksheet.Cells[satirSayisi, 26].Value = sicaklik5label.Text;
            worksheet.Cells[satirSayisi, 27].Value = hizLabel.Text;
            worksheet.Cells[satirSayisi, 28].Value = alinanYolLabel.Text;
            worksheet.Cells[satirSayisi, 29].Value = socDataLabel.Text;
            worksheet.Cells[satirSayisi, 30].Value = sohDataLabel.Text;
            worksheet.Cells[satirSayisi, 31].Value = motorGerilimiLabel.Text;
            worksheet.Cells[satirSayisi, 32].Value = motorSicakligiDataLabel.Text;
           
        }
        // Elde edilen butun verilerin bir python scripti ile internete gonderilmesi
        private void VeriGonder()
        {
            // Python scriptinin yazilmasi icin 
            var psi = new ProcessStartInfo();
            psi.FileName = @"..\..\..\..\env\python.exe"; // python runner exe pythoin runner exe relative yapilmali
            var script = @"..\..\..\SendData.py"; // yazdigimiz scriptin lokasyonu
            // Okunan ve gerekli yere kaydedilen verilerin calistirilacak python scriptine verilecek olan parametre olarak duzenlenmesi
            var veriler = "a=" + pil1label.Text + "&b=" + pil2label.Text + "&c=" + pil3label.Text + "&d=" + pil4label.Text + "&e=" + pil5label.Text + "&f=" + pil6label.Text+
                "&g=" + pil7label.Text + "&h=" + pil8label.Text + "&i=" + pil9label.Text + "&j=" + pil10label.Text + "&k=" + pil11label.Text + "&l=" + pil12label.Text + 
                "&m=" + pil13label.Text + "&n=" + pil14label.Text + "&o=" + pil15label.Text + "&p=" + pil16label.Text + "&q=" + pil17label.Text + "&r=" + pil18label.Text +
                "&s=" + pil19label.Text + "&t=" + pil20label.Text + "&u=" + sicaklik1label.Text + "&v=" + sicaklik2label.Text + "&w=" + sicaklik3label.Text +
                "&x=" + sicaklik4label.Text + "&y=" + sicaklik5label.Text + "&z=" + hizLabel.Text + "&z1=" + alinanYolLabel.Text + 
                "&z2=" + socDataLabel.Text + "&z3=" + sohDataLabel.Text + "&z4=" + motorGerilimiLabel.Text + "&z5=" + motorSicakligiDataLabel.Text;

            psi.Arguments = $"\"{script}\" \"{veriler}\""; // Python scriptini calistirmak icin script yolunu ve argumanlarimizi veriyoruz
            psi.UseShellExecute = false; // shell execute kapatma
            psi.CreateNoWindow = true; // yeni bir pencere yaratma
            psi.RedirectStandardOutput = true; // standart outputu aktar
            psi.RedirectStandardError = true; // standart erroru aktar
            var errors = ""; // script calisirken bir error gelirse
            var results = ""; // script calistiktan sonra dondurdugu deger
            // Konfigurasyon tamamlandi
            using (var process = Process.Start(psi)) // scripti calistir
            {
                errors = process.StandardError.ReadToEnd(); // error varsa degiskene aktar
                results = process.StandardOutput.ReadToEnd(); // sonuclari degiskene aktar
            }
        }
    } 
}
