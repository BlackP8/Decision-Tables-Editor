using Decision_Tables_Editor.Data_level;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decision_Tables_Editor.Logic_level
{
    class TablesImporter
    {
        private OpenFileDialog openFile = new OpenFileDialog();
        private string name = "decisionTable";
        private Data data1 = new Data();

        internal ActionsChecker ActionsChecker
        {
            get => default;
            set
            {
            }
        }

        public MainWindow MainWindow
        {
            get => default;
            set
            {
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
                FileWorker fileWorker = new FileWorker(path);
                DataGridView dataGrid = (DataGridView)page.Controls[name];

                switch (itemName)
                {
                    case "csvMenu":
                        data1.createDataGrid(name, page, fileWorker.parseCSV());
                        break;
                    case "stdMenu":
                        data1.createDataGrid(name, page, fileWorker.parseSTD());
                        break;
                    case "eetdMenu":
                        data1.createDataGrid(name, page, fileWorker.parseEETD());
                        break;
                    case "ekbMenu":
                        data1.createDataGrid(name, page, fileWorker.parseEKB());
                        break;
                    case "importDataCSVMenu":
                        DataTable csvTable = (DataTable)dataGrid.DataSource;
                        csvTable.Merge(fileWorker.parseCSV());
                        clearSpace(csvTable);
                        break;
                    case "importDataSTDMenu":
                        DataTable stdTable = (DataTable)dataGrid.DataSource;
                        stdTable.Merge(fileWorker.parseSTD());
                        clearSpace(stdTable);
                        break;
                    case "importDataEKBMenu":
                        DataTable ekbTable = (DataTable)dataGrid.DataSource;
                        ekbTable.Merge(fileWorker.parseEKB());
                        clearSpace(ekbTable);
                        break;
                    case "importDataEETDMenu":
                        DataTable eetdTable = (DataTable)dataGrid.DataSource;
                        eetdTable.Merge(fileWorker.parseEETD());
                        clearSpace(eetdTable);
                        break;
                }
            }
            catch (Exception ex)
            {
                control.TabPages.Remove(page);
                ActionsChecker.showMessageBox(ex.StackTrace, "Ошибка");
            }
        }

        public void clearSpace(DataTable datable)
        {
            List<List<object>> listTemp = new List<List<object>>();

            for (int i = 0; i < datable.Columns.Count; i++)
            {
                List<object> stack = new List<object>();
                for (int j = 0; j < datable.Rows.Count; j++)
                {
                    object obj = datable.Rows[j][i];
                    if (obj.ToString() != "")
                    {
                        stack.Add(obj);
                    }
                }
                listTemp.Add(stack);
            }

            datable.Clear();

            while (hasData(listTemp) == true)
            {
                for (int i = 0; i < datable.Columns.Count; i++)
                {
                    DataRow dr = datable.NewRow();

                    for (int j = 0; j < listTemp.Count; j++)
                    {
                        if (listTemp[j].Count != 0)
                        {
                            dr[j] = listTemp[j][0];

                            listTemp[j].RemoveAt(0);
                        }
                    }
                    datable.Rows.Add(dr);
                }
            }
        }

        public bool hasData(List<List<object>> listTemp)
        {
            foreach (var item in listTemp)
            {
                if (item.Count != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void addNewTab(TabControl tabControl, ToolStripItem itemName, string tableName)
        {
            string title = tableName;
            string filter;
            TabPage myTabPage = new TabPage(title);
            myTabPage.Name = title;
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

        public void checkPage(TabPage page, TabControl control, string itemName, string filter)
        {
            if (page != null)
            {
                if (page.Controls.ContainsKey(name) == true)
                {
                    importTable(page, control, itemName, filter);
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

        public DataTable createDataTable()
        {
            DataTable data = new DataTable();
            data.Columns.Add("Свойства");
            data.Columns.Add("Значение1");
            return data;
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
    }
}
