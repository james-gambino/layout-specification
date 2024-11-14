namespace LayoutSpecificationApi;

// DTO для запроса на предоставление доступа
public class GrantAccessRequestDto
{
    public Guid UserId { get; set; }
    public SpecificationAccessType AccessType { get; set; }
    public DateTime? ExpiresAt { get; set; } // Опционально: срок действия доступа
    public string? Note { get; set; } // Опционально: примечание
}