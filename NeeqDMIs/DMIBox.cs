using NeeqDMIs.Eyetracking.EyeX;
using NeeqDMIs.Keyboard;
using NeeqDMIs.Microphone;
using NeeqDMIs.MIDI;
using NeeqDMIs.Music;
using NeeqDMIs.Xbox360;

namespace NeeqDMIs
{
    public abstract class DMIBox
    {
        // EYETRACKERS
        public EyeXModule EyeXModule;

        // MUSIC MODULE
        public MusicModule MusicModule;

        // MISCELLANEOUS
        public KeyboardModule KeyboardModule;
        public IMidiModule MidiModule;
        public MicModule MicModule;
        public FFBModule FfbModule;
    }
}
