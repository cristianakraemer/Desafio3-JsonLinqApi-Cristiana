using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;


public class GameManager : MonoBehaviour
{
    public TextAsset jsonFile;

    private PlayerData playerData;

    // Variáveis que servem para salvar os resultados em um novo arquivo Json
    private IEnumerable<Player> topPlayersByPoints;
    private IEnumerable<Player> playersWithoutHeroesByCountry;
    private string mostCreatedHeroClass;
    private string leastCreatedHeroClass;
    private string countryWithMostPlayers;
    private string bestPlatformByAveragePoints;
    private IEnumerable<Player> top10PlayersByTotalGold;

    private void Start()
    {
        if (jsonFile != null)
        {
            playerData = JsonUtility.FromJson<PlayerData>(jsonFile.text);
            if (playerData != null && playerData.players != null)
            {
                // 1. Listar em ordem descendente os 3 jogadores com maior número de pontos
                topPlayersByPoints = playerData.players.OrderByDescending(player => player.points).Take(3);
                Debug.Log("Top 3 Players by Points:");
                foreach (var player in topPlayersByPoints)
                {
                    Debug.Log($"{player.name} - Points: {player.points}");
                }

                // 2. Ordenar por país os jogadores que ainda não criaram heróis
                playersWithoutHeroesByCountry = playerData.players.Where(player => player.heroes.Count == 0)
                    .OrderBy(player => player.countryName);
                Debug.Log("Players without Heroes ordered by Country:");
                foreach (var player in playersWithoutHeroesByCountry)
                {
                    Debug.Log($"{player.name} - Country: {player.countryName}");
                }

                // 3. Qual é a classe de herói mais criada e a menos criada
                var heroClassCounts = playerData.players
                    .SelectMany(player => player.heroes)
                    .GroupBy(hero => hero.heroClassName)
                    .ToDictionary(group => group.Key, group => group.Count());

                mostCreatedHeroClass = heroClassCounts.OrderByDescending(kv => kv.Value).First().Key;
                leastCreatedHeroClass = heroClassCounts.OrderBy(kv => kv.Value).First().Key;

                Debug.Log($"Most Created Hero Class: {mostCreatedHeroClass}");
                Debug.Log($"Least Created Hero Class: {leastCreatedHeroClass}");

                // 4. Qual é o país que possui mais jogadores
                var countryPlayerCounts = playerData.players
                    .GroupBy(player => player.countryName)
                    .ToDictionary(group => group.Key, group => group.Count());

                countryWithMostPlayers = countryPlayerCounts.OrderByDescending(kv => kv.Value).First().Key;
                Debug.Log($"Country with the Most Players: {countryWithMostPlayers}");

                // 5. Qual plataforma possui os jogadores com melhores pontos (considerar a média dos pontos dos jogadores)
                var platformAveragePoints = playerData.players
                    .GroupBy(player => player.platformName)
                    .ToDictionary(group => group.Key, group => group.Average(p => p.points));

                bestPlatformByAveragePoints = platformAveragePoints.OrderByDescending(kv => kv.Value).First().Key;
                Debug.Log($"Platform with the Best Average Points: {bestPlatformByAveragePoints}");

                // 6. Listar os 10 jogadores com maior total de gold
                top10PlayersByTotalGold = playerData.players
                    .OrderByDescending(player => player.heroes.Sum(hero => hero.gold))
                    .Take(10);
                Debug.Log("Top 10 Players by Total Gold:");
                foreach (var player in top10PlayersByTotalGold)
                {
                    int totalGold = player.heroes.Sum(hero => hero.gold);
                    Debug.Log($"{player.name} - Total Gold: {totalGold}");
                }

                // Serializa os resultados em uma string JSON
                string resultJson = CreateResultJson();

                // Para salvar a string JSON em um arquivo
                SaveResultToJsonFile(resultJson);
            }
        }
    }

    private string CreateResultJson()
    {
        // Instância da classe GameResults que armazena os resultados
        var results = new GameResults
        {
            Top3PlayersByPoints = topPlayersByPoints.ToList(),
            PlayersWithoutHeroesByCountry = playersWithoutHeroesByCountry.ToList(),
            MostCreatedHeroClass = mostCreatedHeroClass,
            LeastCreatedHeroClass = leastCreatedHeroClass,
            CountryWithMostPlayers = countryWithMostPlayers,
            BestPlatformByAveragePoints = bestPlatformByAveragePoints,
            Top10PlayersByTotalGold = top10PlayersByTotalGold.ToList()
        };

        // Serializa o objeto de resultados em uma string JSON
        return JsonUtility.ToJson(results, true);
    }

    private void SaveResultToJsonFile(string resultJson)
    {
        string resultFilePath = Path.Combine(Application.dataPath, "Part1", "Results", "Resultados.json");

        try
        {
            File.WriteAllText(resultFilePath, resultJson);
            Debug.Log("Resultados salvos em Resultados.json na pasta Results.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao salvar os resultados: {e.Message}");
        }
    }
}

