#nullable enable
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.Simulation;

class Simulation
{
    public readonly Statistics Statistics;
    public static event Action? Update;
    public delegate void AnimalsMove(List<GameObject>[,] map);
    public static event AnimalsMove? Move;
    public bool IsSimulationContinuing = false;
    private readonly int _delay;
    object lockCells = new();
    private static int MaxTurns { get; set; }
    private readonly List<GameObject>[,] _map;

    public Simulation(List<GameObject>[,] map, int stepsAmount)
    {
        _delay = 200;
        MaxTurns = stepsAmount;
        Statistics = new Statistics(map);
        _map = map;
    }

    public async void Start(Visualisation visualisation)
    {
        while (Statistics.TurnsCount < MaxTurns && IsSimulationContinuing)
        {
            await visualisation.GeneratePriorityMap();
            Thread.Sleep(_delay);
            lock (lockCells)
            {
                Update.Invoke();
                Move.Invoke(_map);
            }
            Statistics.RecordStatistics();
        }
    }
}