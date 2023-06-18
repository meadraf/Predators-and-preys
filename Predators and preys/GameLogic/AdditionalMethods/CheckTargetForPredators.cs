using Predators_and_preys.GameLogic.AdditionalMethods.InterfacesForAdditionalMethods;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

namespace Predators_and_preys.GameLogic.AdditionalMethods;

class CheckTargetForPredators : ICheckTarget
{
    public bool CheckTarget(GameObject target, ObjectWhoCanLookAround animal)
    {
        Random chanceForWrongTarget = new Random();
        if ((chanceForWrongTarget.Next(0, 5) == 0)
            || animal.PairingTargetTest(target))
        {
            animal.Target = target;
            return true;
        }
        if (target is Preys)
        {
            var obj = (Animals)target;
            if (obj.Age > obj.YoungAge)
            {
                animal.Target = target;
                return true;
            }
        }
        return false;
    }
}