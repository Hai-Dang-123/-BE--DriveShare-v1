using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClauseTerm
    {
        public Guid ClauseTermId { get; set; }
        public Guid ClauseTemplateId { get; set; }
        public ClauseTemplate ClauseTemplate { get; set; } = null!;
        public string Content { get; set; } = null!; // Nội dung của điều khoản
        public bool IsMandatory { get; set; } // Điều khoản này có bắt buộc không
        public int DisplayOrder { get; set; } // Thứ tự hiển thị
    }
}
