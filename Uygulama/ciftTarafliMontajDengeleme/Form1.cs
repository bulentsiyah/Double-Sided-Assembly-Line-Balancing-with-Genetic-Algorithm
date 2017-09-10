using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ciftTarafliMontajDengeleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int NesilSayisi;
        int BireySayisi;
        int GenSayisi;
        int SabitGenSayisi;
        int[] GenlerinSureleri;
        int[] ilkToplumBireyUygunlugu;
        ArrayList ilkToplumBireyUygunluguHesaplama = new ArrayList();
        Random Sayi = new Random();
        int[,] ilkToplum;
        int[,] YeniToplum;
        int OrtalamaCalismaSuresi;
        int ElitSayisi;
        int SolEsikDegeri;
        int SagEsikDegeri;
        String LblSolYazi;
        String LblSagYazi;
        int MutasyonSayisi;
        Bitmap bitmap;
        int istasyonlarToplamiSabitSol;
        int istasyonlarToplamiSabitSag;
        int solOnunDegeri;
        int sagDokuzunDegeri;
        private void basla_Click(object sender, EventArgs e)
        {
            ilkDegerleriOlustur();
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    GenSayisi = 8;
                    GenlerinSureleri = new int[GenSayisi + 1];
                    ilkToplum = new int[BireySayisi + 1, GenSayisi + 1];
                    YeniToplum = new int[BireySayisi + 1, GenSayisi + 1];
                    GorevSureleriniGenlereYaz(i);
                    istasyonlarToplamiSabitSol = 394;
                    Genetik();
                }
                else
                {
                    GenSayisi = 12;
                    GenlerinSureleri = new int[GenSayisi + 1];
                    ilkToplum = new int[BireySayisi + 1, GenSayisi + 1];
                    YeniToplum = new int[BireySayisi + 1, GenSayisi + 1];
                    GorevSureleriniGenlereYaz(i);
                    istasyonlarToplamiSabitSag = 264;
                    Genetik();
                   
                    if (sagDokuzunDegeri >= solOnunDegeri)
                    {
                        //MessageBox.Show("sol" + solOnunDegeri + "-----sag" + sagDokuzunDegeri);
                        basla.PerformClick();
                    }
                }
            }
        }
        public void ilkDegerleriOlustur()
        {
            try
            {
                NesilSayisi = Convert.ToInt32(txtNesil.Text);
                BireySayisi = Convert.ToInt32(txtBirey.Text);
            }
            catch
            {
                MessageBox.Show("Girilen Değerler Geçerli Değil.Rakam Giriniz.");
                Application.Exit();
            }

            if (NesilSayisi <= 1 || BireySayisi <= 1 || NesilSayisi >= 250 || BireySayisi >= 250)
            {
                MessageBox.Show("Girilen Değerler Geçerli Değil. Değerler '1' dan Büyük ve 250 den Küçük Olmalıdır.");
                Application.Exit();
            }
            OrtalamaCalismaSuresi = 260;
            ElitSayisi = 2;
            ilkToplumBireyUygunlugu = new int[BireySayisi + 1];
            MutasyonSayisi = 2;
            SolEsikDegeri = 35;
            SagEsikDegeri = 35;
            LblSolYazi = "";
            LblSagYazi = "";
            SabitGenSayisi = 8;
            GrafikSol.Image = new Bitmap(GrafikSol.Width, GrafikSol.Height);
            GrafikSag.Image = new Bitmap(GrafikSag.Width, GrafikSag.Height);
            solOnunDegeri = 0;
            sagDokuzunDegeri = 0;
        }
        public void GorevSureleriniGenlereYaz(int SolveyaSag)
        {
            int[] SolGorevSureleri = { 0, 186, 165, 130, 132, 124, 79, 74, 65 };
            int[] SagGorevSureleri = { 0, 217, 150, 59, 124, 130, 223, 79, 189, 72, 84, 62, 72 };
            if (SolveyaSag == 1)
            {
                for (int j = 1; j <= SolGorevSureleri.Length - 1; j++)
                {
                    GenlerinSureleri[j] = SolGorevSureleri[j];
                }
            }
            else
            {
                for (int j = 1; j <= SagGorevSureleri.Length - 1; j++)
                {
                    GenlerinSureleri[j] = SagGorevSureleri[j];
                }
            }
        }
        public void Genetik()
        {
            ilkToplumOlustur();
            for (int ii = 1; ii <= NesilSayisi; ii++)
            {
                Uygunluk();
                Elitizm();
                Caprazlama();
                Mutasyon();
                ToplumDegistir();
               // ekran();
                if (ii == NesilSayisi)
                {
                    
                    SonUygunluk();
                    EkranCiz();
                }
            }
        }
        public void ekran()
        {
            // asagı full ekran
            string bbb = "";
            for (int kk = 1; kk <= BireySayisi; kk++)
            {
                string bb = "";
                for (int kkk = 1; kkk <= GenSayisi; kkk++)
                {
                    bb = bb + " " + ilkToplum[kk, kkk];
                }
                bbb = bbb + "\n" + "birey " + kk + " gen :" + bb + "uygun: " + ilkToplumBireyUygunlugu[kk];
            }
            MessageBox.Show(bbb);
        }
        public void ilkToplumOlustur()
        {
            ArrayList randomSayilar = new ArrayList();
            for (int iii = 1; iii <= BireySayisi; iii++)
            {
                randomSayilar.Clear();
                for (int jjj = 1; jjj <= GenSayisi; jjj++)
                {
                    int randomSayi = Sayi.Next(1, GenSayisi + 1);
                    if (randomSayilar.IndexOf(randomSayi) == -1)
                    {
                        randomSayilar.Add(randomSayi);
                    }
                    else
                    {
                        jjj = jjj - 1;
                    }
                    if (jjj == GenSayisi)
                    {
                        randomSayilar = kisitKontrol(randomSayilar);

                        for (int jk = 1; jk <= GenSayisi; jk++)
                        {
                            ilkToplum[iii, jk] = Convert.ToInt32(randomSayilar[jk - 1]);
                        }
                    }
                }
            }
        }
        public void Uygunluk()
        {
            ArrayList UygunlukistasyonDizileri = new ArrayList();
            int deger = 0;
            int esik;
            if (GenSayisi > SabitGenSayisi)
            {
                esik = SagEsikDegeri;
            }
            else
            {
                esik = SolEsikDegeri;
            }
            for (int k = 1; k <= BireySayisi; k++)
            {
                UygunlukistasyonDizileri.Clear();
                ilkToplumBireyUygunlugu[k] = 0;
                for (int l = 1; l <= GenSayisi; l++)
                {
                    UygunlukistasyonDizileri.Add(ilkToplum[k, l]);
                    deger = UygunlukDegerleri(UygunlukistasyonDizileri);
                    if (deger <= esik || l == GenSayisi)
                    {
                        ilkToplumBireyUygunlugu[k] = ilkToplumBireyUygunlugu[k] + deger;
                        UygunlukistasyonDizileri.Clear();
                    }
                }
            }
        }
        public int UygunlukDegerleri(ArrayList GelenDiziKumesi)
        {
            int degertopla = 0;
            int degertoplakalan = 0;
            int degertoplabolum = 0;
            int uygunlukDegeri = 0;
            for (int kkk = 0; kkk < GelenDiziKumesi.Count; kkk++)
            {
                degertopla = degertopla + GenlerinSureleri[Convert.ToInt32(GelenDiziKumesi[kkk])];
            }
            degertoplakalan = degertopla % OrtalamaCalismaSuresi;
            degertoplabolum = degertopla / OrtalamaCalismaSuresi;
            uygunlukDegeri = OrtalamaCalismaSuresi - degertoplakalan;
            uygunlukDegeri = uygunlukDegeri * (degertoplabolum + 1);
            if (GelenDiziKumesi.Count == GenSayisi)
            {
                uygunlukDegeri = degertopla;
            }
            return uygunlukDegeri;
        }
        public void Elitizm()
        {
            int EnKucuk, EnKucukindex;
            for (int elit = 1; elit <= BireySayisi; elit++)
            {
                EnKucuk = 100000;
                EnKucukindex = 0;
                int gecici = 0, gecici2 = 0;
                for (int elit2 = elit; elit2 <= BireySayisi; elit2++)
                {
                    if (ilkToplumBireyUygunlugu[elit2] < (EnKucuk))
                    {
                        EnKucuk = ilkToplumBireyUygunlugu[elit2];
                        EnKucukindex = elit2;
                    }
                }
                gecici = ilkToplumBireyUygunlugu[elit];
                ilkToplumBireyUygunlugu[elit] = ilkToplumBireyUygunlugu[EnKucukindex];
                ilkToplumBireyUygunlugu[EnKucukindex] = gecici;
                for (int degisenGenler = 1; degisenGenler <= GenSayisi; degisenGenler++)
                {
                    gecici2 = ilkToplum[elit, degisenGenler];
                    ilkToplum[elit, degisenGenler] = ilkToplum[EnKucukindex, degisenGenler];
                    ilkToplum[EnKucukindex, degisenGenler] = gecici2;
                }
            }
            for (int index = 1; index <= ElitSayisi; index++)
            {
                for (int bb = 1; bb <= GenSayisi; bb++)
                {
                    YeniToplum[index, bb] = ilkToplum[index, bb];
                }
            }
        }
        public void Caprazlama()
        {
            int galip1, galip2, rastgeleBirey1, rastgeleBirey2, rastgeleBirey3, rastgeleBirey4, lopus1, lopus2, gecicilopus;
            for (int carpazi = ElitSayisi + 1; carpazi <= BireySayisi; carpazi = carpazi + 2)
            {
                if (carpazi == BireySayisi)
                {
                    carpazi--;
                }
                rastgeleBirey1 = Sayi.Next(1, BireySayisi);
                rastgeleBirey2 = Sayi.Next(1, BireySayisi);
                rastgeleBirey3 = Sayi.Next(1, BireySayisi);
                rastgeleBirey4 = Sayi.Next(1, BireySayisi);
                lopus1 = Sayi.Next(2, GenSayisi - 1);
                lopus2 = Sayi.Next(2, GenSayisi - 1);
                if (lopus1 > lopus2)
                {
                    gecicilopus = lopus1;
                    lopus1 = lopus2;
                    lopus2 = gecicilopus;
                }
                if (rastgeleBirey1 > rastgeleBirey2)
                {
                    galip1 = rastgeleBirey2;
                }
                else
                {
                    galip1 = rastgeleBirey1;
                }
                if (rastgeleBirey3 > rastgeleBirey4)
                {
                    galip2 = rastgeleBirey4;
                }
                else
                {
                    galip2 = rastgeleBirey3;
                }
                for (int b = 1; b < lopus1; b++)
                {
                    YeniToplum[carpazi, b] = ilkToplum[galip1, b];
                    YeniToplum[carpazi + 1, b] = ilkToplum[galip2, b];
                }
                for (int b = (lopus1); b < lopus2; b++)
                {
                    YeniToplum[carpazi, b] = ilkToplum[galip2, b];
                    YeniToplum[carpazi + 1, b] = ilkToplum[galip1, b];
                }
                for (int b = (lopus2); b <= GenSayisi; b++)
                {
                    YeniToplum[carpazi, b] = ilkToplum[galip1, b];
                    YeniToplum[carpazi + 1, b] = ilkToplum[galip2, b];
                }
                for (int hataliBirey = carpazi; hataliBirey <= carpazi + 1; hataliBirey++)
                {
                    ArrayList HataliGenDuzelt = new ArrayList();
                    ArrayList EksikGenDuzelt = new ArrayList();
                    for (int hataliGen = 1; hataliGen <= GenSayisi; hataliGen++)
                    {
                        if (HataliGenDuzelt.IndexOf(YeniToplum[hataliBirey, hataliGen]) == -1)
                        {
                            HataliGenDuzelt.Add(YeniToplum[hataliBirey, hataliGen]);
                        }
                        else
                        {
                            HataliGenDuzelt.Add("*");
                        }
                    }
                    int ii = 0;
                    for (int EksikGen = 1; EksikGen <= GenSayisi; EksikGen++)
                    {
                        if (HataliGenDuzelt.IndexOf(EksikGen) == -1)
                        {
                            EksikGenDuzelt.Insert(ii, EksikGen);
                            ii++;
                            // MessageBox.Show("bb"+EksikGen);
                        }
                    }
                    while (HataliGenDuzelt.Contains("*"))
                    {
                        int hataGen = HataliGenDuzelt.IndexOf("*");
                        int rastgeleEksik = Sayi.Next(0, EksikGenDuzelt.Count);
                        //MessageBox.Show("rast " + rastgeleEksik);
                        //MessageBox.Show("rast karsılık " + EksikGenDuzelt[rastgeleEksik]);
                        HataliGenDuzelt.RemoveAt(hataGen);
                        HataliGenDuzelt.Insert(hataGen, EksikGenDuzelt[rastgeleEksik]);
                        EksikGenDuzelt.RemoveAt(rastgeleEksik);
                    }
                    HataliGenDuzelt = kisitKontrol(HataliGenDuzelt);
                    for (int arrayiGeneAta = 1; arrayiGeneAta <= GenSayisi; arrayiGeneAta++)
                    {
                        YeniToplum[hataliBirey, arrayiGeneAta] = Convert.ToInt32(HataliGenDuzelt[arrayiGeneAta - 1]);
                    }
                }
            }
        }
        public void Mutasyon()
        {
            int MutandBirey, mutgen1, mutgen2;
            int sabitgen1, sabitgen2;
            for (int mutand = 1; mutand <= MutasyonSayisi; mutand++)
            {
                MutandBirey = Sayi.Next(1, BireySayisi + 1);
                mutgen1 = Sayi.Next(0, GenSayisi);
                mutgen2 = Sayi.Next(0, GenSayisi);
                if (mutgen1 == mutgen2)
                {
                    mutand--;
                    continue;
                }
                ArrayList MutasyonDuzelt = new ArrayList();
                for (int MutasGen = 1; MutasGen <= GenSayisi; MutasGen++)
                {
                    MutasyonDuzelt.Add(YeniToplum[MutandBirey, MutasGen]);
                }
                sabitgen1 = Convert.ToInt32(MutasyonDuzelt[mutgen1]);
                sabitgen2 = Convert.ToInt32(MutasyonDuzelt[mutgen2]);
                MutasyonDuzelt.RemoveAt(mutgen1);
                MutasyonDuzelt.Insert(mutgen1, sabitgen2);
                MutasyonDuzelt.RemoveAt(mutgen2);
                MutasyonDuzelt.Insert(mutgen2, sabitgen1);
                MutasyonDuzelt = kisitKontrol(MutasyonDuzelt);
                for (int MutanGenAta = 1; MutanGenAta <= GenSayisi; MutanGenAta++)
                {
                    YeniToplum[MutandBirey, MutanGenAta] = Convert.ToInt32(MutasyonDuzelt[MutanGenAta - 1]);
                }
            }
        }
        public void ToplumDegistir()
        {
            for (int toplumDegistirBirey = 1; toplumDegistirBirey <= BireySayisi; toplumDegistirBirey++)
            {
                for (int toplumDegistirGen = 1; toplumDegistirGen <= GenSayisi; toplumDegistirGen++)
                {
                    ilkToplum[toplumDegistirBirey, toplumDegistirGen] = YeniToplum[toplumDegistirBirey, toplumDegistirGen];
                }
            }
        }
        public void SonUygunluk()
        {
            int YedininYeriDokuz = 0;
            int UcunYeriOn = 0;
            ArrayList UygunlukistasyonDizileri = new ArrayList();
            int deger = 0;
            int esik;
            int istasyonSayisi = 0;
            if (GenSayisi > SabitGenSayisi)
            {
                esik = SagEsikDegeri;
                istasyonSayisi = -1;
            }
            else
            {
                esik = SolEsikDegeri;
                istasyonSayisi = 0;
            }
            String gengrubu1 = "", gengrubu2 = "", gengrubu3 = "";
            int toplamGenDeger = 0;
            UygunlukistasyonDizileri.Clear();
            ilkToplumBireyUygunlugu[1] = 0;
            for (int l = 1; l <= GenSayisi; l++)
            {
                UygunlukistasyonDizileri.Add(ilkToplum[1, l]);
                deger = UygunlukDegerleri(UygunlukistasyonDizileri);
                if (deger <= esik || l == GenSayisi)
                {
                    ilkToplumBireyUygunlugu[1] = ilkToplumBireyUygunlugu[1] + deger;
                    istasyonSayisi = istasyonSayisi + 2;
                    toplamGenDeger = 0;
                    gengrubu2 = "";
                    for (int gecici_degisken = 0; gecici_degisken < UygunlukistasyonDizileri.Count; gecici_degisken++)
                    {
                        if (GenSayisi > SabitGenSayisi)
                        {
                            if (UygunlukistasyonDizileri.IndexOf(7) != -1)
                            {
                                YedininYeriDokuz = (istasyonSayisi / 2) + 1;
                                sagDokuzunDegeri = YedininYeriDokuz;
                               // MessageBox.Show("sag Degeri: " + YedininYeriDokuz);
                            }
                        }
                        else
                        {
                            if (UygunlukistasyonDizileri.IndexOf(3) != -1)
                            {
                                UcunYeriOn = (istasyonSayisi / 2);
                                solOnunDegeri = UcunYeriOn;
                                //MessageBox.Show("sol Degeri: " + UcunYeriOn);
                            }
                        }
                        toplamGenDeger = toplamGenDeger + GenlerinSureleri[Convert.ToInt32(UygunlukistasyonDizileri[gecici_degisken])];
                        gengrubu2 = gengrubu2 + " " + OperasyonlariDuzelt(UygunlukistasyonDizileri[gecici_degisken].ToString(), GenSayisi);

                         }
                    gengrubu1 = istasyonSayisi.ToString() + ".İstasyon KayıpSüre= " + deger + " dk." + "  ToplamSüre= " + toplamGenDeger + " dk." + "       Atanan Operasyonlar: ";
                    gengrubu3 = gengrubu3 + "\n" + gengrubu1 + gengrubu2;
                    UygunlukistasyonDizileri.Clear();
                }
                if (l == GenSayisi)
                {
                    ilkToplumBireyUygunluguHesaplama.Add(gengrubu3);
                }
            }
        }
        public void EkranCiz()
        {
            string ekranLabeli = "";
            ekranLabeli = ilkToplumBireyUygunluguHesaplama[0].ToString() + "\n" + "Toplam Kayıp Zaman=" + ilkToplumBireyUygunlugu[1] + " dk.";
            if (GenSayisi > SabitGenSayisi)
            {
                LblSagYazi = "\n" + "HATTIN  SAĞ TARAFI" + "\n" + ekranLabeli;
                ResimCizdir(20, 450 - istasyonlarToplamiSabitSag, 1); //
                ResimCizdir(50, 450 - ilkToplumBireyUygunlugu[1], 2);//
            }
            else
            {
                LblSolYazi = "HATTIN  SOL TARAFI" + "\n" + ekranLabeli;
                ResimCizdir(20, 450 - istasyonlarToplamiSabitSol, 1);//
                ResimCizdir(50, 450 - ilkToplumBireyUygunlugu[1], 2); //
            }
            lblEkranYazi.Text = LblSolYazi + "\n" + LblSagYazi;
          
            ilkToplumBireyUygunluguHesaplama.Clear();
        }
        public void ResimCizdir(int x, int y, int Normal)
        {
            int eskix = x;
            int eskiy = 450;
            bitmap = (Bitmap)GrafikSol.Image;
            Graphics g = Graphics.FromImage(bitmap);
            if (GenSayisi > SabitGenSayisi)
            {
                bitmap = (Bitmap)GrafikSag.Image;
                g = Graphics.FromImage(bitmap);
            }
            if (Normal == 1)
            {
                g.DrawString((450 - y).ToString(), new Font("Microsoft Sans Serif", 12), new SolidBrush(Color.Black), x - 20, y - 25);
                g.FillEllipse(new SolidBrush(Color.DarkRed), x - 5, y - 5, 10, 10);
                g.DrawLine(new Pen(Color.DarkRed, 5), (float)eskix, (float)eskiy, (float)x, (float)y);
            }
            else
            {
                g.DrawString((450 - y).ToString(), new Font("Microsoft Sans Serif", 12), new SolidBrush(Color.Black), x - 20, y - 25);
                g.FillEllipse(new SolidBrush(Color.DarkBlue), x - 5, y - 5, 10, 10);
                g.DrawLine(new Pen(Color.DarkBlue, 5), (float)eskix, (float)eskiy, (float)x, (float)y);
            }
            if (GenSayisi > SabitGenSayisi)
            {
                GrafikSag.Image = bitmap;
            }
            else
            {
                GrafikSol.Image = bitmap;
            }
        }
        public String OperasyonlariDuzelt(String OperasyonNumarasi, int GenSayisi)
        {
            String[] SolOperatorler = { "0", "7", "8", "10", "11", "12", "13", "14", "17" };
            String[] SagOperatorler = { "0", "1", "2", "3", "4", "5", "6", "9", "15", "16", "18", "19", "20" };
            String DonusDegeri = "";
            if (GenSayisi > SabitGenSayisi)
            {
                DonusDegeri = SagOperatorler[Convert.ToInt32(OperasyonNumarasi)];
            }
            else
            {
                DonusDegeri = SolOperatorler[Convert.ToInt32(OperasyonNumarasi)];
            }
            return DonusDegeri;
        }
        public ArrayList kisitKontrol(ArrayList kisitGenDuzelt)
        {
            int sabitgen1, sabitgen2, sabitgen11;

            if (GenSayisi > SabitGenSayisi)//sag ıcın
            {
                int BirinYeriBir = kisitGenDuzelt.IndexOf(1);
                int ikininYeriiki = kisitGenDuzelt.IndexOf(2);
                int OnBirinYeriOndokuz = kisitGenDuzelt.IndexOf(11);
                sabitgen1 = 1;
                sabitgen2 = 2;
                sabitgen11 = 11;
                if (OnBirinYeriOndokuz < BirinYeriBir)
                {
                    kisitGenDuzelt.RemoveAt(BirinYeriBir);
                    kisitGenDuzelt.Insert(BirinYeriBir, sabitgen11);
                    kisitGenDuzelt.RemoveAt(OnBirinYeriOndokuz);
                    kisitGenDuzelt.Insert(OnBirinYeriOndokuz, sabitgen1);
                }
                BirinYeriBir = kisitGenDuzelt.IndexOf(1);
                ikininYeriiki = kisitGenDuzelt.IndexOf(2);
                OnBirinYeriOndokuz = kisitGenDuzelt.IndexOf(11);
                if (OnBirinYeriOndokuz < ikininYeriiki)
                {
                    kisitGenDuzelt.RemoveAt(ikininYeriiki);
                    kisitGenDuzelt.Insert(ikininYeriiki, sabitgen11);
                    kisitGenDuzelt.RemoveAt(OnBirinYeriOndokuz);
                    kisitGenDuzelt.Insert(OnBirinYeriOndokuz, sabitgen2);
                }
                BirinYeriBir = kisitGenDuzelt.IndexOf(1);
                ikininYeriiki = kisitGenDuzelt.IndexOf(2);
                OnBirinYeriOndokuz = kisitGenDuzelt.IndexOf(11);
                if (ikininYeriiki < BirinYeriBir)
                {
                    kisitGenDuzelt.RemoveAt(BirinYeriBir);
                    kisitGenDuzelt.Insert(BirinYeriBir, sabitgen2);
                    kisitGenDuzelt.RemoveAt(ikininYeriiki);
                    kisitGenDuzelt.Insert(ikininYeriiki, sabitgen1);
                }
            }
            else
            {
                int BirinYeriYedi = kisitGenDuzelt.IndexOf(1);
                int ikininYeriSekiz = kisitGenDuzelt.IndexOf(2);
                sabitgen1 = 1;
                sabitgen2 = 2;
                if (ikininYeriSekiz < BirinYeriYedi)
                {
                    kisitGenDuzelt.RemoveAt(BirinYeriYedi);
                    kisitGenDuzelt.Insert(BirinYeriYedi, sabitgen2);
                    kisitGenDuzelt.RemoveAt(ikininYeriSekiz);
                    kisitGenDuzelt.Insert(ikininYeriSekiz, sabitgen1);
                }
            }
            return kisitGenDuzelt;
        }
        //public void kisitKontrolSagSol(int[,] kisitGenSolSag)
        //{
        //    ArrayList kisitGenDuzeltSolSag = new ArrayList();
        //    ArrayList kisitSolSagHesap = new ArrayList();
        //    int YedininYeriDokuz = 0;
        //    int UcunYeriOn = 0;
        //    for (int kkk = 1; kkk <= GenSayisi; kkk++)
        //    {
        //        kisitGenDuzeltSolSag.Add(kisitGenSolSag[1, kkk]);
        //    }
        //    if (GenSayisi > SabitGenSayisi)//sag
        //    {
        //        YedininYeriDokuz = kisitGenDuzeltSolSag.IndexOf(7);
        //        for (int abc = 0; abc < YedininYeriDokuz; abc++) //eşit değil kendi dahil(10 olmasın) olmasın
        //        {
        //            kisitSolSagHesap.Add(kisitGenDuzeltSolSag[abc]);

        //        }
        //        sagDokuzunDegeri = KisitSolSagUygunluk(kisitSolSagHesap);
        //        MessageBox.Show("sag Degeri: " + sagDokuzunDegeri);
        //    }
        //    else //SolEsikDegeri;
        //    {
        //        UcunYeriOn = kisitGenDuzeltSolSag.IndexOf(3);
        //        for (int abc = 0; abc <= UcunYeriOn; abc++) //eşit kendi içinde geçen süre(9 bitene kadar gecen dakıka)
        //        {
        //            kisitSolSagHesap.Add(kisitGenDuzeltSolSag[abc]);

        //        }
        //        solOnunDegeri = KisitSolSagUygunluk(kisitSolSagHesap);
        //        MessageBox.Show("sol Değeri: " + solOnunDegeri);

        //    }
        //}
        //public int KisitSolSagUygunluk(ArrayList GelenDiziKumesi)
        //{
        //    int degertopla = 0;
        //    for (int kkk = 0; kkk < GelenDiziKumesi.Count; kkk++)
        //    {
        //        degertopla = degertopla + GenlerinSureleri[Convert.ToInt32(GelenDiziKumesi[kkk])];
        //    }
        //    return degertopla;
        //}
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
