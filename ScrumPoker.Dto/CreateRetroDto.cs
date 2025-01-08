namespace ScrumPoker.Dto;

public class CreateRetroDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsPasswordProtection { get; set; }
    public string? Password { get; set; }
    public bool IsUserNameVisibilityOption { get; set; } 
    public bool IsCommentOption { get; set; } 
    public bool IsReactionOption { get; set; } 
    public bool IsGifOption { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? CreatedBy { get; set; }
}