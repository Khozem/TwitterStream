using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterStreamData;

namespace TwitterStreamService
{
    public interface ITwitterService
    {
        Task<bool> FetchTweets();
        Task<TweeetDetail> GetTweetDetails();
    }
}
