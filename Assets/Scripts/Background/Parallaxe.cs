using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
    /*public GameObject[] backgrounds; // Les éléments de background*/
    public float[] speeds; // Les vitesses de déplacement des éléments de background
    public Material[] materials; // Les matériaux des éléments de background


    // Update est appelé une fois par frame
    void Update()
    {
        //foreach (Material m in materials )
        for (int i = 0; i < materials.Length; i++)
        {
            Vector2 offset = materials[i].GetTextureOffset("_MainTex");
            materials[i].SetTextureOffset("_MainTex", new Vector2(0,offset.y + speeds[i] * Time.deltaTime));
            // Déplacer le background


            // Repositionner le background lorsqu'il sort de l'écran

        }
    }
}