﻿using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

public class Bear : Predators
{

    public Bear()
    {
        SourceImage = "bear.png";
        Priority = 1;
        MaxSatiety = 40;
        MaxSpeed = 2;
        RadiusOfView = 3;
        YoungAge = 20;
    }

    public override Animals BorningChild() => new Bear();
}