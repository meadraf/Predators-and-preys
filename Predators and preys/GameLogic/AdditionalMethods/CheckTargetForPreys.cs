using Predators_and_preys.GameLogic.AdditionalMethods.InterfacesForAdditionalMethods;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;
using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;

namespace Predators_and_preys.GameLogic.AdditionalMethods;

class CheckTargetForPreys : ICheckTarget
{
    public bool CheckTarget(GameObject target, ObjectWhoCanLookAround animal)
    {
        if (target is Grass grass)
        {
            if (grass.IsGrown)
            {
                animal.Target = target;
                return true;
            }
        }

        if (animal.PairingTargetTest(target) || target is Predators)
        {
            animal.Target = target;
            return true;
        }
        return false;
    }
}