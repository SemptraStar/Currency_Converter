namespace CurrencyConverter.Models
{
    public class Twitter
    {
        public long? InReplyToStatusId { get; set; }

        public string Text { get; set; }

        public string in_reply_to_screen_name { get; set; }

        public bool Truncated { get; set; }

        public bool Retweeted { get; set; }

        public string in_reply_to_status_id_str { get; set; }

        public string Source { get; set; }

        public string CreatedAt { get; set; }

        public string in_reply_to_user_id_str { get; set; }

        public object geo { get; set; }

        public long retweet_count { get; set; }

        public object contributors { get; set; }

        public string id_str { get; set; }

        public Entities entities { get; set; }

        public object place { get; set; }

        public object coordinates { get; set; }

        public User user { get; set; }

        public long? in_reply_to_user_id { get; set; }

        public long id { get; set; }

        public bool favorited { get; set; }

        public bool possibly_sensitive { get; set; }
    }
}
