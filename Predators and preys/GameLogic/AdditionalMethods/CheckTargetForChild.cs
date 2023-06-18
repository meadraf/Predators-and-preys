using Predators_and_preys.GameLogic.AdditionalMethods.InterfacesForAdditionalMethods;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.AdditionalMethods;

class CheckTargetForChild : ICheckTarget
{
    public bool CheckTarget(GameObject target, ObjectWhoCanLookAround animal)
    {
        if (target is not null)
        {
            if (target.GetType() == animal.GetType() && !ReferenceEquals(animal, target))
            {
                animal.Target = target;
                return true;
            }
        }
        return false;
    }
}