using System.Collections.Generic;

namespace iBlog.Domain.Entities
{
    public class PostResult
    {
        public int PostCount { get; set; }

        public List<Post> PostList { get; set; }
    }
}