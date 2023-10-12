﻿using System.ComponentModel.DataAnnotations;

namespace RRSEASYPARK.Models.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
    }
}
