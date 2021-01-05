using System;

namespace InterviewProblem.Model
{
    public class Temperature
    {

        public Temperature()
        {
            TcVolts = GenerateTCVolts();
            TimeStamp = DateTime.Now;
        }

        public Temperature(double voltage, DateTime timestamp)
        {
            TcVolts = voltage;
            TimeStamp = timestamp;
        }

        public double TcVolts { get; private set; }

        public double TempC => 5 / TcVolts + 35;

        public double TempF => TempC * 1.8 + 32;

        public bool WithinLimits
        {
            get
            {
                // TODO: Verify Temperature between 95 and 100.3 degress F.
                return TempF > 95 && TempF < 100.3;
            }
        }

        public DateTime TimeStamp { get; set; }

        // 0-10VDC signal
        private double GenerateTCVolts() => new Random().NextDouble() * 10;
    }
}
