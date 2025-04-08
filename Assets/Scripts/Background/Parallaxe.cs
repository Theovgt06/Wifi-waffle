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
            materials[i].SetTextureOffset("_MainTex", new Vector2(transform.position.y * speeds[i], 0));
            // D�placer le background


            // Repositionner le background lorsqu'il sort de l'�cran

        }
    }
}