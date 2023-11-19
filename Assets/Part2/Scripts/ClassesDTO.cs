using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace API_DND
{
    [Serializable]
    public class ClassesDTO
    {
        public string index;
        public string name;
        public string url;
        public List<StartingEquipmentDTO> starting_equipment = new();
        public List<ProficiencyDTO> proficiencies = new();
    }

    [System.Serializable]
    public class RaceDTO
    {
        public string index;
        public string name;
        public string url;
        public List<AbilityBonusesDTO> ability_bonuses;
        public List<TraitDTO> traits;
    }

    [System.Serializable]
    public class AbilityScoreDTO
    {
        public string index;
        public string name;
        public string url;
        public string[] desc;
    }
    public class AbilityBonusesDTO
    {
        public int bonus;
        public AbilityScoreDTO ability_score;
    }

    [System.Serializable]
    public class TraitDTO
    {
        public string index;
        public string name;
        public string url;
        public string[] desc;
    }

    [System.Serializable]
    public class StartingEquipmentDTO
    {
        public int quantity;
        public EquipmentDTO equipment;
    }

    [System.Serializable]
    public class ProficiencyDTO
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class AlignmentDTO
    {
        public string index;
        public string name;
        public string url;
        public string desc;
    }

    [System.Serializable]
    public class EquipmentDTO
    {
        public string index;
        public string name;
        public string url;
        public string desc;
        public DamageRef damage;
        public RangeRef range;
        public float weight;
    }

    [System.Serializable]
    public class Damage_Type
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class ItemListRef
    {
        public string index;
        public string name;
        public string url;
    }

    [System.Serializable]
    public class RangeRef
    {
        public float normal;
        public float Long;
    }

    [System.Serializable]
    public class DamageRef
    {
        public string damage_dice;
        public ItemListRef damage_type;
    }

    [System.Serializable]
    public class WeaponDTO
    {
        public string index;
        public string name;
        public string url;
        public string weapon_category;
        public string weapon_range;
        public string category_range;
        public float weight;
        public ItemListRef equipment_category;
        public ItemListRef[] properties;
        public RangeRef range;
        public DamageRef damage;
    }

    [System.Serializable]
    public class WeaponProperty
    {
        public string index;
        public string name;
        public string url;
        public string[] desc;
    }
}
