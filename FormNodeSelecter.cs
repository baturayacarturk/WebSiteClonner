using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSC.Services;
using WSC.TreeStructure;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WSC
{
    public partial class FormNodeSelecter : Form
    {
        Node _mainRoot;
        private List<Node> _currentLayer;
        private string _destinationDirectory;
        private string _linksDirectory;
        private int _depthLevel;
        private int _subNodeCount;
        private IDownloadSite _service;

        List<TreeNode> currentTreeNodes = new List<TreeNode>();
        public FormNodeSelecter(string url)
        {
            InitializeComponent();
            depthLevelNumbericUpDown.Value = 3;
            subNodeCountNumericUpDown.Value = 3;
            _mainRoot = new Node(url);
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseClick;
            _service = Program.GetService<IDownloadSite>();
        }

        private async void depthSelectButton_Click(object sender, EventArgs e)
        {
            _depthLevel = ((int)depthLevelNumbericUpDown.Value);
            _subNodeCount = ((int)subNodeCountNumericUpDown.Value);
            treeView1.Nodes.Clear();
            _mainRoot.ClearList();
            _service.ClearList();
            await FindPossibleLinks(_depthLevel, _subNodeCount);
        }



        private DateTime lastClickTime = DateTime.MinValue;

        private async void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if ((DateTime.Now - lastClickTime).TotalMilliseconds > 500 &&e.Node.Nodes.Count==0) 
            {
                treeView1.SelectedNode = e.Node;
                
                var links = await _service.GetTheLinks(treeView1.SelectedNode.Text, _subNodeCount);
                foreach (var link in links)
                {
                    treeView1.SelectedNode.Nodes.Add(link);
                }
            }

            lastClickTime = DateTime.Now;
        }




        private async void downloadButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
            }
            _destinationDirectory = saveFileDialog.FileName;
            System.IO.Directory.CreateDirectory(_destinationDirectory);
            _linksDirectory = Path.Combine(_destinationDirectory, "links");
            Directory.CreateDirectory(_linksDirectory);
            List<Node> selectedNodes = new List<Node>();
            GetCheckedNodes(treeView1.Nodes);

            foreach (var item in currentTreeNodes)
            {
                selectedNodes.Add(new Node(item.Text));
            }


            foreach (Node node in selectedNodes)
            {
                await _service.DownloadAsync(node.url, _destinationDirectory, _linksDirectory);
            }


            MessageBox.Show("Download is successfully complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task FindPossibleLinks(int depth, int subNodeCount)
        {
            var links = await _service.GetTheLinks(_mainRoot.url, subNodeCount);
            var current = new List<TreeNode>();
            var captureLayer = new List<TreeNode>();
            int currentTreeViewControllerIndex = 0;

            var stopCurrent = 0;
            var totalNo = 0;
            var index = 0;

            var transferCaptureLayer = new List<TreeNode>();
            foreach (var link in links)
            {
                _mainRoot.AddChild(new Node(link));
                treeView1.Nodes.Add(new TreeNode(link));

            }
            current = treeView1.Nodes.Cast<TreeNode>().ToList();
            _currentLayer = _mainRoot.nodeList;
            depth--;
            while (depth != 0)
            {
                foreach (var node in _currentLayer)
                {

                    if (stopCurrent != 0)
                    {
                        treeView1.SelectedNode = current[0];
                        stopCurrent++;
                    }

                    var nodeLinks = await _service.GetTheLinks(node.url, subNodeCount);
                    if (currentTreeViewControllerIndex < subNodeCount)
                    {
                        treeView1.SelectedNode = current[currentTreeViewControllerIndex];
                    }
                    else
                    {
                        if (transferCaptureLayer.Count() >= index)
                        {
                            treeView1.SelectedNode = transferCaptureLayer[index];
                            index++;
                        }

                    }



                    foreach (var aTag in nodeLinks)
                    {

                        node.AddChild(new Node(aTag));
                        treeView1.SelectedNode.Nodes.Add(aTag);

                    }

                    foreach (TreeNode item in treeView1.SelectedNode.Nodes)
                    {
                        captureLayer.Add(item);

                    }
                    currentTreeViewControllerIndex++;
                    totalNo = captureLayer.Count;

                }
                totalNo = 0;
                depth--;
                _currentLayer = _currentLayer.SelectMany(layer => layer.nodeList).ToList();
                transferCaptureLayer.Clear();
                transferCaptureLayer.AddRange(captureLayer);
                index = 0;
                captureLayer.Clear();
            }
        }

        public TreeNode FromUrl(string url, TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                if (node.Tag.Equals(url)) return node;
                TreeNode next = FromUrl(url, node);
                if (next != null) return next;
            }
            return null;
        }
        public void GetCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (System.Windows.Forms.TreeNode aNode in nodes)
            {
                //edit
                if (aNode.Checked)
                    currentTreeNodes.Add(aNode);

                if (aNode.Nodes.Count != 0)
                    GetCheckedNodes(aNode.Nodes);
            }
        }

    }
}
