using System;

namespace ApiContracts.CommentFolder;

public class CreateCommentDTO
{
    public required int PostId { get; set; }
    public required int AuthorUserId { get; set; }
    public required string Body{ get; set; }
}

