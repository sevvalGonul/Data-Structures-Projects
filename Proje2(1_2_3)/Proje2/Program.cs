using System;
using System.Collections;
using System.Collections.Generic;

namespace Proje2
{
    class Teslimat
    {
        public string yemekAdi;
        public int adet;

        public Teslimat(string yemekAdi, int adet)
        {
            this.yemekAdi = yemekAdi;
            this.adet = adet;
        }
    }

    class Mahalle
    {
        public string mahalleAdi;
        public List<Teslimat> teslimatlar;

        public Mahalle(string mahalleAdi)
        {
            this.mahalleAdi = mahalleAdi;
            teslimatlar = new List<Teslimat>();
        }
        public override string ToString()
        {
            string str = "Mahalle Adı: " + mahalleAdi + ", Teslimatlar: ";
            foreach(Teslimat teslimat in teslimatlar)
            {
                str += String.Format("[{0}, {1}] ", teslimat.yemekAdi, teslimat.adet);
            }
            return str;
        }
    }

    class Stack
    {
        private int maxSize;
        private Mahalle[] stackArray;  // Mahalle tipindeki nesneleri tutacak olan Stack
        private int top;

        public Stack(int size)
        {
            maxSize = size;
            stackArray = new Mahalle[maxSize];
            top = -1;
        }

        public void push(Mahalle newItem)
        {
            stackArray[++top] = newItem;
        }

        public Mahalle pop()
        {
            return stackArray[top--];
        }

        public bool isEmpty()
        {
            return (top == -1);
        }
    }

    class Queue
    {
        private int maxSize;
        private Mahalle[] queueArray;  // Mahalle tipindeki nesneleri tutacak olan Queue
        private int front; private int rear; private int nItems;

        public Queue(int size)
        {
            maxSize = size;
            queueArray = new Mahalle[maxSize];
            front = 0;
            rear = -1;
            nItems = 0;
        }

        public void insert(Mahalle newItem)  // Kuyruğa eleman ekleme
        {
            if (rear == maxSize - 1)
                rear = -1;
            queueArray[++rear] = newItem;
            nItems++;
        }

        public Mahalle remove()
        {
            Mahalle temp = queueArray[front++];
            if (front == maxSize)
                front = 0;
            nItems--;
            return temp;
        }

        public bool isEmpty()
        {
            return (nItems == 0);
        }
    }

    class OncelikliKuyruk
    {
        List<Mahalle> pq;  // Priority Queue

        public OncelikliKuyruk()
        {
            pq = new List<Mahalle>();
        }

        public void ekle(Mahalle yeniMahalle)  // Eleman kuyruğun sonuna eklenir
        {
            pq.Add(yeniMahalle);  // Add metodu yeni gelen elemanı sona ekler
        }

        public Mahalle sil()  // Azalan Öncelik Kuyruğu olduğu için önce en fazla teslimat yapılan mahalleyi silecek olan metod
        {
            int maxTeslimatSay = 0;  // Initialize etmek için max değişkenine olamayacak kadar küçük bir değer veriyorum
            Mahalle maxTeslimatliMahalle = pq[0];  // Silinecek olan elemanı kuyruktaki ilk eleman olarak belirledim. Döngüde güncellenecek.

            foreach (Mahalle mahalle in pq)
            {
                int teslimatSay = mahalle.teslimatlar.Count;  // O mahalledeki teslimat sayısı
                if (teslimatSay > maxTeslimatSay)
                {
                    maxTeslimatSay = teslimatSay;
                    maxTeslimatliMahalle = mahalle;
                }
            }
            // Döngüden çıkınca silinecek olan eleman yani en fazla teslimat yapılacak olan mahalle bulunmuş olur.
            pq.Remove(maxTeslimatliMahalle);  // List classının Remove metoduna argüman olarak silinecek olan eleman verilir ve o eleman listeden kaldırılmış olur
            return maxTeslimatliMahalle;  // Silinen eleman geri döndürülür
        }

