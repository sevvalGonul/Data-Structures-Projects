using System;

namespace Proje4_2
{
    // C# program for insertion in AVL Tree 
    using System;

    class Node
    {
        public int key, height;
        public Node left, right;

        public Node(int d)
        {
            key = d;
            height = 1;
        }
    }

    public class AVLTree
    {

        Node root;

        int height(Node N)  // Ağacın yüksekliğini döndürür
        {
            if (N == null)
                return 0;

            return N.height;
        }

        int max(int a, int b)  // İki sayının maksimumunu döndürür
        {
            return (a > b) ? a : b;
        }

        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation 
            x.right = y;
            y.left = T2;

            // Update heights 
            y.height = max(height(y.left),
                        height(y.right)) + 1;
            x.height = max(height(x.left),
                        height(x.right)) + 1;

            // Return new root 
            return x;
        }

        // A utility function to left
        // rotate subtree rooted with x 
        // See the diagram given above. 
        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation 
            y.left = x;
            x.right = T2;

            // Update heights 
            x.height = max(height(x.left),
                        height(x.right)) + 1;
            y.height = max(height(y.left),
                        height(y.right)) + 1;

            // Return new root 
            return y;
        }

        // Get Balance factor of node N 
        int getBalance(Node N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
        }

        Node insert(Node node, int key)
        {

            /* 1. BST'ye normal bir biçimde ekleme yapar */
            if (node == null)
                return (new Node(key));

            if (key < node.key)
                node.left = insert(node.left, key);
            else if (key > node.key)
                node.right = insert(node.right, key);
            else // Çift anahtara izin yok
                return node;

            /* 2. Yükseklikleri günceller */
            node.height = 1 + max(height(node.left),
                                height(node.right));

            /* 3. Node dengesiz hale geldi mi diye yükseklik farkına bakılır */
            int balance = getBalance(node);

            // Eğer node dengesiz hale gelmişse, 4 durum olabilir: 
            // Left Left Case 
            if (balance > 1 && key < node.left.key)  // Sola doğru bir dengesizlik var ve eklenen değer sola eklenecekse sol-sol durumu
                return rightRotate(node);  // Tek sağa kaydırma ile ağaç dengeli hale getirilir

            // Right Right Case 
            if (balance < -1 && key > node.right.key)  // Sağa doğru bir dengesizlik var ve eklenen değer sağa eklenecekse sağ-sağ durumur
                return leftRotate(node); // Tek sola kaydırma ile ağaç dengeli hale getirilir

            // Left Right Case 
            if (balance > 1 && key > node.left.key) // Sola doğru bir dengesizlik var ve eklenen değer sağaa eklenecekse sol - sağ durumu
            {  // Çift döndürme gerekli:
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case 
            if (balance < -1 && key < node.right.key)
            {  // Çift döndürme gerekli:
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            /* node geri döndürülür */
            return node;
        }

        // A utility function to print preorder traversal 
        // of the tree. 
        // The function also prints height of every node 
        void preOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.key + " ");
                preOrder(node.left);
                preOrder(node.right);
            }
        }

        // Driver code
        public static void Main(String[] args)
        {
            AVLTree tree = new AVLTree();

            /* Constructing tree given in the above figure */
            tree.root = tree.insert(tree.root, 10);
            tree.root = tree.insert(tree.root, 20);
            tree.root = tree.insert(tree.root, 30);
            tree.root = tree.insert(tree.root, 40);
            tree.root = tree.insert(tree.root, 50);
            tree.root = tree.insert(tree.root, 25);

            /* The constructed AVL Tree would be 
                30 
                / \ 
               20  40 
               / \   \ 
              10 25  50 
            */
            Console.Write("Preorder traversal" +
                            " of constructed tree is : ");
            tree.preOrder(tree.root);
        }
    }

}
