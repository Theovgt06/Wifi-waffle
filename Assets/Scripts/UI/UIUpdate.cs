using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DataChanger dataChanger;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoAmountText;
    private int previousScore = -1;
    private int previousAmmoNumber = -1;
    public int previousHealthNumber = 7;
    public int actualScore = 0;
    public int bestScore;
    [SerializeField] public GameObject heartPrefab;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBarGrid;
    [SerializeField] private int maxScore;

    void Start()
    {
        dataChanger = player.GetComponent<DataChanger>();
        for(int i = 0; i<7;i++)
        {
            GameObject heart = Instantiate(heartPrefab, healthBarGrid.transform, false);
        }
        ammoAmountText.text = "x3";
    }

    // Update is called once per frame
    void Update()
    {
        if (actualScore != previousScore)
        {   
            previousScore = actualScore;
            scoreText.text = previousScore.ToString();
        }

        if (dataChanger.currentAmmo != previousAmmoNumber)
        {   
            previousAmmoNumber = dataChanger.currentAmmo;
            Debug.Log(ammoAmountText.text);
            ammoAmountText.text = "x" + previousAmmoNumber.ToString();
        }



        if (dataChanger.currentHealth != previousHealthNumber)
        {
            
            if(dataChanger.currentHealth > previousHealthNumber)
            {
                for (int i = previousHealthNumber; i < dataChanger.currentHealth && i < healthBarGrid.transform.childCount; i++)
                {
                    healthBarGrid.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else if(dataChanger.currentHealth < previousHealthNumber)
            {
                for (int i = dataChanger.currentHealth; i < previousHealthNumber && i < healthBarGrid.transform.childCount; i++)
                {
                    healthBarGrid.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            previousHealthNumber = dataChanger.currentHealth;
        }
    }

    public void ChangeScore(int amount)
    {
        if(actualScore+amount >= maxScore){
            actualScore = maxScore;
            return;
        }
        actualScore+= amount;
    }
    public void SetBestScore()
    {
        bestScore = actualScore;
    }
}
