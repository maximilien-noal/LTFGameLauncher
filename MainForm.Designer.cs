namespace LTFGameLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PlayButton = new System.Windows.Forms.Button();
            this.DisableGraphicalWrapperCheckBox = new System.Windows.Forms.CheckBox();
            this.GraphicsWrapperCheckBoxLabel = new System.Windows.Forms.Label();
            this.ManualButton = new System.Windows.Forms.Button();
            this.SetupButton = new System.Windows.Forms.Button();
            this.Action1Button = new System.Windows.Forms.Button();
            this.Action2Button = new System.Windows.Forms.Button();
            this.Action3Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayButton
            // 
            resources.ApplyResources(this.PlayButton, "PlayButton");
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // DisableGraphicalWrapperCheckBox
            // 
            resources.ApplyResources(this.DisableGraphicalWrapperCheckBox, "DisableGraphicalWrapperCheckBox");
            this.DisableGraphicalWrapperCheckBox.Name = "DisableGraphicalWrapperCheckBox";
            this.DisableGraphicalWrapperCheckBox.UseVisualStyleBackColor = true;
            // 
            // GraphicsWrapperCheckBoxLabel
            // 
            resources.ApplyResources(this.GraphicsWrapperCheckBoxLabel, "GraphicsWrapperCheckBoxLabel");
            this.GraphicsWrapperCheckBoxLabel.Name = "GraphicsWrapperCheckBoxLabel";
            // 
            // ManualButton
            // 
            resources.ApplyResources(this.ManualButton, "ManualButton");
            this.ManualButton.Name = "ManualButton";
            this.ManualButton.UseVisualStyleBackColor = true;
            this.ManualButton.Click += new System.EventHandler(this.ManualButton_Click);
            // 
            // SetupButton
            // 
            resources.ApplyResources(this.SetupButton, "SetupButton");
            this.SetupButton.Name = "SetupButton";
            this.SetupButton.UseVisualStyleBackColor = true;
            this.SetupButton.Click += new System.EventHandler(this.SetupButton_Click);
            // 
            // Action1Button
            // 
            resources.ApplyResources(this.Action1Button, "Action1Button");
            this.Action1Button.Name = "Action1Button";
            this.Action1Button.UseVisualStyleBackColor = true;
            this.Action1Button.Click += new System.EventHandler(this.Action1Button_Click);
            // 
            // Action2Button
            // 
            resources.ApplyResources(this.Action2Button, "Action2Button");
            this.Action2Button.Name = "Action2Button";
            this.Action2Button.UseVisualStyleBackColor = true;
            this.Action2Button.Click += new System.EventHandler(this.Action2Button_Click);
            // 
            // Action3Button
            // 
            resources.ApplyResources(this.Action3Button, "Action3Button");
            this.Action3Button.Name = "Action3Button";
            this.Action3Button.UseVisualStyleBackColor = true;
            this.Action3Button.Click += new System.EventHandler(this.Action3Button_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Action3Button);
            this.Controls.Add(this.Action2Button);
            this.Controls.Add(this.Action1Button);
            this.Controls.Add(this.SetupButton);
            this.Controls.Add(this.ManualButton);
            this.Controls.Add(this.GraphicsWrapperCheckBoxLabel);
            this.Controls.Add(this.DisableGraphicalWrapperCheckBox);
            this.Controls.Add(this.PlayButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.CheckBox DisableGraphicalWrapperCheckBox;
        private System.Windows.Forms.Label GraphicsWrapperCheckBoxLabel;
        private System.Windows.Forms.Button ManualButton;
        private System.Windows.Forms.Button SetupButton;
        private System.Windows.Forms.Button Action1Button;
        private System.Windows.Forms.Button Action2Button;
        private System.Windows.Forms.Button Action3Button;
    }
}

