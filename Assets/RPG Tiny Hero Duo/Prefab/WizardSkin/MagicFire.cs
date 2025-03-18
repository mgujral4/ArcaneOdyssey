using UnityEngine;

public class MagicStaff : MonoBehaviour
{
    public GameObject MagicBomb; // Prefab of the magic bomb
    public Transform wand;       // Reference to the wand transform
    public float speed = 500f;   // Speed of the magic bomb
    public float evaporateTime = 3f; // Time before the magic bomb disappears
    //public PlayerHealthManager playerHealth;
    
    public float bonusHealth = 50f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootMagicBomb();
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
          //  if (playerHealth.currentHealth <= 200)
          //  {
          //      playerHealth.currentHealth += bonusHealth;
          //  }

           // playerHealth.HealthBar.SetHealth(playerHealth.currentHealth);
            
        }



    }

    void ShootMagicBomb()
    {
        // Instantiate the magic bomb at the wand's position and rotation
        GameObject magic = Instantiate(MagicBomb, wand.position, wand.rotation);

        // Get the Rigidbody component from the instantiated object
        Rigidbody rb = magic.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(wand.forward * speed, ForceMode.Impulse); // Apply force in the wand's forward direction
        }

        // Destroy the magic bomb after a set time
        Destroy(magic, evaporateTime);
    }
}