using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

public class Rabbit : Preys
{
    public Rabbit()
    {
        SourceImage = "rabbit.png";
        Priority = 8;
        MaxSpeed = 2;
        MaxSatiety = 1;
        Saturability = 2;
        RadiusOfView = 20;
        YoungAge = 12;
    }
    public override Animals BorningChild() => new Rabbit();
}