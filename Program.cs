using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RadOncPractice.Tree;

namespace RadOncPractice
{
    class Program
    {
        public static void Main()
        {
            BinaryTree tree = new BinaryTree(3);
            tree.Init();
            var rootNode = tree.RootNode();
            tree.StoreContentToDictionary();
            Console.WriteLine("\n");
            Console.WriteLine("enter the level upto which we want to store the node info");
            string inputLevelNodes = Console.ReadLine();
            tree.FillDataToLevels(rootNode, 1, Convert.ToInt32(inputLevelNodes));
            Console.WriteLine("\n");
            Console.WriteLine("Data after filling nodes upto a level");
            Console.WriteLine("\n");
            tree.DisplayLevel(rootNode, 1);
            Console.WriteLine("\n");
            
            while (true)
            {
                var getData = tree.ReadEnteredData();
                tree.BFSSearch(rootNode, getData.ToLower());              // new method BFS call
                var val = tree.GetFlagValue();
                var result = val.Equals(true) ? "Data present in tree" : " You have entered an unknown data ...hence not present in tree";
                Console.WriteLine(result);
                Console.WriteLine("Do you want to continue type: YES/NO");
                string yesOrNo = Console.ReadLine();
                string defaultValue = "yes";
                string noValue = "no";
                if (defaultValue.Equals(yesOrNo.ToLower()))
                {
                    var getEntereddata = tree.ReadEnteredData();
                    tree.BFSSearch(rootNode, getEntereddata.ToLower());
                }
                else if (noValue.Equals(yesOrNo.ToLower()))
                {
                    Console.WriteLine("Exiting from program");
                    break;
                }
                else
                {
                    Console.WriteLine("You have entered unknow data....please enter correct data to search again..");
                    var Data = tree.ReadEnteredData();
                    tree.BFSSearch(rootNode, Data.ToLower());
                }
            }
            
        }
    }
}
