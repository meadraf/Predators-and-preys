using Predators_and_preys.GameLogic.MoveLogic;
using Predators_and_preys.GameLogic.TypesOfAnimals;
using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

namespace Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

public abstract class Animals : ObjectWhoCanLookAround, IFactory
{
    private int _age;

    public bool DeadlyHungerLevel;
    public int HungerLevel { get; set; }
    public int YoungAge { get; set; }
    public int Age
    {
        get => _age;
        set
        {
            _age = value;
            if (_age < 0)
                _age = 0;
        }
    }
    public abstract Animals BorningChild();
    public Animals()
    {
        DeadlyHungerLevel = false;
        Age = 20;
        Simulation.Simulation.Move += Move;
        Simulation.Simulation.Update += GrowingUp;
        Simulation.Simulation.Update += GettingHunger;
    }
    private void GrowingUp()
    {
        Age++;
    }
    private void GettingHunger()
    {
        if (Satiety == 0 && Age > YoungAge)
            HungerLevel++;
        if (Satiety > 0)
            HungerLevel = 0;
        if (HungerLevel == 50)
            DeadlyHungerLevel = true;
    }

    private void Move(List<GameObject>[,] map)
    {

        MoveLogicStrategy moveStrategy = new MoveLogicStrategy();
        
        if (this is Predators)
            moveStrategy.SetConcreteStrategy(new ConcreteMoveLogicB());
        else if (this is Preys)
            moveStrategy.SetConcreteStrategy(new ConcreteMoveLogicC());

        moveStrategy.MoveStrategy(map, this);
    }
}