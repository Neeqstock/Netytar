using NeeqDMIs.ATmega;
using NeeqDMIs.Music;
using Netytar.DMIbox;
using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Tobii.Interaction;
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

        private WpfInteractorAgent wpfInteractorAgent;

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

        public WpfInteractorAgent WpfInteractorAgent { get => wpfInteractorAgent; set => wpfInteractorAgent = value; }

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
                Rack.DMIBox.HasAButtonGaze = true;
                Rack.DMIBox.LastGazedButton = (System.Windows.Controls.Button)sender;
            }
            else
            {
                Rack.DMIBox.HasAButtonGaze = false;
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
                    Rack.DMIBox.NetytarSurface.Scale = selectedScale;
                }

                txtNoteName.Text = Rack.DMIBox.SelectedNote.ToStandardString();
                txtPitch.Text = Rack.DMIBox.SelectedNote.ToPitchValue().ToString();
                if (Rack.DMIBox.Blow)
                {
                    txtIsBlowing.Text = "B";
                }
                else
                {
                    txtIsBlowing.Text = "_";
                }

                /*
                try
                {
                    txtEyePosX.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.X.ToString();
                    txtEyePosY.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y.ToString();
                    txtEyePosZ.Text = NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Z.ToString();
                }
                catch
                {

                }*/

                txtTest.Text = Rack.DMIBox.TestString;

            }
        }

        private void StartNetytar(object sender, RoutedEventArgs e)
        {
            AddScaleListItems();

            NetytarSetup netytarSetup = new NetytarSetup(this);
            netytarSetup.Setup();

            //wpfInteractorAgent = NetytarRack.DMIBox.TobiiModule.TobiiHost.InitializeWpfAgent();

            InitializeVolumeBar();
            InitializeSensorPortText();

            if (Rack.DMIBox.NetytarControlMode == NetytarControlModes.Keyboard)
            {
                indCtrlKeyboard.Background = ActiveBrush;
            }

            if (Rack.DMIBox.NetytarControlMode == NetytarControlModes.BreathSensor)
            {
                indCtrlBreath.Background = ActiveBrush;
            }

            btnStart.IsEnabled = false;
            btnStart.Foreground = new SolidColorBrush(Colors.Black);

            CheckMidiPort();

            breathSensorValue = 0;

            UpdateIndicators();

            NetytarStarted = true; // LEAVE AT THE END!
        }

        private void UpdateIndicators()
        {
            switch (Rack.DMIBox.NetytarControlMode)
            {
                case NetytarControlModes.BreathSensor:
                    indCtrlKeyboard.Background = BlankBrush;
                    indCtrlBreath.Background = ActiveBrush;
                    indCtrlEyePos.Background = BlankBrush;
                    indCtrlEyeVel.Background = BlankBrush;
                    break;
                case NetytarControlModes.EyePos:
                    indCtrlKeyboard.Background = BlankBrush;
                    indCtrlBreath.Background = BlankBrush;
                    indCtrlEyePos.Background = ActiveBrush;
                    indCtrlEyeVel.Background = BlankBrush;
                    break;
                case NetytarControlModes.Keyboard:
                    indCtrlKeyboard.Background = ActiveBrush;
                    indCtrlBreath.Background = BlankBrush;
                    indCtrlEyePos.Background = BlankBrush;
                    indCtrlEyeVel.Background = BlankBrush;
                    break;
                case NetytarControlModes.EyeVel:
                    indCtrlKeyboard.Background = BlankBrush;
                    indCtrlBreath.Background = BlankBrush;
                    indCtrlEyePos.Background = BlankBrush;
                    indCtrlEyeVel.Background = ActiveBrush;
                    break;
            }
            switch (Rack.DMIBox.ModulationControlMode)
            {
                case ModulationControlModes.On:
                    indModulationControl.Background = ActiveBrush;
                    break;
                case ModulationControlModes.Off:
                    indModulationControl.Background = BlankBrush;
                    break;
            }
            switch (Rack.DMIBox.BreathControlMode)
            {
                case BreathControlModes.Switch:
                    indBreathSwitch.Background = ActiveBrush;
                    break;
                case BreathControlModes.Dynamic:
                    indBreathSwitch.Background = BlankBrush;
                    break;
            }
        }

        private void CheckMidiPort()
        {
            if (Rack.DMIBox.MidiModule.IsMidiOk())
            {
                lblMIDIch.Foreground = ActiveBrush;
            }
            else
            {
                lblMIDIch.Foreground = WarningBrush;
            }
        }

        private void InitializeSensorPortText()
        {
            txtSensorPort.Foreground = WarningBrush;
            txtSensorPort.Text = Rack.DMIBox.SensorReader.PortPrefix + SensorPort;
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
            Rack.DMIBox.NetytarSurface.DrawScale();
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
            Rack.DMIBox.NetytarSurface.Scale = new Scale(Rack.DMIBox.SelectedNote.ToAbsNote(), scaleCode); 
        }

        public void ReceiveNoteChange()
        {
            txtPitch.Text = Rack.DMIBox.SelectedNote.ToPitchValue().ToString();
            txtNoteName.Text = Rack.DMIBox.SelectedNote.ToStandardString();
        }

        public void ReceiveBlowingChange()
        {
            if (Rack.DMIBox.Blow)
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
            Rack.DMIBox.AutoScroller.Enabled = !Rack.DMIBox.AutoScroller.Enabled;
        }

        private void BtnFFBTest_Click(object sender, RoutedEventArgs e)
        {
            //Rack.DMIBox.FfbModule.FlashFFB();
        }

        private void LstScaleChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rack.DMIBox.NetytarSurface.Scale = Scale.FromString(((ListBoxItem)lstScaleChanger.SelectedItem).Content.ToString());
        }

        private void btnMIDIchMinus_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.MidiModule.OutDevice--;
                lblMIDIch.Text = "MP" + Rack.DMIBox.MidiModule.OutDevice.ToString();

                CheckMidiPort();
            }
        }

        private void btnMIDIchPlus_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.MidiModule.OutDevice++;
                lblMIDIch.Text = "MP" + Rack.DMIBox.MidiModule.OutDevice.ToString();

                CheckMidiPort();
            }
        }

        private void btnCtrlKeyboard_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.NetytarControlMode = NetytarControlModes.Keyboard;
                Rack.DMIBox.ResetModulationAndPressure();

                breathSensorValue = 0;

                UpdateIndicators();
            }
        }

        private void btnCtrlBreath_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.NetytarControlMode = NetytarControlModes.BreathSensor;
                Rack.DMIBox.ResetModulationAndPressure();

                breathSensorValue = 0;

                UpdateIndicators();
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
            txtSensorPort.Text = Rack.DMIBox.SensorReader.PortPrefix + SensorPort.ToString();

            if (Rack.DMIBox.SensorReader.Connect(SensorPort))
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
            Rack.DMIBox.Dispose();
            Close();
        }

        private void btnCtrlEyePos_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.NetytarControlMode = NetytarControlModes.EyePos;
                Rack.DMIBox.ResetModulationAndPressure();

                breathSensorValue = 0;

                UpdateIndicators();
            }
        }



        private void btnExit_Activate(object sender, RoutedEventArgs e)
        {

        }

        
        private void btnCalibrateHeadPose_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (NetytarStarted)
            {
                btnCalibrateHeadPose.Background = new SolidColorBrush(Colors.LightGreen);

                if(Rack.DMIBox.TobiiModule.LastHeadPoseData != null && Rack.DMIBox.TobiiModule.LastHeadPoseData.HasHeadPosition)
                {
                    Rack.DMIBox.HeadPoseBaseX = Rack.DMIBox.TobiiModule.LastHeadPoseData.HeadRotation.X;
                    Rack.DMIBox.HeadPoseBaseY = Rack.DMIBox.TobiiModule.LastHeadPoseData.HeadRotation.Y;
                    Rack.DMIBox.HeadPoseBaseZ = Rack.DMIBox.TobiiModule.LastHeadPoseData.HeadRotation.Z;
                }

                Rack.DMIBox.CalibrateGyroBase();
                Rack.DMIBox.CalibrateAccBase();


            }
        }

        
        private void btnCalibrateHeadPose_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCalibrateHeadPose.Background = new SolidColorBrush(Colors.Black);
        }

        private void btnTestClick(object sender, RoutedEventArgs e)
        {
            throw (new NotImplementedException("Test button is not set!"));
        }

        private void btnModulationControlSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                if (Rack.DMIBox.ModulationControlMode == ModulationControlModes.Off)
                {
                    Rack.DMIBox.ModulationControlMode = ModulationControlModes.On;
                }
                else if (Rack.DMIBox.ModulationControlMode == ModulationControlModes.On)
                {
                    Rack.DMIBox.ModulationControlMode = ModulationControlModes.Off;
                }
            }

            breathSensorValue = 0;

            UpdateIndicators();
        }

        private void btnBreathControlSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                if (Rack.DMIBox.BreathControlMode == BreathControlModes.Switch)
                {
                    Rack.DMIBox.BreathControlMode = BreathControlModes.Dynamic;
                }
                else if (Rack.DMIBox.BreathControlMode == BreathControlModes.Dynamic)
                {
                    Rack.DMIBox.BreathControlMode = BreathControlModes.Switch;
                }
            }

            breathSensorValue = 0;

            UpdateIndicators();
        }

        private void btnCtrlEyeVel_Click(object sender, RoutedEventArgs e)
        {
            if (NetytarStarted)
            {
                Rack.DMIBox.NetytarControlMode = NetytarControlModes.EyeVel;
                Rack.DMIBox.ResetModulationAndPressure();

                breathSensorValue = 0;

                UpdateIndicators();
            }
        }
    }
}