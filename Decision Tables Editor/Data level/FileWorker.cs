using Decision_Tables_Editor.Logic_level;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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

            return dt;
        }

        public DataTable parseEETD()
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

            return dt;
        }

        public DataTable parseEKB()
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

            return dt;
        }

        public DataTable parseCSV()
        {
            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(path, Encoding.Default);
            TablesImporter tablesImporter = new TablesImporter();

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

            //return tablesImporter.transponeDataTable(dt);
            return dt;
        }
    }
}
