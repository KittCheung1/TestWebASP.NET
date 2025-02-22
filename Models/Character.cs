﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieWebASP.NET.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Alias { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        [MaxLength(200)]
        public string Picture { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
