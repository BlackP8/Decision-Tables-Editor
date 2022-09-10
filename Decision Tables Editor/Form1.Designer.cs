
namespace Decision_Tables_Editor
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.проектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.csvMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.stdMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ekbMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.eetdMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addTableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addRowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addColumnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteColumnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортироватьДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataCSVMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataSTDMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataEKBMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataEETDMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.checkRulesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.проектToolStripMenuItem,
            this.tableMenu,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectMenu,
            this.saveProjectMenu,
            this.exitMenu});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(58, 23);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // openProjectMenu
            // 
            this.openProjectMenu.Image = global::Decision_Tables_Editor.Properties.Resources.открыть;
            this.openProjectMenu.Name = "openProjectMenu";
            this.openProjectMenu.Size = new System.Drawing.Size(201, 24);
            this.openProjectMenu.Text = "Открыть проект";
            this.openProjectMenu.Click += new System.EventHandler(this.openProjectMenu_Click);
            // 
            // saveProjectMenu
            // 
            this.saveProjectMenu.Image = global::Decision_Tables_Editor.Properties.Resources.сохр;
            this.saveProjectMenu.Name = "saveProjectMenu";
            this.saveProjectMenu.Size = new System.Drawing.Size(201, 24);
            this.saveProjectMenu.Text = "Сохранить проект";
            this.saveProjectMenu.Click += new System.EventHandler(this.saveProjectMenu_Click);
            // 
            // exitMenu
            // 
            this.exitMenu.Image = global::Decision_Tables_Editor.Properties.Resources.exit;
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(201, 24);
            this.exitMenu.Text = "Выход";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // проектToolStripMenuItem
            // 
            this.проектToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createProjectMenu,
            this.importDataMenu});
            this.проектToolStripMenuItem.Name = "проектToolStripMenuItem";
            this.проектToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.проектToolStripMenuItem.Text = "Проект";
            // 
            // createProjectMenu
            // 
            this.createProjectMenu.Image = global::Decision_Tables_Editor.Properties.Resources.create;
            this.createProjectMenu.Name = "createProjectMenu";
            this.createProjectMenu.Size = new System.Drawing.Size(244, 24);
            this.createProjectMenu.Text = "Создать новый";
            this.createProjectMenu.Click += new System.EventHandler(this.createProjectMenu_Click);
            // 
            // importDataMenu
            // 
            this.importDataMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csvMenu,
            this.stdMenu,
            this.ekbMenu,
            this.eetdMenu});
            this.importDataMenu.Image = global::Decision_Tables_Editor.Properties.Resources.import2;
            this.importDataMenu.Name = "importDataMenu";
            this.importDataMenu.Size = new System.Drawing.Size(244, 24);
            this.importDataMenu.Text = "Импортировать таблицу";
            // 
            // csvMenu
            // 
            this.csvMenu.Name = "csvMenu";
            this.csvMenu.Size = new System.Drawing.Size(260, 24);
            this.csvMenu.Text = "Из CSV";
            this.csvMenu.Click += new System.EventHandler(this.csvMenu_Click);
            // 
            // stdMenu
            // 
            this.stdMenu.Name = "stdMenu";
            this.stdMenu.Size = new System.Drawing.Size(260, 24);
            this.stdMenu.Text = "Из STD (дерево решений)";
            this.stdMenu.Click += new System.EventHandler(this.stdMenu_Click);
            // 
            // ekbMenu
            // 
            this.ekbMenu.Name = "ekbMenu";
            this.ekbMenu.Size = new System.Drawing.Size(260, 24);
            this.ekbMenu.Text = "Из EKB (база знаний)";
            this.ekbMenu.Click += new System.EventHandler(this.ekbMenu_Click);
            // 
            // eetdMenu
            // 
            this.eetdMenu.Name = "eetdMenu";
            this.eetdMenu.Size = new System.Drawing.Size(260, 24);
            this.eetdMenu.Text = "Из EETD (дерево событий)";
            this.eetdMenu.Click += new System.EventHandler(this.eetdMenu_Click);
            // 
            // tableMenu
            // 
            this.tableMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTableMenu,
            this.addTableMenu,
            this.deleteTableMenu,
            this.addRowMenu,
            this.addColumnMenu,
            this.deleteRowMenu,
            this.deleteColumnMenu,
            this.сохранитьТаблицуToolStripMenuItem,
            this.импортироватьДанныеToolStripMenuItem,
            this.checkRulesMenu});
            this.tableMenu.Name = "tableMenu";
            this.tableMenu.Size = new System.Drawing.Size(78, 23);
            this.tableMenu.Text = "Таблица";
            // 
            // saveTableMenu
            // 
            this.saveTableMenu.Image = global::Decision_Tables_Editor.Properties.Resources.inproject;
            this.saveTableMenu.Name = "saveTableMenu";
            this.saveTableMenu.Size = new System.Drawing.Size(298, 24);
            this.saveTableMenu.Text = "Сохранить таблицу в проект";
            this.saveTableMenu.Click += new System.EventHandler(this.saveTableMenu_Click);
            // 
            // addTableMenu
            // 
            this.addTableMenu.Image = global::Decision_Tables_Editor.Properties.Resources.addtable;
            this.addTableMenu.Name = "addTableMenu";
            this.addTableMenu.Size = new System.Drawing.Size(298, 24);
            this.addTableMenu.Text = "Добавить таблицу";
            this.addTableMenu.Click += new System.EventHandler(this.addTableMenu_Click);
            // 
            // deleteTableMenu
            // 
            this.deleteTableMenu.Image = global::Decision_Tables_Editor.Properties.Resources.deltable;
            this.deleteTableMenu.Name = "deleteTableMenu";
            this.deleteTableMenu.Size = new System.Drawing.Size(298, 24);
            this.deleteTableMenu.Text = "Удалить таблицу";
            this.deleteTableMenu.Click += new System.EventHandler(this.deleteTableMenu_Click);
            // 
            // addRowMenu
            // 
            this.addRowMenu.Image = global::Decision_Tables_Editor.Properties.Resources.addrow;
            this.addRowMenu.Name = "addRowMenu";
            this.addRowMenu.Size = new System.Drawing.Size(298, 24);
            this.addRowMenu.Text = "Добавить строку";
            this.addRowMenu.Click += new System.EventHandler(this.addRowMenu_Click);
            // 
            // addColumnMenu
            // 
            this.addColumnMenu.Image = global::Decision_Tables_Editor.Properties.Resources.addcol;
            this.addColumnMenu.Name = "addColumnMenu";
            this.addColumnMenu.Size = new System.Drawing.Size(298, 24);
            this.addColumnMenu.Text = "Добавить столбец";
            this.addColumnMenu.Click += new System.EventHandler(this.addColumnMenu_Click);
            // 
            // deleteRowMenu
            // 
            this.deleteRowMenu.Image = global::Decision_Tables_Editor.Properties.Resources.delrow;
            this.deleteRowMenu.Name = "deleteRowMenu";
            this.deleteRowMenu.Size = new System.Drawing.Size(298, 24);
            this.deleteRowMenu.Text = "Удалить строку";
            this.deleteRowMenu.Click += new System.EventHandler(this.deleteRowMenu_Click);
            // 
            // deleteColumnMenu
            // 
            this.deleteColumnMenu.Image = global::Decision_Tables_Editor.Properties.Resources.delcol;
            this.deleteColumnMenu.Name = "deleteColumnMenu";
            this.deleteColumnMenu.Size = new System.Drawing.Size(298, 24);
            this.deleteColumnMenu.Text = "Удалить столбец";
            this.deleteColumnMenu.Click += new System.EventHandler(this.deleteColumnMenu_Click);
            // 
            // сохранитьТаблицуToolStripMenuItem
            // 
            this.сохранитьТаблицуToolStripMenuItem.Image = global::Decision_Tables_Editor.Properties.Resources.saveCSV;
            this.сохранитьТаблицуToolStripMenuItem.Name = "сохранитьТаблицуToolStripMenuItem";
            this.сохранитьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(298, 24);
            this.сохранитьТаблицуToolStripMenuItem.Text = "Экспортировать таблицу (CSV)";
            this.сохранитьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьТаблицуToolStripMenuItem_Click);
            // 
            // импортироватьДанныеToolStripMenuItem
            // 
            this.импортироватьДанныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDataCSVMenu,
            this.importDataSTDMenu,
            this.importDataEKBMenu,
            this.importDataEETDMenu});
            this.импортироватьДанныеToolStripMenuItem.Image = global::Decision_Tables_Editor.Properties.Resources.importData;
            this.импортироватьДанныеToolStripMenuItem.Name = "импортироватьДанныеToolStripMenuItem";
            this.импортироватьДанныеToolStripMenuItem.Size = new System.Drawing.Size(298, 24);
            this.импортироватьДанныеToolStripMenuItem.Text = "Импортировать данные";
            // 
            // importDataCSVMenu
            // 
            this.importDataCSVMenu.Name = "importDataCSVMenu";
            this.importDataCSVMenu.Size = new System.Drawing.Size(137, 24);
            this.importDataCSVMenu.Text = "Из CSV";
            this.importDataCSVMenu.Click += new System.EventHandler(this.importDataCSVMenu_Click);
            // 
            // importDataSTDMenu
            // 
            this.importDataSTDMenu.Name = "importDataSTDMenu";
            this.importDataSTDMenu.Size = new System.Drawing.Size(137, 24);
            this.importDataSTDMenu.Text = "Из STD";
            this.importDataSTDMenu.Click += new System.EventHandler(this.importDataSTDMenu_Click);
            // 
            // importDataEKBMenu
            // 
            this.importDataEKBMenu.Name = "importDataEKBMenu";
            this.importDataEKBMenu.Size = new System.Drawing.Size(137, 24);
            this.importDataEKBMenu.Text = "Из EKB";
            this.importDataEKBMenu.Click += new System.EventHandler(this.importDataEKBMenu_Click);
            // 
            // importDataEETDMenu
            // 
            this.importDataEETDMenu.Name = "importDataEETDMenu";
            this.importDataEETDMenu.Size = new System.Drawing.Size(137, 24);
            this.importDataEETDMenu.Text = "Из EETD";
            this.importDataEETDMenu.Click += new System.EventHandler(this.importDataEETDMenu_Click);
            // 
            // checkRulesMenu
            // 
            this.checkRulesMenu.Image = global::Decision_Tables_Editor.Properties.Resources.check;
            this.checkRulesMenu.Name = "checkRulesMenu";
            this.checkRulesMenu.Size = new System.Drawing.Size(298, 24);
            this.checkRulesMenu.Text = "Проверить совпадающие строки";
            this.checkRulesMenu.Click += new System.EventHandler(this.checkRulesMenu_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructionMenu,
            this.documentationMenu});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(76, 23);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // instructionMenu
            // 
            this.instructionMenu.Image = global::Decision_Tables_Editor.Properties.Resources.about;
            this.instructionMenu.Name = "instructionMenu";
            this.instructionMenu.Size = new System.Drawing.Size(203, 24);
            this.instructionMenu.Text = "Рук. пользователя";
            this.instructionMenu.Click += new System.EventHandler(this.instructionMenu_Click);
            // 
            // documentationMenu
            // 
            this.documentationMenu.Image = global::Decision_Tables_Editor.Properties.Resources.doc;
            this.documentationMenu.Name = "documentationMenu";
            this.documentationMenu.Size = new System.Drawing.Size(203, 24);
            this.documentationMenu.Text = "Тех. документация";
            this.documentationMenu.Click += new System.EventHandler(this.documentationMenu_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 629);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(333, 629);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(863, 629);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 658);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор таблиц решений";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableMenu;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem saveProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.ToolStripMenuItem createProjectMenu;
        private System.Windows.Forms.ToolStripMenuItem importDataMenu;
        private System.Windows.Forms.ToolStripMenuItem csvMenu;
        private System.Windows.Forms.ToolStripMenuItem stdMenu;
        private System.Windows.Forms.ToolStripMenuItem ekbMenu;
        private System.Windows.Forms.ToolStripMenuItem eetdMenu;
        private System.Windows.Forms.ToolStripMenuItem addTableMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteTableMenu;
        private System.Windows.Forms.ToolStripMenuItem addRowMenu;
        private System.Windows.Forms.ToolStripMenuItem addColumnMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteRowMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteColumnMenu;
        private System.Windows.Forms.ToolStripMenuItem instructionMenu;
        private System.Windows.Forms.ToolStripMenuItem documentationMenu;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьТаблицуToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem saveTableMenu;
        private System.Windows.Forms.ToolStripMenuItem импортироватьДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataCSVMenu;
        private System.Windows.Forms.ToolStripMenuItem importDataSTDMenu;
        private System.Windows.Forms.ToolStripMenuItem importDataEKBMenu;
        private System.Windows.Forms.ToolStripMenuItem importDataEETDMenu;
        private System.Windows.Forms.ToolStripMenuItem checkRulesMenu;
    }
}

