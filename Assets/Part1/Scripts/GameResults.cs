using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameResults // Para salvar os resultados num novo arquivo Json.
{
    public List<Player> Top3PlayersByPoints;
    public List<Player> PlayersWithoutHeroesByCountry;
    public string MostCreatedHeroClass;
    public string LeastCreatedHeroClass;
    public string CountryWithMostPlayers;
    public string BestPlatformByAveragePoints;
    public List<Player> Top10PlayersByTotalGold;
}
