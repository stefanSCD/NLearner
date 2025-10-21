using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NLearner.ViewModels
{
    public class NoteEditViewModel
    {
        [HiddenInput]
        [Required]
        public Guid Id { get; set; }

        [HiddenInput]
        [Required]
        public Guid ProjectId { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string Content { get; set; } = string.Empty;

        [BindNever]
        [Display(Name = "Created")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime CreatedDate { get; set; }

        [BindNever]
        [Display(Name = "Last updated")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime UpdatedDate { get; set; }

        [BindNever]
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [BindNever]
        public bool IsNew => Id == Guid.Empty;
    }
}