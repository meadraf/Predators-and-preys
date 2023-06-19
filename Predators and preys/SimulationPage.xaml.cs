using Predators_and_preys.GameLogic;
using Predators_and_preys.GameLogic.Simulation;
using ContentPage = Microsoft.Maui.Controls.ContentPage;

namespace Predators_and_preys;

public partial class SimulationPage : ContentPage
{
    private GameModel _gameModel;
    private readonly MapConfig _mapConfig;

    public SimulationPage(MapConfig mapConfig)
    {
        InitializeComponent();
        _mapConfig = mapConfig;
        Task.Run(StartGame);
    }

    private async Task StartGame()
    {
        _gameModel = new GameModel(_mapConfig.NumOfPreys, _mapConfig.NumOfPredators, _mapConfig.MapSize);
        var simulation = new Simulation(_gameModel.Map, _mapConfig.NumOfTurns);
        var visualisation = new Visualisation(_gameModel.Map, this, simulation);
        visualisation.CreateVisualisationPage();
        await Task.Run(async () =>
        {
            while (!visualisation.MapReady)
            {
                await Task.Delay(10);
            }

            simulation.Start(visualisation);
        });
    }
}