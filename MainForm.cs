using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LTFGameLauncher
{
    public partial class MainForm : Form
    {
        private string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Process _proc = new Process();
        private Timer _waitTimer = new Timer();

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

            if(string.IsNullOrWhiteSpace(Properties.Settings.Default.PlayButtonText) == false)
            {
                this.PlayButton.Text = Properties.Settings.Default.PlayButtonText;
            }
            
            this.DisableGraphicalWrapperCheckBox.Location = new Point(this.Width / 2 - this.DisableGraphicalWrapperCheckBox.Width / 2, this.DisableGraphicalWrapperCheckBox.Location.Y);
            this.GraphicsWrapperCheckBoxLabel.Location = new Point(this.Width / 2 - this.GraphicsWrapperCheckBoxLabel.Width / 2, this.GraphicsWrapperCheckBoxLabel.Location.Y);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsGraphicalWrapperDisabled = this.DisableGraphicalWrapperCheckBox.Checked;
            Properties.Settings.Default.Save();
            _waitTimer.Dispose();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                if(string.IsNullOrWhiteSpace(Properties.Settings.Default.WarningMessage) == false)
                {
                    MessageBox.Show(Properties.Settings.Default.WarningMessage, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if(Properties.Settings.Default.KillProcessOnExit == false)
                {
                    Process.Start(Path.Combine(_workDir, Properties.Settings.Default.GameExecutable));
                    Application.Exit();
                }
                _proc = Process.Start(Path.Combine(_workDir, Properties.Settings.Default.GameExecutable));
                WindowState = FormWindowState.Minimized;
                _proc.WaitForInputIdle(5000);
                _waitTimer.Tick += _waitTimer_Tick;
                _waitTimer.Interval = 2000;
                _waitTimer.Enabled = true;
                _waitTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.GameExecutable + " :(");
            }
        }

        private void _waitTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _proc.Refresh();
                if(_proc.MainWindowHandle == IntPtr.Zero || _proc.MainWindowHandle == null)
                {
                    _proc.Kill();
                    Application.Exit();
                }
            }
            catch
            {
                try
                {
                    _proc.Kill();
                    Application.Exit();
                }
                catch
                {
                }
                Application.Exit();
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
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.SetupExeName));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.SetupExeName + " :(");
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
