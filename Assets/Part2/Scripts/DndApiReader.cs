using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using API_DND;
using System.Linq;
using System.IO;
using static Unity.VisualScripting.Icons;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.GridLayoutGroup;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine.XR;
using UnityEngine.Networking;

public class DndApiReader : MonoBehaviour
{
    public ClassesDTO[] classes;
    public RaceDTO[] races;
    public AbilityScoreDTO[] abilityScore;
    public TraitDTO[] traits;
    public EquipmentDTO[] equipment;
    public Damage_Type[] damage;
    public ProficiencyDTO[] proficiencies;
    public AlignmentDTO[] alignment;
    public WeaponDTO[] weapons;
    public WeaponProperty[] weaponProperties;

    private DndClient dndClient;

    private void Start()
    {
        dndClient = new DndClient();

        StartCoroutine(GetClasses(new string[] { "barbarian", "bard", "cleric", "druid", "fighter", "monk", "paladin", "ranger", "rogue", "sorcerer", "warlock", "wizard" }));
        StartCoroutine(GetRaces(new string[] { "dragonborn", "dwarf", "elf", "gnome", "half-elf", "half-orc", "halfling", "human", "tiefling" }));
        StartCoroutine(GetAbilityScores(new string[] { "cha", "con", "dex", "int", "str", "wis" }));
        StartCoroutine(GetTraits(new string[] { "artificers-lore", "brave", "breath-weapon", "damage-resistance", "darkvision", "draconic-ancestry", "draconic-ancestry-black", 
            "draconic-ancestry-blue", "draconic-ancestry-brass", "draconic-ancestry-bronze", "draconic-ancestry-copper", "draconic-ancestry-gold", "draconic-ancestry-green", 
            "draconic-ancestry-red", "draconic-ancestry-silver", "draconic-ancestry-white", "dwarven-combat-training", "dwarven-resilience", "dwarven-toughness", "elf-weapon-training", 
            "extra-language", "fey-ancestry", "gnome-cunning", "halfling-nimbleness", "hellish-resistance", "high-elf-cantrip", "infernal-legacy", "keen-senses", "lucky", "menacing", 
            "naturally-stealthy", "relentless-endurance", "savage-attacks", "skill-versatility", "stonecunning", "tinker", "tool-proficiency", "trance" }));
        StartCoroutine(GetEquipment(new string[] { "abacus", "acid-vial", "alchemists-fire-flask", "alchemists-supplies", "alms-box", "amulet", "animal-feed-1-day", "antitoxin-vial", "arrow", "backpack", "bagpipes" }));
        StartCoroutine(GetProficiencies(new string[] { "alchemists-supplies", "all-armor", "bagpipes", "battleaxes", "blowguns", "breastplate", "brewers-supplies", "chain-mail", "chain-shirt", "clubs" }));
        StartCoroutine(GetAlignment(new string[] { "chaotic-neutral", "chaotic-evil", "chaotic-good", "lawful-neutral", "lawful-evil", "lawful-good", "neutral", "neutral-evil", "neutral-good" }));
        StartCoroutine(GetDamageTypes(new string[] { "acid", "cold", "fire", "force", "lightning", "necrotic", "piercing", "poison", "psychic", "radiant", "slashing", "thunder" }));
        StartCoroutine(GetWeapons1(new string[] { "ammunition", "finesse", "heavy", "light", "loading", "monk", "reach", "special", "thrown", "two-handed", "versatile" }));
        StartCoroutine(GetWeapons2(new string[] { "club" }));
    }
    
    IEnumerator GetClasses(string[] classNames)
    {
        foreach (string className in classNames)
        {
            yield return StartCoroutine(dndClient.GetClasses(className, (response) =>
            {
                var classData = JsonUtility.FromJson<ClassesDTO>(response);
                SaveJsonToFile(classData, $"class_{className}.json");
                if (classes == null)
                    classes = new ClassesDTO[] { classData };
                else
                    classes = classes.Concat(new ClassesDTO[] { classData }).ToArray();
            }));
        }
    }

    IEnumerator GetRaces(string[] raceNames)
    {
        foreach (string raceName in raceNames)
        {
            yield return StartCoroutine(dndClient.GetRaces(raceName, (response) =>
            {
                var raceData = JsonUtility.FromJson<RaceDTO>(response);
                SaveJsonToFile(raceData, $"race_{raceName}.json");
                if (races == null)
                    races = new RaceDTO[] { raceData };
                else
                    races = races.Concat(new RaceDTO[] { raceData }).ToArray();
            }));
        }
    }

