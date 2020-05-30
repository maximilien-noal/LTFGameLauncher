using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LTFGameLauncher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _waitTimer = new DispatcherTimer();

        private readonly string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private string _action1Label = "";

        private string _action2Label = "";

        private string _action3Label = "";

        private string _action4Label = "";

        private Uri _background;

        private string _graphicsWrapperName = "dgVoodoo2";

        private bool _isAction1ButtonVisible = true;

        private bool _isAction2ButtonVisible = true;

        private bool _isAction3ButtonVisible = true;

        private bool _isAction4ButtonVisible = true;

        private bool _isDisableGraphicalWrapperCheckBoxVisible = true;

        private bool _isGraphicalWrapperDisabled;

        private bool _isManualButtonVisible = true;

        private string _playButtonText = "";

        private Process _proc = new Process();

        private string _windowTitle = "Lanceur ";

        public MainViewModel()
        {
            PlayButtonCommand = new RelayCommand(PlayButtonCommand_Execute);
            ManualButtonCommand = new RelayCommand(ManualButtonCommand_Execute);
            Action1ButtonCommand = new RelayCommand(Action1ButtonCommand_Execute);
            Action2ButtonCommand = new RelayCommand(Action2ButtonCommand_Execute);
            Action3ButtonCommand = new RelayCommand(Action3ButtonCommand_Execute);
            Action4ButtonCommand = new RelayCommand(Action4ButtonCommand_Execute);

            if (File.Exists("background.png"))
            {
                Background = new Uri(Path.Combine(_workDir, "background.png"));
            }
            else if (File.Exists("background.jpg"))
            {
                Background = new Uri(Path.Combine(_workDir, "background.jpg"));
            }

            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() != "FR")
            {
                WindowTitle = "Launcher ";
            }

            App.Current.Exit += OnAppExit;

            WindowTitle += string.Format(" {0}", Properties.Settings.Default.GameName);

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.GraphicalWrapperName))
            {
                IsDisableGraphicalWrapperCheckBoxVisible = false;
            }

            GraphicsWrapperName = Properties.Settings.Default.GraphicalWrapperName;
            IsGraphicalWrapperDisabled = Properties.Settings.Default.IsGraphicalWrapperDisabled;

            if (File.Exists(Path.Combine(_workDir, "manuel.pdf")))
            {
                IsManualButtonVisible = true;
            }
            else if (File.Exists(Path.Combine(_workDir, "manual.pdf")))
            {
                IsManualButtonVisible = true;
            }
            else
            {
                IsManualButtonVisible = false;
            }

            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction1Exe))
            {
                IsAction1ButtonVisible = false;
            }
            else
            {
                Action1Label = Properties.Settings.Default.AdditionAction1Name;
            }

            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction2Exe))
            {
                IsAction2ButtonVisible = false;
            }
            else
            {
                Action2Label = Properties.Settings.Default.AdditionAction2Name;
            }

            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction3Exe))
            {
                IsAction3ButtonVisible = false;
            }
            else
            {
                Action3Label = Properties.Settings.Default.AdditionAction3Name;
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.AdditionAction4Exe))
            {
                IsAction4ButtonVisible = false;
            }
            else
            {
                Action4Label = Properties.Settings.Default.AdditionAction4Name;
            }

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.PlayButtonText) == false)
            {
                PlayButtonText = Properties.Settings.Default.PlayButtonText;
            }
        }

        public RelayCommand Action1ButtonCommand { get; private set; }

        public string Action1Label
        {
            get => _action1Label;
            set => Set(nameof(Action1Label), ref _action1Label, value);
        }

        public RelayCommand Action2ButtonCommand { get; private set; }

        public string Action2Label
        {
            get => _action2Label;
            set => Set(nameof(Action2Label), ref _action2Label, value);
        }

        public RelayCommand Action3ButtonCommand { get; private set; }

        public string Action3Label
        {
            get => _action3Label;
            set => Set(nameof(Action3Label), ref _action3Label, value);
        }

        public RelayCommand Action4ButtonCommand { get; private set; }

        public string Action4Label
        {
            get => _action4Label;
            set => Set(nameof(Action4Label), ref _action4Label, value);
        }

        public Uri Background
        {
            get => _background;
            set => Set(nameof(Background), ref _background, value);
        }

        public string GraphicsWrapperName
        {
            get => _graphicsWrapperName;
            set => Set(nameof(GraphicsWrapperName), ref _graphicsWrapperName, value);
        }

        public bool IsAction1ButtonVisible
        {
            get => _isAction1ButtonVisible;
            set => Set(nameof(IsAction1ButtonVisible), ref _isAction1ButtonVisible, value);
        }

        public bool IsAction2ButtonVisible
        {
            get => _isAction2ButtonVisible;
            set => Set(nameof(IsAction2ButtonVisible), ref _isAction2ButtonVisible, value);
        }

        public bool IsAction3ButtonVisible
        {
            get => _isAction3ButtonVisible;
            set => Set(nameof(IsAction3ButtonVisible), ref _isAction3ButtonVisible, value);
        }

        public bool IsAction4ButtonVisible
        {
            get => _isAction4ButtonVisible;
            set => Set(nameof(IsAction4ButtonVisible), ref _isAction4ButtonVisible, value);
        }

        public bool IsDisableGraphicalWrapperCheckBoxVisible
        {
            get => _isDisableGraphicalWrapperCheckBoxVisible;
            set => Set(nameof(IsDisableGraphicalWrapperCheckBoxVisible), ref _isDisableGraphicalWrapperCheckBoxVisible, value);
        }

        public bool IsGraphicalWrapperDisabled
        {
            get => _isGraphicalWrapperDisabled;
            set => Set(nameof(IsGraphicalWrapperDisabled), ref _isGraphicalWrapperDisabled, value);
        }

        public bool IsManualButtonVisible
        {
            get => _isManualButtonVisible;
            set => Set(nameof(IsManualButtonVisible), ref _isManualButtonVisible, value);
        }

        public bool IsPlayButtonTextUndefined => string.IsNullOrWhiteSpace(_playButtonText);

        public RelayCommand ManualButtonCommand { get; private set; }

        public RelayCommand PlayButtonCommand { get; private set; }

        public string PlayButtonText
        {
            get => _playButtonText;
            set => Set(nameof(PlayButtonText), ref _playButtonText, value);
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(nameof(WindowTitle), ref _windowTitle, value);
        }

        private void Action1ButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction1Exe));
                App.Current.MainWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction1Exe + " ? :(");
            }
        }

        private void Action2ButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction2Exe));
                App.Current.MainWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction2Exe + " ? :(");
            }
        }

        private void Action3ButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction3Exe));
                App.Current.MainWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction3Exe + " ? :(");
            }
        }

        private void Action4ButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                Process.Start(Path.Combine(_workDir, Properties.Settings.Default.AdditionAction4Exe));
                App.Current.MainWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.AdditionAction4Exe + " ? :(");
            }
        }

        private void EnableOrDisableGraphicsWrapper()
        {
            try
            {
                string[] wrapperFiles = { "D3DImm.DLL", "DDraw.DLL", "D3D8.dll", "D3D9.dll", "Glide.dll", "Glide2x.dll", "Glide3x.dll" };
                foreach (var path in wrapperFiles)
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

        private void ManualButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                if (File.Exists(Path.Combine(_workDir, "manuel.pdf")))
                {
                    Process.Start(Path.Combine(_workDir, "manuel.pdf"));
                }
                else if (File.Exists(Path.Combine(_workDir, "manual.pdf")))
                {
                    Process.Start(Path.Combine(_workDir, "manual.pdf"));
                }
                App.Current.MainWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de manuel.pdf / manual.pdf ? :(");
            }
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            Properties.Settings.Default.IsGraphicalWrapperDisabled = IsGraphicalWrapperDisabled;
            Properties.Settings.Default.Save();
        }

        private void PlayButtonCommand_Execute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.WarningMessage) == false)
                {
                    System.Windows.MessageBox.Show(Properties.Settings.Default.WarningMessage, "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (Properties.Settings.Default.KillProcessOnExit == false)
                {
                    Process.Start(Path.Combine(_workDir, Properties.Settings.Default.GameExecutable));
                    App.Current.MainWindow.Close();
                }
                else
                {
                    _proc = Process.Start(Path.Combine(_workDir, Properties.Settings.Default.GameExecutable));
                    App.Current.MainWindow.WindowState = WindowState.Minimized;
                    _proc.WaitForInputIdle(5000);
                    _waitTimer.Tick += WaitTimer_Tick;
                    _waitTimer.Interval = TimeSpan.FromMilliseconds(2000);
                    _waitTimer.IsEnabled = true;
                    _waitTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pas de " + Properties.Settings.Default.GameExecutable + " :(");
            }
        }

        private void RenameFile(string ddrawPath, string ddrawDisabledPath)
        {
            if (IsGraphicalWrapperDisabled)
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

        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _proc.Refresh();
                if (_proc.MainWindowHandle == IntPtr.Zero || _proc.MainWindowHandle == null)
                {
                    _proc.Kill();
                    App.Current.MainWindow.Close();
                }
            }
            catch
            {
                App.Current.MainWindow.Close();
            }
        }
    }
}