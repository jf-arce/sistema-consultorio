namespace webapi.Shared;

public interface ISoftDelete
{
    DateTime? DeletedAt { get; set; }
}
