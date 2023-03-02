using System;
using System.Collections.Generic;

namespace Project2_4_
{
    class Kuyruk
    {
        List<int> queueList;

        public Kuyruk()
        {
            queueList = new List<int>();
        }

        public void ekle(int yeniEleman)  // Kuyruk yapısında elemanlar sondan eklenir
        {
            queueList.Add(yeniEleman);  // Add metodu sondan ekleme yapar
        }

        public int sil()  // Kuyruk yapısında elemanlar baştan silinir.
        {
            int silinen = queueList[0];
            queueList.RemoveAt(0);  // En baştaki (0.indexteki) elemanı silme
            return silinen;
        }

        public bool bosMu()
        {
            return (queueList.Count == 0);
        }

    }

    class OncelikliKuyruk
    {
        List<int> pq;

        public OncelikliKuyruk()
        {
            pq = new List<int>();
        }

        public void ekle(int yeniEleman)
        {
            pq.Add(yeniEleman);  // Gelen eleman kuyruğun sonuna eklenir
        }

        public int oncelikliSil()  // Artan sırada öncelik kuyruğu olduğu için önce en küçük olan elemanı silecek olan metod
        {
            int min = pq[0];  // Öncelik kuyruğunun ilk elemanını minimum olarak initialize ettim, döngüde güncellenecek.
            foreach(int sayi in pq)
            {
                if (sayi < min)
                    min = sayi;
            }
            // Döngüden çıkınca en küçük eleman bulunmuş olur ve remove metoduyla o elemanı listeden kaldırıyorum:
            pq.Remove(min);
            return min;  // Öncelikli kuyrukta en az sayıda ürünü olan müşteri silinir ve ürünlerinin sayısı döndürülür.
        }

        public bool bosMu()
        {
            return (pq.Count == 0);
        }
    }
    class Program
    {
        static int okutmaSuresi = 3;  // Herbir ürünün okutulması 3 saniye sürmektedir.
        static void Main(string[] args)
        {
            int[] urunSay = { 6, 7, 2, 1, 12, 5, 3, 7, 4, 2 };  // Kasada bekleyen herbir müşterinin ürün sayıları
            int musteriSay = urunSay.Length;

            Kuyruk kuyruk = new Kuyruk();
            OncelikliKuyruk pq = new OncelikliKuyruk();

            for (int i = 0; i < musteriSay; i ++)  // Herbir müşterinin ürün sayıları kuyruk yapılarına eklenir.
            {  
                kuyruk.ekle(urunSay[i]);
                pq.ekle(urunSay[i]);
            }

            int[] kuyrukIslemSureleri = new int[musteriSay];  // Kuyruk yapısındaki herbir müsterinin işlem tamamlanma sürelerini tutacak olan dizi
            int[] pqIslemSureleri = new int[musteriSay];  // Öncelikli Kuyruk yapısındaki herbir müsterinin işlem tamamlanma sürelerini tutacak olan dizi

            for (int i = 0; i < musteriSay; i++)
            {
                if (i == 0)  // Bu kuyruklardaki ilk müştesi ise, işlem süresi kendi ürünlerini okutma süresine eşittir
                { 
                    kuyrukIslemSureleri[i] = kuyruk.sil() * okutmaSuresi;
                    pqIslemSureleri[i] = pq.oncelikliSil() * okutmaSuresi;
                }
                else  // Diğer müşteriler için işlem tamamlanma süresi, kendinden önceki müşterinin işlem tamamlanma süresi ile kendisinin ürünlerini okutma süresi toplamına eşittir.
                {
                    kuyrukIslemSureleri[i] = kuyrukIslemSureleri[i - 1] + kuyruk.sil() * okutmaSuresi;
                    pqIslemSureleri[i] = pqIslemSureleri[i - 1] + pq.oncelikliSil() * okutmaSuresi;
                }
            }

            int kuyrukİslemToplam = 0;
            Console.Write("Kuyruktaki herbir müşterinin işlem tamamlanma süreleri: ");
            foreach (int sure in kuyrukIslemSureleri)
            {
                Console.Write(sure + " ");
                kuyrukİslemToplam += sure;
            }

            Console.Write("\nÖncelikli Kuyruktaki herbir müşterinin işlem tamamlanma süreleri: ");
            int pqİslemToplam = 0;
            foreach (int sure in pqIslemSureleri)
            {
                Console.Write(sure + " ");
                pqİslemToplam += sure;
            }

            Console.WriteLine("\nKuyruk yapısı kullanılan kasada ortalama işlem tamamlanma süresi: " + (double)kuyrukİslemToplam/musteriSay);
            Console.WriteLine("Öncelikli Kuyruk yapısı kullanılan kasada ortalama işlem tamamlanma süresi: " + (double)pqİslemToplam/musteriSay);

        }
    }
}
