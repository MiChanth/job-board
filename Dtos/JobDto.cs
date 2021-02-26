using System;

namespace JobBoardApi.Dtos
{
    public record JobDto{
        public Guid Id { get; init; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Country { get; set; }
    }
}