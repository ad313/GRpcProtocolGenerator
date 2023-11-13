namespace EasyLogin
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxUser = new ListBox();
            listBoxSite = new ListBox();
            btnStart = new Button();
            listBoxInstance = new ListBox();
            btnClear = new Button();
            SuspendLayout();
            // 
            // listBoxUser
            // 
            listBoxUser.FormattingEnabled = true;
            listBoxUser.ItemHeight = 17;
            listBoxUser.Location = new Point(529, 12);
            listBoxUser.Name = "listBoxUser";
            listBoxUser.Size = new Size(240, 310);
            listBoxUser.TabIndex = 0;
            // 
            // listBoxSite
            // 
            listBoxSite.FormattingEnabled = true;
            listBoxSite.ItemHeight = 17;
            listBoxSite.Location = new Point(12, 12);
            listBoxSite.Name = "listBoxSite";
            listBoxSite.Size = new Size(511, 310);
            listBoxSite.TabIndex = 1;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 582);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 27);
            btnStart.TabIndex = 2;
            btnStart.Text = "启动";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // listBoxInstance
            // 
            listBoxInstance.FormattingEnabled = true;
            listBoxInstance.ItemHeight = 17;
            listBoxInstance.Location = new Point(12, 343);
            listBoxInstance.Name = "listBoxInstance";
            listBoxInstance.Size = new Size(757, 225);
            listBoxInstance.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(93, 582);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 27);
            btnClear.TabIndex = 4;
            btnClear.Text = "清理";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 622);
            Controls.Add(btnClear);
            Controls.Add(listBoxInstance);
            Controls.Add(btnStart);
            Controls.Add(listBoxSite);
            Controls.Add(listBoxUser);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            IsMdiContainer = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "网站自动登录器 V1.0";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxUser;
        private ListBox listBoxSite;
        private Button btnStart;
        private ListBox listBoxInstance;
        private Button btnClear;
    }
}