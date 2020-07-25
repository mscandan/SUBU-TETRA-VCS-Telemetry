namespace SubuTetraTelemetri
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.dataDisplayTextbox = new System.Windows.Forms.TextBox();
            this.portListCombo = new System.Windows.Forms.ComboBox();
            this.baudRateTextBox = new System.Windows.Forms.TextBox();
            this.portBaglanButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.portDisconnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataDisplayTextbox
            // 
            this.dataDisplayTextbox.Location = new System.Drawing.Point(534, 32);
            this.dataDisplayTextbox.Multiline = true;
            this.dataDisplayTextbox.Name = "dataDisplayTextbox";
            this.dataDisplayTextbox.ReadOnly = true;
            this.dataDisplayTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataDisplayTextbox.Size = new System.Drawing.Size(647, 610);
            this.dataDisplayTextbox.TabIndex = 0;
            this.dataDisplayTextbox.TextChanged += new System.EventHandler(this.dataDisplayTextbox_TextChanged);
            // 
            // portListCombo
            // 
            this.portListCombo.FormattingEnabled = true;
            this.portListCombo.Location = new System.Drawing.Point(26, 32);
            this.portListCombo.Name = "portListCombo";
            this.portListCombo.Size = new System.Drawing.Size(128, 33);
            this.portListCombo.TabIndex = 1;
            this.portListCombo.Text = "Port";
            // 
            // baudRateTextBox
            // 
            this.baudRateTextBox.Location = new System.Drawing.Point(188, 31);
            this.baudRateTextBox.Name = "baudRateTextBox";
            this.baudRateTextBox.Size = new System.Drawing.Size(145, 33);
            this.baudRateTextBox.TabIndex = 2;
            // 
            // portBaglanButton
            // 
            this.portBaglanButton.Location = new System.Drawing.Point(26, 91);
            this.portBaglanButton.Name = "portBaglanButton";
            this.portBaglanButton.Size = new System.Drawing.Size(128, 51);
            this.portBaglanButton.TabIndex = 3;
            this.portBaglanButton.Text = "Connect";
            this.portBaglanButton.UseVisualStyleBackColor = true;
            this.portBaglanButton.Click += new System.EventHandler(this.portBaglanButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // portDisconnectButton
            // 
            this.portDisconnectButton.Location = new System.Drawing.Point(188, 91);
            this.portDisconnectButton.Name = "portDisconnectButton";
            this.portDisconnectButton.Size = new System.Drawing.Size(145, 51);
            this.portDisconnectButton.TabIndex = 4;
            this.portDisconnectButton.Text = "Disconnect";
            this.portDisconnectButton.UseVisualStyleBackColor = true;
            this.portDisconnectButton.Click += new System.EventHandler(this.portDisconnectButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.portDisconnectButton);
            this.Controls.Add(this.portBaglanButton);
            this.Controls.Add(this.baudRateTextBox);
            this.Controls.Add(this.portListCombo);
            this.Controls.Add(this.dataDisplayTextbox);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "SUBU - TETRA TELEMETRI 2020";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dataDisplayTextbox;
        private System.Windows.Forms.ComboBox portListCombo;
        private System.Windows.Forms.TextBox baudRateTextBox;
        private System.Windows.Forms.Button portBaglanButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button portDisconnectButton;
    }
}

