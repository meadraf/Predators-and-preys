using Predators_and_preys.GameLogic.MoveLogic.IMoveLogic;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.MoveLogic;

class ConcreteMoveLogicC : IConcreteMoveLogic
{
    void IConcreteMoveLogic.Move(List<GameObject>[,] map, Animals animal)
    {
        if (animal.Target is not null && animal.Coordinate == animal.Target.Coordinate)
        {
            if (animal.Satiety == animal.MaxSatiety
                && animal.Target.GetType() == animal.GetType())
                animal.PairingAnimals(map);
            else if (animal.Target is Grass)
                animal.Eat(map);
        }

    }
}