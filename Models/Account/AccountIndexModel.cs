using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models.Account
{
    public class AccountIndexModel
    {
        public IEnumerable<AccountProfileModel> Accounts { get; set; }
        public string SearchQuery { get; set; }
    }
}
