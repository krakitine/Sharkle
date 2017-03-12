using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace Sharkle
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string[] files;
        private Timer timer;

        private int counter;
        private int Imagecounter;
        private bool goingUp;

        ImageSource _IdleImageSource = null;
        public ImageSource IdleImageSource
        {
            get
            {
                return _IdleImageSource;
            }
            set
            {
                _IdleImageSource = value;
                OnPropertyChanged("IdleImageSource");
            }
        }

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 5;
            this.Top = desktopWorkingArea.Bottom - this.Height - 5;

            timer = new Timer();
            timer.Elapsed += timer_Tick;
            timer.Interval = 100;
            Imagecounter = 4;
            counter = 0;
            goingUp = true;

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var key = string.Format("sharkle_idle_{0}", counter);
                IdleImageSource = (ImageSource)Resources[key];
            }));
            if (counter == Imagecounter)
                goingUp = false;
            if (counter == 0)
                goingUp = true;
            if (goingUp)
                counter++;
            else
                counter--;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
