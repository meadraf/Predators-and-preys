using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

public abstract class ObjectWhoHasTarget : GameObject
{
    public GameObject Target;
    public int MaxSpeed { get; set; }
    public Point TargetMovement()
    {
        Point newCoordinate = new();
        Point coordinateDifferences = new()
        {
            X = Target.Coordinate.X - Coordinate.X,
            Y = Target.Coordinate.Y - Coordinate.Y
        };

        if (this is Preys && Target is Predators)
        {
            coordinateDifferences =
                new Point(-coordinateDifferences.X, -coordinateDifferences.Y);
        }
        newCoordinate =
            new Point(CheckCoordinateDiferences(coordinateDifferences.X),
                CheckCoordinateDiferences(coordinateDifferences.Y));
        newCoordinate += new Size(Coordinate.X, Coordinate.Y);

        return newCoordinate;
    }

    private int CheckCoordinateDiferences(int coordinateDifferences) =>
        MaxSpeed < Math.Abs(coordinateDifferences) ?
            MaxSpeed * Math.Sign(coordinateDifferences) : coordinateDifferences;

}