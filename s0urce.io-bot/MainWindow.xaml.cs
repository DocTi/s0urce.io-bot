using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using s0urce.io_bot_core.Core;

namespace s0urce.io_bot
{
    public partial class MainWindow : Window
    {
        CdmPanel cdmPanel;

        public MainWindow()
        {
            InitializeComponent();

            ProcessListBox.ItemsSource = Process.
                GetProcesses()
                .Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle) && !string.IsNullOrWhiteSpace(p.ProcessName));
            ProcessListBox.DisplayMemberPath = nameof(Process.MainWindowTitle);

#if DEBUG
            //
#endif
        }

        //TODO Rename
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(ProcessListBox.SelectedItem is Process selectProcess))
            {
                throw new Exception();
            }

            cdmPanel = CdmPanel.AutoInit(selectProcess);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            var result = cdmPanel.RecognizeText();
            TestLog.Text += $"\n{result}";
        }
    }
}
