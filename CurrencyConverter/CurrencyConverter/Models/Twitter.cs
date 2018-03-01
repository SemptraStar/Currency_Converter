namespace CurrencyConverter.Models
{
    public class Twitter
    {
        public long? InReplyToStatusId { get; set; }

        public string Text { get; set; }

        public string InReplyToScreenName { get; set; }

        public bool Truncated { get; set; }

        public bool Retweeted { get; set; }

        public string InReplyToStatusIdStr { get; set; }

        public string Source { get; set; }

        public string CreatedAt { get; set; }

        public string InReplyToUserIdStr { get; set; }

        public object geo { get; set; }

        public long RetweetCount { get; set; }

        public object Contributors { get; set; }

        public string IdStr { get; set; }

        public Entities Entities { get; set; }

        public object Place { get; set; }

        public object Coordinates { get; set; }

        public User User { get; set; }

        public long? InReplyToUserId { get; set; }

        public long Id { get; set; }

        public bool Favorited { get; set; }

        public bool PossiblySensitive { get; set; }
    }
}
