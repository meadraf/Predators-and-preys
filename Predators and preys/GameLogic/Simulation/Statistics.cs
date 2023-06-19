using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;
using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

namespace Predators_and_preys.GameLogic.Simulation;

public class Statistics : IEnumerable<TurnInfo>, INotifyPropertyChanged
{
    private readonly List<TurnInfo> _turnsCollection;
    private int _turnsCount;
    private int _curentPredators;
    private int _currentPreys;

    public int TurnsCount
    {
        get => _turnsCount;
        private set
        {
            _turnsCount = value;
            OnPropertyChanged();
        }
    }

    public int CurrentPredators
    {
        get => _curentPredators;
        set
        {
            _curentPredators = value;
            OnPropertyChanged();
        }
    }

    public int CurrentPreys
    {
        get => _currentPreys;
        set
        {
            _currentPreys = value;
            OnPropertyChanged();
        }
    }

    private readonly List<GameObject>[,] _map;

    public TurnInfo CurrentTurnInfo => this[TurnsCount - 1];

    public Statistics(List<GameObject>[,] map)
    {
        Simulation.Update += RecordStatistics;
        _map = map;
        TurnsCount = 0;
        _turnsCollection = new List<TurnInfo>();
        RecordStatistics();
    }

    public IEnumerator<TurnInfo> GetEnumerator() => _turnsCollection.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _turnsCollection.GetEnumerator();

    public TurnInfo this[int index]
    {
        get => _turnsCollection[index];
        private set => _turnsCollection[index] = value;
    }

    public void RecordStatistics()
    {
        TurnsCount++;
        _turnsCollection.Add(new TurnInfo());
        this[TurnsCount - 1].TurnNumber = TurnsCount;
        CountPredators();
        CountPreys();
        CountGrass();
    }

    private int CountPredators()
    {
        foreach (var cell in _map)
        {
            foreach (var animal in cell.OfType<Predators>())
            {
                this[TurnsCount - 1].PredatorSpecietyCounter.TryAdd(animal.GetType(), 0);
                this[TurnsCount - 1].PredatorSpecietyCounter[animal.GetType()]++;
                this[TurnsCount - 1].PredatorsCount++;
            }
        }

        CurrentPredators = this[TurnsCount - 1].PredatorsCount;
        return this[TurnsCount - 1].PredatorsCount;
    }

    private int CountPreys()
    {
        foreach (var cell in _map)
        {
            foreach (var animal in cell.OfType<Preys>())
            {
                this[TurnsCount - 1].PreySpecietyCounter.TryAdd(animal.GetType(), 0);
                this[TurnsCount - 1].PreySpecietyCounter[animal.GetType()]++;
                this[TurnsCount - 1].PreysCount++;
            }
        }

        CurrentPreys = this[TurnsCount - 1].PreysCount;
        return this[TurnsCount - 1].PreysCount;
    }

    private void CountGrass()
    {
        foreach (var cell in _map)
        {
            foreach (var grass in cell)
            {
                if (grass is Grass grassObject)
                {
                    if (grassObject.GrowRate < 10)
                        this[TurnsCount - 1].GrassEatenCount++;
                    if (grassObject.GrowRate == 10)
                        this[TurnsCount - 1].GrassGrowCount++;
                }
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}