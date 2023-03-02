using System;
using System.Collections;

namespace Proje3_2
{
    class Program
    {
        static Hashtable bornova = new Hashtable();  // Yeni bir hashtable nesnesi oluşturma

        static void nufusGuncelle(char basHarf)  // Baş harfi verilen mahallelerin toplam nüfusuna 1 ekleyerek Hash Tablosunda güncelleyen metod
        {
            Hashtable temp = (Hashtable)bornova.Clone();
            foreach (object mahalleAdi in temp.Keys)
            {
                if(Convert.ToString(mahalleAdi)[0].Equals(basHarf))
                {
                    bornova[mahalleAdi] = Convert.ToInt32(bornova[mahalleAdi]) + 1;
                }
            }
        }

        static void hashTableYazdir()
        {
            foreach (object mahalle in bornova.Keys)
                Console.WriteLine(mahalle + " - " + bornova[mahalle]);
        }
        static void Main(string[] args)
        {            
            bornova.Add("Kızılay", 15795);
            bornova.Add("Erzene", 35135);
            bornova.Add("Kazımdirik", 33934);
            bornova.Add("Yeşilova", 31008);
            bornova.Add("Atatürk", 28912);
            bornova.Add("İnönü", 25778);
            bornova.Add("Evka3", 20445);
            bornova.Add("Evka4", 14630);
            bornova.Add("Mevlana", 25492);
            bornova.Add("Doğanlar", 21461);

            hashTableYazdir();
            nufusGuncelle('K');
            Console.WriteLine("\nK harfiyle başlayan mahallelerin nüfusları güncellendikten sonra: ");
            hashTableYazdir();
        }
    }
}
