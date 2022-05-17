using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Decision_Tables_Editor.Data_level;
using System.Collections;
using System.IO;
using System.Drawing;

namespace Decision_Tables_Editor.Logic_level
{
    class TableEditor
    {
        private string name = "decisionTable";
        private Data data;
        private TabPage currentPage;

        public MainWindow MainWindow
        {
            get => default;
            set
            {
            }
        }

        public TableEditor(TabPage currentPage)
        {
            this.currentPage = currentPage;
        }

        public void addRow()
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
                        ActionsChecker.showMessageBox("Выделите нужную строку/ячейку!", "Ошибка");
                    }
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

        public void deleteRow()
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
                        ActionsChecker.showMessageBox("Выделите нужную строку/ячейку!", "Ошибка");
                    }
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

        public void deleteColumn()
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
                        ActionsChecker.showMessageBox("Выделите нужный столбец/ячейку!", "Ошибка");
                    }
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

        public void addNewColumn()
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    string columnName = Interaction.InputBox("Пожалуйста введите название нового столбца:", "Новый столбец");
                    int columnsCount = 0;
                    if (columnName == "")
                    {
                        ActionsChecker.showMessageBox("Вы должны ввести имя столбца! Повторите операцию!", "Ошибка");
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
                            ActionsChecker.showMessageBox("Столбец с таким именем уже существует!", "Ошибка");
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
                    ActionsChecker.showMessageBox("Таблица не найдена!", "Ошибка");
                }
            }
            else
            {
                ActionsChecker.showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void deleteTable(TabControl tabControl, TreeView tree)
        {
            try
            {
                tabControl.TabPages.Remove(currentPage);
                data = new Data();
                data.delTableFromProject(currentPage.Text, tree);
            }
            catch
            {
                ActionsChecker.showMessageBox("Вкладка не найдена!", "Ошибка");
            }
        }

        public void removeDuplicateRows()
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                    DataTable currentTable = (DataTable)dataGrid.DataSource;

                    var UniqueRows = currentTable.AsEnumerable().Distinct(DataRowComparer.Default);
                    DataTable dtNew = UniqueRows.CopyToDataTable();

                    if (dtNew.Rows.Count != currentTable.Rows.Count || dtNew.Columns.Count != currentTable.Columns.Count)
                    {
                        var result = MessageBox.Show("Обнаружены одинаковые строки. Вы хотите удалить дубликаты?", "Предупреждение",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            dataGrid.DataSource = dtNew;
                            ActionsChecker.showMessageBox("Дубликаты удалены!", "Предупреждение");
                        }
                    }
                    else
                    {
                        ActionsChecker.showMessageBox("Совпадающих строк не найдено!", "Предупреждение");
                    }
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

        public void saveTableCSV()
        {
            if (currentPage != null)
            {
                if (currentPage.Controls.ContainsKey(name) == true)
                {
                    string tableName = currentPage.Text;
                    DataGridView dataGrid = (DataGridView)currentPage.Controls["decisionTable"];
                    SaveFileDialog sfd = new SaveFileDialog();

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
                                    ActionsChecker.showMessageBox(ex.Message, "Ошибка");
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
                                    ActionsChecker.showMessageBox("Таблица экспортирована успешно!", "Предупреждение");
                                }
                                catch (Exception ex)
                                {
                                    ActionsChecker.showMessageBox(ex.Message, "Ошибка");
                                }
                            }
                        }
                    }
                    else
                    {
                        ActionsChecker.showMessageBox("Нет записей для сохранения!", "Ошибка");
                    }
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
