using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_patterns
{
    internal interface IDatabase
    {
        //void userDetails(string username);

        Task<IEnumerable<UserDetails>> GetUserDetailsAsync();
        Task<int> insertUserdata(UserDetails userDetails);

        void updateUserdata(string username);
    }
}
