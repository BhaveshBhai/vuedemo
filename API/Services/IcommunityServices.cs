using APIOnly.Models;

namespace APIOnly.Services
{
    public interface IcommunityServices
    {
        List<TblCommunity> GetCommunities();
        TblCommunity GetCommunitiesById(int id);
        void UpdateCommunity(TblCommunity community);
    }
}
