using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XInput.Wrapper;
using XInputDotNetPure;

namespace NeeqDMIs.Xbox360
{
    /// <summary>
    /// A module which can send Force Feedback inputs to Xbox360 controllers. Can be used to create a simple haptic feedback for a virtual instrument.
    /// </summary>
    public class FFBModule
    {
        private float flashFFBLeftLow = 1.0f;
        private float flashFFBRightHi = 1.0f;
        private int flashFFBDuration = 30;
        private PlayerIndex playerIndex = PlayerIndex.One;
        private Timer FFBtimer;

        public float FlashFFBLeftLow { get => flashFFBLeftLow; set => flashFFBLeftLow = value; }
        public float FlashFFBRightHi { get => flashFFBRightHi; set => flashFFBRightHi = value; }
        public int FlashFFBDuration { get => flashFFBDuration; set => flashFFBDuration = value; }
        public PlayerIndex PlayerIndex { get => playerIndex; set => playerIndex = value; }

        /// <summary>
        /// Creates a Force Feedback controller module with default values.
        /// </summary>
        public FFBModule()
        {
            FFBtimer = new Timer();
            FFBtimer.Interval = FlashFFBDuration;
            FFBtimer.Tick += FFBtimerTick;
        }

        /// <summary>
        /// Creates a Force Feedback controller module.
        /// </summary>
        /// <param name="flashFFBLeftLow">Left low freuqency actuator. Default is 1.</param>
        /// <param name="flashFFBRightHi">Right high frequency actuator. Default is 1.</param>
        /// <param name="flashFFBDuration">Force feedback duration, in milliseconds. Default is 10.</param>
        public FFBModule(float flashFFBLeftLow, float flashFFBRightHi, int flashFFBDuration)
        {
            this.FlashFFBLeftLow = flashFFBLeftLow;
            this.FlashFFBRightHi = flashFFBRightHi;
            this.FlashFFBDuration = flashFFBDuration;

            FFBtimer = new Timer();
            FFBtimer.Interval = FlashFFBDuration;
            FFBtimer.Tick += FFBtimerTick;
        }

        private void FFBtimerTick(object sender, EventArgs e)
        {
            FFBtimer.Stop();
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
        }

        public void FlashFFB()
        {
            FFBtimer.Start();
            GamePad.SetVibration(PlayerIndex, FlashFFBLeftLow, FlashFFBRightHi);
        }
    }
}
