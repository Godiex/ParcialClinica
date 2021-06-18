using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Initialize
{
    public class Start
    {

        private readonly MyContext _context;
        public Start(MyContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (!_context.Roles.Any()) RoleInitialize.InitializeRole(_context);
        }

    }
}
