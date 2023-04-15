namespace Model;

public class Hand
{
    public HandClass Class { get; set; }
    public Rank Rank { get; set; }
    public Rank[] KickerRanks { get; set; }

    public Hand(HandClass handClass, Rank rank, Rank[] kickerRanks)
    {
        this.Class = handClass;
        this.Rank = rank;
        this.KickerRanks = kickerRanks;
    }
}
