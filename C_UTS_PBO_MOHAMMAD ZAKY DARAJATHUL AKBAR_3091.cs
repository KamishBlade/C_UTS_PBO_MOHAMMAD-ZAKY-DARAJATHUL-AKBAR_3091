using System;
using System.Collections.Generic;
using System.Linq;

abstract class BukuDasar
{
    public abstract int Id { get; set; }
    public abstract string Judul { get; set; }
    public abstract string Penulis { get; set; }
    public abstract int TahunTerbit { get; set; }
    public abstract string Status { get; set; }

    public abstract void TampilkanInfo();
}

class Buku : BukuDasar
{
    public override int Id { get; set; }
    public override string Judul { get; set; }
    public override string Penulis { get; set; }
    public override int TahunTerbit { get; set; }
    public override string Status { get; set; }

    public override void TampilkanInfo()
    {
        Console.WriteLine($"ID: {Id}, Judul: {Judul}, Penulis: {Penulis}, Tahun: {TahunTerbit}, Status: {Status}");
    }
}

class Perpustakaan
{
    public string Nama { get; set; }
    public string Alamat { get; set; }
    private List<Buku> KoleksiBuku = new List<Buku>();

    public Perpustakaan(string nama, string alamat)
    {
        Nama = nama;
        Alamat = alamat;
    }

    public void TambahBuku(Buku bukuBaru)
    {
        if (KoleksiBuku.Any(b => b.Id == bukuBaru.Id))
        {
            Console.WriteLine("ID buku sudah ada.");
            return;
        }
        KoleksiBuku.Add(bukuBaru);
        Console.WriteLine("Buku berhasil ditambahkan.");
    }

    public void TampilkanSemuaBuku()
    {
        if (KoleksiBuku.Count == 0)
        {
            Console.WriteLine("Belum ada buku di koleksi.");
            return;
        }

        Console.WriteLine("\n--- Daftar Buku ---");
        foreach (var buku in KoleksiBuku)
        {
            buku.TampilkanInfo();
        }
    }

    public void CariBuku()
    {
        Console.Write("Masukkan ID atau Judul Buku: ");
        string input = Console.ReadLine();
        var hasil = KoleksiBuku
                    .Where(b => b.Id.ToString() == input || b.Judul.Contains(input, StringComparison.OrdinalIgnoreCase))
                    .ToList();

        if (hasil.Count == 0)
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
        else
        {
            Console.WriteLine("Hasil Pencarian:");
            foreach (var buku in hasil)
                buku.TampilkanInfo();
        }
    }

    public void UbahBuku()
    {
        Console.Write("Masukkan ID buku yang ingin diubah: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var buku = KoleksiBuku.FirstOrDefault(b => b.Id == id);
            if (buku == null)
            {
                Console.WriteLine("Buku tidak ditemukan.");
                return;
            }

            Console.Write("Judul baru: ");
            buku.Judul = Console.ReadLine();
            Console.Write("Penulis baru: ");
            buku.Penulis = Console.ReadLine();
            Console.Write("Tahun terbit baru: ");
            buku.TahunTerbit = int.Parse(Console.ReadLine());
            Console.Write("Status (tersedia/dipinjam): ");
            buku.Status = Console.ReadLine();

            Console.WriteLine("Data buku berhasil diperbarui.");
        }
        else
        {
            Console.WriteLine("ID tidak valid.");
        }
    }

    public void HapusBuku()
    {
        Console.Write("Masukkan ID buku yang ingin dihapus: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var buku = KoleksiBuku.FirstOrDefault(b => b.Id == id);
            if (buku == null)
            {
                Console.WriteLine("Buku tidak ditemukan.");
                return;
            }

            KoleksiBuku.Remove(buku);
            Console.WriteLine("Buku berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("ID tidak valid.");
        }
    }
}

class Program
{
    static void Main()
    {
        Perpustakaan perpustakaan = new Perpustakaan("Perpustakaan Indonesia", "Jl. Ilmu No. 1");

        int pilihan;
        do
        {
            Console.WriteLine($"\n=== {perpustakaan.Nama} ===");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Lihat Semua Buku");
            Console.WriteLine("3. Cari Buku");
            Console.WriteLine("4. Ubah Buku");
            Console.WriteLine("5. Hapus Buku");
            Console.WriteLine("6. Keluar");
            Console.Write("Pilih menu (1-6): ");
            int.TryParse(Console.ReadLine(), out pilihan);

            switch (pilihan)
            {
                case 1:
                    Buku bukuBaru = new Buku();
                    Console.Write("ID Buku: ");
                    bukuBaru.Id = int.Parse(Console.ReadLine());
                    Console.Write("Judul: ");
                    bukuBaru.Judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    bukuBaru.Penulis = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    bukuBaru.TahunTerbit = int.Parse(Console.ReadLine());
                    Console.Write("Status (tersedia/dipinjam): ");
                    bukuBaru.Status = Console.ReadLine();

                    perpustakaan.TambahBuku(bukuBaru);
                    break;
                case 2:
                    perpustakaan.TampilkanSemuaBuku();
                    break;
                case 3:
                    perpustakaan.CariBuku();
                    break;
                case 4:
                    perpustakaan.UbahBuku();
                    break;
                case 5:
                    perpustakaan.HapusBuku();
                    break;
                case 6:
                    Console.WriteLine("Keluar dari program...");
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        } while (pilihan != 6);
    }
}
