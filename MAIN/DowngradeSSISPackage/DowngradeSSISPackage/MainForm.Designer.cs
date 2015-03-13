namespace DowngradeSSISPackage
{
    partial class MainForm
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
            this.txtSourcePackage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetPackage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDowngrade = new System.Windows.Forms.Button();
            this.btnSelectSourcePackage = new System.Windows.Forms.Button();
            this.btnSelectTargetPackage = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.linkCodeplex = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtSourcePackage
            // 
            this.txtSourcePackage.Location = new System.Drawing.Point(8, 31);
            this.txtSourcePackage.Name = "txtSourcePackage";
            this.txtSourcePackage.Size = new System.Drawing.Size(234, 20);
            this.txtSourcePackage.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Package";
            // 
            // txtTargetPackage
            // 
            this.txtTargetPackage.Location = new System.Drawing.Point(8, 84);
            this.txtTargetPackage.Name = "txtTargetPackage";
            this.txtTargetPackage.Size = new System.Drawing.Size(234, 20);
            this.txtTargetPackage.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target Package";
            // 
            // btnDowngrade
            // 
            this.btnDowngrade.Location = new System.Drawing.Point(195, 247);
            this.btnDowngrade.Name = "btnDowngrade";
            this.btnDowngrade.Size = new System.Drawing.Size(93, 32);
            this.btnDowngrade.TabIndex = 2;
            this.btnDowngrade.Text = "Downgrade";
            this.btnDowngrade.UseVisualStyleBackColor = true;
            this.btnDowngrade.Click += new System.EventHandler(this.btnDowngrade_Click);
            // 
            // btnSelectSourcePackage
            // 
            this.btnSelectSourcePackage.Location = new System.Drawing.Point(248, 29);
            this.btnSelectSourcePackage.Name = "btnSelectSourcePackage";
            this.btnSelectSourcePackage.Size = new System.Drawing.Size(40, 23);
            this.btnSelectSourcePackage.TabIndex = 3;
            this.btnSelectSourcePackage.Text = "...";
            this.btnSelectSourcePackage.UseVisualStyleBackColor = true;
            this.btnSelectSourcePackage.Click += new System.EventHandler(this.btnSelectSourcePackage_Click);
            // 
            // btnSelectTargetPackage
            // 
            this.btnSelectTargetPackage.Location = new System.Drawing.Point(248, 82);
            this.btnSelectTargetPackage.Name = "btnSelectTargetPackage";
            this.btnSelectTargetPackage.Size = new System.Drawing.Size(40, 23);
            this.btnSelectTargetPackage.TabIndex = 3;
            this.btnSelectTargetPackage.Text = "...";
            this.btnSelectTargetPackage.UseVisualStyleBackColor = true;
            this.btnSelectTargetPackage.Click += new System.EventHandler(this.btnSelectTargetPackage_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(8, 122);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(280, 115);
            this.txtStatus.TabIndex = 4;
            // 
            // linkCodeplex
            // 
            this.linkCodeplex.AutoSize = true;
            this.linkCodeplex.Location = new System.Drawing.Point(8, 257);
            this.linkCodeplex.Name = "linkCodeplex";
            this.linkCodeplex.Size = new System.Drawing.Size(155, 13);
            this.linkCodeplex.TabIndex = 5;
            this.linkCodeplex.TabStop = true;
            this.linkCodeplex.Text = "SSISDowngrade.codeplex.com";
            this.linkCodeplex.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCodeplex_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 291);
            this.Controls.Add(this.linkCodeplex);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnSelectTargetPackage);
            this.Controls.Add(this.btnSelectSourcePackage);
            this.Controls.Add(this.btnDowngrade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTargetPackage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSourcePackage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Downgrade SSIS Package";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourcePackage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetPackage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDowngrade;
        private System.Windows.Forms.Button btnSelectSourcePackage;
        private System.Windows.Forms.Button btnSelectTargetPackage;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.LinkLabel linkCodeplex;
    }
}

