using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCollection.DALL.Repositories.Queries
{
    public class GameQueries
    {
        public static string INSERT = @"INSERT INTO [dbo].[Games]
           ([Name]
           ,[Developper]
           ,[Console]
           ,[Genre])
     VALUES
           (@Name
           , @Developper
           , @Console
           , @Genre)";

        public static string UPDATE = @"UPDATE [dbo].[Games]
                                       SET[Name] = @Name
                                          ,[Developper] = @Developper
                                          ,[Console] = @Console
                                          ,[Genre] = @Genre
                                    WHERE [Identifier] = @Identifier";
        public static string DELETE = "DELETE FROM [dbo].[Games] WHERE [Identifier] = @Identifier";
        public static string GET_BY_ID = @"SELECT [Identifier]
                              ,[Name]
                              ,[Developper]
                              ,[Console]
                              ,[Genre]
                            FROM[GameCollectionNew].[dbo].[Games]
                           WHERE Identifier = @Identifier";
        public static string GET_ALL = @"SELECT [Identifier]
                              ,[Name]
                              ,[Developper]
                              ,[Console]
                              ,[Genre]
                            FROM [GameCollectionNew].[dbo].[Games]
                            ORDER BY Name ASC";
    }
}
