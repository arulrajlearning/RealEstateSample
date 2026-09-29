namespace ZipListing;

public sealed class ListingRelevanceScorer
{
    public double CalculateScore(Listing listing, decimal targetBudget, DateOnly referenceDate)
    {
        if (targetBudget <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetBudget), "targetBudget must be greater than 0.");
        }

        var priceDelta = Math.Abs((double)(listing.Price - targetBudget));
        var budgetScore = 1d - Math.Min(priceDelta / (double)targetBudget, 1d);

        var daysOld = Math.Max(0, referenceDate.DayNumber - listing.ListedDate.DayNumber);
        var recencyScore = Math.Exp(-daysOld / 30d);

        var weightedScore = (budgetScore * 0.7d) + (recencyScore * 0.3d);
        return Math.Clamp(weightedScore, 0d, 1d);
    }
}