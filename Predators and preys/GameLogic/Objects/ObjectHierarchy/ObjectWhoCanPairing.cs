using Predators_and_preys.GameLogic.AdditionalMethods;
using Predators_and_preys.GameLogic.TypesOfAnimals;
using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

namespace Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

public abstract class ObjectWhoCanPairing : ObjectWhoCanEat
{
    public void PairingAnimals(List<GameObject>[,] map)
    {
        var obj = (Animals) Target;
        obj.Satiety = 0;
        obj.Target = null;
        Satiety = 0;
        Target = null;

        Borning(map);
    }

    public bool PairingTargetTest(GameObject target)
    {
        if (Satiety == MaxSatiety)
        {
            if (target is not null)
            {
                if (GetType() == target.GetType() && !ReferenceEquals(this, target))
                {
                    var pairingTarget = (Animals) target;
                    return pairingTarget.Satiety == MaxSatiety;
                }
            }
        }

        return false;
    }

    private void Borning(List<GameObject>[,] map)
    {
        var factory = new FactoryOnBorningAnimals();
        factory.SetFather((Animals) this);
        Random chanceOfTwins = new();
        int numberOfChild;

        if (this is Predators || this is Preys && chanceOfTwins.Next(0, 2) == 0)
            numberOfChild = 1;
        else
            numberOfChild = 2;
        for (int i = 0; i < numberOfChild; i++)
        {
            var newChild = factory.BorningChild();
            newChild.Age = 0;
            ActionsOnMap.AddObject(Coordinate, map, newChild);
        }
    }
}