        public bool bosMu()
        {
            return (pq.Count == 0);
        }
    }
    class Program
    {
        static string[] yemekListesi = { "İçli Köfte", "Pizza", "Hamburger", "Mantı", "Döner", "Börek", "Pilav", "Türlü", "Simit", "Kızartma" };
        static Random random = new Random();
        static ArrayList bilesikVeriYapisiOlustur(string[] mahalleAdlari, int[] teslimatSayisi)
        {
            ArrayList motoKurye = new ArrayList();
            for(int i = 0; i < mahalleAdlari.Length; i++)  // Herbir mahalle için:
            {
                string mahalleAdi = mahalleAdlari[i];
                int teslimatSay = teslimatSayisi[i];
                Mahalle mahalle = new Mahalle(mahalleAdi);  // Yeni bir mahalle ve o mahalledeki teslimatları tutmak için bir GenericList oluşturdu.
                
                for(int j = 0; j < teslimatSay; j++)  // O mahalledeki herbir teslimat için:
                {
                    int rastgeleSayi = random.Next(0, yemekListesi.Length); // 0 ile yemek listesinin uzunluğu arasında rastgele bir sayi
                    Teslimat teslimat = new Teslimat(yemekListesi[rastgeleSayi], random.Next(1,15));  // yemekListesinde rastgeleSayiya karşılık gelen yemek ile 1-15 arasında bir adette oluşturulan teslimat
                    mahalle.teslimatlar.Add(teslimat);  // mahalle nesnesinin içindeki teslimatlar GenericList'ine teslimat nesnesi ekleme
                }

                motoKurye.Add(mahalle);
            }
            return motoKurye;
        }

        static void yazdir(ArrayList motoKurye)
        {
            int teslimatSay = 0;  // Toplam teslimat sayısı
            foreach (Mahalle mahalle in motoKurye)
            {
                Console.Write("Mahalle Adı: " + mahalle.mahalleAdi + ", ");
                Console.Write("Teslimatlar: ");

                foreach (Teslimat teslimat in mahalle.teslimatlar)
                {
                    Console.Write("[{0}, {1}] ", teslimat.yemekAdi, teslimat.adet);
                    teslimatSay += 1;
                }
                Console.WriteLine();
            }

            // Toplam liste ve teslimat sayısı yazdırma:
            Console.WriteLine("\nArrayList içindeki GenericList Sayısı (Mahalle Sayısı) : " + motoKurye.Count);
            Console.WriteLine("Toplam teslimat sayısı: " + teslimatSay + "\n");
        }
        static void Main(string[] args)
        {
            string[] mahalleAdlari = { "Özkanlar", "Evka 3", "Atatürk", "Erzene", "Kazımdirik", "Mevlana", "Doğanlar", "Ergene" };
            int[] teslimatSayisi = { 5, 2, 7, 2, 7, 3, 0, 1};

            ArrayList motoKurye = bilesikVeriYapisiOlustur(mahalleAdlari, teslimatSayisi);
            yazdir(motoKurye);


            // Yığıt, kuyruk ve öncelikli kuyruk oluşturma ve tüm mahalleleri ArrayList'ten çekerek bu veri yapılarına eklema:
            Stack stack = new Stack(mahalleAdlari.Length);
            Queue queue = new Queue(mahalleAdlari.Length);
            OncelikliKuyruk priorityQueue = new OncelikliKuyruk();
            
            foreach (Mahalle mahalle in motoKurye)
            {
                stack.push(mahalle);
                queue.insert(mahalle);
                priorityQueue.ekle(mahalle);
            }

            // Yığıttaki elemanları ekrana yazdırma:
            Console.WriteLine("Yığıt:");
            while(!stack.isEmpty())
            {
                Console.WriteLine(stack.pop().ToString());
            }

            Console.WriteLine("\nKuyruk: ");
            // Kuyruktaki elemanları ekrana yazdırma:
            while(!queue.isEmpty())
                Console.WriteLine(queue.remove().ToString());

            Console.WriteLine("\nÖncelikli Kuyruk: ");
            // Öncelikli Kuyruktaki elemanları ekrana yazdırma:
            while (!priorityQueue.bosMu())
                Console.WriteLine(priorityQueue.sil().ToString());
        }
    }
}
