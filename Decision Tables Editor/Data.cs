using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace Decision_Tables_Editor
{
    class Data
    {
        private static string filePath;
        private static string fileName;
        private OpenFileDialog openFile = new OpenFileDialog();
        private static XmlDocument document;
        private XDocument projectFile;

        public Data() { }

        public void createProjectFile(TreeView tree, ToolStripMenuItem import, ToolStripMenuItem table, string file)
        {
            filePath = file;
            fileName = Path.GetFileNameWithoutExtension(filePath);
            //Populate with data here if necessary, then save to make sure it exists
            projectFile = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"), new XComment("XML File for storing "),
                new XElement("Tables"));
            projectFile.Save(file);

            document = toXmlDocument(projectFile);
            fillTree(tree, document);
            import.Enabled = true;
            table.Enabled = true;
        }

        public void saveXML(string path)
        {
            string projectPath = path;
            XmlDocument project = new XmlDocument();
            project.Load(filePath);
            project.Save(projectPath);
        }

        public void parseSTD(string tableName, string path, TabPage page)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Состояние", typeof(string));
            dt.Columns.Add("Переход", typeof(string));
            dt.Columns.Add("Результат", typeof(string));
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;
            Dictionary<string, string> states = new Dictionary<string, string>();

            foreach (XElement el in document.Descendants("State"))
            {
                states.Add(el.Attribute("id").Value.ToString(), el.Attribute("name").Value.ToString());
            }

            foreach (XElement el in document.Descendants("Transition"))
            {
                newRow = dt.NewRow();
                newRow["Состояние"] = states[el.Attribute("state-from").Value.ToString()];
                newRow["Результат"] = states[el.Attribute("state-to").Value.ToString()];
                newRow["Переход"] = el.Attribute("name").Value.ToString();
                dt.Rows.Add(newRow);
            }

            createDataGrid(tableName, page, dt);
        }

        public void parseEETD(string tableName, string path, TabPage page)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("События", typeof(string));
            dt.Columns.Add("Условия", typeof(string));
            dt.Columns.Add("Следствия", typeof(string));
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;

            foreach (XElement el in document.Descendants("Level"))
            {
                newRow = dt.NewRow();
                newRow["События"] = "Название: " + el.Attribute("name").Value.ToString();
                int k = 0;
                foreach (XElement elm in document.Descendants("Event"))
                {
                    if (elm.Attribute("type").Value == "Initial event")
                    {
                        newRow["События"] += Environment.NewLine + "Описание: " + elm.Attribute("name").Value.ToString();

                        IEnumerable<XElement> parameters = document.Element("Diagram").Elements("Level").Elements("Event").Elements("Parameter");
                        foreach (XElement param in parameters)
                        {
                            newRow["Условия"] += param.Attribute("name").Value.ToString() + " - " + param.Attribute("value").Value.ToString() + Environment.NewLine;
                        }
                    }
                    else
                    {
                        k++;
                        newRow["Следствия"] += "Следствие " + k + ": " + elm.Attribute("name").Value.ToString() + Environment.NewLine;
                        foreach (XElement param in elm.Descendants("Parameter"))
                        {
                            newRow["Следствия"] += param.Attribute("name").Value.ToString() + " - " + param.Attribute("value").Value.ToString() + Environment.NewLine;
                        }
                    }
                }
                dt.Rows.Add(newRow);
            }

            createDataGrid(tableName, page, dt);
        }

        public void parseEKB(string tableName, string path, TabPage page)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Условия", typeof(string));
            dt.Columns.Add("Действия", typeof(string));
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;
            int k = 0;

            foreach (XElement rules in document.Descendants("Rule"))
            {
                foreach (XElement conditions in rules.Descendants("Conditions"))
                {
                    newRow = dt.NewRow();
                    newRow["Условия"] += "Номер: " + rules.Element("ID").Value + Environment.NewLine;
                    foreach (XElement condition in conditions.Descendants("Condition"))
                    {

                        foreach (XElement fact in condition.Descendants("Name"))
                        {
                            k++;
                            if (k == 1)
                            {
                                newRow["Условия"] += "Имя: " + fact.Value + Environment.NewLine;
                                foreach (XElement slot in condition.Descendants("Slot"))
                                {
                                    newRow["Условия"] += "Критерий: " + slot.Element("Name").Value + Environment.NewLine 
                                        + "Значение: " + slot.Element("Value").Value + Environment.NewLine + condition.Element("Operator").Value + Environment.NewLine;
                                }
                            }
                        }
                        k = 0;  
                    }

                    foreach (XElement actions in rules.Descendants("Actions"))
                    {
                        foreach (XElement action in actions.Descendants("Action"))
                        {
                            foreach (XElement slot in action.Descendants("Slot"))
                            {
                                newRow["Действия"] = slot.Element("Name").Value + Environment.NewLine
                                    + slot.Element("Value").Value;
                            }
                        }
                    }

                    dt.Rows.Add(newRow);
                }
            }

            createDataGrid(tableName, page, dt);
        }

        public void createDataGrid(string gridName, TabPage page, DataTable data)
        {
            DataGridView decisionTable = new DataGridView();
            decisionTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            decisionTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            decisionTable.Name = gridName;
            page.Controls.Add(decisionTable);
            decisionTable.Dock = DockStyle.Fill;
            decisionTable.DataSource = data;
        }

        public XmlDocument getProject(TreeView tree, ToolStripMenuItem import, ToolStripMenuItem table)
        {
            try
            {
                openFile.Filter = "XML Files (*.xml)|*.xml";
                openFile.Multiselect = false;
                openFile.ShowDialog();
                filePath = openFile.FileName;
                fileName = Path.GetFileNameWithoutExtension(filePath);

                using (var myStream = openFile.OpenFile())
                {
                    try
                    {
                        //  Successfully return the XML
                        XmlDocument parsedMyStream = new XmlDocument();
                        parsedMyStream.Load(myStream);
                        fillTree(tree, parsedMyStream);
                        import.Enabled = true;
                        table.Enabled = true;
                        return parsedMyStream;
                    }
                    catch (XmlException ex)
                    {
                        Controller.showMessageBox(ex.Message, "Ошибка");
                        return createEmptyXmlDocument();
                    }
                }

            }
            catch
            {
                Controller.showMessageBox("Операция прервана!", "Ошибка");
                return createEmptyXmlDocument();
            }
        }

        public void convertXmlNodeToTreeNode(XmlNode xmlNode, TreeNodeCollection treeNodes)
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
                    convertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                convertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }

        public XmlDocument toXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public XDocument toXDocument(XmlDocument xmlDocument)
        {
            return XDocument.Parse(xmlDocument.OuterXml);
        }

        public XmlDocument createEmptyXmlDocument()
        {
            //  Return an empty XmlDocument if the open file window was closed
            XmlDocument emptyMyStream = new XmlDocument();
            return emptyMyStream;
        }

        public void createTreeXML(TreeView tree, XmlDocument doc)
        {
            if (doc.ChildNodes.Count == 0)
            {
                Controller.showMessageBox("Файл пуст!", "Ошибка");
            }
            else
            {
                convertXmlNodeToTreeNode(doc, tree.Nodes);
            }
        }

        public void convertTableToXML(DataGridView dataGrid, TabPage currentPage, TreeView tree)
        {
            DataTable currentTable = (DataTable)dataGrid.DataSource;
            currentTable.TableName = currentPage.Text;
            XmlDocument project = new XmlDocument();
            project.Load(filePath);
            XDocument doc = toXDocument(project);
            IEnumerable<XElement> parameters = doc.Element("Tables").Elements(currentPage.Text);

            if (parameters.Count() > 0)
            {
                doc.Element("Tables").Element(currentPage.Text).Remove();
            }

            XElement xe = new XElement(currentPage.Text, currentTable.AsEnumerable().Select(row =>
                                                            new XElement("Строка_" + currentTable.Rows.IndexOf(row),
                                                                currentTable.Columns.Cast<DataColumn>().Select(col =>
                                                                    new XElement(col.ColumnName, row[col])))));
           doc.Root.Add(xe);
           project = toXmlDocument(doc);
           project.Save(filePath);
           fillTree(tree, project);
        }

        public void fillTree(TreeView tree, XmlDocument doc)
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

        public DataTable getDataTable(string dtName)
        {
            XmlDocument currentProject = new XmlDocument();
            currentProject.Load(filePath);
            XDocument xproject = toXDocument(currentProject);
            DataTable dt = new DataTable();
            XElement setup = (from p in xproject.Descendants("Tables").Descendants(dtName).Descendants("Строка_0") select p).First();

            foreach (XElement xe in setup.Descendants()) // build your DataTable 
            {
                dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt
            }

            var all = from p in xproject.Descendants("Tables").Descendants(dtName) select p;

            foreach (XElement xe in all)
            {  
                foreach (XElement xe2 in xe.Descendants())
                {
                    DataRow dr = dt.NewRow();
                    foreach (XElement xe3 in xe2.Descendants())
                    {
                        //Controller.showMessageBox(xe3.Value.ToString(), "Ошибка");
                        dr[xe3.Name.ToString()] = xe3.Value.ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();

            return dt;
        }

        public void delTableFromProject(string name, TreeView tree)
        {
            XmlDocument currentProject = new XmlDocument();
            currentProject.Load(filePath);
            XDocument xproject = toXDocument(currentProject);

            IEnumerable<XElement> parameters = xproject.Element("Tables").Elements(name);
            parameters.Remove();

            currentProject = toXmlDocument(xproject);
            currentProject.Save(filePath);
            fillTree(tree, currentProject);
        }
    }
}
