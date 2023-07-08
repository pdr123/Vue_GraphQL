﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InchargeId { get; set; }
    }
}
