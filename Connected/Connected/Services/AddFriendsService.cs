using System.Collections.Generic;
using System.Linq;
using Connected.Models;

namespace Connected.Services
{
    /// <summary>
    /// This service handles tasks such as getting a list of all
    /// friends a user has, add/remove friend connections etc.
    /// </summary>
    public class AddFriendsService
    {
        #region Member variables and constructor
        private readonly IAppDataContext _db;

        public AddFriendsService(IAppDataContext dbContext)
        {
            _db = dbContext ?? (IAppDataContext) new ApplicationDbContext();
        }
        #endregion

        #region Get functions
        public List<string> GetFriendsFor(string user)
        {
            var result = (from x in _db.FriendConnections
                          where x.Friend1 == user
                          select x.Friend2).ToList();

            var moreResults = (from x in _db.FriendConnections
                               where x.Friend2 == user
                               select x.Friend1).ToList();

            result.AddRange(moreResults);

            return result;
        }

        // TODO: there could be more functions here, such as
        // GetLatestFriends etc.

        #endregion

        #region Add friends
        // TODO: more functions here which create a new friend connection
        #endregion
    }
}