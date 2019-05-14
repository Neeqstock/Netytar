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

        private GazePointEventArgs lastGazePoint;
        private EyePositionEventArgs lastEyePosition;
        public GazePointEventArgs LastGazePoint { get => lastGazePoint; set => lastGazePoint = value; }
        public EyePositionEventArgs LastEyePosition { get => lastEyePosition; set => lastEyePosition = value; }

        #region Host and streams
        private EyeXHost eyeXHost = new EyeXHost();
        #endregion

        #region Behaviors lists
        private List<AEyeXBlinkBehavior> blinkBehaviors = new List<AEyeXBlinkBehavior>();
        private List<IEyeXGazePointBehavior> gazePointBehaviors = new List<IEyeXGazePointBehavior>();
        private List<IEyeXEyePositionBehavior> eyePositionBehaviors = new List<IEyeXEyePositionBehavior>();

        public List<AEyeXBlinkBehavior> BlinkBehaviors { get => blinkBehaviors; set => blinkBehaviors = value; }
        public List<IEyeXGazePointBehavior> GazePointBehaviors { get => gazePointBehaviors; set => gazePointBehaviors = value; }
        public List<IEyeXEyePositionBehavior> EyePositionBehaviors { get => eyePositionBehaviors; set => eyePositionBehaviors = value; }
        #endregion

        public EyeXModule(GazePointDataMode gazePointDataMode)
        {
            var GazePointStream = eyeXHost.CreateGazePointDataStream(gazePointDataMode);
            var EyePositionStream = eyeXHost.CreateEyePositionDataStream();

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
            LastEyePosition = e;

            blinkProcessor.ReceiveEyePosition(e.LeftEye, e.RightEye);
            foreach(IEyeXEyePositionBehavior behavior in eyePositionBehaviors)
            {
                behavior.ReceiveEyePosition(e);
            }
        }

        private void GazePointNext(object sender, GazePointEventArgs e)
        {
            LastGazePoint = e;

            MouseEmulator.ReceiveGazePoint(e.X, e.Y);
            foreach(IEyeXGazePointBehavior behavior in gazePointBehaviors)
            {
                behavior.ReceiveGazePoint(e);
            }
        }
    }
}