using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace tpmodul8_1302210055
{
    class CovidConfig
    {
        public string SatuanSuhu { get; set; }
        public int BatasHariDeman { get; set; }
        public string PesanDitolak { get; set; }
        public string PesanDiterima { get; set; }

        public CovidConfig()
        {
            // Set nilai default jika file konfigurasi belum ada
            SatuanSuhu = "celcius";
            BatasHariDeman = 14;
            PesanDitolak = "\nAnda tidak diperbolehkan masuk ke dalam gedung ini";
            PesanDiterima = "\nAnda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        public void LoadConfig(string filename)
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText("D:\\Praktek Smstr 4\\KPL\tpmodul8_1302210055\tpmodul8_1302210055\\covid_config.json");
                CovidConfig config = JsonConvert.DeserializeObject<CovidConfig>(json);

                // Mengganti nilai default dengan nilai dari file konfigurasi
                SatuanSuhu = config.SatuanSuhu;
                BatasHariDeman = config.BatasHariDeman;
                PesanDitolak = config.PesanDitolak;
                PesanDiterima = config.PesanDiterima;
            }
            else
            {
                Console.WriteLine("File konfigurasi tidak ditemukan, menggunakan nilai default.");
            }
        }

        public void UbahSatuan()
        {
            if (SatuanSuhu == "celcius")
            {
                SatuanSuhu = "fahrenheit";
            }
            else
            {
                SatuanSuhu = "celcius";
            }
        }
    }

    class Program
    {
        static void Main()
        {
            CovidConfig config = new CovidConfig();
            config.LoadConfig("covid_config.json");

            // Menampilkan judul aplikasi
            Console.WriteLine("Selamat datang di aplikasi pemeriksaan kesehatan Covid-19!\n");

            // Menampilkan pertanyaan untuk input pertama
            Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai {config.SatuanSuhu}");
            string inputan1 = Console.ReadLine();
            double suhu = double.Parse(inputan1);

            // Menampilkan pertanyaan untuk input kedua
            Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
            string inputan2 = Console.ReadLine();
            int hari = int.Parse(inputan2);

            // Menampilkan pilihan untuk mengubah nilai suhu
            Console.WriteLine("\nApakah anda ingin mengubah suhu? (Y/N)");
            string inputan3 = Console.ReadLine();

            // Jika user ingin mengubah suhu, membaca nilai baru dan mengubah nilai suhu
            if (inputan2 == "Y" || inputan2 == "y")
            {
                Console.WriteLine($"\nMasukkan nilai baru untuk suhu dalam {config.SatuanSuhu}:");
                string input4 = Console.ReadLine();
                double newSuhu = double.Parse(inputan3);
                suhu = newSuhu;
            }

            // Panggil method untuk mengubah satuan suhu
            config.UbahSatuan();
            Console.WriteLine($"\nSatuan suhu sekarang: {config.SatuanSuhu}");

            // Menampilkan pertanyaan untuk input pertama
            Console.WriteLine($"\nBerapa suhu badan anda saat ini? Dalam {config.SatuanSuhu}:");
            string input5 = Console.ReadLine();
            double suhubaru = double.Parse(input5);
            suhu = suhubaru;

            // Menampilkan pertanyaan untuk input kedua
            Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
            string input6 = Console.ReadLine();
            int haribaru = int.Parse(input6);
            hari = haribaru;

            bool bolehMasuk = false;

            if (config.SatuanSuhu == "celcius")
            {
                if (suhu >= 36.5 && suhu <= 37.5)
                {
                    bolehMasuk = true;
                }
            }
            else
            {
                if (suhubaru >= 97.7 && suhubaru <= 99.5)
                {
                    bolehMasuk = true;
                }
            }

            if (bolehMasuk && haribaru < config.BatasHariDeman)
            {
                Console.WriteLine(config.PesanDiterima);
            }
            else
            {
                Console.WriteLine(config.PesanDitolak);
            }
        }
    }
}
