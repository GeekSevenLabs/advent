namespace Advent.Announcements.Domain.Notices;

public class Notice : Entity, IAgreggateRoot
{
    protected Notice() { }

    public Notice(
        string title,
        string description,
        DateTime startDate,
        DateTime? endDate,
        Guid createdByUserId)
    {
        Validate(title, description, startDate, endDate, createdByUserId);

        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CreatedByUserId = createdByUserId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedByUserId { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        if (IsExpired())
            throw new InvalidOperationException(
                "Não é possível ativar um aviso expirado.");

        IsActive = true;
    }

    public bool IsExpired()
    {
        return EndDate.HasValue && EndDate.Value < DateTime.UtcNow;
    }

    private static void Validate(
        string title,
        string description,
        DateTime startDate,
        DateTime? endDate,
        Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new InvalidOperationException(
                "O título do aviso não pode ser nulo ou vazio.");

        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException(
                "A descrição do aviso não pode ser nula ou vazia.");

        if (createdByUserId == Guid.Empty)
            throw new InvalidOperationException(
                "O campo \"Criado por usuário\" não pode estar vazio.");

        if (startDate == default)
            throw new InvalidOperationException(
                "É necessário ter uma data de início.");

        if (endDate.HasValue && endDate.Value < startDate)
            throw new InvalidOperationException(
                "A data de término não pode ser anterior à data de início.");
    }
}
