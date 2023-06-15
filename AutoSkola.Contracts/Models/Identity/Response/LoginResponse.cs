using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Contracts.Models.Identity.Response
{
    public class LoginResponse
    {
        public DateTime expires { get; set; }
        public string token { get; set; }
        public UserResponse painter { get; set; }

        public IList<string> role { get; set; }

        public string error { get; set; } = "";

    }
}
