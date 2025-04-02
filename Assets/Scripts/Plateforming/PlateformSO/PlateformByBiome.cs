using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlateformByBiome", menuName = "Scriptable Objects/PlateformByBiome")]
public class PlateformByBiome : ScriptableObject
{

    public string plateformBiome;
    public List<PlateformByColor> plateformByBiomes = new List<PlateformByColor>();
}
