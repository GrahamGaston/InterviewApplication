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

        public double TempC => TcVolts * 10;

        public double TempF => TempC * 1.8 + 32;

        public bool BelowLimit
        {
            get
            {
                // TODO: Verify Temperature between 90 and 99 degress F.
                return TempF < 90;
            }
        }

        public DateTime TimeStamp { get; set; }

        //private const double UpperC = 41;

        // 0-10VDC signal
        private double GenerateTCVolts() => new Random().NextDouble() * 10;
    }
}
