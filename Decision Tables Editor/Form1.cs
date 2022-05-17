using Decision_Tables_Editor.Logic_level;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decision_Tables_Editor
{
    public partial class MainWindow : Form
    {
        private ProjectController controller;
        private TabPage currentPage;
        private ActionsChecker actionsChecker = new ActionsChecker();
        private TableEditor tableEditor;
        private TablesImporter tableImporter = new TablesImporter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void csvMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem csvMenu_Clicked = (ToolStripItem)sender;
            actionsChecker.checkDialogWindow(tabControl1, csvMenu_Clicked);
        }

        private void addTableMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem addTableMenu_Clicked = (ToolStripItem)sender;
            actionsChecker.checkDialogWindow(tabControl1, addTableMenu_Clicked);
        }

        private void deleteTableMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.deleteTable(tabControl1, treeView1);
        }

        private void addRowMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.addRow();
        }

        private void deleteRowMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.deleteRow();
        }

        private void deleteColumnMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.deleteColumn();
        }

        private void addColumnMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.addNewColumn();
        }

        private void exitMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createProjectMenu_Click(object sender, EventArgs e)
        {
            controller = new ProjectController(treeView1, importDataMenu, tableMenu);
            controller.createProject();
        }

        private void сохранитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.saveTableCSV();
        }

        private void saveTableMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            controller = new ProjectController(treeView1, importDataMenu, tableMenu);
            controller.saveTableInProject(currentPage);
        }

        private void openProjectMenu_Click(object sender, EventArgs e)
        {
            controller = new ProjectController(treeView1, importDataMenu, tableMenu);
            controller.openProject();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            importDataMenu.Enabled = false;
            tableMenu.Enabled = false;
        }

        private void stdMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem stdMenu_Clicked = (ToolStripItem)sender;
            actionsChecker.checkDialogWindow(tabControl1, stdMenu_Clicked);
        }

        private void eetdMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem eetdMenu_Clicked = (ToolStripItem)sender;
            actionsChecker.checkDialogWindow(tabControl1, eetdMenu_Clicked);
        }

        private void ekbMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem ekbMenu_Clicked = (ToolStripItem)sender;
            actionsChecker.checkDialogWindow(tabControl1, ekbMenu_Clicked);
        }

        private void saveProjectMenu_Click(object sender, EventArgs e)
        {
            controller = new ProjectController(treeView1, importDataMenu, tableMenu);
            controller.saveProject();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            controller = new ProjectController(treeView1, importDataMenu, tableMenu);
            controller.getTreeNode(tabControl1);
        }

        private void checkRulesMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            tableEditor = new TableEditor(currentPage);
            tableEditor.removeDuplicateRows();
        }

        private void importDataCSVMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            string filter = "CSV Files|*.csv";
            ToolStripItem importDataCSV = (ToolStripItem)sender;
            tableImporter.checkPage(currentPage, tabControl1, importDataCSV.Name, filter);
        }

        private void importDataSTDMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            string filter = "XML Files (*.xml)|*.xml";
            ToolStripItem importDataSTD = (ToolStripItem)sender;
            tableImporter.checkPage(currentPage, tabControl1, importDataSTD.Name, filter);
        }

        private void importDataEKBMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            string filter = "EKB Files (*.ekb)|*.ekb";
            ToolStripItem importDataEKB = (ToolStripItem)sender;
            tableImporter.checkPage(currentPage, tabControl1, importDataEKB.Name, filter);
        }

        private void importDataEETDMenu_Click(object sender, EventArgs e)
        {
            currentPage = tabControl1.SelectedTab;
            string filter = "XML Files (*.xml)|*.xml";
            ToolStripItem importDataEETD = (ToolStripItem)sender;
            tableImporter.checkPage(currentPage, tabControl1, importDataEETD.Name, filter);
        }

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }
    }
}
