//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetireHappy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AvgExpenditure
    {
        [Key]
        public int eId { get; set; }
        public string category { get; set; }
        public Nullable<System.DateTime> recordYear { get; set; }
        public Nullable<double> avgAmount { get; set; }
        public Nullable<int> count { get; set; }
    }
}