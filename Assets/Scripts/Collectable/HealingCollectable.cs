using UnityEngine;

<<<<<<<< HEAD:Assets/Scripts/Collectable/CollectableHeal.cs
public class CollectableHeal : MonoBehaviour
========
public class HealingCollectable : MonoBehaviour
>>>>>>>> 9774dc9333b90580992b2e73539a21770e82a7fb:Assets/Scripts/Collectable/HealingCollectable.cs
{
    public int playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth += 1;
        }

    }
}
