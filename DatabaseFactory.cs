using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_patterns
{
    internal enum DatabaseType
    {
        SQL,
        Cosmos
    }


    internal class DatabaseFactory
    {
        public static IDatabase Createdatabase(DatabaseType databaseType)
        {
            IDatabase database ;
            switch (databaseType)
            {
                case DatabaseType.SQL:
                    database = new SQLDatabase();
                    break;
                case DatabaseType.Cosmos:
                    database = new CosmosDatabase();
                    break;
                default:
                    database = new SQLDatabase();
                    break;
            }
            return database;
        }

    }
}
