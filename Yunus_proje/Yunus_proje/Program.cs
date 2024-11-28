using System;
using System.Collections.Generic;

// Interface: IPerson
public interface IPerson
{
    void BilgiGoster();
}

// Temel sınıf: Person (base class)
public abstract class Person : IPerson
{
    public string Ad { get; set; }
    public string Soyad { get; set; }

    public Person(string ad, string soyad)
    {
        Ad = ad;
        Soyad = soyad;
    }

    // Polymorphism: Metodun türetilmiş sınıflarda override edilmesi
    public abstract void BilgiGoster();
}

// Öğrenci Sınıfı
public class Ogrenci : Person
{
    public long OgrenciNo { get; set; }

    public Ogrenci(string ad, string soyad, long ogrenciNo) : base(ad, soyad)
    {
        OgrenciNo = ogrenciNo;
    }

    // BilgiGoster metodunu özelleştiriyoruz
    public override void BilgiGoster()
    {
        Console.WriteLine($"Öğrenci: {Ad} {Soyad}, Öğrenci No: {OgrenciNo}");
    }
}

// Öğretim Görevlisi Sınıfı
public class OgretimGorevlisi : Person
{
    public string Bolum { get; set; }

    public OgretimGorevlisi(string ad, string soyad, string bolum) : base(ad, soyad)
    {
        Bolum = bolum;
    }

    // BilgiGoster metodunu özelleştiriyoruz
    public override void BilgiGoster()
    {
        Console.WriteLine($"Öğretim Görevlisi: {Ad} {Soyad}, Bölüm: {Bolum}");
    }
}

// Ders Sınıfı
public class Ders
{
    public string DersAdi { get; set; }
    public double Kredi { get; set; }
    public OgretimGorevlisi OgretimGorevlisi { get; set; }
    public List<Ogrenci> KayitliOgrenciler { get; set; }

    public Ders(string dersAdi, double kredi, OgretimGorevlisi ogretimGorevlisi)
    {
        DersAdi = dersAdi;
        Kredi = kredi;
        OgretimGorevlisi = ogretimGorevlisi;
        KayitliOgrenciler = new List<Ogrenci>();
    }

    // Öğrenci kaydetme metodu
    public void OgrenciKaydet(Ogrenci ogrenci)
    {
        KayitliOgrenciler.Add(ogrenci);
    }

    // Ders bilgilerini ve kayıtlı öğrencileri listeleyen metod
    public void DersBilgileriniGoster()
    {
        Console.WriteLine($"Ders Adı: {DersAdi}, Kredi: {Kredi}, Öğretim Görevlisi: {OgretimGorevlisi.Ad} {OgretimGorevlisi.Soyad}");
        Console.WriteLine("Kayıtlı Öğrenciler:");
        foreach (var ogrenci in KayitliOgrenciler)
        {
            Console.WriteLine($"- {ogrenci.Ad} {ogrenci.Soyad}, Öğrenci No: {ogrenci.OgrenciNo}");
        }
    }
}

