using UnityEngine;

public class AttackMech : MonoBehaviour
{
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    
    public void AttackMechanics(float damage, Transform attackPoint, float attackRange, LayerMask EnemyLayers)
    {
        Collider[] hitEnemies =Physics.OverlapSphere(attackPoint.position,attackRange,EnemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<HealthManager>().TakeDamage(damage);
            
        }
        
    }
}
