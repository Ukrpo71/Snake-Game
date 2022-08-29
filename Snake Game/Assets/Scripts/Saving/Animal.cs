using System;

[Serializable]
public class Animal
{
    public AnimalType AnimalType;
    public int NumberOfCollectedAnimals;

    public Animal(AnimalType animalType, int numberOfCollectedAnimals)
    {
        AnimalType = animalType;
        NumberOfCollectedAnimals = numberOfCollectedAnimals;
    }
}
public enum AnimalType
{
    Chicken,
    Cow
}
