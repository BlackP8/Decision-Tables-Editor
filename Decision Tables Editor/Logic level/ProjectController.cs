using Decision_Tables_Editor.Data_level;
using Decision_Tables_Editor.Logic_level;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Decision_Tables_Editor
{
    class ProjectController
    {
        private Data data1 = new Data();
        private string name = "decisionTable";
        private SaveFileDialog sfd = new SaveFileDialog();
        private static XmlDocument document;
        private ToolStripMenuItem import;
        private ToolStripMenuItem table;
        private TreeView tree;

        public ProjectController(TreeView tree, ToolStripMenuItem import, ToolStripMenuItem table)
        {
            this.import = import;
            this.table = table;
            this.tree = tree;
        }


        public ProjectController() { }

        public MainWindow MainWindow
        {
            get => default;
            set
            {
            }
        }

        public void openProject()
        {
            document = data1.getProject(tree, import, table);
        }

        public void saveProject()
        {
            TreeNode node = tree.TopNode;
            if (node != null)
            {
                try
                {
                    sfd.Filter = "XML Files (*.xml)|*.xml";
                    sfd.FileName = node.Text + ".xml";
                    bool fileError = false;
                   
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                XmlDocument project = new XmlDocument();
                                project.Load(sfd.FileName);
                                File.Delete(sfd.FileName);
                                project.Save(sfd.FileName);
                            }
                            catch (IOException ex)
                            {
                                fileError = true;
                                ActionsChecker.showMessageBox(ex.StackTrace, "Ошибка");
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                data1.saveXML(sfd.FileName);
                                ActionsChecker.showMessageBox("Проект сохранен!", "Предупреждение");
                            }
                            catch (Exception ex)
                            {
                                ActionsChecker.showMessageBox(ex.StackTrace, "Ошибка");
                            }
                        }
                    }
                }
                catch
                {
                    ActionsChecker.showMessageBox("Операция прервана!", "Предупреждение");
                }
            }
            else
            {
                ActionsChecker.showMessageBox("Проект не найден!", "Ошибка");
            }
        }

        public void createProject()
        {
            string projectName = Interaction.InputBox("Пожалуйста введите нaзвание проекта:", "Новый проект");
            if (projectName == "")
            {
                ActionsChecker.showMessageBox("Вы должны ввести название проекта! Повторите операцию!", "Ошибка");
            }
            else
            {
                sfd.Filter = "XML Files (*.xml)|*.xml";
                sfd.FileName = projectName + ".xml";
                bool fileError = false;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            ActionsChecker.showMessageBox(ex.Message, "Ошибка");
                        }
                    }
                    if (!fileError)
                    {
                        data1.createProjectFile(tree, import, table, sfd.FileName);
                    }
                }
            }
        }

        public void getTreeNode(TabControl tabControl)
        {
            TreeNode currentNode = tree.SelectedNode;

            string title = currentNode.Text;
            TabPage newPage = new TabPage(title);
            tabControl.TabPages.Add(newPage);
            tabControl.SelectedTab = newPage;

            data1.createDataGrid(name, newPage, data1.getDataTable(currentNode.Text));
        }

        public void saveTableInProject(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                    data1.convertTableToXML(dataGrid, currentPage, tree);
                }
                else
                {
                    ActionsChecker.showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                ActionsChecker.showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }
    }
}
