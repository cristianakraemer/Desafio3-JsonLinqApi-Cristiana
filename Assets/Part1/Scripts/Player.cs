using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int id;
    public string name;
    public int points;
    public int countryIndex;
    public string platformName;
    public string countryName;
    public List<Hero> heroes;
}

[System.Serializable]
public class Hero
{
    public int heroClassIndex;
    public string heroClassName;
    public int gold;
}

[System.Serializable]
public class PlayerData
{
    public List<Player> players;
}

