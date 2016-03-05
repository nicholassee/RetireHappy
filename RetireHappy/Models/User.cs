using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RetireHappy.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "What is your age?")]
        [Range(12, 99)]
        public int age { get; set; }

        [Display(Name = "What is your gender?")]
        [Required(ErrorMessage = "Please select your gender")]
        public string gender { get; set; }

        [Display(Name = "Your Expected Retirement Age")]
        [Range(18, 99)]
        public int expRetAge { get; set; }

        [Display(Name = "Retirement Duration(years)")]
        [Range(1, 99)]
        public int retDuration { get; set; }

        [Display(Name = "Your Monthly Take Home Pay")]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public float monIncome { get; set; }

        [Display(Name = "Monthly Expenditure")]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public float avgMonExpenditure { get; set; }

        [Display(Name = "Current Monthly Savings Amount")]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public float curSavingAmt { get; set; }

        [Display(Name = "Desired Monthly Retirement Income")]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public float desiredMonRetInc { get; set; }

        [Display(Name = "Departure Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime timestamp { get; set; }

        [Display(Name = "Inflation Rate")]
        public float inflationRate { get; set; }

    }
}