    IEnumerator GetAbilityScores(string[] abilityScoreNames)
    {
        foreach (string abilityScoreName in abilityScoreNames)
        {
            yield return StartCoroutine(dndClient.GetAbilityScore(abilityScoreName, (response) =>
            {
                var abilityScoreData = JsonUtility.FromJson<AbilityScoreDTO>(response);
                SaveJsonToFile(abilityScoreData, $"ability_{abilityScoreName}.json");
                if (abilityScore == null)
                    abilityScore = new AbilityScoreDTO[] { abilityScoreData };
                else
                    abilityScore = abilityScore.Concat(new AbilityScoreDTO[] { abilityScoreData }).ToArray();
            }));
        }
    }

    IEnumerator GetTraits(string[] traitNames)
    {
        foreach (string traitName in traitNames)
        {
            yield return StartCoroutine(dndClient.GetTraits(traitName, (response) =>
            {
                var traitData = JsonUtility.FromJson<TraitDTO>(response);
                if (traits == null)
                    traits = new TraitDTO[] { traitData };
                else
                    traits = traits.Concat(new TraitDTO[] { traitData }).ToArray();
            }));
        }
    }

    IEnumerator GetEquipment(string[] equipmentNames)
    {
        foreach (string equipmentName in equipmentNames)
        {
            yield return StartCoroutine(dndClient.GetEquipment(equipmentName, (response) =>
            {
                var equipmentData = JsonUtility.FromJson<EquipmentDTO>(response);
                if (equipment == null)
                    equipment = new EquipmentDTO[] { equipmentData };
                else
                    equipment = equipment.Concat(new EquipmentDTO[] { equipmentData }).ToArray();
            }));
        }
    }

    IEnumerator GetProficiencies(string[] proficiencyNames)
    {
        foreach (string proficiencyName in proficiencyNames)
        {
            yield return StartCoroutine(dndClient.GetProficiencies(proficiencyName, (response) =>
            {
                var proficiencyData = JsonUtility.FromJson<ProficiencyDTO>(response);
                if (proficiencies == null)
                    proficiencies = new ProficiencyDTO[] { proficiencyData };
                else
                    proficiencies = proficiencies.Concat(new ProficiencyDTO[] { proficiencyData }).ToArray();
            }));
        }
    }

    IEnumerator GetAlignment(string[] alignmentNames)
    {
        foreach (string alignmentName in alignmentNames)
        {
            yield return StartCoroutine(dndClient.GetAlignment(alignmentName, (response) =>
            {
                var alignmentData = JsonUtility.FromJson<AlignmentDTO>(response);
                if (alignment == null)
                    alignment = new AlignmentDTO[] { alignmentData };
                else
                    alignment = alignment.Concat(new AlignmentDTO[] { alignmentData }).ToArray();
            }));
        }
    }

    IEnumerator GetDamageTypes(string[] damageTypeUrls)
    {
        foreach (string damageTypeUrl in damageTypeUrls)
        {
            yield return StartCoroutine(dndClient.GetDamageType(damageTypeUrl, (response) =>
            {
                var damageTypeData = JsonUtility.FromJson<Damage_Type>(response);
                if (damage == null)
                    damage = new Damage_Type[] { damageTypeData };
                else
                    damage = damage.Concat(new Damage_Type[] { damageTypeData }).ToArray();
            }));
        }
    }

    IEnumerator GetWeapons1(string[] weaponNames)
    {
        foreach (string weaponName in weaponNames)
        {
            yield return StartCoroutine(dndClient.GetWeapon1(weaponName, (response) =>
            {
                var weaponData = JsonUtility.FromJson<WeaponProperty>(response);
                SaveJsonToFile(weaponData, $"weapon1_{weaponName}.json");
                if (weaponProperties == null)
                    weaponProperties = new WeaponProperty[] { weaponData };
                else
                    weaponProperties = weaponProperties.Concat(new WeaponProperty[] { weaponData }).ToArray();
            }));
        }
    }

    IEnumerator GetWeapons2(string[] weaponNames)
    {
        foreach (string weaponName in weaponNames)
        {
            yield return StartCoroutine(dndClient.GetWeapon2(weaponName, (response) =>
            {
                var weaponData = JsonUtility.FromJson<WeaponDTO>(response);
                SaveJsonToFile(weaponData, $"weapon2_{weaponName}.json");
                if (weapons == null)
                    weapons = new WeaponDTO[] { weaponData };
                else
                    weapons = weapons.Concat(new WeaponDTO[] { weaponData }).ToArray();
            }));
        }
    }

    private void SaveJsonToFile(object data, string fileName)
    {
        string path = Application.dataPath + "/Part2/Scripts/ResultsJson/" + fileName;
        string formattedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, formattedJson);
    }
}
