using System;

namespace JobBoardApi.Models
{
    public record Job
    {
        public Guid Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }
    }
}