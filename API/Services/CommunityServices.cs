using APIOnly.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APIOnly.Services
{
    public class CommunityServices : IcommunityServices
    {
        public List<TblCommunity> GetCommunities()
        {
            using (var db = new PeopleContext())
            {
                return db.TblCommunities.ToList();
            }
        }

        public TblCommunity GetCommunitiesById(int id)
        {
            try
            {
                using (var db = new PeopleContext())
                {
                    var idParameter = new SqlParameter("@ID", id);
                    var result = db.TblCommunities.FromSqlRaw("exec sp_getCommunitybyId @ID", idParameter).ToList();
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void UpdateCommunity(TblCommunity community)
        {
            if (community != null)
            {
                using (var db = new PeopleContext())
                {
                    var idParameter = new SqlParameter("@Id", community.Id);
                    var GivenNameParameter = new SqlParameter("@GivenName", community.GivenName);
                    var Surname = new SqlParameter("@Surname", community.Surname);
                    var BirthDate = new SqlParameter("@BirthDate", community.BirthDate);
                    var Email = new SqlParameter("@Email", community.Email);

                    var unused = db.Database.ExecuteSqlRaw("exec sp_UpdateCommunity @Id,@GivenName,@Surname,@Birthdate,@Email",
                        idParameter, GivenNameParameter, Surname, BirthDate, Email);
                }
            }

        }
    }
}
