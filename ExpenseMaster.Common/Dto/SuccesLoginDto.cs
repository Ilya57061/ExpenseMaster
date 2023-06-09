using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseMaster.Common.Dto
{
    public class SuccesLoginDto
    {
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}
