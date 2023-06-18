namespace Predators_and_preys;

public class MapConfig
{
    public MapConfig()
    {
        MapSize = 20;
        NumOfPreys = MaxOfPreys;
        NumOfPredators = MaxOfPredators;
    }

    public int NumOfPredators { get; set; }

    public int MaxOfPredators
    {
        get
        {
            return MapSize switch
            {
                15 => 30,
                30 => 60,
                _ => 40
            };
        }
    }

    public int NumOfPreys { get; set; }

    public int MaxOfPreys
    {
        get
        {
            return MapSize switch
            {
                15 => 50,
                30 => 100,
                _ => 70
            };
        }
    }

    public int NumOfTurns { get; set; }
    public int MapSize { get; set; } 
}