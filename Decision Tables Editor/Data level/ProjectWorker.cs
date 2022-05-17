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
using Decision_Tables_Editor.Data_level;
using Decision_Tables_Editor.Logic_level;
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
        private XMLWorker worker = new XMLWorker();
        private TreeWorker treeWorker = new TreeWorker();

        public Data() { }

        internal TableEditor TableEditor
        {
            get => default;
            set
            {
            }
        }

        internal ProjectController ProjectController
        {
            get => default;
            set
            {
            }
        }

        internal TablesImporter TablesImporter
        {
            get => default;
            set
            {
            }
        }

        public void createProjectFile(TreeView tree, ToolStripMenuItem import, ToolStripMenuItem table, string file)
        {
            filePath = file;
            fileName = Path.GetFileNameWithoutExtension(filePath);
            //Populate with data here if necessary, then save to make sure it exists
            projectFile = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"), new XComment("XML File for storing "),
                new XElement("Tables"));
            projectFile.Save(file);

            document = worker.toXmlDocument(projectFile);
            treeWorker.fillTree(tree, document, fileName);
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
                        treeWorker.fillTree(tree, parsedMyStream, fileName);
                        import.Enabled = true;
                        table.Enabled = true;
                        return parsedMyStream;
                    }
                    catch (XmlException ex)
                    {
                        ActionsChecker.showMessageBox(ex.Message, "Ошибка");
                        return worker.createEmptyXmlDocument();
                    }
                }

            }
            catch
            {
                ActionsChecker.showMessageBox("Операция прервана!", "Ошибка");
                return worker.createEmptyXmlDocument();
            }
        }

        public void convertTableToXML(DataGridView dataGrid, TabPage currentPage, TreeView tree)
        {
            DataTable currentTable = (DataTable)dataGrid.DataSource;
            currentTable.TableName = currentPage.Text;
            XmlDocument project = new XmlDocument();
            project.Load(filePath);
            XDocument doc = worker.toXDocument(project);
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
            project = worker.toXmlDocument(doc);
            project.Save(filePath);
            treeWorker.fillTree(tree, project, fileName);
        }

        public DataTable getDataTable(string dtName)
        {
            XmlDocument currentProject = new XmlDocument();
            currentProject.Load(filePath);
            XDocument xproject = worker.toXDocument(currentProject);
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
            XDocument xproject = worker.toXDocument(currentProject);

            IEnumerable<XElement> parameters = xproject.Element("Tables").Elements(name);
            parameters.Remove();

            currentProject = worker.toXmlDocument(xproject);
            currentProject.Save(filePath);
            treeWorker.fillTree(tree, currentProject, fileName);
        }
    }
}
