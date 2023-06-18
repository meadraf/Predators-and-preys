using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.MoveLogic.IMoveLogic;

interface IConcreteMoveLogic
{
    void Move(List<GameObject>[,] map, Animals animal);
}