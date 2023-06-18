using Predators_and_preys.GameLogic.AdditionalMethods;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

public abstract class Predators : Animals
{
    private GameObject EatingTarget(List<GameObject>[,] map)
    {
        if (Target is not null)
            if (!map[Coordinate.X, Coordinate.Y].Contains(Target))
                Target = null;
            else
                ActionsOnMap.DeleteObject(map, Target);

        return Target;
    }

    public override void Eat(List<GameObject>[,] map)
    {
        var food = EatingTarget(map);
        if (food != null)
            Satiety += food.Saturability;
        Target = null;
    }
}