
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje1
{
    class Program
    {
        static Random random = new Random();
        static int genislik = 100;
        static int yukseklik = 100;
        static int n = 20;
        static void Main(string[] args)
        {
            
            double[,] matris1 = rastgeleNoktaUret(n, genislik, yukseklik);
            //double[,] matris2 = rastgeleNoktaUret(50, 100, 100);
            ikiBoyutluMatrisYazdir(matris1);

            Console.WriteLine("-------------------------");

            double[,] dm = uzaklikMatrisiUret(matris1);
            ikiBoyutluMatrisYazdir(dm);

            for (int tur = 1;tur<11;tur++)
            {
                Console.WriteLine(tur+". Tur : ");
                enYakinKomsuYontemi(dm);
                Console.WriteLine("");
            }
            

            /*
            Console.WriteLine("Matris1");
            for (int i = 0; i < matris1.GetLength(0); i++)
            {
                for (int j = 0; j < matris1.GetLength(1); j++)
                {
                    Console.Write(matris1[i, j]+" ");
                }
                Console.WriteLine();
            }
            */
            /*
            Console.WriteLine("/n Matris2");
            for (int i = 0; i < matris2.GetLength(0); i++)
            {
                for (int j = 0; j < matris2.GetLength(1); j++)
                {
                    Console.Write(matris2[i, j] + " ");
                }
                Console.WriteLine();
            }
            */
            Console.ReadKey();
        }
        static double[,] rastgeleNoktaUret(int n,int genislik,int yukseklik)
        {
            
            double[,] matris = new double[n, 2];

            for (int i = 0; i < n; i++)
            {
                double x = random.Next(0, genislik + 1);
                double y = random.Next(0, yukseklik + 1);
                matris[i, 0] = x;
                matris[i, 1] = y;

            }
            return matris;
        }

        static double[,] uzaklikMatrisiUret(double[,] noktalarMatrisi)
        {
            // n,n yerine getlength te olabilir
            double[,] uzaklikMatrisi = new double[n, n];
            
            for( int i = 0; i < n; i++)
            {
                double x1 = noktalarMatrisi[i, 0];
                double y1 = noktalarMatrisi[i, 1];
                for (int j = 0; j < n; j++)
                {
                    double x2 = noktalarMatrisi[j, 0];
                    double y2 = noktalarMatrisi[j, 1];

                    double uzaklik = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    uzaklikMatrisi[i, j] = uzaklik;
                }
            }
            return uzaklikMatrisi;
        }
        static void ikiBoyutluMatrisYazdir(double[,] matris)
        {
            for (int i = 0; i < matris.GetLength(0); i++)
            {
                for (int j = 0; j < matris.GetLength(1); j++)
                {
                    // 5 karakterlik yer ayirilacak
                    Console.Write("{0:0.00} ",matris[i, j]);
                }
                Console.WriteLine();
            }

        }

        static void enYakinKomsuYontemi(double[,] dm)
        {
            int bulunulanNokta = random.Next(0, n );
            ArrayList ugrananNoktalar = new ArrayList();
            ugrananNoktalar.Add(bulunulanNokta);
            double toplamYol = 0;

            while (ugrananNoktalar.Count != n)
            {
                double minUzaklik = 0;

                int minUzakliktaNokta = 0;//!!!!!!!!!!!!! if e hic girmezse default 0 yaptim
                for (int i = 0;i<n;i++)
                {
                    if (!ugrananNoktalar.Contains(i))
                    {
                        minUzakliktaNokta = i;
                        minUzaklik = dm[bulunulanNokta, i];
                        break;
                    }
                }

                for (int i = 0; i<n; i++)
                {
                    double uzaklik = dm[bulunulanNokta, i];//bulunulan nokta ile i. nokta arasindaki uzaklik
                    if ((uzaklik < minUzaklik) && (uzaklik != 0) && (!ugrananNoktalar.Contains(i))){ //!!!!!!!!!!!!!!!!!
                        minUzaklik = uzaklik;
                        minUzakliktaNokta = i;
                    }
                }
                bulunulanNokta = minUzakliktaNokta;
                ugrananNoktalar.Add(bulunulanNokta);
                toplamYol += minUzaklik;
            }

            Console.Write("Ugranan noktalar: ");
            foreach(int nokta in ugrananNoktalar)
            {
                Console.Write(nokta+" ");
            }
            Console.WriteLine("     Toplam yol: {0:0.00}",toplamYol);
        }


    }
}
