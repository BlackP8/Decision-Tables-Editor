using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Decision_Tables_Editor.Data_level
{
    class TreeWorker
    {
        internal Data Data
        {
            get => default;
            set
            {
            }
        }

        public void convertXmlNodeToTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes, string fileName)
        {
            TreeNode newTreeNode = treeNodes.Add(fileName);

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                    xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    newTreeNode.Text = xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    convertXmlNodeToTreeNode(attribute, newTreeNode.Nodes, fileName);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                convertXmlNodeToTreeNode(childNode, newTreeNode.Nodes, fileName);
            }
        }

        public void fillTree(TreeView tree, XmlDocument doc, string fileName)
        {
            tree.Nodes.Clear();
            tree.Nodes.Add(fileName);
            tree.Nodes[0].Nodes.Add(new TreeNode(doc.DocumentElement.Name));

            TreeNode tNode = new TreeNode();
            tNode = tree.Nodes[0].Nodes[0];

            addNode(doc.DocumentElement, tNode);
        }

        public void addNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            // If the node has child nodes, the function will call itself.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;

                for (int i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    addNode(xNode, tNode);
                }
            }
            else
            {
                // Here you need to pull the data from the XmlNode based on the
                // type of node, whether attribute values are required, and so forth.
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }
    }
}
