using Decision_Tables_Editor.Logic_level;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Decision_Tables_Editor.Data_level
{
    class FileWorker
    {
        private string path;

        public FileWorker() { }

        public FileWorker(string path) {
            this.path = path;
        }

        internal TablesImporter TablesImporter
        {
            get => default;
            set
            {
            }
        }

        public DataTable parseSTD()
        {
            // Создание источника данных таблицы
            DataTable dt = new DataTable();

            // Создание столбцов
            dt.Columns.Add("Состояние", typeof(string));
            dt.Columns.Add("Переход", typeof(string));
            dt.Columns.Add("Результат", typeof(string));

            //Загрузка файла
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;
            Dictionary<string, string> states = new Dictionary<string, string>();

            // Поиск состояний
            foreach (XElement el in document.Descendants("State"))
            {
                states.Add(el.Attribute("id").Value.ToString(), el.Attribute("name").Value.ToString());
            }

            // Поиск переходов и добавление в строки таблицы
            foreach (XElement el in document.Descendants("Transition"))
            {
                newRow = dt.NewRow();
                newRow["Состояние"] = states[el.Attribute("state-from").Value.ToString()];
                newRow["Результат"] = states[el.Attribute("state-to").Value.ToString()];
                newRow["Переход"] = el.Attribute("name").Value.ToString();
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public DataTable parseEETD()
        {
            // Создание источника данных таблицы
            DataTable dt = new DataTable();
            // Создание столбцов
            dt.Columns.Add("События", typeof(string));
            dt.Columns.Add("Условия", typeof(string));
            dt.Columns.Add("Следствия", typeof(string));
            //Загрузка файла
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;

            // Поиск и извлечение событий на разных уровнях
            foreach (XElement el in document.Descendants("Level"))
            {
                newRow = dt.NewRow();
                newRow["События"] = "Название: " + el.Attribute("name").Value.ToString();
                int k = 0;

                foreach (XElement elm in document.Descendants("Event"))
                {
                    // Поиск и извлечение начального события
                    if (elm.Attribute("type").Value == "Initial event")
                    {
                        newRow["События"] += Environment.NewLine + "Описание: " + elm.Attribute("name").Value.ToString();
                        IEnumerable<XElement> parameters = document.Element("Diagram").Elements("Level").Elements("Event").Elements("Parameter");

                        // Поиск и извлечение условий событий
                        foreach (XElement param in parameters)
                        {
                            newRow["Условия"] += param.Attribute("name").Value.ToString() + " - " + param.Attribute("value").Value.ToString() + Environment.NewLine;
                        }
                    }
                    else
                    {
                        // Поиск и извлчение следствий событий
                        k++;
                        newRow["Следствия"] += "Следствие " + k + ": " + elm.Attribute("name").Value.ToString() + Environment.NewLine;

                        foreach (XElement param in elm.Descendants("Parameter"))
                        {
                            newRow["Следствия"] += param.Attribute("name").Value.ToString() + " - " + param.Attribute("value").Value.ToString() + Environment.NewLine;
                        }
                    }
                }
                // Добавление новой строки
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public DataTable parseEKB()
        {
            // Создание источника данных таблицы
            DataTable dt = new DataTable();

            // Создание столбцов
            dt.Columns.Add("Условия", typeof(string));
            dt.Columns.Add("Действия", typeof(string));
            //Загрузка файла
            XDocument document = XDocument.Load(path);
            DataRow newRow = null;
            int k = 0;

            // Поиск по правилам и условиям
            foreach (XElement rules in document.Descendants("Rule"))
            {
                foreach (XElement conditions in rules.Descendants("Conditions"))
                {
                    // Извлечение условий и запись в строку
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

                    // Поиск действий и запись в строку
                    foreach (XElement actions in rules.Descendants("Actions"))
                    {
                        foreach (XElement action in actions.Descendants("Action"))
                        {
                            foreach (XElement slot in action.Descendants("Slot"))
                            {
                                newRow["Действия"] = slot.Element("Name").Value + ": "
                                    + slot.Element("Value").Value;
                            }
                        }
                    }

                    // Добавление новой строки
                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }

        public DataTable parseCSV()
        {
            DataTable dt = new DataTable();
            string[] rows = File.ReadAllLines(path, Encoding.Default);
            string[] headerNames = rows[0].Split(';');

            foreach (var header in headerNames)
            {
                dt.Columns.Add(header);
            }

            for (int i = 1; i < rows.Length; i++)
            {
                string[] dataWords = rows[i].Split(';');
                if (dataWords.Length <= headerNames.Length)
                {
                    dt.Rows.Add(dataWords);
                }
            }

            return dt;
        }
    }
}
