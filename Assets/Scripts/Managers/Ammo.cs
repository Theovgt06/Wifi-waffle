using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int maxAmmo = 7;
    [SerializeField]private int currentAmmo;
    [SerializeField]private int startingAmmo = 5;
    public void AddAmmo(int ammoToAdd)
    {
        
        if (currentAmmo + ammoToAdd > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else
        {
            currentAmmo += ammoToAdd;
        }
    }
    // Corrected method declaration
    public void useAmmo()
    {
        currentAmmo--;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