// Program sınıfı
public class Program
{
    public static void Main()
    {
        bool devam = true;
        List<OgretimGorevlisi> ogretmenler = new List<OgretimGorevlisi>();
        List<Ders> dersListesi = new List<Ders>();
        List<Ogrenci> ogrenciler = new List<Ogrenci>();

        // Ana menü
        while (devam)
        {
            Console.Clear();
            Console.WriteLine("1. Öğretim Görevlisi Tanımla");
            Console.WriteLine("2. Ders Tanımla");
            Console.WriteLine("3. Öğrenci Tanımla");
            Console.WriteLine("4. Öğrenciyi Derse Kaydet");
            Console.WriteLine("5. Ders Bilgilerini Görüntüle");
            Console.WriteLine("6. Kayıtlı Öğretmen ve Dersleri Görüntüle");
            Console.WriteLine("7. Çıkış");
            Console.Write("Seçiminizi yapınız: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    // Öğretim Görevlisi Tanımla
                    Console.Write("Öğretim Görevlisi Adı ve Soyadı: ");
                    string[] ogretimGorevlisiBilgileri = Console.ReadLine().Split(' ');
                    string ogretimAd = ogretimGorevlisiBilgileri[0];
                    string ogretimSoyad = ogretimGorevlisiBilgileri[1];
                    Console.Write("Öğretim Görevlisi Bölümü: ");
                    string ogretimBolum = Console.ReadLine();
                    ogretmenler.Add(new OgretimGorevlisi(ogretimAd, ogretimSoyad, ogretimBolum));
                    Console.WriteLine("Öğretim Görevlisi başarıyla tanımlandı!");
                    break;

                case "2":
                    // Ders Tanımla
                    if (ogretmenler.Count == 0)
                    {
                        Console.WriteLine("Önce bir öğretim görevlisi tanımlamanız gerekiyor!");
                        break;
                    }
                    Console.Write("Ders Adı: ");
                    string dersAdi = Console.ReadLine();
                    Console.Write("Ders Kredi: ");
                    double dersKredi;
                    while (!double.TryParse(Console.ReadLine(), out dersKredi) || dersKredi <= 0)
                    {
                        Console.Write("Geçersiz kredi! Lütfen geçerli bir kredi girin: ");
                    }
                    // Öğretmen seçimi
                    Console.WriteLine("Öğretmen Seçin:");
                    for (int i = 0; i < ogretmenler.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ogretmenler[i].Ad} {ogretmenler[i].Soyad}");
                    }
                    int ogretmenSecim = int.Parse(Console.ReadLine()) - 1;
                    dersListesi.Add(new Ders(dersAdi, dersKredi, ogretmenler[ogretmenSecim]));
                    Console.WriteLine("Ders başarıyla tanımlandı!");
                    break;

                case "3":
                    // Öğrenci Tanımla
                    Console.Write("Öğrenci Adı ve Soyadı: ");
                    string[] ogrenciBilgileri = Console.ReadLine().Split(' ');
                    string ogrenciAd = ogrenciBilgileri[0];
                    string ogrenciSoyad = ogrenciBilgileri[1];
                    Console.Write("Öğrenci No (11 basamak): ");
                    long ogrenciNo;
                    while (!long.TryParse(Console.ReadLine(), out ogrenciNo) || ogrenciNo.ToString().Length != 11)
                    {
                        Console.Write("Geçersiz öğrenci numarası! Lütfen 11 basamaklı geçerli bir öğrenci numarası girin: ");
                    }
                    ogrenciler.Add(new Ogrenci(ogrenciAd, ogrenciSoyad, ogrenciNo));
                    Console.WriteLine("Öğrenci başarıyla tanımlandı!");
                    break;

                case "4":
                    // Öğrenciyi Derse Kaydet
                    if (ogrenciler.Count == 0 || dersListesi.Count == 0)
                    {
                        Console.WriteLine("Öğrenci veya ders bulunmamaktadır.");
                        break;
                    }

                    // Öğrenci seçimi
                    Console.WriteLine("Öğrenci Seçin:");
                    for (int i = 0; i < ogrenciler.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {ogrenciler[i].Ad} {ogrenciler[i].Soyad} - Öğrenci No: {ogrenciler[i].OgrenciNo}");
                    }
                    int ogrenciSecim = int.Parse(Console.ReadLine()) - 1;

                    // Ders seçimi
                    Console.WriteLine("Ders Seçin:");
                    for (int i = 0; i < dersListesi.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {dersListesi[i].DersAdi} - Öğretmen: {dersListesi[i].OgretimGorevlisi.Ad} {dersListesi[i].OgretimGorevlisi.Soyad}");
                    }
                    int dersSecim = int.Parse(Console.ReadLine()) - 1;

                    dersListesi[dersSecim].OgrenciKaydet(ogrenciler[ogrenciSecim]);
                    Console.WriteLine("Öğrenci derse kaydedildi!");
                    break;

                case "5":
                    // Ders Bilgilerini Görüntüle
                    if (dersListesi.Count > 0)
                    {
                        foreach (var ders in dersListesi)
                        {
                            ders.DersBilgileriniGoster();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Henüz bir ders tanımlanmadı!");
                    }
                    break;

                case "6":
                    // Kayıtlı Öğretmen ve Dersleri Görüntüle
                    Console.WriteLine("Kayıtlı Öğretmen ve Dersler:");
                    foreach (var ders in dersListesi)
                    {
                        Console.WriteLine($"Ders: {ders.DersAdi}, Öğretim Görevlisi: {ders.OgretimGorevlisi.Ad} {ders.OgretimGorevlisi.Soyad}, Kredi: {ders.Kredi}");
                    }
                    break;

                case "7":
                    // Çıkış
                    devam = false;
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim! Lütfen 1 ile 7 arasında geçerli bir seçenek girin.");
                    break;
            }
            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}