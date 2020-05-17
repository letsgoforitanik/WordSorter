using System;
using System.Collections.Generic;

namespace WordSorter
{
    class WeightedWord
    {
        public WeightedWord(string word)
        {
            Word = word.ToLower();
            CalculateWeight();
        }

        public string Word { get; }
        public double Weight { get; private set; }

        private void CalculateWeight()
        {
            double positionValue = 10000000000000;

            foreach (var l in Word)
            {
                Weight += positionValue * (l - 96);
                positionValue /= 100;
            }
        }

    }

    delegate double Sorter<T>(T element);

    class BinaryTree<T>
    {
        class Node
        {
            public T Element;
            public Node Left;
            public Node Right;
            public Node(T element) => Element = element;
        }

        Node initial = null;
        public Sorter<T> Sorter { get; set; }
        public int Length { get; private set; }
        public void AddElement(T element)
        {
            AddNode(ref initial, new Node(element));
            Length++;
        }

        private void AddNode(ref Node current, Node toBeAdded)
        {
            if (current == null)
            {
                current = toBeAdded;
            }
            else if (Sorter(toBeAdded.Element) > Sorter(current.Element))
            {
                AddNode(ref current.Right, toBeAdded);
            }
            else if (Sorter(toBeAdded.Element) < Sorter(current.Element))
            {
                AddNode(ref current.Left, toBeAdded);
            }
        }

        public List<T> ToList()
        {
            var list = new List<T>();
            Flatten(initial, list);
            return list;
        }

        private void Flatten(Node node, List<T> list)
        {
            if (node == null) return;
            Flatten(node.Left, list);
            list.Add(node.Element);
            Flatten(node.Right, list);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {      
            var wordList = new string[] { "forgetful", "development", "show", "blue", "futuristic", "remarkable", "jolly", "force", "pocket", "comfortable", "pipe", "beneficial" };
            var binaryTree = new BinaryTree<WeightedWord> { Sorter = w => w.Weight };
            foreach (var word in wordList) binaryTree.AddElement(new WeightedWord(word));
            binaryTree.ToList().ForEach(ww => Console.WriteLine(ww.Word));
            Console.ReadKey();
        }

    }
}
