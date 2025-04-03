using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Plateform", menuName = "Scriptable Objects/Plateform")]
public class PlateformByBiome : ScriptableObject
{
    public string plateformBiome;
    public List<PlateformByColor> plateformByBiomes = new List<PlateformByColor>();

    [Serializable] // Très important pour la sérialisation
    public class PlateformByColor
    {
        public string plateformeColor;
        public List<GameObject> plateformeByType = new List<GameObject>();

    }

    
}

