using System;
using System.Collections;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Design_patterns
{
    internal class CosmosDatabase : IDatabase
    {
        string cosmosEndpoint = "";
        string cosmosKey = "";
        string databaseName = "";
        string containerName = "";



        //   private Microsoft.Azure.Cosmos.Container _container;

        public async Task<int> insertUserdata(UserDetails userDetails)
        {
            CosmosClient _cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
            Database _database = _cosmosClient.GetDatabase(databaseName);
            Container _container = _database.GetContainer(containerName);


            dynamic item = new
            {
                id = Guid.NewGuid().ToString(), // Auto-generate unique id
                UserName = userDetails.Username,
                Salary = userDetails.Salary,
                Comments = userDetails.Comments,
            };


            var response = await _container.CreateItemAsync(item);
            return 1; 

        }

        public void updateUserdata(string username)
        {
            Console.WriteLine("Update into Cosmos database");
        }

        public async Task<IEnumerable<UserDetails>> GetUserDetailsAsync()
        {       
            string query = "SELECT * FROM c";
            var results = new List<UserDetails>();

            CosmosClient _cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);
            Database _database = _cosmosClient.GetDatabase(databaseName);
            Container _container = _database.GetContainer(containerName);

            var queryDefinition = new QueryDefinition(query);
            FeedIterator<UserDetails> resultSet = _container.GetItemQueryIterator<UserDetails>(queryDefinition);

            while (resultSet.HasMoreResults)
            {

                var response = await resultSet.ReadNextAsync();
                FeedResponse<UserDetails> currentResultSet = response;
                results.AddRange(currentResultSet);

            }

            return results;
            
        }

      
    }

}


