using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace LTFGameLauncher
{
    public partial class MainForm : Form
    {
        private string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainForm()
        {
            InitializeComponent();
            this.Text += string.Format(" {0}", Properties.Settings.Default.GameName);

            if(string.IsNullOrWhiteSpace(Properties.Settings.Default.GraphicalWrapperName))
            {
                this.DisableGraphicalWrapperCheckBox.Visible = false;
                this.GraphicsWrapperCheckBoxLabel.Visible = false;
            }
            this.DisableGraphicalWrapperCheckBox.Text = this.DisableGraphicalWrapperCheckBox.Text.Replace("dgVoodoo2", Properties.Settings.Default.GraphicalWrapperName);
            this.DisableGraphicalWrapperCheckBox.Checked = Properties.Settings.Default.IsGraphicalWrapperDisabled;

            Application.ApplicationExit += Application_ApplicationExit;

            if (File.Exists(Path.Combine(_workDir, "manuel.pdf")))
            {
                ManualButton.Visible = true;
            }
            else if (File.Exists(Path.Combine(_workDir, "manual.pdf")))
            {
                ManualButton.Visible = true;
            }
            else
            {
                ManualButton.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SetupExeName))
            {
                SetupButton.Visible = false;
            }

            if(String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction1Exe))
            {
                Action1Button.Visible = false;
            }
            else
            {
                Action1Button.Text = Properties.Settings.Default.AdditionAction1Name;
            }
            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction2Exe))
            {
                Action2Button.Visible = false;
            }
            else
            {
                Action2Button.Text = Properties.Settings.Default.AdditionAction2Name;
            }

            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction3Exe))
            {
                Action3Button.Visible = false;
            }
            else
            {
                Action3Button.Text = Properties.Settings.Default.AdditionAction3Name;
            }

            this.ManualButton.Location = new Point(Convert.ToInt32(this.Width / 2 - this.ManualButton.Width / 1.7), this.ManualButton.Location.Y);
            this.SetupButton.Location = new Point(Convert.ToInt32(this.Width / 2 - this.SetupButton.Width / 1.7), this.SetupButton.Location.Y);
            this.Action3Button.Location = new Point(Convert.ToInt32(this.Width / 2 - this.Action3Button.Width / 1.7), this.Action3Button.Location.Y);
            this.PlayButton.Location = new Point(Convert.ToInt32(this.Width / 2 - this.PlayButton.Width / 1.7), this.PlayButton.Location.Y);
            this.DisableGraphicalWrapperCheckBox.Location = new Point(this.Width / 2 - this.DisableGraphicalWrapperCheckBox.Width / 2, this.DisableGraphicalWrapperCheckBox.Location.Y);
            this.GraphicsWrapperCheckBoxLabel.Location = new Point(this.Width / 2 - this.GraphicsWrapperCheckBoxLabel.Width / 2, this.GraphicsWrapperCheckBoxLabel.Location.Y);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsGraphicalWrapperDisabled = this.DisableGraphicalWrapperCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                if(string.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction3Exe) == false)
                {
                    MessageBox.Show(Properties.Settings.Default.AdditionAction3Exe, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Process.Start(Path.Combine(_workDir, "lancer.bat"));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de lancer.bat ? :(");
            }
        }

        private void EnableOrDisableGraphicsWrapper()
        {
            try
            {
                string[] dgVoodooFiles = { "D3DImm.DLL", "DDraw.DLL", "D3D8.dll", "Glide.dll", "Glide2x.dll", "Glide3x.dll" };
                foreach (var path in dgVoodooFiles)
                {
                    string fullPath = Path.Combine(_workDir, path);
                    string fullPathDisabled = Path.Combine(_workDir, string.Format("{0}{1}", Path.GetFileNameWithoutExtension(path), "Disabled.dll"));
                    RenameFile(fullPath, fullPathDisabled);
                }
            }
            catch
            {

            }
        }

        private void RenameFile(string ddrawPath, string ddrawDisabledPath)
        {
            if (this.DisableGraphicalWrapperCheckBox.Checked)
            {
                if (File.Exists(ddrawPath))
                {
                    if (File.Exists(ddrawDisabledPath))
                    {
                        File.Delete(ddrawDisabledPath);
                    }
                    File.Move(ddrawPath, ddrawDisabledPath);
                }
            }
            else
            {
                if (File.Exists(ddrawDisabledPath))
                {
                    if (File.Exists(ddrawPath) == false)
                    {
                        File.Move(ddrawDisabledPath, ddrawPath);
                    }
                }
            }
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                if(File.Exists(Path.Combine(_workDir, "manuel.pdf")))
                {
                    Process.Start(Path.Combine(_workDir, "manuel.pdf"));
                }
                else if(File.Exists(Path.Combine(_workDir, "manual.pdf")))
                {
                    Process.Start(Path.Combine(_workDir, "manual.pdf"));
                }
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de manuel.pdf / manual.pdf ? :(");
            }
        }

        private void SetupButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, "Setup.exe"));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de setup.exe ? :(");
            }
        }

        private void Action1Button_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction1Exe));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction1Exe +  " ? :(");
            }
        }

        private void Action2Button_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction2Exe));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction2Exe + " ? :(");
            }
        }

        private void Action3Button_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction3Exe));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction3Exe + " ? :(");
            }
        }
    }
}
