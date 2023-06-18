using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

public class Tiger : Predators
{
    public Tiger()
    {
        SourceImage = "tiger.png";
        Priority = 2;
        MaxSatiety = 30;
        MaxSpeed = 2;
        RadiusOfView = 4;
        YoungAge = 23;
    }
    public override Animals BorningChild() => new Tiger();
}