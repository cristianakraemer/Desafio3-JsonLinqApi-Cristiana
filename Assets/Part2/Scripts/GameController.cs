using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using API_DND;
using System.IO;
using System;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    protected DndClient client;

    [SerializeField]
    protected TMP_Dropdown dropdownClasses;

    [SerializeField]
    protected TMP_Dropdown dropdownRaces;

    [SerializeField]
    protected TMP_Dropdown dropdownAbilityScore;

    [SerializeField]
    protected TMP_Dropdown dropdownTraits;

    [SerializeField]
    protected TMP_Dropdown dropdownEquipment;

    [SerializeField]
    protected TMP_Dropdown dropdownProficiencies;

    [SerializeField]
    protected TMP_Dropdown dropdownAlignment;

    [SerializeField]
    protected TMP_Dropdown dropdownWeapon;

    public ClassesDTO[] classes;
    public RaceDTO[] races;
    public AbilityScoreDTO[] abilityScore;
    public TraitDTO[] traits; 
    public EquipmentDTO[] equipment;  
    public ProficiencyDTO[] proficiencies; 
    public AlignmentDTO[] alignment;
    public WeaponProperty[] weapon;

    private void Awake()
    {
        InitializeRefs();

        client = new DndClient();

        // Serve para aparecer os resultados no Console.
        StartCoroutine(client.GetClasses("", GenericApiResponse));
        StartCoroutine(client.GetRaces("", GenericApiResponse));
        StartCoroutine(client.GetAbilityScore("", GenericApiResponse));
        StartCoroutine(client.GetTraits("", GenericApiResponse));
        StartCoroutine(client.GetEquipment("", GenericApiResponse));
        StartCoroutine(client.GetProficiencies("", GenericApiResponse));
        StartCoroutine(client.GetAlignment("", GenericApiResponse));
        StartCoroutine(client.GetWeapon("", GenericApiResponse));

        // Serve para aparecer os resultados no TMP_Dropdown.
        LoadClasses();
        LoadRaces();
        LoadAbilityScores();
        LoadTraits();
        LoadEquipment();
        LoadProficiencies();
        LoadAlignment();
        LoadWeapon();
    }

    protected void InitializeRefs()
    {
        // Desativa as opções A, B e C após carregar os dados da API
        dropdownClasses.ClearOptions();
        dropdownRaces.ClearOptions();
        dropdownAbilityScore.ClearOptions();
        dropdownTraits.ClearOptions();
        dropdownEquipment.ClearOptions();
        dropdownProficiencies.ClearOptions();
        dropdownAlignment.ClearOptions();
        dropdownWeapon.ClearOptions();
    }

    protected void LoadClasses() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetClasses("", (data) => {
            var list = GenericParseData<ResourceListDTO<ClassesDTO>>(data);
            classes = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownClasses.AddOptions(options);
        }));
    }
   
     protected void LoadRaces() // Para aparecer os resultados no TMP_Dropdown
    {
         StartCoroutine(client.GetRaces("", (data) => {
             var list = GenericParseData<ResourceListDTO<RaceDTO>>(data);
             races = list.results;
             List<TMP_Dropdown.OptionData> options = new();
             options.AddRange(
                 list.results
                 .Select(r => new TMP_Dropdown.OptionData(r.name))
                 .ToList()
             );
             dropdownRaces.AddOptions(options);
         }));
     }

    protected void LoadAbilityScores() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetAbilityScore("", (data) => {
            var list = GenericParseData<ResourceListDTO<AbilityScoreDTO>>(data);
            abilityScore = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownAbilityScore.AddOptions(options);
        }));
    }

    protected void LoadTraits() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetTraits("", (data) => {
            var list = GenericParseData<ResourceListDTO<TraitDTO>>(data);
            traits = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownTraits.AddOptions(options);
        }));       
    }

    protected void LoadEquipment() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetEquipment("", (data) => {
            var list = GenericParseData<ResourceListDTO<EquipmentDTO>>(data);
            equipment = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownEquipment.AddOptions(options);
        }));
    }

    protected void LoadProficiencies() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetProficiencies("", (data) => {
            var list = GenericParseData<ResourceListDTO<ProficiencyDTO>>(data);
            proficiencies = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownProficiencies.AddOptions(options);
        }));
    }

    protected void LoadAlignment() // Para aparecer os resultados no TMP_Dropdown
    {
        StartCoroutine(client.GetAlignment("", (data) => {
            var list = GenericParseData<ResourceListDTO<AlignmentDTO>>(data);
            alignment = list.results;
            List<TMP_Dropdown.OptionData> options = new();
            options.AddRange(
                list.results
                .Select(r => new TMP_Dropdown.OptionData(r.name))
                .ToList()
            );
            dropdownAlignment.AddOptions(options);
        }));
    }

     protected void LoadWeapon() // Para aparecer os resultados no TMP_Dropdown
    {
         StartCoroutine(client.GetWeapon("", (data) => {
             var list = GenericParseData<ResourceListDTO<WeaponProperty>>(data);
             weapon = list.results;
             List<TMP_Dropdown.OptionData> options = new();
             options.AddRange(
             list.results
                 .Select(r => new TMP_Dropdown.OptionData($"{r.name}"))
                 .ToList()
             );
             dropdownWeapon.AddOptions(options);
         }));
     }

    protected void GenericApiResponse(string data)
    {
        Debug.Log(data);
    }

    protected T GenericParseData<T>(string data)
    {
        return JsonUtility.FromJson<T>(data);
    }
}
