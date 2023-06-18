using Point = System.Drawing.Point;

namespace Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

public abstract class GameObject
{
    public string SourceImage { get; set; }
    public int Saturability { get; set; }
    public Point Coordinate { get; set; }

    public int Priority { get; set; }

}