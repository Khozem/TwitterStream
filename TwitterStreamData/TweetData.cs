namespace TwitterStreamData
{

    public class Data
    {
        public List<string> edit_history_tweet_ids { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }

    public class MatchingRule
    {
        public string id { get; set; }
        public string tag { get; set; }
    }

    public class TweetData
    {
        public Data data { get; set; }
        public List<MatchingRule> matching_rules { get; set; }
    }


}
