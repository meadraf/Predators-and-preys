using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.AdditionalMethods;

abstract class ActionsOnMap
{
    public static void AddObject(System.Drawing.Point newCoordinate, List<GameObject>[,] map, GameObject obj)
    {
        map[newCoordinate.X, newCoordinate.Y].Add(obj);
        obj.Coordinate = newCoordinate;
    }
    public static GameObject DeleteObject(List<GameObject>[,] map, GameObject obj)
    {
        map[obj.Coordinate.X, obj.Coordinate.Y].Remove(obj);
        return obj;
    }

}