﻿#nullable enable
using Predators_and_preys.GameLogic.AdditionalMethods;
using Predators_and_preys.GameLogic.MoveLogic.IMoveLogic;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Predators_and_preys.GameLogic.MoveLogic;

class MoveLogicStrategy
{
    private IConcreteMoveLogic? _concreteMoveLogic;

    public void SetConcreteStrategy(IConcreteMoveLogic concreteMoveLogic)
    {
        _concreteMoveLogic = concreteMoveLogic;
    }

    public void MoveStrategy(List<GameObject>[,] map, Animals animal)
    {
        if (animal.DeadlyHungerLevel)
        {
            ActionsOnMap.DeleteObject(map, animal);
            return;
        }
        animal.Target = null;
        if (!map[animal.Coordinate.X, animal.Coordinate.Y].Contains(animal))
            return;

        if (animal.Age < animal.YoungAge)
            animal.SetStragetyForView(new CheckTargetForChild());
        else if (animal is Preys)
            animal.SetStragetyForView(new CheckTargetForPreys());
        else
            animal.SetStragetyForView(new CheckTargetForPredators());

        animal.CheckAround(map);
        Point newCoordinate;
        var movedObject = ActionsOnMap.DeleteObject(map, animal);

        if (animal.Target is not null)
            newCoordinate = animal.TargetMovement();
        else
        {
            var random = Direction.RandomDirection();
            newCoordinate = random;
            newCoordinate += new Size(animal.Coordinate.X, animal.Coordinate.Y);
        }
        while (!animal.InsideBound(newCoordinate, map))
            newCoordinate = Direction.ChangeDirection(newCoordinate, map);

        ActionsOnMap.AddObject(newCoordinate, map, animal);

        _concreteMoveLogic?.Move(map, animal);

    }
}