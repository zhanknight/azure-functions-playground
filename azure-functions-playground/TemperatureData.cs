﻿namespace azure_functions_playground
{
    public partial class PlaygroundHttpFunction
    {
        public class TemperatureData
        {
            public int DeviceId { get; set; }
            public DateTime TimeStamp { get; set; }
            public double Temperature { get; set; }
        }
    }
}
