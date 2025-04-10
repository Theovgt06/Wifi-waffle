using System.Collections;
using UnityEngine;


public class Parallaxe : MonoBehaviour
{
    /*public GameObject[] backgrounds; // Les �l�ments de background*/
    public float[] speeds; // Les vitesses de d�placement des �l�ments de background
    public Material[] materials; // Les mat�riaux des �l�ments de background
    public Texture2D[] textures;
    private int currentIndex = 0;

    public Vector2[] tilingValues; // Valeurs de tiling différentes pour chaque sprite (ex : [1,1], [2,2], etc.)

    // Update est appel� une fois par frame
    void Start()
    {
        StartCoroutine(ChangeTextureRoutine());
    }
    
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

    IEnumerator ChangeTextureRoutine()
    {
        while (true)
        {
            Texture2D tex = textures[currentIndex];
            
            // Changer la texture
            materials[0].mainTexture = tex;
            
            // Appliquer un nouveau tiling
            materials[0].SetTextureScale("_MainTex", tilingValues[0]);
            
            // Passer à l'index suivant
            currentIndex = (currentIndex + 1) % textures.Length;
            
            yield return new WaitForSeconds(0.5f);
        }
    }


}