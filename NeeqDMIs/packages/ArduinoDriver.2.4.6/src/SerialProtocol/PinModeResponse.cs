﻿namespace ArduinoDriver.SerialProtocol
{
    public class PinModeResponse : ArduinoResponse
    {
        public int Pin { get; private set; }
        public PinMode Mode { get; private set; }

        public PinModeResponse(byte pin, PinMode mode)
        {
            Pin = pin;
            Mode = mode;            
        }
    }
}
