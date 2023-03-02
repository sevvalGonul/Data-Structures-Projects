/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author x
 */
public class Test {
    public static void main(String[] args) {
        Heap maxHeap = new Heap(50);
        // Bornovadaki 10 mahalleyi max heap'e yerleştirme:
        maxHeap.insert("Kızılay", 15795);
        maxHeap.insert("Erzene", 35135);
        maxHeap.insert("Kazımdirik", 33934);
        maxHeap.insert("Yeşilova", 31008);
        maxHeap.insert("Atatürk", 28912);
        maxHeap.insert("İnönü", 25778);
        maxHeap.insert("Evka3", 20445);
        maxHeap.insert("Evka4", 14630);
        maxHeap.insert("Mevlana", 25492);
        maxHeap.insert("Doğanlar", 21461);
        
        // Nüfusu en fazla olan 3 mahalleyi sıra ile Heap’ten çekme:        
        System.out.println(maxHeap.remove());  // toString otomatik olarak çağrılır
        System.out.println(maxHeap.remove());
        System.out.println(maxHeap.remove());


    }
}
