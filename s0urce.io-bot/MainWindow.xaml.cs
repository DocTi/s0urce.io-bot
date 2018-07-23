using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace s0urce.io_bot
{
    public partial class MainWindow : Window
    {
        private IEnumerable<Process> processes;

        public MainWindow()
        {
            InitializeComponent();

            processes = Process
                .GetProcesses()
                .Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle) && !string.IsNullOrWhiteSpace(p.ProcessName));

            ProcessBox.ItemsSource = processes;
            ProcessBox.DisplayMemberPath = nameof(Process.MainWindowTitle);
        }


    }
}
