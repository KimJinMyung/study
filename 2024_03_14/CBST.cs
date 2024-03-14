using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024_03_14
{
    internal class CBST
    {
        /*
         * 이진 탐색 트리 (BinarySearchTree)
         * 이진 속성, 탐색 속성을 적용한 트리
         * 이진 탐색을 통한 탐색영역을 절반으로 줄여가며 탐색가능.
         * 균형을 잡은 트리에서 유용.
         * 
         * 이진 : 부모 노드는 최대 2개의 자식 노들을 가질 수 있다.
         * 탐색 : 자신의 노드보다 작은 값은 왼쪽, 큰값들은 오른쪽에 위치한다.
         * 
         * [탐색]
        // 17을 탐색?
        //                       23 (↙)
        //         ┌──────┴──────┐
        //         11 (↘)                     38
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19 (↙)         31          65
        //   └─┐ ┌─┴─┐       ┌─┘          └─┐
        //        6 [17]    22       24                  87
         * 루트 노드부터 시작하여 탐색하는 값과 비교.
         * 작은 경우 왼쪽 자식 노드로...
         * 큰 경우 오른쪽 자식 노드로 감.
         * 
         * 1. 23과 17을 비교 => (17 < 23) -> 왼쪽으로 감.
         *    ㄴ이제 오른쪽 서브 트리는 탐색할 필요가 없음.
         * 2. 11과 17을 비교 => (17 > 11) -> 오른쪽으로 감.
         * 3. 19와 17을 비교 => (17 < 19) -> 왼쪽으로 감.
         * 4. 17과 17을 비교 => (17 == 17) -> 탐색 완료.         
         * 
         * [삽입]
        // 35를 삽입
        // 탐색이랑 비슷함.
        // 루트 노드부터 시작해서 삽입하는 값과 비교.
        // 작은 경우는 왼쪽 자식 노드로..
        // 큰 경우는 오른쪽 자식 노드로 이동.

        // [삽입 전]
        //                       23 (↘)
        //         ┌──────┴──────┐
        //         11                          38 (↙)
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31(↘)      65
        //   └─┐ ┌─┴─┐       ┌─┘          └─┐
        //        6 17      22       24                  87

        // [삽입 후]
        //                       23 
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31          65
        //   └─┐ ┌─┴─┐       ┌─┴─┐      └─┐
        //        6 17      22       24      35          87
         * 
         * 1. 23과 35를 비교 => (35 > 25) -> 오른쪽으로 감
         * 2. 38과 35를 비교 => (35 < 38) -> 왼쪽으로 감.
         * 3. 31과 35를 비교 => (35 > 31) -> 오른쪽으로 감.
         * 4. 비어있는 공간이면 삽입.
         * 
         * [삭제]
         * 1. 자식이 0개인 경우 어떻게 지울것인가..
         *  ㄴ 22를 삭제한다고 하면 부모 노드의 자식 노드를 null로 변경
         * 
        // [삭제 전]
        //                       23 
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31          65
        //   └─┐  ┌─┴─┐      ┌─┴─┐          
        //        6  17      22      24      35           
        //
        // [삭제 후]
        //                       23 
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31          65
        //   └─┐  ┌─┘          ┌─┴─┐          
        //        6  17              24      35         

         * 2. 자식이 1개인 노드의 삭제 : 삭제하는 노드의 부모와 자식을 연결 후 삭제
         * 38을 삭제 (23과 31을 연결.)
         *
        // [삭제 전]
        //                       23 
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31          65
        //   └─┐  ┌─┴─┐       ┌─┴─┐          
        //        6  17      22       24      35        
        // 
        // [삭제 후]
        //                       23 
        //         ┌──────┴──────┐
        //         11                          31 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              24          35          
        //   └─┐ ┌─┴─┐                
        //        6 17      22             
        
         * 2. 자식이 2개인 노드의 삭제 : 삭제하는 노드를 기준으로 오른쪽 자식중 가장 작은 값 노드와 교체 후 삭제
         * 23을 삭제
         * 
        // 1. 오른쪽 자식중 가장 작은 값 노드를 탐색
        //                       23 
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              24          65
        //   └─┐  ┌─┴─┐      ┌─┘          
        //        6  17      22      31       

        // 2. 가장 작은 값 노드와 교체
        //                       24
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              23          65
        //   └─┐  ┌─┴─┐      ┌─┘          
        //        6  17      22      31       

        // 3. 삭제
        //                       24
        //         ┌──────┴──────┐
        //         11                          38 
        //   ┌──┴──┐              ┌──┴──┐
        //   3           19              31          65
        //   └─┐  ┌─┴─┐               
        //        6  17      22             

        // 불균형 구조일때 어떻게 해결되는 것인지 조사할 것.
         */

        public class BinarySearchTree<T> where T : IComparable<T>
        {
            //노드를 나타내는 클래스
            private class Node
            {
                public T item;
                public Node parent;
                public Node left;
                public Node right;

                public Node(T item, Node parent, Node Left, Node right) 
                { 
                    this.item = item;
                    this.parent = parent;
                    this.left = Left;
                    this.right = right;
                }                                
            }

            private Node root;

            public BinarySearchTree()
            {
                this.root = null;
            }

            //삽입
            public bool Add(T item)
            {
                // 루트 노드가 없으면 새로이 생성해서 집어넣어라.
                if (root == null)
                {
                    Node newNode = new Node(item, null, null, null);
                    root = newNode;
                    return true;
                }

                Node current = root;

                while (current != null)
                {
                    // item이 current의 item보다 작으면 음수, 같으면 0, 크면 양수 반환
                    if (item.CompareTo(current.item) < 0)
                    {
                        //왼쪽 노드의 값이 없으면
                        if (current.left == null)
                        {
                            Node newNode = new Node(item, null, null, null);
                            current.left = newNode;
                            newNode.parent = current;
                            break;
                        }
                        current = current.left;
                    }
                    else if (item.CompareTo(current.item)>0)
                    {
                        if(current.right == null)
                        {
                            Node newNode = new Node(item, null, null, null);
                            current.right = newNode;
                            newNode.parent = current;
                            break;
                        }
                        current = current.right;
                    }
                    // 중복 방지
                    else // if (item.CompareTo(current.item)==0)
                    {
                        return false;
                    }
                }
                return true;
            } // end of Add

            public bool Remove(T item) 
            {
                //아이템이 트리에 있으면 해당 노드를 삭제
                //없으면 false 반환

                Node  findNode = FindNode(item);

                if (findNode != null)
                {
                    EraseNode(findNode);
                    return true;
                }
                else
                {
                    return false ;
                }
            }

            public bool Contatins(T item)
            {
                //찾아서 있으면 true, 없으면 false

                Node node = FindNode(item);
                if (node != null)
                {
                    return true;
                }else return false ;
            }

            public void Clear()
            {
                root = null;
            }

            //찾기
            public Node FindNode(T item)
            {
                if (root == null) return null;
                Node current = root;
                while (current != null)
                {
                    if ( item.CompareTo(current.item) < 0)
                    {
                        current = current.left;
                    }
                    else if (item.CompareTo(current.item) > 0)
                    {
                        current = current.right;
                    }
                    else 
                    { 
                        return current; 
                    }
                }
                return null;
            }

            public void EraseNode(Node node)
            {
                //입력한 아이템이 노드에 있으면 해당 노드를 제거.

                //자식이 없으면
                if (node.left == null && node.right == null)
                {
                    Node parent = node.parent;

                    if (parent == null) root = null;
                    else if (parent.left == null) parent.left = null;
                    else if (parent.right == null) parent.right = null;
                }
                //자식을 한개만 가지는 경우
                else if (node.left != null || node.right != null)
                {
                    Node parent = node.parent;
                    Node child = node.left != null ? node.left : node.right;

                    if (parent == null)
                    {
                        root = child;
                        child.parent = null;
                    }else if (parent.left == node)
                    {
                        parent.left = child;
                        child.parent = parent;
                    }else if (parent.right == node)
                    {
                        parent.right = child;
                        child.parent = parent;
                    }
                }
                //자식을 두개 모두 가지고 있으면.
                else
                {
                    Node nextNode = node.right;
                    while (nextNode.left != null)
                    {
                        nextNode = nextNode.left;
                    }
                    node.item = nextNode.item;
                    EraseNode(nextNode);
                }
            }
        }

        static void Main()
        {
            //사용하면 전위 순회로 실행되어 오름차순으로 정렬된다.
            BinarySearchTree<int> bst = new BinarySearchTree<int>();

            bst.Add(50);
            bst.Add(30);
            bst.Add(70);
            bst.Add(20);
            bst.Add(40);
            bst.Add(60);
            bst.Add(80);

            Console.WriteLine(bst.Contatins(40));
        }
    }
}
