using System.Runtime.InteropServices;

namespace _2024_03_14
{
    /*
     * 트리 (Tree)
     * 계층적인 자료를 나타내는 자주 사용하는 자료구조
     * 
     * 부모노드가 여러 자식노드들을 가질 수 있는 1 : N 구조
     * 
     * 비선형적인 구조
     * 대칭적인 구조를 나타내고 있다.
     * 
     * [구성 요소]
     * 부모   : 루트 노드 방향으로 직접 연결된 노드
     * 자식   : 루트 노드 반대 방향으로 직접 연결된 노드
     * 뿌리(root)     : 부모 노드가 없는 최상위 노드, 트리의 깊이 0에 하나만 존재
     * 가지(branch)   : 부모 노드와 자식노드가 모두 있는 노드, 트리의 중간에 존재
     * 잎     : 자식 노드가 없는 노드, 트리의 끝에 존재
     * 길이   : 출발 노드에서 도착노드까지 거치는 수
     * 깊이   : 루트 노드부터의 길이, 0부터 시작.
     * 차수   : 자식 노드의 갯수
     * 
     * 계층 구조를 가질 수 있는 대칭적인 자료 구조,
     * 효율적인 검색에 용이
     * 
     * 자식 노드의 갯수가 정해져있으면 배열로,
     * 정해지지 않았으면 리스트로 구현한다.
     * 
     * [이진 트리] binary tree
     * 부모 노드가 자식 노드를 최대 2개까지만 가질 수 있는 트리.
     * 일반적으로 이진트리로 구현한다.
     * 
     * [이진 트리 순회 방식]
     * 비선형적인 자료구조이기 때문에 순서에 대하여 규칙이 있어야한다.
     * 전위 : 노드 -> 왼쪽 -> 오른쪽
     * 중위 : 왼쪽 -> 노드 -> 오른쪽
     * 후위 : 왼쪽 -> 오른쪽 -> 노드
     *
     *===============================================================
     */

    //Recursive Function : 재귀 함수
    //자기 자신을 호출하는 것
    //반드시 종료 조건이 호출해야 한다.
    //없으면 무한 루프가 발생하여 스택 오버 플로우가 발생한다.
    //단점 : 반복문보다 속도가 떨어질 수 있음.

    //public void Print()
    //{
    //    Print();
    //}

    //구현 방식  
    //0. 이진 트리의 노드 클래스 제작
    public class BinaryNode<T>
    {                            
        //1. 데이터를 담을 배열 및 리스트 선언
        public T item;

        //2. 부모 노드 선언
        public BinaryNode<T> parent;

        //3. 자식 노드 2개를 선언.
        public BinaryNode<T> left;
        public BinaryNode<T> right;

        public BinaryNode(T item)
        {
            this.item = item;            
        }
    }

    //0-1. 이진 트리 클래스 제작
    public class BinaryTree<T>
    {
        //4. 루트 노드 선언
        private BinaryNode<T> root;

        public BinaryTree(BinaryNode<T> root)
        {
            this.root = root;
        }

        //전위 순회
        public void PreOrder(BinaryNode<T> node)
        {
            if (root != null)
            {
                Console.Write(node.item + " -> ");
                PreOrder(node.left);
                PreOrder(node.right);
            }
        }

        //중위 순회
        public void InOrder(BinaryNode<T> node)
        {
            if(root != null)
            {
                PreOrder(root.left);
                Console.WriteLine(node.item + " -> ");
                PreOrder(root.right);
            }
        }

        //하위 순회
        public void PostOrder(BinaryNode<T> node)
        {
            if (root != null)
            {
                PreOrder(root.left);
                PreOrder(root.right);
                Console.WriteLine(node.item + " -> ");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryNode<char> root = new BinaryNode<char>('A');
            root.left = new BinaryNode<char>('B');
            root.right = new BinaryNode<char>('C');
            root.left.left = new BinaryNode<char>('D');
            root.left.right = new BinaryNode<char>('E');
            root.right.left = new BinaryNode<char>('F');
            root.right.right = new BinaryNode<char>('G');

            BinaryTree<char> tree = new BinaryTree<char>(root);

            Console.WriteLine("전위 순회");
            tree.PreOrder(root);
        }
    }
}
