namespace WSC
{
    partial class FormNodeSelecter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.depthSelectButton = new System.Windows.Forms.Button();
            this.depthLevelNumbericUpDown = new System.Windows.Forms.NumericUpDown();
            this.downloadButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.subNodeCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.depthLevelLabel = new System.Windows.Forms.Label();
            this.subNodeCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.depthLevelNumbericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subNodeCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // depthSelectButton
            // 
            this.depthSelectButton.Location = new System.Drawing.Point(581, 96);
            this.depthSelectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.depthSelectButton.Name = "depthSelectButton";
            this.depthSelectButton.Size = new System.Drawing.Size(75, 20);
            this.depthSelectButton.TabIndex = 0;
            this.depthSelectButton.Text = "Select";
            this.depthSelectButton.UseVisualStyleBackColor = true;
            this.depthSelectButton.Click += new System.EventHandler(this.depthSelectButton_Click);
            // 
            // depthLevelNumbericUpDown
            // 
            this.depthLevelNumbericUpDown.Location = new System.Drawing.Point(581, 20);
            this.depthLevelNumbericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.depthLevelNumbericUpDown.Name = "depthLevelNumbericUpDown";
            this.depthLevelNumbericUpDown.Size = new System.Drawing.Size(75, 23);
            this.depthLevelNumbericUpDown.TabIndex = 1;
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(581, 247);
            this.downloadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 20);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(25, 20);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(451, 224);
            this.treeView1.TabIndex = 3;
            // 
            // subNodeCountNumericUpDown
            // 
            this.subNodeCountNumericUpDown.Location = new System.Drawing.Point(581, 55);
            this.subNodeCountNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.subNodeCountNumericUpDown.Name = "subNodeCountNumericUpDown";
            this.subNodeCountNumericUpDown.Size = new System.Drawing.Size(75, 23);
            this.subNodeCountNumericUpDown.TabIndex = 5;
            // 
            // depthLevelLabel
            // 
            this.depthLevelLabel.AutoSize = true;
            this.depthLevelLabel.Location = new System.Drawing.Point(510, 20);
            this.depthLevelLabel.Name = "depthLevelLabel";
            this.depthLevelLabel.Size = new System.Drawing.Size(39, 15);
            this.depthLevelLabel.TabIndex = 6;
            this.depthLevelLabel.Text = "Depth";
            // 
            // subNodeCountLabel
            // 
            this.subNodeCountLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.subNodeCountLabel.AutoSize = true;
            this.subNodeCountLabel.Location = new System.Drawing.Point(510, 55);
            this.subNodeCountLabel.Name = "subNodeCountLabel";
            this.subNodeCountLabel.Size = new System.Drawing.Size(59, 15);
            this.subNodeCountLabel.TabIndex = 7;
            this.subNodeCountLabel.Text = "Sub Node";
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(510, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Count";
            // 
            // FormNodeSelecter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 287);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subNodeCountLabel);
            this.Controls.Add(this.depthLevelLabel);
            this.Controls.Add(this.subNodeCountNumericUpDown);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.depthLevelNumbericUpDown);
            this.Controls.Add(this.depthSelectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormNodeSelecter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Depth Level...";
            ((System.ComponentModel.ISupportInitialize)(this.depthLevelNumbericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subNodeCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button depthSelectButton;
        private System.Windows.Forms.NumericUpDown depthLevelNumbericUpDown;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.NumericUpDown subNodeCountNumericUpDown;
        private System.Windows.Forms.Label depthLevelLabel;
        private System.Windows.Forms.Label subNodeCountLabel;
        private System.Windows.Forms.Label label1;
        

    }
}