using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

public class Pig : Preys
{
    public Pig()
    {
        SourceImage = "pig.png";
        Priority = 6;
        MaxSatiety = 3;
        Saturability = 5;
        RadiusOfView = 20;
        MaxSpeed = 1;
        YoungAge = 10;
    }
    public override Animals BorningChild() => new Pig();
}