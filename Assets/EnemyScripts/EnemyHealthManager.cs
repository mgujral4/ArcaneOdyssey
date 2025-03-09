using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     float maxHealth = 100;
    public float currentHealth;
    public Animator animator;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
