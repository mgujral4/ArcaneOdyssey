using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float maxHealth = 100;
    public float currentHealth;
    Animator animator;
    //public healthBarScript HealthBar;
   
    
    
    
    void Start()
    {
        //HealthBar = FindObjectOfType<healthBarScript>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        //HealthBar.setMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //HealthBar.SetHealth(currentHealth);
        animator.SetTrigger("tanking");

        if (currentHealth <= 0)
        {
           
            Die();
        }
    }

    


    void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log("Enemy died!");
        //HealthBar.SetHealth(0);
        
        
        Destroy(gameObject,3F);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}