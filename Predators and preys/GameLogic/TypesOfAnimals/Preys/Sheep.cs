using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

public class Sheep : Preys
{
    public Sheep()
    {
        SourceImage = "sheep.png";
        Priority = 7;
        MaxSatiety = 2;
        Saturability = 4;
        RadiusOfView = 20;
        MaxSpeed = 1;
        YoungAge = 12;
    }
    public override Animals BorningChild() => new Sheep();
}