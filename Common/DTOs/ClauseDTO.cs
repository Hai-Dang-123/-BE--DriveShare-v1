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
    public class CreateClauseTemplateDTO
    {
        public string Version { get; set; } = null!;
        public string Title { get; set; } = null!;
        public List<ClauseContentDTO>? ClauseContents { get; set; }
    }
    public class UpdateClauseTemplateDTO
    {
        public Guid ClauseId { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public List<ClauseContentUpdateDTO> ClauseContents { get; set; }
    }

    public class ClauseContentDTO
    {
        
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class ClauseContentUpdateDTO
    {
        public Guid? ClauseTermId { get; set; } 
        public string Content { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public int DisplayOrder { get; set; }
    }

}
