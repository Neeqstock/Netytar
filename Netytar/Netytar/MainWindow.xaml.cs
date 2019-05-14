using NeeqDMIs.ATmega;
using NeeqDMIs.Music;
using Netytar.DMIbox;
using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Tobii.Interaction.Wpf;

namespace Netytar
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int breathSensorValue = 0;
        public int BreathSensorValue { get => breathSensorValue; set => breathSensorValue = value; }

        private Scale StartingScale = ScalesFactory.Cmaj;

        private Scale lastScale;
        private Scale selectedScale;
        public Scale SelectedScale { get => selectedScale; set => selectedScale = value; }

        private const int BreathMax = 340;

        private readonly SolidColorBrush ActiveBrush = new SolidColorBrush(Colors.LightGreen);
        private readonly SolidColorBrush WarningBrush = new SolidColorBrush(Colors.DarkRed);
        private readonly SolidColorBrush BlankBrush = new SolidColorBrush(Colors.Black);

        private int sensorPort = 11;
        public int SensorPort
        {
            get { return sensorPort; }
            set
            {
                if(value > 0)
                {
                    sensorPort = value;
                }
            }
        }

        private bool NetytarStarted = false;
        
        private Timer updater;

        private double velocityBarMaxHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            updater = new Timer();
            updater.Interval = 10;
            updater.Tick += UpdateWindow;
            updater.Start();

            lastScale = StartingScale;
            SelectedScale = StartingScale;
            
            //Behaviors.SetIsGazeAware(btnNetytarSelect, true);
            //Behaviors.AddHasGazeChangedHandler(btnNetytarSelect, eyeGazeHandler);
            
        }

        private void eyeGazeHandler(object sender, HasGazeChangedRoutedEventArgs e)
        {
            if (e.HasGaze)
            {
                NetytarRack.DMIBox.HasAButtonGaze = true;
                NetytarRack.DMIBox.LastGazedButton = (System.Windows.Controls.Button)sender;
            }
            else
            {
                NetytarRack.DMIBox.HasAButtonGaze = false;
            }
        }

        private void UpdateWindow(object sender, EventArgs e)
        {
            if (NetytarStarted)
            {
                VelocityBar.Height = (velocityBarMaxHeight * breathSensorValue) / BreathMax;

                if (SelectedScale.GetName().Equals(lastScale.GetName()) == false)
                {
                    lastScale = selectedScale;
                    NetytarRack.DMIBox.NetytarSurface.Scale = selectedScale;
                }

                txtNoteName.Text = NetytarRack.DMIBox.SelectedNote.ToStandardString();
                txtPitch.Text = NetytarRack.DMIBox.SelectedNote.ToPitchValue().ToString();
                if (NetytarRack.DMIBox.Blow)
                {
                    txtIsBlowing.Text = "B";
                }
                else
                {
                    txtIsBlowing.Text = "_";
                }

                
                try
                {
                    txtEyePosX.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.X.ToString();
                    txtEyePosY.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y.ToString();
                    txtEyePosZ.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Z.ToString();
                }
                catch
                {

                }
                

            }
        }

        private void StartNetytar(object sender, RoutedEventArgs e)
        {
            AddScaleListItems();

            NetytarSetup netytarSetup = new NetytarSetup(this);
            netytarSetup.Setup();

            InitializeVolumeBar();
            InitializeSensorPortText();

            if(NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.Keyboard)
            {
                indCtrlKeyboard.Background = ActiveBrush;
            }

            if (NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.BreathSensor)
            {
                indCtrlBreath.Background = ActiveBrush;
            }

            NetytarStarted = true;

            btnStart.IsEnabled = false;
            btnStart.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void InitializeSensorPortText()
        {
            txtSensorPort.Foreground = WarningBrush;
            txtSensorPort.Text = NetytarRack.DMIBox.SensorReader.PortPrefix + SensorPort;
            UpdateSensorConnection();
        }

        private void InitializeVolumeBar()
        {
            velocityBarMaxHeight = VelocityBarBorder.ActualHeight;
            VelocityBar.Height = 0;
            MaxBar.Height = VelocityBar.Height = (velocityBarMaxHeight * 127) / BreathMax;
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.NetytarSurface.DrawScale();
        }

        private void AddScaleListItems()
        {
            foreach (Scale scale in ScalesFactory.GetList())
            {
                ListBoxItem item = new ListBoxItem() { Content = scale.GetName() };
                lstScaleChanger.Items.Add(item);
            }
        }

        internal void ChangeScale(ScaleCodes scaleCode)
        {
            NetytarRack.DMIBox.NetytarSurface.Scale = new Scale(NetytarRack.DMIBox.SelectedNote.ToAbsNote(), scaleCode); 
        }

        public void ReceiveNoteChange()
        {
            txtPitch.Text = NetytarRack.DMIBox.SelectedNote.ToPitchValue().ToString();
            txtNoteName.Text = NetytarRack.DMIBox.SelectedNote.ToStandardString();
        }

        public void ReceiveBlowingChange()
        {
            if (NetytarRack.DMIBox.Blow)
            {
                txtIsBlowing.Text = "B";
            }
            else
            {
                txtIsBlowing.Text = "_";
            }
        }

        private void BtnScroll_Click(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.AutoScroller.Enabled = !NetytarRack.DMIBox.AutoScroller.Enabled;
        }

        private void BtnFFBTest_Click(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.FfbModule.FlashFFB();
        }

        private void LstScaleChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NetytarRack.DMIBox.NetytarSurface.Scale = Scale.FromString(((ListBoxItem)lstScaleChanger.SelectedItem).Content.ToString());
        }

        private void btnMIDIchMinus_Click(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.MidiModule.OutDevice--;
        }

        private void btnMIDIchPlus_Click(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.MidiModule.OutDevice++;
        }

        private void btnCtrlKeyboard_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                indCtrlKeyboard.Background = ActiveBrush;
                indCtrlBreath.Background = BlankBrush;
                indCtrlEyePos.Background = BlankBrush;

                NetytarRack.DMIBox.NetytarControlMode = NetytarControlModes.Keyboard;
            }
        }

        private void btnCtrlBreath_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                indCtrlKeyboard.Background = BlankBrush;
                indCtrlBreath.Background = ActiveBrush;
                indCtrlEyePos.Background = BlankBrush;

                NetytarRack.DMIBox.NetytarControlMode = NetytarControlModes.BreathSensor;
            }
        }

        private void btnSensorPortPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                SensorPort++;
                UpdateSensorConnection();
            }
        }

        private void UpdateSensorConnection()
        {
            txtSensorPort.Text = NetytarRack.DMIBox.SensorReader.PortPrefix + SensorPort.ToString();

            if (NetytarRack.DMIBox.SensorReader.Connect(SensorPort))
            {
                txtSensorPort.Foreground = ActiveBrush;
            }
            else
            {
                txtSensorPort.Foreground = WarningBrush;
            }
        }

        private void btnSensorPortMinus_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                SensorPort--;
                UpdateSensorConnection();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NetytarRack.DMIBox.Dispose();
            Close();
        }

        private void btnCtrlEyePos_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                indCtrlKeyboard.Background = BlankBrush;
                indCtrlBreath.Background = BlankBrush;
                indCtrlEyePos.Background = ActiveBrush;

                NetytarRack.DMIBox.NetytarControlMode = NetytarControlModes.EyePos;
            }
        }

        

        private void btnExit_Activate(object sender, RoutedEventArgs e)
        {

        }

        
        private void btnCalibrateEyePos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCalibrateEyePos.Background = new SolidColorBrush(Colors.LightGreen);

            if(NetytarRack.DMIBox.EyeXModule.LastEyePosition != null)
            {
                NetytarRack.DMIBox.EyePosBaseX = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.X;
                NetytarRack.DMIBox.EyePosBaseY = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y;
                NetytarRack.DMIBox.EyePosBaseZ = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Z;
            }
        }

        
        private void btnCalibrateEyePos_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCalibrateEyePos.Background = new SolidColorBrush(Colors.Black);
        }

        private void btnTestClick(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(NetytarRack.DMIBox.EyePosBaseY + "    " + NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y + "      " + (float)(NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y - NetytarRack.DMIBox.EyePosBaseY));
        }
    }
}