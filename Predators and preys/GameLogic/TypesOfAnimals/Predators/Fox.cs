using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

public class Fox : Predators
{
    public Fox()
    {
        SourceImage = "fox.png";
        Priority = 4;
        MaxSatiety = 20;
        MaxSpeed = 2;
        RadiusOfView = 4;
        YoungAge = 18;
    }
    public override Animals BorningChild() => new Fox();
}