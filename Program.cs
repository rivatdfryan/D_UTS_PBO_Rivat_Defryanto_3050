using System;
using System.Collections.Generic;

public class Buku
{
    public int ID { get; set; }
    public string Judul { get; set; }
    public string Penulis { get; set; }
    public int TahunTerbit { get; set; }
    public bool Status { get; set; } 

    public Buku(int id, string judul, string penulis, int tahunTerbit, bool status)
    {
        ID = id;
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahunTerbit;
        Status = status;
    }
}

public class BukuDigital : Buku
{
    public string FormatFile { get; set; }

    public BukuDigital(int id, string judul, string penulis, int tahunTerbit, bool status, string formatFile)
        : base(id, judul, penulis, tahunTerbit, status)
    {
        FormatFile = formatFile;
    }
}

public class Perpustakaan
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    private List<Buku> KoleksiBuku { get; set; }

    public Perpustakaan(string nama, string alamat)
    {
        Nama = nama;
        Alamat = alamat;
        KoleksiBuku = new List<Buku>();
    }

    public void TambahBuku(Buku buku)
    {
        KoleksiBuku.Add(buku);
    }

    public List<Buku> LihatSemuaBuku()
    {
        return KoleksiBuku;
    }

    public Buku CariBuku(int id)
    {
        return KoleksiBuku.Find(buku => buku.ID == id);
    }

    public void HapusBuku(int id)
    {
        var buku = CariBuku(id);
        if (buku != null)
        {
            KoleksiBuku.Remove(buku);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Perpustakaan perpustakaan = new Perpustakaan("Perpustakaan Kota", "Jl. unej no 1");
        bool jalan = true;

        while (jalan)
        {
            Console.WriteLine("\n===== SISTEM KELOLA PERPUSTAKAAN ======");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Tambah Buku Digital");
            Console.WriteLine("3. Lihat Buku");
            Console.WriteLine("4. Update Buku");
            Console.WriteLine("5. Hapus Buku");
            Console.WriteLine("6. Keluar");
            Console.Write("Pilih menu: ");

            string pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    TambahBuku(perpustakaan);
                    break;
                case "2":
                    TambahBukuDigital(perpustakaan);
                    break;
                case "3":
                    LihatSemuaBuku(perpustakaan);
                    break;
                case "4":
                    UpdateBuku(perpustakaan);
                    break;
                case "5":
                    HapusBuku(perpustakaan);
                    break;
                case "6":
                    jalan = false;
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Coba lagi.");
                    break;
            }
        }
    }

    private static void TambahBuku(Perpustakaan perpustakaan)
    {
        Console.Write("Masukkan ID Buku: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Judul Buku: ");
        string judul = Console.ReadLine();
        Console.Write("Masukkan Penulis Buku: ");
        string penulis = Console.ReadLine();
        Console.Write("Masukkan Tahun Terbit: ");
        int tahun = int.Parse(Console.ReadLine());
        Console.Write("Apakah buku tersedia? (y/n): ");
        string statusInput = Console.ReadLine();
        bool status = statusInput.ToLower() == "y";

        Buku bukuBaru = new Buku(id, judul, penulis, tahun, status);
        perpustakaan.TambahBuku(bukuBaru);
        Console.WriteLine("Buku berhasil ditambahkan");
    }

    private static void TambahBukuDigital(Perpustakaan perpustakaan)
   
        Console.Write("Masukkan ID Buku Digital: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Masukkan Judul Buku Digital: ");
        string judul = Console.ReadLine();
        Console.Write("Masukkan Penulis Buku Digital: ");
        string penulis = Console.ReadLine();
        Console.Write("Masukkan Tahun Terbit: ");
        int tahun = int.Parse(Console.ReadLine());
        Console.Write("Apakah buku tersedia? (y/n): ");
        string statusInput = Console.ReadLine();
        bool status = statusInput.ToLower() == "y";
        Console.Write("Masukkan Format File (contoh: PDF): ");
        string formatFile = Console.ReadLine();

        BukuDigital bukuDigitalBaru = new BukuDigital(id, judul, penulis, tahun, status, formatFile);
        perpustakaan.TambahBuku(bukuDigitalBaru);
        Console.WriteLine("Buku Digital berhasil ditambahkan");
    }

    private static void LihatSemuaBuku(Perpustakaan perpustakaan)
    {
        var bukuList = perpustakaan.LihatSemuaBuku();
        Console.WriteLine("\nDaftar Buku:");
        foreach (var buku in bukuList)
        {
            if (buku is BukuDigital bukuDigital)
            {
                Console.WriteLine($"[Digital] ID: {bukuDigital.ID}, Judul: {bukuDigital.Judul}, Penulis: {bukuDigital.Penulis}, Tahun: {bukuDigital.TahunTerbit}, Tersedia: {bukuDigital.Status}, Format: {bukuDigital.FormatFile}");
            }
            else
            {
                Console.WriteLine($"ID: {buku.ID}, Judul: {buku.Judul}, Penulis: {buku.Penulis}, Tahun: {buku.TahunTerbit}, Tersedia: {buku.Status}");
            }
        }
    }

    private static void UpdateBuku(Perpustakaan perpustakaan)
    {
        Console.Write("Masukkan ID Buku yang ingin diupdate: ");
        int id = int.Parse(Console.ReadLine());
        var buku = perpustakaan.CariBuku(id);
        if (buku != null)
        {
            Console.Write("Masukkan Judul Baru: ");
            buku.Judul = Console.ReadLine();
            Console.Write("Masukkan Penulis Baru: ");
            buku.Penulis = Console.ReadLine();
            Console.Write("Masukkan Tahun Terbit Baru: ");
            buku.TahunTerbit = int.Parse(Console.ReadLine());
            Console.Write("Apakah buku tersedia? (y/n): ");
            string statusInput = Console.ReadLine();
            buku.Status = statusInput.ToLower() == "y";

            Console.WriteLine("Update berhasil");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan");
        }
    }

    private static void HapusBuku(Perpustakaan perpustakaan)
    {
        Console.Write("Masukkan ID Buku yang ingin dihapus: ");
        int id = int.Parse(Console.ReadLine());
        perpustakaan.HapusBuku(id);
        Console.WriteLine("Buku berhasil dihapus");
    }
}