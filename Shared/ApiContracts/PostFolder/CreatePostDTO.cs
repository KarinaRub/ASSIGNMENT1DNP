using System;

namespace ApiContracts.PostFolder;

public class CreatePostDTO
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required int AuthorUserId{ get; set;}
}

