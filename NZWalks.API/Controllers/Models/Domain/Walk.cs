﻿namespace NZWalks.API.Controllers.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public Guid id { get; set; }
        public string Description {  get; set; }

        public double LengthInKm {  get; set; }

        public string? WalkImageUrl {  get; set; }

        public Guid DifficultyId { get; set; }

        // Navigation properties

        public Difficulty Difficulty { get; set; }
    }
}
