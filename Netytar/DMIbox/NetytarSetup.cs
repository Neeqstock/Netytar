using NeeqDMIs.ATmega;
using NeeqDMIs.Eyetracking.Eyetribe;
using NeeqDMIs.Eyetracking.PointFilters;
using NeeqDMIs.Eyetracking.Tobii;
using NeeqDMIs.Eyetracking.Utils;
using NeeqDMIs.Keyboard;
using NeeqDMIs.MIDI;
using NeeqDMIs.Music;
using Netytar.DMIbox.KeyboardBehaviors;
using Netytar.DMIbox.SensorBehaviors;
using Netytar.DMIbox.TobiiBehaviors;
using System;
using System.Windows.Interop;
using Tobii.Interaction.Framework;

namespace Netytar.DMIbox
{
    public class NetytarSetup
    {
        public NetytarSetup(MainWindow window)
        {
            Rack.DMIBox.NetytarMainWindow = window;
        }

        public void Setup()
        {
            IntPtr windowHandle = new WindowInteropHelper(Rack.DMIBox.NetytarMainWindow).Handle;

            Rack.DMIBox.KeyboardModule = new KeyboardModule(windowHandle);

            // MIDI
            Rack.DMIBox.MidiModule = new MidiModuleNAudio(1, 1);
            MidiDeviceFinder midiDeviceFinder = new MidiDeviceFinder(Rack.DMIBox.MidiModule);
            midiDeviceFinder.SetToLastDevice();

            // EYETRACKER
            if(Rack.DMIBox.Eyetracker == Eyetracker.Tobii)
            {
                Rack.DMIBox.TobiiModule = new TobiiModule(GazePointDataMode.Unfiltered);
                Rack.DMIBox.TobiiModule.Start();
                Rack.DMIBox.TobiiModule.HeadPoseBehaviors.Add(new HPBpitchPlay(10, 15, 1.5f, 30f));
                Rack.DMIBox.TobiiModule.HeadPoseBehaviors.Add(new HPBvelocityPlay(8, 12, 2f, 120f, 0.2f));
            }

            if(Rack.DMIBox.Eyetracker == Eyetracker.Eyetribe)
            {
                Rack.DMIBox.EyeTribeModule = new EyeTribeModule();
                Rack.DMIBox.EyeTribeModule.Start();
                Rack.DMIBox.EyeTribeModule.MouseEmulator = new MouseEmulator(new PointFilterBypass());
                Rack.DMIBox.EyeTribeModule.MouseEmulatorGazeMode = GazeMode.Raw;
            }


            // MISCELLANEOUS
            Rack.DMIBox.SensorReader = new SensorModule("COM", 9600);

            // BEHAVIORS
            Rack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBemulateMouse());
            Rack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBsimulateBlow());
            Rack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBselectScale());

            Rack.DMIBox.TobiiModule.BlinkBehaviors.Add(new EBBselectScale(Rack.DMIBox.NetytarMainWindow));
            Rack.DMIBox.TobiiModule.BlinkBehaviors.Add(new EBBrepeatNote());

            Rack.DMIBox.SensorReader.Behaviors.Add(new SBbreathSensor(20, 28, 1.5f)); // 15 20
            //Rack.DMIBox.SensorReader.Behaviors.Add(new SBaccelerometerTest());
            Rack.DMIBox.SensorReader.Behaviors.Add(new SBreadSerial());


            // SURFACE
            IDimension dimension = new DimensionInvert();
            IColorCode colorCode = new ColorCodeStandard();
            IButtonsSettings buttonsSettings = new ButtonsSettingsInvert();
            NetytarSurfaceDrawModes drawMode = NetytarSurfaceDrawModes.AllLines;

            Rack.DMIBox.AutoScroller = new AutoScroller(Rack.DMIBox.NetytarMainWindow.scrlNetytar, 0, 100, new PointFilterMAExpDecaying(0.1f)); // OLD was 100, 0.1f
            Rack.DMIBox.NetytarSurface = new NetytarSurface(Rack.DMIBox.NetytarMainWindow.canvasNetytar, dimension, colorCode, buttonsSettings, drawMode);
            Rack.DMIBox.NetytarSurface.DrawButtons();
            Rack.DMIBox.NetytarSurface.Scale = ScalesFactory.Cmaj;
        }
    }
}
