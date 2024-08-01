﻿namespace api.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Color { get; set; } = String.Empty;
        public int UserId { get; set; }
        //public User User { get; set; } = new User();

        
    }
}
