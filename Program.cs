using Design_patterns;



UserDetails userDetails = new UserDetails();
userDetails.Id = Guid.NewGuid().ToString();
userDetails.Username = "user-1";
userDetails.Salary = "1001";
userDetails.Comments = "Testing data form SQL";


var databasetype = DatabaseFactory.Createdatabase(DatabaseType.SQL);

await GetUserDetails(databasetype);
// await databaseInser(userDetails, databasetype);
//databaseUpdate(username, databasetype);
async Task GetUserDetails( IDatabase database)
{
   var pp = await database.GetUserDetailsAsync();

    foreach (var item in pp)
    {
        Console.WriteLine($"ID: {item.Id}, Username: {item.Username}, Salary: {item.Salary}, Comments:{ item.Comments}");
    }
}
async Task databaseInser(UserDetails userDetails, IDatabase database)
{
    var result = await database.insertUserdata(userDetails);
}

void databaseUpdate(string username, IDatabase database)
{
    database.updateUserdata(username);
}


