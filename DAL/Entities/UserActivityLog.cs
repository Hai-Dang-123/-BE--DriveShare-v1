using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserActivityLog
    {
        public Guid UserActivityLogId { get; set; }
        public Guid UserViolationId { get; set; }
        public UserViolation UserViolation { get; set; } = null!;
        public DateTime ActivityDate { get; set; }
        public string Action { get; set; } = null!;
        public string Metadata { get; set; } = null!;
    }
}
