using System;
using System.Collections.Generic;

namespace Proje3_1
{
    class YemekSinifi
    {
        public string ad;
        public int adet;
        public double fiyat;

        public YemekSinifi(string ad, int adet, int fiyat)
        {
            this.ad = ad;
            this.adet = adet;
            this.fiyat = fiyat;
        }

        public override string ToString()
        {
            return String.Format("[Yemek Adı: {0}, Adet : {1}, Fiyat: {2}]", ad, adet, fiyat);
        }
    }

    class Mahalle
    {
        public string mahalleAdi;
        public List<YemekSinifi[]> siparisListesi;  // siparisListesinin herbir elemanı o mahalleye ait siparişleri tutan bir dizi, dizinin her elemanı da siparişi oluşturan yiyecek bilgilerini tutan YemekSinifi nesnesi

        public Mahalle(string mahalleAdi)
        {
            this.mahalleAdi = mahalleAdi;
            siparisListesi = new List<YemekSinifi[]>();
        }
    }

    class TreeNode  // Herbir düğüm mahalle nesnelerinden oluşur
    {
        public Mahalle mahalle;  // data
        public TreeNode leftChild;
        public TreeNode rightChild;

        public TreeNode(Mahalle mahalle)
        {
            this.mahalle = mahalle;
        }

        public void displayNode()
        {
            Console.WriteLine("Mahalle Adı: " + mahalle.mahalleAdi);
            int sayac = 1;
            foreach(YemekSinifi[] dizi in mahalle.siparisListesi)
            {
                Console.Write(sayac + ".Sipariş: ");
                foreach(YemekSinifi yemek in dizi)
                {
                    Console.Write(yemek.ToString() + " ");
                }
                Console.WriteLine();
                sayac++;
            }
        }
    }
    class Tree
    {
        private TreeNode root;
        int maxDepth;

        public Tree()
        {
            root = null;
            maxDepth = 0;
        }

        public TreeNode getRoot()
        {
            return root;
        }

        public void insert(Mahalle mahalle)  // Önce geliyorsa leftChild, sonra geliyorsa rightChild
        {
            TreeNode newNode = new TreeNode(mahalle);
            if (root == null)
                root = newNode;
            else
            {
                TreeNode current = root;
                TreeNode parent;
                while(true)
                {
                    parent = current;
                    if(mahalle.mahalleAdi.CompareTo(current.mahalle.mahalleAdi) < 0)  // Eklenen mahallenin adı şu an üzerinde bulunan düğümün mahalle adından önce geliyorsa:
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else // Sonra geliyorsa:
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                }
            }
        }

        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot, int depth)
        {
            if (localRoot != null)
            {
                depth++;
                if (depth > maxDepth)  // Maximum derinliği(ağacın derinliğini) bulma
                    maxDepth = depth;

                inOrder(localRoot.leftChild,depth);
                localRoot.displayNode();
                inOrder(localRoot.rightChild,depth);
            }
        }

        public void traverseAndFindDepth()  // Ağacı inorder olarak dolaşır ve derinliği ekrana yazdırır
        {
            inOrder(root, -1);
            Console.WriteLine("\nAğacın derinliği: " + maxDepth + "\n");
        }

        public TreeNode find(string mahalleAdi)  // Adı verilen mahalleyi ağaçta bulur
        {
            TreeNode current = root;
            while(current != null)
            {
                if (mahalleAdi.CompareTo(current.mahalle.mahalleAdi) < 0)  // Aranan mahalle current'taki mahalleden önce geliyorsa
                    current = current.leftChild;
                else if (mahalleAdi.CompareTo(current.mahalle.mahalleAdi) > 0)  // Sonra geliyorsa
                    current = current.rightChild;
                else // mahalleAdi current'taki mahalle adına eşitse aranan node bulunmuş demektir
                    return current;
            }
            return null;  // Aranan mahalle bulunamadı
        }

