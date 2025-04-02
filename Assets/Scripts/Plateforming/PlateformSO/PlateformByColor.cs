using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlateformByColor", menuName = "Scriptable Objects/Plateform")]
public class PlateformByColor : ScriptableObject
{
    public string PlateformeColor;
    public List<GameObject> plateformeByType = new List<GameObject>();
}
