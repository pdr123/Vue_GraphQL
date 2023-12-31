﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        public int NoOfAttempts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public ResultStatus Status { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set;}
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