        public void listOrders(string mahalleAdi)  // Adı verilen mahalledeki 150 TL üstündeki siparişlerin bilgilerini listeleyen metod
        {
            TreeNode node = find(mahalleAdi);

            Console.WriteLine(mahalleAdi + " mahallesindeki 150 TL üstündeki siparişler: ");
            foreach(YemekSinifi[] siparis in node.mahalle.siparisListesi)
            {
                double siparisToplam = 0;
                foreach (YemekSinifi yemek in siparis)
                {
                    siparisToplam += yemek.adet * yemek.fiyat;
                }
                if(siparisToplam > 150)
                {
                    foreach (YemekSinifi yemek in siparis)
                        Console.Write(yemek.ToString() + " ");
                    Console.WriteLine();
                }
            }
        }

        public int countFood(TreeNode localRoot, string yiyecekAdi)  // Ağacı kuyruk yapısı sayesinde iterative olarak dolaşır ve adı verilen bir yiyecek/içeceğin tüm ağaçta kaç adet sipariş verildiğini döndürür
        {
            int yiyecekSay = 0;
            Queue<TreeNode> kuyruk = new Queue<TreeNode>();
            kuyruk.Enqueue(localRoot);
            while (kuyruk.Count > 0)
            {
                TreeNode current = kuyruk.Dequeue();
                foreach (YemekSinifi[] siparis in current.mahalle.siparisListesi)
                {
                    foreach (YemekSinifi yemek in siparis)
                    {
                        if (yemek.ad.Equals(yiyecekAdi))
                        {
                            yiyecekSay += yemek.adet;
                            yemek.fiyat = yemek.fiyat * 0.9;  // Yemeğin birim fiyatına %10 indirim uygulandı
                        }
                    }
                }
                if (current.leftChild != null)
                    kuyruk.Enqueue(current.leftChild);
                if (current.rightChild != null)
                    kuyruk.Enqueue(current.rightChild);
            }
            return yiyecekSay;
        }
    }
    class Program
    {
        static Random random = new Random();
        static string[] mahalleAdlari = { "Evka3", "Özkanlar", "Atatürk", "Erzene", "Kazımdirik" };
        static string[] yiyecekListesi = { "Ayran", "Kola", "Simit", "İçli Köfte", "Hamburger", "Mantı", "Döner", "Börek", "Pilav", "Türlü", "Pizza", "Kızartma" };
        static void Main(string[] args)
        {
            Tree agac = new Tree();
            for(int i = 0; i < mahalleAdlari.Length; i++)
            {
                Mahalle mahalle = new Mahalle(mahalleAdlari[i]);
                int siparisSay = random.Next(5, 10);
                for(int j = 0; j < siparisSay; j++)  // 5-10 arasında rastgele sayıda sipariş içeren sipariş listesi oluşturmak için:
                {
                    int turSay = random.Next(3, 5);  // Her bir siparişte de 3-5 arasında rastgele yiyecek/içecek türü bilgisi olsun
                    YemekSinifi[] siparisBilgileri = new YemekSinifi[turSay];
                    for(int k = 0; k < turSay; k++)
                    {
                        int rnd = random.Next(0, yiyecekListesi.Length);
                        siparisBilgileri[k] = new YemekSinifi(yiyecekListesi[rnd], random.Next(1, 8), 3 * (rnd + 1));
                    }
                    mahalle.siparisListesi.Add(siparisBilgileri);
                }
                agac.insert(mahalle);
            }


            agac.traverseAndFindDepth();

            agac.listOrders("Atatürk");

            Console.WriteLine("\nPizza toplamda " + agac.countFood(agac.getRoot(), "Pizza") + " adette sipariş edildi.");
            Console.WriteLine("Güncellenmiş fiyatlar: "); 
            agac.inOrder(agac.getRoot(), -1);  // Pizzanın fiyatı 33 TL idi. Pizzanın geçtiği her yerde birim fiyatına %10 indirim uygulanarak güncellenmiş fiyatlarda 29,7'ye düştü.




        }
    }
}
