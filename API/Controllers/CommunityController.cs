using APIOnly.Models;
using APIOnly.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIOnly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private IcommunityServices communityService;
        public CommunityController(IcommunityServices icommunityServices)
        {
            communityService = icommunityServices;
        }

        // GET: api/<CommunityController>
        [HttpGet]
        public IEnumerable<TblCommunity> Get()
        {
            return communityService.GetCommunities().ToArray();
        }

        // GET api/<CommunityController>/5
        [HttpGet("{id}")]
        public TblCommunity Get(int id)
        {
            return communityService.GetCommunitiesById(id);
        }


        // PUT api/<CommunityController>/5
        [HttpPut]
        public void Put(TblCommunity value)
        {
            communityService.UpdateCommunity(value);
        }
    }
}
