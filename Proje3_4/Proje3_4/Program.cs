using System;

namespace Proje3_4
{
    class InsertionSort
    {
        public void sort(int[] arr)  // Insertion sortu kullanarak diziyi sıralar
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)  // i, 1'den başlar ve her adımda bir key değeri belirlenir.
            {
                int key = arr[i];
                int j = i - 1;

                // Dizide 0'dan i-1'e kadar olan indisli elemanlar(key değerinin solunda kalanlar) eğer key değerinden büyükse bir sağa kaydırılır:
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];  // Elemanları kaydırma
                    j = j - 1;
                }
                arr[j + 1] = key;  // key değerini diziye yerleştirme
            }
        }
    }
    class Program
    {
        static void printArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");

            Console.Write("\n");
        }
        public static void Main()
        {
            int[] arr = { 12, 11, 13, 5, 6, -1, 2, 3, 17 };
            Console.Write("İnitial Array: ");
            printArray(arr);
            InsertionSort ob = new InsertionSort();
            ob.sort(arr);
            Console.Write("Sorted Array: ");
            printArray(arr);
        }
    }
}
