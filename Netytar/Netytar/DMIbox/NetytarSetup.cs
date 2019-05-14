using NeeqDMIs.ATmega;
using NeeqDMIs.Eyetracking.EyeX;
using NeeqDMIs.Eyetracking.Filters;
using NeeqDMIs.Keyboard;
using NeeqDMIs.MIDI;
using NeeqDMIs.Music;
using NeeqDMIs.Xbox360;
using Netytar.DMIbox.EyeXBehaviors;
using Netytar.DMIbox.KeyboardBehaviors;
using Netytar.DMIbox.SensorBehaviors;
using System;
using System.Windows.Interop;
using Tobii.EyeX.Framework;

namespace Netytar.DMIbox
{
    public class NetytarSetup
    {
        public NetytarSetup(MainWindow window)
        {
            NetytarRack.DMIBox.NetytarMainWindow = window;
        }

        public void Setup()
        {
            IntPtr windowHandle = new WindowInteropHelper(NetytarRack.DMIBox.NetytarMainWindow).Handle;

            NetytarRack.DMIBox.KeyboardModule = new KeyboardModule(windowHandle);

            // MIDI
            NetytarRack.DMIBox.MidiModule = new MidiModuleNAudio(1, 1);
            MidiDeviceFinder midiDeviceFinder = new MidiDeviceFinder(NetytarRack.DMIBox.MidiModule);
            midiDeviceFinder.SetToLastDevice();

            // EYETRACKER
            NetytarRack.DMIBox.EyeXModule = new EyeXModule(GazePointDataMode.Unfiltered);
            NetytarRack.DMIBox.EyeXModule.Start();

            // MISCELLANEOUS
            NetytarRack.DMIBox.FfbModule = new FFBModule();
            NetytarRack.DMIBox.SensorReader = new SensorReader("COM", 9600);

            // BEHAVIORS
            NetytarRack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBemulateMouse());
            NetytarRack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBsimulateBlow());
            NetytarRack.DMIBox.KeyboardModule.KeyboardBehaviors.Add(new KBselectScale());

            NetytarRack.DMIBox.EyeXModule.BlinkBehaviors.Add(new EBBselectScale(NetytarRack.DMIBox.NetytarMainWindow));
            NetytarRack.DMIBox.EyeXModule.BlinkBehaviors.Add(new EBBrepeatNote());

            NetytarRack.DMIBox.SensorReader.Behaviors.Add(new SBbreathSensor(15, 20, 1.5f));

            NetytarRack.DMIBox.EyeXModule.EyePositionBehaviors.Add(new EPBdynamicsBehavior(10, 15, 1.5f, 30f));

            // SURFACE
            IDimension dimension = new DimensionStretch();
            IKeysColorCode keysColorCode = new KeysColorCodeStandard();
            ILinesColorCode linesColorCode = new LinesColorCodeStandard();
            IButtonsSettings buttonsSettings = new ButtonsSettings1();
            NetytarSurfaceDrawModes drawMode = NetytarSurfaceDrawModes.AllLines;

            NetytarRack.DMIBox.AutoScroller = new AutoScroller(NetytarRack.DMIBox.NetytarMainWindow.scrlNetytar, 0, 100, new ExpDecayingFilter(0.5f)); // OLD was 100, 0.1f
            NetytarRack.DMIBox.NetytarSurface = new NetytarSurface(NetytarRack.DMIBox.NetytarMainWindow.canvasNetytar, dimension, keysColorCode, linesColorCode, buttonsSettings, drawMode);
            NetytarRack.DMIBox.NetytarSurface.DrawButtons();
            NetytarRack.DMIBox.NetytarSurface.Scale = ScalesFactory.Cmaj;
        }
    }
}
