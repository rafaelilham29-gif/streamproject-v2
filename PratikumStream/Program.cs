using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;

namespace PraktikumStream
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Membuat Objek Mahasiswa Asli (NIM menggunakan angka biasa agar sesuai tipe int)
            List<string> matkulMhs = new List<string> { "Pemrograman Jaringan", "Sistem Operasi", "Jaringan Komputer" };
            Mahasiswa mhsOriginal = new Mahasiswa(24131310112, "Rafael Ilham", "Teknologi Informasi", matkulMhs);

            Console.WriteLine("=== 1. DATA OBJEK ASLI DI MEMORI ===");
            mhsOriginal.TampilkanInfo();

            // 2. Serialisasi ke JSON & XML
            string jsonString = JsonSerializer.Serialize(mhsOriginal, new JsonSerializerOptions { WriteIndented = true });
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mahasiswa));
            using StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, mhsOriginal);
            string xmlString = stringWriter.ToString();

            // 3. Menyimpan teks ke file fisik (Tugas Mandiri Poin 3)
            File.WriteAllText("mahasiswa.json", jsonString);
            File.WriteAllText("mahasiswa.xml", xmlString);
            Console.WriteLine("[INFO] File mahasiswa.json dan mahasiswa.xml berhasil disimpan ke disk!\n");

            // 4. Membaca kembali dari file dan Deserialisasi (Tugas Mandiri Poin 4)
            string jsonFromFile = File.ReadAllText("mahasiswa.json");
            Mahasiswa? mhsFromJson = JsonSerializer.Deserialize<Mahasiswa>(jsonFromFile);
            Console.WriteLine("=== 2. HASIL DESERIALISASI DARI FILE JSON ===");
            mhsFromJson?.TampilkanInfo();

            string xmlFromFile = File.ReadAllText("mahasiswa.xml");
            using StringReader stringReader = new StringReader(xmlFromFile);
            Mahasiswa? mhsFromXml = (Mahasiswa?)xmlSerializer.Deserialize(stringReader);
            Console.WriteLine("=== 3. HASIL DESERIALISASI DARI FILE XML ===");
            mhsFromXml?.TampilkanInfo();
        }
    }
}