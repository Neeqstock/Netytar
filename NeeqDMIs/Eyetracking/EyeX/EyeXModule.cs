using EyeXFramework;
using NeeqDMIs.Eyetracking.Filters;
using System.Collections.Generic;
using Tobii.EyeX.Framework;

namespace NeeqDMIs.Eyetracking.EyeX
{
    public class EyeXModule
    {
        private EyeXBlinkProcessor blinkProcessor;
        private EyeXMouseEmulator mouseEmulator;
        public EyeXMouseEmulator MouseEmulator { get => mouseEmulator; }

        #region Host and streams
        private EyeXHost eyeXHost = new EyeXHost();
        private GazePointDataStream GazePointStream;
        private EyePositionDataStream EyePositionStream;
        #endregion

        #region Behaviors lists
        private List<IEyeXBlinkBehavior> blinkBehaviors = new List<IEyeXBlinkBehavior>();
        private List<IEyeXGazePointBehavior> gazePointBehaviors = new List<IEyeXGazePointBehavior>();
        private List<IEyeXEyePositionBehavior> eyePositionBehaviors = new List<IEyeXEyePositionBehavior>();

        public List<IEyeXBlinkBehavior> BlinkBehaviors { get => blinkBehaviors; set => blinkBehaviors = value; }
        public List<IEyeXGazePointBehavior> GazePointBehaviors { get => gazePointBehaviors; set => gazePointBehaviors = value; }
        public List<IEyeXEyePositionBehavior> EyePositionBehaviors { get => eyePositionBehaviors; set => eyePositionBehaviors = value; }
        #endregion

        public EyeXModule()
        {
            GazePointStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            EyePositionStream = eyeXHost.CreateEyePositionDataStream();

            GazePointStream.Next += GazePointNext;
            EyePositionStream.Next += EyePositionNext;

            blinkProcessor = new EyeXBlinkProcessor(this);
            mouseEmulator = new EyeXMouseEmulator(new NoFilter());
        }

        public void Start()
        {
            eyeXHost.Start();
        }

        public void Dispose()
        {
            eyeXHost.Dispose();
        }

        private void EyePositionNext(object sender, EyePositionEventArgs e)
        {
            blinkProcessor.ReceiveEyePosition(e.LeftEye, e.RightEye);
            foreach(IEyeXEyePositionBehavior behavior in eyePositionBehaviors)
            {
                behavior.ReceiveEyePosition(e);
            }
        }

        private void GazePointNext(object sender, GazePointEventArgs e)
        {
            MouseEmulator.ReceiveGazePoint(e.X, e.Y);
            foreach(IEyeXGazePointBehavior behavior in gazePointBehaviors)
            {
                behavior.ReceiveGazePoint(e);
            }
        }
    }
}