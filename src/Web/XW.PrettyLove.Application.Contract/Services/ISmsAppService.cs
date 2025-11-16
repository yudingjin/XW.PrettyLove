using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XW.PrettyLove.Application
{
    public interface ISmsAppService
    {
        Task<bool> SendSmsAsync(string phone, string content);
    }
}
