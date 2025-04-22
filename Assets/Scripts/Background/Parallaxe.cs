using System.Collections;
using UnityEngine;



public class Parallaxe : MonoBehaviour
{
    /*public GameObject[] backgrounds; // Les �l�ments de background*/


    public float[] speeds; // Les vitesses de d�placement des �l�ments de background
    public Material[] materials; // Les mat�riaux des �l�ments de background
    public Texture2D[] textures1;
    public Texture2D[] textures2;

    public Texture2D[] textures3;

    public bool firstCoroutineOn;
    public bool secondCoroutineOn;
    public bool thirdCoroutineOn;

    private int currentIndex = 0;

    public Vector2[] tilingValues; // Valeurs de tiling différentes pour chaque sprite (ex : [1,1], [2,2], etc.)
    private int i = 0;
    // Update est appel� une fois par frame
    void Start()
    {
        StartCoroutine(ChangeTextureRoutineFirst());
        
    }
    
    void Update()
    {
        
        Vector2 offset = materials[i].GetTextureOffset("_MainTex");
        materials[i].SetTextureOffset("_MainTex", new Vector2(0,offset.y + speeds[i] * Time.deltaTime));
    }

    IEnumerator ChangeTextureRoutineFirst()
    {
        while (true)

        {
            firstCoroutineOn = true;
            secondCoroutineOn = false;
            thirdCoroutineOn = false ;
            Texture2D tex = textures1[currentIndex];
            
            // Changer la texture
            materials[i].mainTexture = tex;
            
            // Appliquer un nouveau tiling
            materials[i].SetTextureScale("_MainTex", tilingValues[i]);
            
            // Passer à l'index suivant
            currentIndex = (currentIndex + 1) % textures1.Length;
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ChangeTextureRoutineSecond()
    {
        while (true)
        {
            firstCoroutineOn = false;
            secondCoroutineOn = true;
            thirdCoroutineOn = false;
            Texture2D tex = textures2[currentIndex];
            
            // Changer la texture
            materials[i].mainTexture = tex;
            
            // Appliquer un nouveau tiling
            materials[i].SetTextureScale("_MainTex", tilingValues[i]);
            
            // Passer à l'index suivant
            currentIndex = (currentIndex + 1) % textures2.Length;
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ChangeTextureRoutineThird()
    {
        while (true)
        {
            firstCoroutineOn = false;
            secondCoroutineOn = false;
            thirdCoroutineOn = true;
            Texture2D tex = textures3[currentIndex];
            
            // Changer la texture
            materials[i].mainTexture = tex;
            
            // Appliquer un nouveau tiling
            materials[i].SetTextureScale("_MainTex", tilingValues[i]);
            
            // Passer à l'index suivant
            currentIndex = (currentIndex + 1) % textures3.Length;
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void CallSecondCoroutine()
    {
        if(firstCoroutineOn){
        StopCoroutine(ChangeTextureRoutineFirst());
        StartCoroutine(ChangeTextureRoutineSecond());
        }
    }

    public void CallThirdCoroutine()
    {
        if(secondCoroutineOn){
        StopCoroutine(ChangeTextureRoutineSecond());
        StartCoroutine(ChangeTextureRoutineThird());
        }
    }

}