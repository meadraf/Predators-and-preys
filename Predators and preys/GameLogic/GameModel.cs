using Predators_and_preys.GameLogic.AdditionalMethods;
using Point = System.Drawing.Point;
using Predators_and_preys.GameLogic.Objects;
using Predators_and_preys.GameLogic.Objects.ObjectHierarchy;
using Predators_and_preys.GameLogic.TypesOfAnimals.Predators;
using Predators_and_preys.GameLogic.TypesOfAnimals.Preys;

namespace Predators_and_preys.GameLogic;

class GameModel
{
    public readonly List<GameObject>[,] Map;
    private readonly Random _random = new();
    private int _rowNumber;
    private int _columnNumber;
    private readonly List<Animals> _predatorsTypeList;

    public GameModel(int preys, int predators, int mapSize)
    {
        _predatorsTypeList = new List<Animals>
        {
            new Tiger(), new Wolf(), new Bear(), new Fox()
        };
        var preysTypeList = new List<Animals>
        {
            new Deer(), new Pig(), new Rabbit(), new Sheep()
        };
        
        Map = new List<GameObject>[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        for (int j = 0; j < mapSize; j++)
            Map[i, j] = new List<GameObject>();
        GenerateGrass(mapSize*(mapSize/2));
        GenerateAnimals(preys, preysTypeList);
        GenerateAnimals(predators, _predatorsTypeList);
    }

    private void GenerateAnimals(int amountOfAnimals, List<Animals> animalsTypeList)
    {
        for (int i = 0; i < amountOfAnimals; i++)
        {
            ActionsOnMap.AddObject(new Point(_random.Next(0, Map.GetLength(0)), _random.Next(0, Map.GetLength(1))),
                Map, animalsTypeList[_random.Next(0, _predatorsTypeList.Count)].BorningChild());
        }
    }

    private void GenerateGrass(int amountOfGrass)
    {
        for (int i = 0; i < amountOfGrass; i++)
        {
            do
            {
                _rowNumber = _random.Next(0, Map.GetLength(0));
                _columnNumber = _random.Next(0, Map.GetLength(1));
            } while (Map[_rowNumber, _columnNumber].Count != 0);

            var grass = new Grass
            {
                Coordinate = new Point(_rowNumber, _columnNumber)
            };
            ActionsOnMap.AddObject(grass.Coordinate, Map, grass);
        }
    }
}