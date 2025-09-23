using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class RuleRepository : GenericRepository<Rule>, IRuleRepository
    {
        private readonly DriverShareAppContext _context;
        public RuleRepository(DriverShareAppContext context) : base(context)
        {
            _context = context;
        }
    }
}
