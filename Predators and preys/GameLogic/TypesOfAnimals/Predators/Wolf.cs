using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

public class Wolf : Predators
{
    public Wolf()
    {
        SourceImage = "wolf.png";
        Priority = 3;
        MaxSatiety = 24;
        MaxSpeed = 2;
        RadiusOfView = 4;
        YoungAge = 19;
    }
    public override Animals BorningChild()=> new Wolf();
}