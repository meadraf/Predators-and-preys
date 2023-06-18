using System.Collections.ObjectModel;
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
            var buttonContainer = AddButtonsToContainer();
            var startStop = CreateStopButton();
            
            var scrollView = new ScrollView();
            var horizontalStack = new HorizontalStackLayout()
            {
                BackgroundColor = new Color(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20,20,20,20),
            };
            var verticalStack = new VerticalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
            };
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
                        HeightRequest = 30,
                        WidthRequest = 30,
                        BindingContext = item
                    };
                    image.SetBinding(Image.SourceProperty, new Binding($"ValueString"));
                    horizontalStackLayout.Children.Add(image);
                }

                verticalStack.Children.Add(horizontalStackLayout);
            }
            
            horizontalStack.Children.Add(startStop);
            scrollView.Content = horizontalStack;
            _gamePage.Content = scrollView;
            MapReady = true;
        });
    }

    private HorizontalStackLayout AddButtonsToContainer()
    {
        var horizontalContainer = new HorizontalStackLayout
        {
            BackgroundColor = new Color(255, 255, 255),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 10
        };
        var stopButton = CreateStopButton();
        //var statisticButton = CreateGoToStatisticButton();
        horizontalContainer.Add(stopButton);
        //horisontalContainer.Add(statisticButton);
        return horizontalContainer;
    }

    /*private Button CreateGoToStatisticButton()
    {
        var goToStatisticButton = new Button
        {
            BackgroundColor = new Color(50, 170, 255),
            Text = "Go to statistic",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = new Color(255, 255, 255),
            WidthRequest = 200,
        };
        goToStatisticButton.Clicked += (senter, e) =>
        {
            if (_simulation.IsSimulationContinuing)
                _simulation.IsSimulationContinuing = false;
            lock (lockCells)
            {
                //MainThread.BeginInvokeOnMainThread(() =>
                //   _gamePage.Navigation.PushAsync(new StatisticMenu(_simulation.Statistics)));
            }
        };

        return goToStatisticButton;
    }*/
    private Button CreateStopButton()
    {
        var stopButton = new Button
        {
            Text = "Start",
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 100,
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