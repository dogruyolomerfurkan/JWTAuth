namespace Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}
