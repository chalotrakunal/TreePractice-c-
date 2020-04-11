using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RadOncPractice
{
    public class Tree
    {
        public class Node
        {
            public string _data;
            private int _level;
            public Node _left;
            public Node _right;
            public Node _parent;
            public string _name;
            public string _leftNode;
            public string _rightNode;
            private bool _isLeft;
            public Node(int level, Node parent, bool isLeftNode, string item = "0")
            {
                _data = item;
                _left = null;
                _right = null;
                _level = level;
                _parent = parent;
                _isLeft = isLeftNode;
                SetName();
            }
            public void CreateChildren()
            {
                _left = new Node(_level + 1, this, true);  // i am the parent 
                _right = new Node(_level + 1, this, false);
            }
            public void SetName()
            {
                var fullName = string.Empty;
                if (_parent == null || _level == 1)
                {
                    fullName = "1";
                    _name = fullName;
                }
                else
                {
                    var whoami = _isLeft ? "1" : "2";
                    fullName = _parent._name + "." + whoami;
                    _name = fullName;
                }
            }
            public string GetName()
            {
                return _name;
            }
        }
        public class BinaryTree
        {
            
            public Node _rootNode;
            int _level;

            public Node RootNode()
            {
                return _rootNode;
            }
            public BinaryTree(int level)
            {
                _level = level;
            }
            public void CreateLevel(Node node, int level)
            {
                if (level < _level)
                {
                    node.CreateChildren();
                    CreateLevel(node._left, level + 1);
                    CreateLevel(node._right, level + 1);
                }
            }
            public void DisplayLevel(Node node, int level)
            {
                if (level <= _level)
                {
                    Console.WriteLine("Node Information : " + node.GetName());
                    Console.WriteLine("Node Level : " + level);
                    Console.WriteLine("Data stored in node is : " + node._data);
                    Console.WriteLine("--------------------------------------------");
                    DisplayLevel(node._left, level + 1);
                    DisplayLevel(node._right, level + 1);
                }
            }
            /*
            public void StoreNodeName(Node node,int level,string info)
            {
                if (level <= _level)
                {
                    if (node.GetName().Equals(info))
                    {
                        Console.WriteLine("Enter the Name that we want to store in node");
                        string inputNodeName = Console.ReadLine();
                        node._data = inputNodeName;
                    }
                    StoreNodeName(node._left,level+1,info);
                    StoreNodeName(node._right,level+1,info);
                }
            }
            */
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            public void StoreContentToDictionary()
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\RadOnc\nodeinfo.txt");
                foreach (string line in lines)
                {
                    if (line.Trim() != string.Empty)
                    {
                        var data = line.Split(':');
                        Dict.Add(data[0], data[1]);
                    }
                }
            }
            public void FillDataToLevels(Node node, int level, int inputlevel)
            {
                if (level <= _level)
                {
                    if (inputlevel >= level)
                    {
                        if (Dict.ContainsKey(node.GetName()))
                        {
                            var nodeData = Dict[node.GetName()];
                            node._data = nodeData;
                        }
                    }
                    FillDataToLevels(node._left, level + 1, inputlevel);
                    FillDataToLevels(node._right, level + 1, inputlevel);
                }
            }

            public string ReadEnteredData()
            {
                Console.WriteLine("Enter the data that we want to search in tree");
                string dataEntered = Console.ReadLine();
                return dataEntered;
            }
            //public void ContinueAgainOrNOt()
            //{
            //    Console.WriteLine("Do you want to continue type: YES/NO");
            //    string yesOrNo = Console.ReadLine();
            //    string defaultValue = "yes";
            //    string noValue = "no";
            //    if (defaultValue.Equals(yesOrNo.ToLower()))
            //    {
            //        var getEntereddata = ReadEnteredData();
            //        var val = DFSSearch(_rootNode, 1, getEntereddata.ToLower());
            //        ContinueAgainOrNOt();

            //    }
            //    else if (noValue.Equals(yesOrNo.ToLower()))
            //    {
            //        Console.WriteLine("Exiting from program");
            //    }
            //    else
            //    {
            //        Console.WriteLine("You have entered and unknow data.. please give correct data");
            //        ContinueAgainOrNOt();
            //    }

            //}
            
            private bool flag = false;
            public bool GetFlagValue()
            {
                return flag;
            }
            //public void DFSSearch(Node node, int level, string data)
            //{
            //    if (level <= _level)
            //    {
            //        var nodeData = node._data;
            //        if (nodeData.ToLowerInvariant().Equals(data))
            //        {
            //            Console.WriteLine("location of Data in DFS search method is : " + node.GetName() + " " + "and the data value is : " + node._data);
            //            flag = true;
                       
            //        }
            //        DFSSearch(node._left, level + 1, data);
            //        DFSSearch(node._right, level + 1, data);
            //    }
            //}
            public void BFSSearch(Node root,string data)
            {
                Queue<Node> queue = new Queue<Node>();
                if (root == null)
                    return;
                queue.Enqueue(root);
                while(queue.Count!=0)
                {
                    
                    Node node = (Node)queue.Dequeue();

                    var nodeData = node._data;
                    if (nodeData.ToLower().Equals(data.ToLower()))
                    {
                        Console.WriteLine("Search succesfull and the data is :" +node._data);
                        flag = true;
                    }
                    Console.WriteLine("The location of Data in DFS search method is : " + node.GetName() + " " + "and the data value is : " + node._data);
                    // above  console Writeline is just 
                    if (node._left != null)
                        queue.Enqueue(node._left);
                    if (node._right != null)
                        queue.Enqueue(node._right);
                }
            }
            public void Init()
            {
                _rootNode = new Node(1, null, true);
                CreateLevel(_rootNode, 1);
                DisplayLevel(_rootNode, 1);
            }
        }
    }
}