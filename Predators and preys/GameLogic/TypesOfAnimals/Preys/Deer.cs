using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

public class Deer : Preys
{
    public Deer()
    {
        SourceImage = "deer_female.png";
        Priority = 5;
        MaxSatiety = 4;
        Saturability = 8;
        RadiusOfView = 20;
        MaxSpeed = 1;
        YoungAge = 20;
    }
    public override Animals BorningChild() => new Deer();
}