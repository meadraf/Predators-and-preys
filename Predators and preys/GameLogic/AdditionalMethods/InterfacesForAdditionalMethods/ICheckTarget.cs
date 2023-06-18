using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.AdditionalMethods.InterfacesForAdditionalMethods;

public interface ICheckTarget
{
    public bool CheckTarget(GameObject target, ObjectWhoCanLookAround animal);
}