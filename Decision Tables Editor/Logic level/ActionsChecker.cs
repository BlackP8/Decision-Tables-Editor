using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Decision_Tables_Editor.Logic_level
{
    class ActionsChecker
    {
        private TablesImporter tablesImporter = new TablesImporter();

        public MainWindow MainWindow
        {
            get => default;
            set
            {
            }
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
                        tablesImporter.addNewTab(tabControl, itemName, tableName);
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
    }
}
