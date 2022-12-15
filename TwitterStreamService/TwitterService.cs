using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitterStreamData;
using TwitterStreamExceptionHandling;

namespace TwitterStreamService
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterStorage _twitterStorage;

        public TwitterService(ITwitterStorage twitterStorage ) {
            _twitterStorage = twitterStorage;
        }

        public async Task<bool> FetchTweets()
        {
            await Task.Run(() => { ProcessStream(); });
            return true;
        }

        public async Task<TweeetDetail> GetTweetDetails()
        {
            //throw new BusinessValidationException();
            return new TweeetDetail { TweetReceived = await _twitterStorage.TweetReceived(), Top10Tags = await _twitterStorage.GetTopHashTag() };
        }

        public async Task<bool> ProcessStream()
        {
            var _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.twitter.com")
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                "AAAAAAAAAAAAAAAAAAAAACB0kQEAAAAA7tKN%2BmcqSue0FdwUeXtfd4Kh6qw%3DzBmoGd4cbwa7PxeZpkMNhUbvoL5mzlB35FFpzLKOWLsPmV93uo");


            using var stream = await _httpClient.GetStreamAsync("/2/tweets/search/stream");

            while (stream.CanRead)
            {
                StreamReader reader = new StreamReader(stream);
                var tweet = reader.ReadLine();
                Console.WriteLine(tweet);

                var tweetobj = JsonSerializer.Deserialize<TweetData>(tweet);
                if (tweetobj != null)
                {
                    await _twitterStorage.AddTweet(tweetobj);
                }
            }

            return true;
        }
    }
}
