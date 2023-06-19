using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic;

class Visualisation : BindableObject
{
    private readonly ObservableCollection<ObservableCollection<MyString>> _priorityMap;

    private readonly List<GameObject>[,] _gameModelMap;
    private readonly SimulationPage _gamePage;
    object lockCells = new();
    private bool _mapReady;
    private readonly Simulation.Simulation _simulation;

    public int ImageSize
    {
        get
        {
            return _gameModelMap.GetLength(1) switch
            {
                20 => 30,
                30 => 20,
                _ => 40
            };
        }
    }

    public bool MapReady
    {
        get => _mapReady;
        private set
        {
            if (_mapReady != value)
            {
                _mapReady = value;
                OnPropertyChanged();
            }
        }
    }

    public Visualisation(List<GameObject>[,] gameModel, SimulationPage gamePage, Simulation.Simulation simulation)
    {
        _simulation = simulation;
        _gamePage = gamePage;
        _gameModelMap = gameModel;
        _priorityMap = new ObservableCollection<ObservableCollection<MyString>>();
        InitializePriorityMap();
    }

    private void InitializePriorityMap()
    {
        lock (lockCells)
        {
            for (int i = 0; i < _gameModelMap.GetLength(0); i++)
            {
                var row = new ObservableCollection<MyString>();
                for (int j = 0; j < _gameModelMap.GetLength(1); j++)
                {
                    if (GetPriorityItem(_gameModelMap[i, j]) != null)
                        row.Add(new MyString(GetPriorityItem(_gameModelMap[i, j])));
                    else
                        row.Add(new MyString("empty.png"));
                }

                _priorityMap.Add(row);
            }
        }
    }

    private string GetPriorityItem(List<GameObject> cell)
    {
        lock (lockCells)
        {
            if (cell.Count == 0 || cell is null)
                return null;
            return cell.ToList()
                .Where(item => item is not null)
                .Select(item => (item.Priority, item.SourceImage)).Min().SourceImage;
        }
    }

    public void CreateVisualisationPage()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var startStop = CreateStopButton();
            var horizontalStack = new HorizontalStackLayout()
            {
                BackgroundColor = new Color(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20, 20, 20, 20),
            };
            var verticalStack = new VerticalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            var labelsLayout = new VerticalStackLayout() { Margin = new Thickness(40,0,0,0)};
            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20,30,0,0)
            };

            CreateLabels(grid);

            grid.AddColumnDefinition(new ColumnDefinition() {Width = 110});
            grid.AddColumnDefinition(new ColumnDefinition() {Width = 110});
            grid.AddRowDefinition(new RowDefinition() {Height = 50});
            grid.AddRowDefinition(new RowDefinition() {Height = 50});
            grid.AddRowDefinition(new RowDefinition() {Height = 50});

            horizontalStack.Children.Add(verticalStack);
            foreach (var row in _priorityMap)
            {
                var horizontalStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                foreach (var item in row)
                {
                    var image = new Image
                    {
                        HeightRequest = ImageSize,
                        WidthRequest = ImageSize,
                        BindingContext = item
                    };
                    image.SetBinding(Image.SourceProperty, new Binding($"ValueString"));
                    horizontalStackLayout.Children.Add(image);
                }

                verticalStack.Children.Add(horizontalStackLayout);
            }

            labelsLayout.Children.Add(grid);
            labelsLayout.Children.Add(startStop);
            horizontalStack.Children.Add(labelsLayout);
            _gamePage.Content = horizontalStack;
            MapReady = true;
        });
    }

    private void CreateLabels(Grid grid)
    {
        var turn = new Label {FontSize = 17, TextColor = new Color(0,0,0), Text = "Turn: "};
        var preys = new Label {FontSize = 17, Text = "Preys: "};
        var predators = new Label {FontSize = 17, Text = "Predators: "};

        var turnsCount = new Label {FontSize = 17, BindingContext = _simulation.Statistics,};
        var preysCount = new Label {FontSize = 17, BindingContext = _simulation.Statistics,};
        var predatorsCount = new Label {FontSize = 17, BindingContext = _simulation.Statistics,};
        
        
        turnsCount.SetBinding(Label.TextProperty, nameof(_simulation.Statistics.TurnsCount));
        preysCount.SetBinding(Label.TextProperty, nameof(_simulation.Statistics.CurrentPreys));
        predatorsCount.SetBinding(Label.TextProperty, nameof(_simulation.Statistics.CurrentPredators));
        
        grid.Add(turn, 0, 0);
        grid.Add(turnsCount, 1,0);
        grid.Add(preys, 0, 1);
        grid.Add(preysCount, 1,1);
        grid.Add(predators, 0, 2);
        grid.Add(predatorsCount, 1,2);
    }

    private Button CreateStopButton()
    {
        var stopButton = new Button
        {
            Text = "Start",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 100,
            Margin = new Thickness(150, 10, 0, 0)
        };
        stopButton.Clicked += async (sender, e) =>
        {
            if (_simulation.IsSimulationContinuing)
            {
                _simulation.IsSimulationContinuing = false;
                stopButton.Text = "Continue";
            }
            else
            {
                _simulation.IsSimulationContinuing = true;
                stopButton.Text = "Stop";
                await Task.Run(() => { _simulation.Start(this); });
            }
        };
        return stopButton;
    }

    public Task GeneratePriorityMap()
    {
        Dispatcher.Dispatch(UpdatePriorityMap);
        return Task.CompletedTask;
    }

    private void UpdatePriorityMap()
    {
        lock (lockCells)
        {
            for (int i = 0; i < _gameModelMap.GetLength(0); i++)
            {
                for (int j = 0; j < _gameModelMap.GetLength(1); j++)
                {
                    if (GetPriorityItem(_gameModelMap[i, j]) != null)
                        _priorityMap[i][j].ValueString = GetPriorityItem(_gameModelMap[i, j]);
                    else
                        _priorityMap[i][j].ValueString = "empty.png";
                }
            }
        }
    }
}