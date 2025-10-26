namespace FiapCloudGames.Domain.Promotions.ValueObjects;

public record ValidityPeriod
{
    public ValidityPeriod(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("A data de fim de vigência não pode ser anterior à data de início.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public static ValidityPeriod Create(DateTime rawInputStartDate, DateTime rawInputEndDate)
    {
        return new ValidityPeriod(rawInputStartDate, rawInputEndDate);
    }

    public bool IsActive(DateTime now)
    {
        return now >= StartDate && now <= EndDate;
    }
}