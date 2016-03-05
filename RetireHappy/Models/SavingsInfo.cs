using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetireHappy.Models
{
    public class SavingsInfo
    {
        public int Id { get; set; }

        [Display(Name = "Estimate Monthly Savings Required")]
        public float calcRetSavings { get; set; }

        [Display(Name = "Risk Level")]
        public string riskLevel { get; set; }

        [Display(Name = "Expenditure (%)")]
        public float expPercent { get; set; }

        [Display(Name = "Shortfall (%)")]
        public float diffPercent { get; set; }
        
        public float calculate(int expRetAge, int currentAge, float desiredMonRetInc, float inflationRate, int retDuration)
        {
            float annualInfSum = 0;
            int limit = expRetAge + retDuration;
            // calculate annual inflation adjusted avg expenditure
            for (int i = expRetAge; i <= limit; i++)
            {
                int diffInAge = i - currentAge;
                // to find total expenditure needed
                annualInfSum += ((float)(desiredMonRetInc * Math.Pow(inflationRate, diffInAge)) * 12);
            }

            // calculate PV as of retirement age assuming interest rate is 1% annually
            float pvAsOfRetAge = (float)(annualInfSum / Math.Pow((1 + 0.01), retDuration));

            // calculate PV as of current age
            float pvAsOfCurAge = (float)(pvAsOfRetAge / Math.Pow((1 + 0.01), (expRetAge - currentAge)));

            // calculate monthly savings using PV as of current age
            float calcRetSavings = pvAsOfCurAge / ((expRetAge - currentAge) * 12);

            return calcRetSavings;
        }
        public float computeRiskLevel(float calcRetSavings, float curSavingAmt)
        {
            // to calculate risk level using inflation adjusted current monthly savings
            float riskLevelDiff = ((calcRetSavings - curSavingAmt) / curSavingAmt) * 100;
            return riskLevelDiff;
        }
    }
}