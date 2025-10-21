using System;
using System.Reflection;
namespace ApiContracts.CommentFolder;

public class CommentDTO{
    public int Id { get; set; }
    public required int PostId { get; set; }
    public required int AuthorUserId { get; set; }
    public required string Body {get; set;}

}
