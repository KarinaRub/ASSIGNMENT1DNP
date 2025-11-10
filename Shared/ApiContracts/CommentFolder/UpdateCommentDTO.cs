using System;

namespace ApiContracts.CommentFolder
{
    public class UpdateCommentDTO
    {
        public required int PostId { get; set; }
        public required int AuthorUserId { get; set; }
        public required string Body { get; set; }
    }
}

