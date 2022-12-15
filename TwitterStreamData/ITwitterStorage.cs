namespace TwitterStreamData
{

    public interface ITwitterStorage
    {
        Task<bool> AddTweet(TweetData data);
        Task<int> TweetReceived();
        Task<List<string>> GetTopHashTag();

    }
}
