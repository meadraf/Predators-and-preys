using Predators_and_preys.GameLogic.AdditionalMethods.InterfacesForAdditionalMethods;
using Point = System.Drawing.Point;

namespace Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

public abstract class ObjectWhoCanLookAround : ObjectWhoCanPairing
{
    public int RadiusOfView { get; set; }

    private ICheckTarget _strategy;
    public bool InsideBound(Point point, List<GameObject>[,] map)
    {
        return point is {X: >= 0, Y: >= 0} 
               && point.X < map.GetLength(0) && point.Y < map.GetLength(1);
    }
    private bool OnBound(Point point)
    {
        return Coordinate.X + RadiusOfView == point.X ||
               Coordinate.X - RadiusOfView == point.X ||
               Coordinate.Y + RadiusOfView == point.Y ||
               Coordinate.Y - RadiusOfView == point.Y;
    }
    public bool CheckFieldOfView(List<GameObject>[,] map,
        Func<GameObject, ObjectWhoCanLookAround, bool> CheckTarget)
    {
        if (Target is not null)
            return true;
        var isVisited = new HashSet<Point>();
        isVisited.Add(Coordinate);
        var bfs = new Queue<Point>();
        bfs.Enqueue(Coordinate);
        while (bfs.Count != 0)
        {
            var point = bfs.Dequeue();
            if (map[point.X, point.Y].Any(obj => !ReferenceEquals(this, obj) && CheckTarget(obj, this)))
            {
                return true;
            }
            if (!OnBound(point))
                for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    var nextPoint = new Point(point.X + i, point.X + j);
                    if (InsideBound(nextPoint, map))
                    {
                        if (!isVisited.Contains(nextPoint))
                        {
                            isVisited.Add(nextPoint);
                            bfs.Enqueue(nextPoint);
                        }
                    }
                }
        }
        return false;

    }

    public void SetStragetyForView(ICheckTarget strategyAge)
    {
        _strategy = strategyAge;
    }
    public void CheckAround(List<GameObject>[,] map)
    {
        CheckFieldOfView(map, _strategy.CheckTarget);
    }
}