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
    [SerializeField] public GameObject heartPrefab;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject ammoNumber;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBarGrid;

    void Start()
    {
        dataChanger = player.GetComponent<DataChanger>();
        scoreText = score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        ammoAmountText = ammoNumber.GetComponent<TextMeshProUGUI>();
        for(int i = 0; i<7;i++)
        {
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.SetParent(healthBarGrid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dataChanger.actualScore != previousScore)
        {   
            previousScore = dataChanger.actualScore;
            scoreText.text = previousScore.ToString();
        }

        if (dataChanger.currentAmmo != previousAmmoNumber)
        {   
            previousAmmoNumber = dataChanger.currentAmmo;
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
}
