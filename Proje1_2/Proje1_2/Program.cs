using System;
using System.Collections;

namespace Proje1_2
{
    class SinirHucresi
    {
        Random random = new Random(); 
        double w1, w2;  // Ağırlıklar
        int[,] veriSeti;
        int dogruSay;
        static double lambda = 0.05;  // Öğrenme Katsayısı

        public SinirHucresi(int[,] veri_seti)
        {
            w1 = random.NextDouble() * 2 - 1;  // nextDouble metodu 0 ile 1 arasında rastgele değer üretir
            w2 = random.NextDouble() * 2 - 1;  // Ağırlıkları -1,1 arasında üretmek için formülizasyon
            veriSeti = veri_seti;
            dogruSay = 0;
        }

        public double toplamaİslevi(double x1, double x2)  // Girdilerle ağırlık değerlerini çarpıp toplar
        {
            return x1 * w1 + x2 * w2;
        }

        public int esikFonksiyonu(double toplam)
        {
            if (toplam < 0)  // Nöronun daha hızlı öğrenmesi için 0.5 olan eşik değerini 0 olarak değiştirdik
                return -1;
            else  // toplam >= 0
                return 1;
        }

        public void agirliklariGuncelle(int output, int target, double x1, double x2)
        {
            if (output != target)  // Ağın ürettiği çıktı(output) ve beklenen değer(target) birbirinden farklı ise ağırlıklar güncellenir:
            {
                w1 += lambda * (target - output) * x1;
                w2 += lambda * (target - output) * x2;
            }
            else  // output ve target değerleri aynı ise doğru olarak sınıflandırılmıştır:
                dogruSay += 1;
        }

        public double epoch()  // Tüm veri setini işler ve doğruluk değerini döndürür
        {
            dogruSay = 0;
            for (int i = 0; i < veriSeti.GetLength(0); i++)
            {
                double x1 = veriSeti[i, 0];
                x1 /= 10;
                double x2 = veriSeti[i, 1];
                x2 /= 10;
                int target = veriSeti[i, 2];

                double toplam = toplamaİslevi(x1, x2);
                int output = esikFonksiyonu(toplam);
                agirliklariGuncelle(output, target, x1, x2);

            }
            return dogruSay * 100 / (double)veriSeti.GetLength(0);  // doğruluk değeri (accuracy) = doğru sınıflandırılan örnek sayısı / toplam örnek sayısı
        }

        public double testEt()
        {  // Eğitilen ağı test eder, ağırlıkları güncellemez, doğruluk değerini döndürür
            dogruSay = 0;
            for (int i = 0; i < veriSeti.GetLength(0); i++)
            {
                double x1 = veriSeti[i, 0];
                x1 = x1 / 10;
                double x2 = veriSeti[i, 1];
                x2 = x2 / 10;
                int target = veriSeti[i, 2];

                double toplam = toplamaİslevi(x1, x2);
                int output = esikFonksiyonu(toplam);

                if (output == target)
                    dogruSay += 1;
            }
            return dogruSay * 100 / (double)veriSeti.GetLength(0);
        }
        class Program
        {
            static void Main(string[] args)
            {
                int[,] egitimSeti = { { 6, 5, 1 }, { 2, 4, 1 }, { -3, -5, -1 }, { -1, -1, -1 }, { 1, 1, 1 }, { -2, 7, 1 }, { -4, -2, -1 }, { -6, 3, -1 } };  // x1, x2, target

                SinirHucresi sinirHucresi = new SinirHucresi(egitimSeti);

                int epochSay = 10;  // 10 ve 100 olarak güncellenebilir
                for (int i = 1; i < epochSay + 1; i++)
                {
                    Console.WriteLine(i + ". Epokta Doğruluk Değeri: % " + sinirHucresi.epoch());
                }

                int[,] testSeti = { { 5, 7, 1 }, { -3, 8, 1 }, { -7, -9, -1 }, { 3, -4, -1 }, { 5, 2, 1 } };
                sinirHucresi.veriSeti = testSeti;  // Eğitilmiş sinir hücresinin veri setine test seti aktarılır

                Console.WriteLine("Test Sonucunda Doğruluk Değeri: % " + sinirHucresi.testEt());


            }
        }
    }
}
