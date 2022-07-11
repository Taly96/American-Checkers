using System.Windows.Forms;

namespace GameUI
{
    partial class FormGameSettings 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameSettings));
            this.BoardSizeLabel = new System.Windows.Forms.Label();
            this.radioButtonSizeSmall = new System.Windows.Forms.RadioButton();
            this.radioButtonSizeMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonSizeLarge = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeLabel
            // 
            this.BoardSizeLabel.AutoSize = true;
            this.BoardSizeLabel.Location = new System.Drawing.Point(12, 9);
            this.BoardSizeLabel.Name = "BoardSizeLabel";
            this.BoardSizeLabel.Size = new System.Drawing.Size(91, 20);
            this.BoardSizeLabel.TabIndex = 1;
            this.BoardSizeLabel.Text = "Board Size:";
            // 
            // radioButtonSizeSmall
            // 
            this.radioButtonSizeSmall.AutoSize = true;
            this.radioButtonSizeSmall.Location = new System.Drawing.Point(16, 32);
            this.radioButtonSizeSmall.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonSizeSmall.Name = "radioButtonSizeSmall";
            this.radioButtonSizeSmall.Size = new System.Drawing.Size(67, 24);
            this.radioButtonSizeSmall.TabIndex = 2;
            this.radioButtonSizeSmall.TabStop = true;
            this.radioButtonSizeSmall.Text = "6 x 6";
            this.radioButtonSizeSmall.UseVisualStyleBackColor = true;
            // 
            // radioButtonSizeMedium
            // 
            this.radioButtonSizeMedium.AutoSize = true;
            this.radioButtonSizeMedium.Location = new System.Drawing.Point(88, 32);
            this.radioButtonSizeMedium.Name = "radioButtonSizeMedium";
            this.radioButtonSizeMedium.Size = new System.Drawing.Size(67, 24);
            this.radioButtonSizeMedium.TabIndex = 3;
            this.radioButtonSizeMedium.TabStop = true;
            this.radioButtonSizeMedium.Text = "8 x 8";
            this.radioButtonSizeMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonSizeLarge
            // 
            this.radioButtonSizeLarge.AutoSize = true;
            this.radioButtonSizeLarge.Location = new System.Drawing.Point(161, 32);
            this.radioButtonSizeLarge.Name = "radioButtonSizeLarge";
            this.radioButtonSizeLarge.Size = new System.Drawing.Size(85, 24);
            this.radioButtonSizeLarge.TabIndex = 4;
            this.radioButtonSizeLarge.TabStop = true;
            this.radioButtonSizeLarge.Text = "10 x 10";
            this.radioButtonSizeLarge.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(12, 67);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(64, 20);
            this.labelPlayers.TabIndex = 5;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(14, 99);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(69, 20);
            this.labelPlayer1.TabIndex = 6;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxPlayer1.Location = new System.Drawing.Point(132, 96);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(100, 26);
            this.textBoxPlayer1.TabIndex = 7;
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(18, 140);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(95, 24);
            this.checkBoxPlayer2.TabIndex = 8;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(132, 138);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(100, 26);
            this.textBoxPlayer2.TabIndex = 9;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(239, 248);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(96, 36);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 323);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButtonSizeLarge);
            this.Controls.Add(this.radioButtonSizeMedium);
            this.Controls.Add(this.radioButtonSizeSmall);
            this.Controls.Add(this.BoardSizeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label BoardSizeLabel;
        private System.Windows.Forms.RadioButton radioButtonSizeSmall;
        private System.Windows.Forms.RadioButton radioButtonSizeMedium;
        private System.Windows.Forms.RadioButton radioButtonSizeLarge;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button buttonDone;
    }
}

