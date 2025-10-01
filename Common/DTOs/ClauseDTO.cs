using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ClauseDTO
    {
       
    }
    public class CreateClauseDTO
    {
        [Required(ErrorMessage ="Version not null")]
        public string Version { get; set; }
        [Required(ErrorMessage = "Description not null")]
        public string Description { get; set; }
    }
    public class UpdateClauseDTO
    {
        [Required(ErrorMessage = "ClauseId not null")]
        public Guid ClauseId { get; set; }
        [Required(ErrorMessage = "Version not null")]
        public string Version { get; set; }
        [Required(ErrorMessage = "Description not null")]
        public string Description { get; set; }
    }
    public class GetClauseDTO
    {
        public Guid ClauseId { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}
