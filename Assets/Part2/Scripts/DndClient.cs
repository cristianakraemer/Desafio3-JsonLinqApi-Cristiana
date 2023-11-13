using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using API_DND;
using System.Diagnostics;

public class DndClient : ApiClient
{
    public DndClient(string baseUrl = "https://www.dnd5eapi.co") : base(baseUrl) { }

    public IEnumerator GetClasses(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/classes/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetRaces(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/races/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetAbilityScore(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/ability-scores/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetTraits(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/traits/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetEquipment(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/equipment/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetProficiencies(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/proficiencies/{index}");
        return Dispatch(req, callback);
    }

    public IEnumerator GetAlignment(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/alignments/{index}");
        return Dispatch(req, callback);
    }

     public IEnumerator GetWeapon(string index, Action<string> callback)
     {
         var req = UnityWebRequest.Get($"{BaseUrl}/api/weapon-properties{index}");
         return Dispatch(req, callback);
     }

    public IEnumerator GetDamageType(string index, Action<string> callback)
    {
        var req = UnityWebRequest.Get($"{BaseUrl}/api/damage-types/{index}");
        return Dispatch(req, callback);
    }
}
