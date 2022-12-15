using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterStreamService;

namespace TwitterStream.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        [HttpGet(Name = "StartTwitterStream")]
        public async Task<bool> Get()
        {
            await _twitterService.FetchTweets();
            return true;
        }



        [HttpGet]
        [Route("GetTweetCount")]
        public async Task<TweeetDetail> GetTweetDetail()
        {
            return await _twitterService.GetTweetDetails();
        }

    }
}
