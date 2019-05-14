using EyeXFramework;
using System.Collections.Generic;

namespace NeeqDMIs.Eyetracking.EyeX
{
    public class EyeXBlinkProcessor
    {
        #region EyeXModule
        private EyeXModule EyeXModule;
        internal EyeXBlinkProcessor(EyeXModule eyeXModule)
        {
            EyeXModule = eyeXModule;
        }
        #endregion

        #region Counters
        private int leftCloseCounter = 0;
        private int rightCloseCounter = 0;
        private int doubleCloseCounter = 0;
        private int leftOpenCounter = 0;
        private int rightOpenCounter = 0;
        private int doubleOpenCounter = 0;

        
        #endregion

        #region Props and Fields
        private int lThresh;
        private int rThresh;
        private int doubleThresh;
        private int lMaxDur;
        private int rMaxDur;
        private int doubleMaxDur;

        public int LMaxDur { get => lMaxDur; set => lMaxDur = value; }
        public int RMaxDur { get => rMaxDur; set => rMaxDur = value; }
        public int DoubleMaxDur { get => doubleMaxDur; set => doubleMaxDur = value; }
        public int LThresh { get => lThresh; set => lThresh = value; }
        public int RThresh { get => rThresh; set => rThresh = value; }
        public int DoubleThresh { get => doubleThresh; set => doubleThresh = value; }
        #endregion

        public void ReceiveEyePosition(EyePosition lEye, EyePosition rEye)
        {
            #region Counters update
            if (!lEye.IsValid && rEye.IsValid) // LEFT BLINK
            {
                leftOpenCounter = 0;
                leftCloseCounter++;
                rightOpenCounter++;
                rightCloseCounter = 0;
                doubleOpenCounter = 0;
                doubleCloseCounter = 0;
            }
            else if(lEye.IsValid && !rEye.IsValid) // RIGHT BLINK
            {
                leftOpenCounter++;
                leftCloseCounter = 0;
                rightOpenCounter = 0;
                rightCloseCounter++;
                doubleOpenCounter = 0;
                doubleCloseCounter = 0;
            }
            else if(!lEye.IsValid && !rEye.IsValid) // DOUBLE BLINK
            {
                leftOpenCounter = 0;
                leftCloseCounter = 0;
                rightOpenCounter = 0;
                rightCloseCounter = 0;
                doubleOpenCounter = 0;
                doubleCloseCounter++;
            }
            else if (lEye.IsValid && rEye.IsValid) // NO BLINK
            {
                leftOpenCounter++;
                leftCloseCounter = 0;
                rightOpenCounter++;
                rightCloseCounter = 0;
                doubleOpenCounter++;
                doubleCloseCounter = 0;
            }
            #endregion

            #region Send messages to behaviors
            foreach (AEyeXBlinkBehavior behavior in EyeXModule.BlinkBehaviors)
            {
                behavior.ReceiveCounters(leftOpenCounter, leftCloseCounter, rightOpenCounter, rightCloseCounter, doubleOpenCounter, doubleCloseCounter);
            }
            #endregion
        }   
    }
}

