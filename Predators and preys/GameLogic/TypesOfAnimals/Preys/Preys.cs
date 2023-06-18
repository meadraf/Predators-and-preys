using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

public abstract class Preys : Animals
{
    public override void Eat(List<GameObject>[,] map)
    {
        var Grass = (Grass)map[Coordinate.X, Coordinate.Y][0];
        Grass.Eaten();
        Satiety += Grass.Saturability;
        Target = null;
    }
}