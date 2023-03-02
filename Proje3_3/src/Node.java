
public class Node {
       private int iData;          // data item (key)  -- nüfus
       private String mahalleAdi;
       
// -------------------------------------------------------------
   public Node(String mAdi,int key)           // constructor
      { iData = key;
        mahalleAdi = mAdi;
      }
// -------------------------------------------------------------
   public int getKey()
      { return iData; }
// -------------------------------------------------------------
   public void setKey(int id)
      { iData = id; }
   
   public String toString() {
       return "Mahalle Adı: " + mahalleAdi + ", Nüfus: " + iData;
   }
}
