using RawInput_dll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace NeeqDMIs.Keyboard
{
    public sealed class KeyboardModule
    {
        private readonly RawInput _rawinput;

        public KeyboardModule(IntPtr parentHandle)
        {
            _rawinput = new RawInput(parentHandle, CaptureOnlyInForeground);

            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory

            _rawinput.KeyPressed += OnKeyPressed;
        }
        ~KeyboardModule()
        {
            _rawinput.KeyPressed -= OnKeyPressed;
            keyboardBehaviors.Clear();
        }

        #region Props and Fields
        /// <summary>
        /// Defines if the application will be listening only when it's on foreground, or always.
        /// </summary>
        public bool CaptureOnlyInForeground { get => captureOnlyInForeground; set => captureOnlyInForeground = value; }

        private bool captureOnlyInForeground = true;
        private List<AKeyboardBehavior> keyboardBehaviors = new List<AKeyboardBehavior>();
        #endregion

        #region Internal bloat
        private static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (null == ex) return;

            // Log this error. Logging the exception doesn't correct the problem but at least now
            // you may have more insight as to why the exception is being thrown.
            Debug.WriteLine("Unhandled Exception: " + ex.Message);
            Debug.WriteLine("Unhandled Exception: " + ex);
            MessageBox.Show(ex.Message);
        }
        #endregion

        /// <summary>
        /// Contains all the behavior modules set.
        /// </summary>
        public List<AKeyboardBehavior> KeyboardBehaviors { get => keyboardBehaviors; set => keyboardBehaviors = value; }

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            foreach (AKeyboardBehavior behavior in KeyboardBehaviors)
            {
                behavior.ReceiveEvent(e);
            }
        }

        /*
        private string internalKeyboardName = "Keyboard_07";
        private string externalKeyboardName = "Keyboard_02";
        private const string singleKeyboardName = "Keyboard_04";

        private int internalKeybNum = 5;
        private int externalKeybNum = 1;

        private const string keyTransP12R = "RIGHT";
        private const string keyTransM12R = "LEFT";
        private const string keyTransP12L = "UP";
        private const string keyTransM12L = "DOWN";

        private const string keyTransP1G = "ADD";
        private const string keyTransM1G = "SUBTRACT";

        private string rightHandKeyboard;
        private string leftHandKeyboard;

        private List<string> rightHandPressedKeys = new List<string>();
        private List<string> leftHandPressedKeys = new List<string>();

        private const string MAKE = "MAKE";
        private const string BREAK = "BREAK";
        */





    }

}
