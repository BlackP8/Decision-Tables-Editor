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
    class Controller
    {
        private OpenFileDialog openFile = new OpenFileDialog();
        private Data data1 = new Data();
        private string name = "decisionTable";
        private SaveFileDialog sfd = new SaveFileDialog();
        private static XmlDocument document;
        private ToolStripMenuItem import;
        private ToolStripMenuItem table;

        public Controller(ToolStripMenuItem import, ToolStripMenuItem table)
        {
            this.import = import;
            this.table = table;
        }

        public Controller() { }

        public void openProject(TreeView tree)
        {
            document = data1.getProject(tree, import, table);
        }

        public void saveProject(TreeView tree)
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
                                showMessageBox(ex.StackTrace, "Ошибка");
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                data1.saveXML(sfd.FileName);
                                showMessageBox("Проект сохранен!", "Предупреждение");
                            }
                            catch (Exception ex)
                            {
                                showMessageBox(ex.StackTrace, "Ошибка");
                            }
                        }
                    }
                }
                catch
                {
                    showMessageBox("Операция прервана!", "Предупреждение");
                }
            }
            else
            {
                showMessageBox("Проект не найден!", "Ошибка");
            }
        }

        public void createProject(TreeView tree)
        {
            string projectName = Interaction.InputBox("Пожалуйста введите нaзвание проекта:", "Новый проект");
            if (projectName == "")
            {
                showMessageBox("Вы должны ввести название проекта! Повторите операцию!", "Ошибка");
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
                            showMessageBox(ex.Message, "Ошибка");
                        }
                    }
                    if (!fileError)
                    {
                        data1.createProjectFile(tree, import, table, sfd.FileName);
                    }
                }
            }
        }

        public void importTable(TabPage page, TabControl control, string itemName, string filter)
        {
            try
            {
                openFile.Filter = filter;
                openFile.Multiselect = false;
                openFile.ShowDialog();
                string path = openFile.FileName;

                switch (itemName)
                {
                    case "csvMenu":
                        importTableCSV(page, path);
                        break;
                    case "stdMenu":
                        data1.parseSTD(name, path, page);
                        break;
                    case "eetdMenu":
                        data1.parseEETD(name, path, page);
                        break;
                    case "ekbMenu":
                        data1.parseEKB(name, path, page);
                        break;
                }
            }
            catch (Exception ex)
            {
                control.TabPages.Remove(page);
                showMessageBox(ex.Message, "Ошибка");
            }
        }

        public void importTableCSV(TabPage page, string path)
        {
                DataTable dt = new DataTable();
                string[] lines = File.ReadAllLines(path, Encoding.Default);
                int k = 0;

                if (lines.Length > 0)
                {
                    string firstLine = lines[0];
                    string[] headerLabels = firstLine.Split(';', ',');

                    for (int i = 0; i < headerLabels.Length; i++)
                    {
                        dt.Columns.Add(headerLabels[i]);
                    }

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] dataWords = lines[i].Split(';', ',');
                        DataRow dr = dt.NewRow();
                        int columnIndex = 0;

                        foreach (string headerWord in headerLabels)
                        {
                            dr[headerWord] = dataWords[columnIndex++];
                        }
                        dt.Rows.Add(dr);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    data1.createDataGrid(name, page, transponeDataTable(dt));
                }
        }

        public void removeDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
        }

        public DataTable transponeDataTable(DataTable initialDataTable)
        {
            DataTable newData = new DataTable("Table");
            newData.Columns.Add("Свойства");
            for (int i = 0; i < initialDataTable.Rows.Count; i++)
            {
                newData.Columns.Add("Значение" + (i + 1));
            }

            for (int i = 0; i < initialDataTable.Columns.Count; i++)
            {
                DataRow newRow = newData.NewRow();

                newRow[0] = initialDataTable.Columns[i].Caption;
                for (int j = 0; j < initialDataTable.Rows.Count; j++)
                {
                    newRow[j + 1] = initialDataTable.Rows[j][i];
                }

                newData.Rows.Add(newRow);
            }

            return newData;
        }

        public void addNewTab(TabControl tabControl, ToolStripItem itemName, string tableName)
        {
            string title = tableName;
            string filter;
            TabPage myTabPage = new TabPage(title);
            tabControl.TabPages.Add(myTabPage);
            tabControl.SelectedTab = myTabPage;

            switch (itemName.Name)
            {
                case "csvMenu":
                    filter = "CSV Files|*.csv";
                    importTable(myTabPage, tabControl, itemName.Name, filter);
                    tabControl.SelectedTab = myTabPage;
                    break;
                case "addTableMenu":
                    data1.createDataGrid(name, myTabPage, createDataTable());
                    tabControl.SelectedTab = myTabPage;
                    break;
                case "stdMenu":
                    filter = "XML Files (*.xml)|*.xml";
                    importTable(myTabPage, tabControl, itemName.Name, filter);
                    tabControl.SelectedTab = myTabPage;
                    break;
                case "eetdMenu":
                    filter = "XML Files (*.xml)|*.xml";
                    importTable(myTabPage, tabControl, itemName.Name, filter);
                    tabControl.SelectedTab = myTabPage;
                    break;
                case "ekbMenu":
                    filter = "EKB Files (*.ekb)|*.ekb";
                    importTable(myTabPage, tabControl, itemName.Name, filter);
                    tabControl.SelectedTab = myTabPage;
                    break;
            }
        }

        public DataTable createDataTable()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Свойства");
            data.Columns.Add("Значение1");
            return data;
        }

        public static void showMessageBox(string message, string typeOfMessage)
        {
            if (typeOfMessage == "Ошибка")
            {
                System.Windows.MessageBox.Show(message, typeOfMessage, MessageBoxButton.OK, MessageBoxImage.Error);
                Console.Write(message);
            }
            else
            {
                System.Windows.MessageBox.Show(message, typeOfMessage, MessageBoxButton.OK, MessageBoxImage.Warning);
                Console.Write(message);
            }
        }

        public string checkTabNames(TabControl tabControl, string tableName)
        {
            string message = "";
            foreach (TabPage tb in tabControl.TabPages)
            {
                if (tb.Text == tableName)
                {
                    message = " ";
                }
            }
            return message;
        }

        public void checkDialogWindow(TabControl tabControl, ToolStripItem itemName)
        {
            try
            {
                string tableName = Interaction.InputBox("Пожалуйста введите имя таблицы:", "Новая таблица");
                if (tableName == "")
                {
                    showMessageBox("Вы должны ввести имя таблицы! Повторите операцию!", "Ошибка");
                }
                else
                {
                    if (checkTabNames(tabControl, tableName) == "")
                    {
                        addNewTab(tabControl, itemName, tableName);
                    }
                    else
                    {
                        showMessageBox("Таблица с таким именем уже существует!", "Ошибка");
                    }
                }
            }
            catch (Exception ex)
            {
                showMessageBox(ex.Message, "Ошибка");
            }
        }

        public void addRow(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                        DataTable data = (DataTable)dataGrid.DataSource;
                        data.AcceptChanges();
                        DataRow newBlankRow = data.NewRow();
                        if (dataGrid.CurrentCell != null)
                        {
                            data.Rows.InsertAt(newBlankRow, dataGrid.CurrentCell.RowIndex);

                            data.AcceptChanges();
                        }
                        else
                        {
                            showMessageBox("Выделите нужную строку/ячейку!", "Ошибка");
                        }
                }
                else
                {
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void deleteRow(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    try
                    {
                        DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                        int rowIndex = dataGrid.SelectedCells[0].RowIndex;
                        dataGrid.Rows.RemoveAt(rowIndex);
                        DataTable data = (DataTable)dataGrid.DataSource;
                        data.AcceptChanges();
                    }
                    catch
                    {
                        showMessageBox("Выделите нужную строку/ячейку!", "Ошибка");
                    }
                }
                else
                {
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void deleteColumn(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                    if (dataGrid.CurrentCell != null)
                    {
                        int columnIndex = dataGrid.CurrentCell.ColumnIndex;
                        string columnName = dataGrid.Columns[columnIndex].Name;
                        dataGrid.Columns.Remove(columnName);
                        DataTable data = (DataTable)dataGrid.DataSource;
                        data.AcceptChanges();
                    }
                    else
                    {
                        showMessageBox("Выделите нужный столбец/ячейку!", "Ошибка");
                    }
                }
                else
                {
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void addNewColumn(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    string columnName = Interaction.InputBox("Пожалуйста введите название нового столбца:", "Новый столбец");
                    int columnsCount = 0;
                    if (columnName == "")
                    {
                        showMessageBox("Вы должны ввести имя столбца! Повторите операцию!", "Ошибка");
                    }
                    else
                    {
                        DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                        for (int i = 0; i < dataGrid.Columns.Count; i++)
                        {
                            if (dataGrid.Columns[i].Name == columnName)
                            {
                                columnsCount++;
                            }
                        }

                        if (columnsCount > 0)
                        {
                            showMessageBox("Столбец с таким именем уже существует!", "Ошибка");
                        }
                        else
                        {
                            dataGrid.Columns.Add(columnName, columnName);
                            DataTable data = (DataTable)dataGrid.DataSource;
                            data.AcceptChanges();
                        }
                    }
                }
                else
                {
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void saveTableCSV(TabPage currentPage)
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    string tableName = currentPage.Text;
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];

                    if (dataGrid.Rows.Count > 0)
                    {
                        sfd.Filter = "CSV (*.csv)|*.csv";
                        sfd.FileName = tableName + ".csv";
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
                                    showMessageBox(ex.Message, "Ошибка");
                                }
                            }
                            if (!fileError)
                            {
                                try
                                {
                                    //DataTable oldTable = (DataTable)dataGrid.DataSource;
                                    //DataTable transTable = new DataTable();
                                    //string[] columnNames = new string[oldTable.Rows.Count];

                                    //for (int i = 0; i < oldTable.Rows.Count; i++)
                                    //{
                                    //    columnNames[i] = oldTable.Rows[i].ItemArray[0].ToString();
                                    //}

                                    //for (int i = 0; i < oldTable.Rows.Count; i++)
                                    //{
                                    //    transTable.Columns.Add(columnNames[i]);
                                    //}

                                    //for (int i = 1; i < oldTable.Columns.Count; i++)
                                    //{
                                    //    DataRow newRow = transTable.NewRow();

                                    //    for (int j = 0; j < oldTable.Rows.Count; j++)
                                    //    {
                                    //        newRow[j] = oldTable.Rows[j][i];
                                    //    }
                                    //    transTable.Rows.Add(newRow);
                                    //}
                                    //dataGrid.DataSource = transTable;


                                    int columnCount = dataGrid.ColumnCount;
                                    string[] outputCsv = new string[dataGrid.Rows.Count];

                                    for (int i = 0; i < columnCount; i++)
                                    {
                                        outputCsv[0] += dataGrid.Columns[i].HeaderText.ToString() + ";";
                                    }

                                    for (int i = 1; i < dataGrid.RowCount; i++)
                                    {
                                        for (int j = 0; j < columnCount; j++)
                                        {
                                            outputCsv[i] += dataGrid.Rows[i - 1].Cells[j].Value.ToString() + ";";
                                        }
                                    }

                                    //dataGrid.DataSource = oldTable;


                                    File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                                    showMessageBox("Таблица экспортирована успешно!", "Предупреждение");
                                }
                                catch (Exception ex)
                                {
                                    showMessageBox(ex.Message, "Ошибка");
                                }
                            }
                        }
                    }
                    else
                    {
                        showMessageBox("Нет записей для сохранения!", "Ошибка");
                    }
                }
                else
                {
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void getTreeNode(TreeView tree, TabControl tabControl)
        {
            TreeNode currentNode = tree.SelectedNode;

            string title = currentNode.Text;
            TabPage newPage = new TabPage(title);
            tabControl.TabPages.Add(newPage);
            tabControl.SelectedTab = newPage;

            data1.createDataGrid(name, newPage, data1.getDataTable(currentNode.Text));
        }

        public void saveTableInProject(TabPage currentPage, TreeView tree)
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
                    showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void deleteTable(TabPage currentPage, TabControl tabControl, TreeView tree)
        {
            try
            {
                tabControl.TabPages.Remove(currentPage);
                data1.delTableFromProject(currentPage.Text, tree);
            }
            catch
            {
                Controller.showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }
    }
}
