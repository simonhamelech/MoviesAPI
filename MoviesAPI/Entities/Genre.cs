﻿using MoviesAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities
{
    public class Genre //: IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "The field with name {0} is required")]
        [Required]
        [StringLength(40)]
        [FirstLetterUppercase]
        public string Name { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        var firstLetter = Name[0].ToString();

        //        if (firstLetter != firstLetter.ToUpper())
        //        {
        //            yield return new ValidationResult("First letter should be uppercase", new string[] { nameof(Name) });
        //        }
        //    }
        //}
    }
}
