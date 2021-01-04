using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblem.Model
{
    public class Temperature
    {
        private static double uVperC = 41;

        private double TCVolts 
        { 
            get
            {
                Random rand = new Random();
                double temp = rand.Next(900, 1060) / 10;
                return (rand.NextDouble() * 820) + 3526;
            }
        
        }
        public double Temp 
        { 
            get
            {
                // TODO: Convert to Farenheit
                return TCVolts / uVperC;
            }                
        }
        public bool BelowLimit { 
            get
            {
                // TODO: Verify Temperature between 90 and 99 degress F.
                return false;
            }
        }
    }
}
