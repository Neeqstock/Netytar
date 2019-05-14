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

        bool leftClose = false;
        bool leftOpen = false;
        bool rightClose = false;
        bool rightOpen = false;
        bool doubleClose = false;
        bool doubleOpen = false;
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

            #region Threshold evaluation
            if (leftOpenCounter == LThresh)
            {
                leftOpen = true;
            }
            if (leftCloseCounter == LThresh)
            {
                leftClose = true;
            }
            if (rightOpenCounter == RThresh)
            {
                rightOpen = true;
            }
            if (rightCloseCounter == RThresh)
            {
                rightClose = true;
            }
            if (doubleOpenCounter == DoubleThresh)
            {
                doubleOpen = true;
            }
            if (doubleCloseCounter == DoubleThresh)
            {
                doubleClose = true;
            }
            #endregion

            #region Send messages to behaviors
            foreach (IEyeXBlinkBehavior behavior in EyeXModule.BlinkBehaviors)
            {
                if (leftOpen)
                {
                    behavior.Receive_leftOpen();
                }
                if (leftClose)
                {
                    behavior.Receive_leftClose();
                }
                if (rightOpen)
                {
                    behavior.Receive_rightOpen();
                }
                if (rightClose)
                {
                    behavior.Receive_rightClose();
                }
                if (doubleOpen)
                {
                    behavior.Receive_doubleOpen();
                }
                if (doubleClose)
                {
                    behavior.Receive_doubleClose();
                }
            }
            #endregion

            #region Reset booleans
            leftOpen = false;
            leftClose = false;
            rightOpen = false;
            rightClose = false;
            doubleOpen = false;
            doubleClose = false;
            #endregion
        }   
    }
}

