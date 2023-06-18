using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;

namespace Predators_and_preys.GameLogic.TypesOfAnimals;

class FactoryOnBorningAnimals : IFactory
{
    private IFactory? _father;
    public void SetFather(Animals father)
    {
        _father = (IFactory)father;
    }
    public Animals BorningChild()
    {
        return _father.BorningChild();
    }
}