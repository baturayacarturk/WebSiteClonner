using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSC.TreeStructure
{
    public class Node
    {
        public string url { get; set; }
        public List<Node> nodeList { get; set; }

        public Node()
        {
            nodeList = new List<Node>();
        }
        public Node(string url)
        {
            this.url = url;
            nodeList = new List<Node>();
        }

        public void AddChild(Node nodeToInsert)
        {
            nodeList.Add(nodeToInsert);
        }
        public void ClearList()
        {
            nodeList.Clear();
        }
        public List<Node> GetListRef(List<Node> listOfNodes)
        {
            var returnList = new List<Node>();
            foreach (var item in listOfNodes)
            {
                var currentNodeChild = item.nodeList;
                foreach (var child in currentNodeChild)
                {
                    returnList.Add(child);
                }
            }
            return returnList;
        }


    }
}
