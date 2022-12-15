using Moq;
using System.Collections.Generic;
using TwitterStreamData;
using TwitterStreamService;
using Xunit;

namespace TwitterStreamTest
{
    public class TwitterServiceTest
    {
        private readonly ITwitterService _twitterService;
        private readonly Mock<ITwitterStorage> _twitterStorage;        
        
        public TwitterServiceTest()
        {
            _twitterStorage = new Mock<ITwitterStorage>();
            _twitterService = new TwitterService(_twitterStorage.Object);
            
        }

        [Fact]
        public void GetTweetDetail_Sanity()
        {
            // arrange
            _twitterStorage.Setup(t => t.TweetReceived().Result).Returns(1);
            _twitterStorage.Setup(t => t.GetTopHashTag().Result).Returns(new List<string> { "tag1" });

            // act
            var detail =  _twitterService.GetTweetDetails().Result;
            
            //assert
            Assert.NotNull(detail);
            Assert.Equal(1, detail.TweetReceived);
            Assert.Equal("tag1", detail.Top10Tags[0]);
        }
    }
}