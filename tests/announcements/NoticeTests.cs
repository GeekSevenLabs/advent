using Advent.Announcements.Domain.Notices;

namespace Advent.Announcements.Tests;

public class NoticeTests
{
    private readonly Guid _validUserId = Guid.NewGuid();
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.UtcNow);

    [Fact]
    public void Constructor_WithValidData_ShouldCreateNoticeAndInitializeBaseProperties()
    {
        // Arrange
        var title = "Aviso de Manutenção";
        var description = "O sistema ficará offline por 2 horas.";
        var startDate = _today;

        // Act
        var notice = new Notice(title, description, startDate, null, _validUserId);

        // Assert
        Assert.NotEqual(Guid.Empty, notice.Id); // Verifica se o Guid v7 foi gerado
        Assert.False(notice.IsDeleted); // Começa ativo (não deletado)
        Assert.Null(notice.DeletedAt);
        Assert.True(notice.CreatedAt <= DateTimeOffset.UtcNow);
        Assert.Equal(title, notice.Title);
    }

    [Fact]
    public void Deactivate_ShouldSoftDeleteNotice()
    {
        // Arrange
        var notice = new Notice("Título", "Descrição", _today, null, _validUserId);

        // Act
        notice.Deactivate();

        // Assert
        Assert.True(notice.IsDeleted);
        Assert.NotNull(notice.DeletedAt);
        Assert.True(notice.DeletedAt <= DateTimeOffset.UtcNow);
    }

    [Fact]
    public void Activate_WhenNoticeIsNotExpired_ShouldRecoverNotice()
    {
        // Arrange
        var notice = new Notice("Título", "Descrição", _today, null, _validUserId);
        notice.Deactivate(); // Primeiro deletamos

        // Act
        notice.Activate();

        // Assert
        Assert.False(notice.IsDeleted);
        Assert.Null(notice.DeletedAt);
    }

    [Fact]
    public void Activate_WhenNoticeIsExpired_ShouldThrowException()
    {
        // Arrange
        var pastDate = _today.AddDays(-5);
        var notice = new Notice("Título", "Descrição", pastDate.AddDays(-1), pastDate, _validUserId);
        notice.Deactivate();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => notice.Activate());
        Assert.Equal("Não é possível ativar um aviso expirado.", exception.Message);
    }

    [Theory]
    [InlineData(0, false)]  // Expira hoje: Não é considerado expirado ainda (Value < today)
    [InlineData(-1, true)]  // Expirou ontem: Expirado
    [InlineData(1, false)]  // Expira amanhã: Não expirado
    public void IsExpired_ShouldVerifyCorrectlyBasedOnToday(int daysFromToday, bool expectedIsExpired)
    {
        // Arrange
        var endDate = _today.AddDays(daysFromToday);
        var notice = new Notice("Título", "Descrição", _today.AddDays(-10), endDate, _validUserId);

        // Act
        var result = notice.IsExpired();

        // Assert
        Assert.Equal(expectedIsExpired, result);
    }

    [Fact]
    public void Constructor_EndDateBeforeStartDate_ShouldThrowException()
    {
        // Arrange
        var startDate = _today;
        var endDate = _today.AddDays(-1);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            new Notice("Título", "Descrição", startDate, endDate, _validUserId));

        Assert.Equal("A data de término não pode ser anterior à data de início.", exception.Message);
    }
}
