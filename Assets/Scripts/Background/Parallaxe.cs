using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
    /*public GameObject[] backgrounds; // Les �l�ments de background*/
    public float[] speeds; // Les vitesses de d�placement des �l�ments de background
    public Material[] materials; // Les mat�riaux des �l�ments de background


    // Update est appel� une fois par frame
    void Update()
    {
        //foreach (Material m in materials )
        for (int i = 0; i < materials.Length; i++)
        {
            Vector2 offset = materials[i].GetTextureOffset("_MainTex");
            materials[i].SetTextureOffset("_MainTex", new Vector2(0,offset.y + speeds[i] * Time.deltaTime));
            // D�placer le background


            // Repositionner le background lorsqu'il sort de l'�cran

        }
    }
}