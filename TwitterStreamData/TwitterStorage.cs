using System.Linq;
using System.Text.RegularExpressions;

namespace TwitterStreamData
{
    public class TwitterStorage : ITwitterStorage
    {
        public static Dictionary<string, TweetData> TweetStreamData = new Dictionary<string, TweetData>();

        public async Task<bool> AddTweet(TweetData tweetData)
        {
            TweetStreamData.Add(tweetData.data.id, tweetData);
            return true;
        }

        public async Task<int> TweetReceived()
        {
            return TweetStreamData.Count;
        }


        public async Task<List<string>> GetTopHashTag()
        {
            var allTweets = TweetStreamData.Values.Select(x => x.data.text).ToList();
            
            Dictionary<string, int> hashtags= new Dictionary<string, int>();

            foreach(string tweet in allTweets)
            {
                MatchCollection mcol = Regex.Matches(tweet, @"#\b\S+?\b ");

                foreach(Match m in mcol) {

                    if (hashtags.ContainsKey(m.Value))
                    {
                        var count = hashtags[m.Value];
                        hashtags[m.Value] = count++;
                    }
                    else
                        hashtags.Add(m.Value, 1);                
                }

            }

            var orderedhashtags = hashtags.OrderBy(x => x.Value).Take(10).Select(x => x.Key).ToList();

            return orderedhashtags;

        }

    }
}
