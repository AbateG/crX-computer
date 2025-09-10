namespace CR_COMPUTER.Domain.Enums
{
    /// <summary>
    /// Defines the different statuses a task can have
    /// </summary>
    public enum TaskStatus
    {
        NotStarted = 1,
        InProgress = 2,
        OnHold = 3,
        Completed = 4,
        Cancelled = 5
    }
}
