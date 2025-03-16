using UnityEngine;

public class GenCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5F;
    public LayerMask EnemyLayers;
    public float HeavyAttackRange = 1F;
    public float baseDamage = 10F;
    public float HeavyDamage = 40F;
    public float HeavyAttackRate = 0.1F;
    private float nextAttackTime = 0F;

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { Attack();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.time >= nextAttackTime)
            {
                HeavyAttack();
                nextAttackTime = Time.time + 1f / HeavyAttackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        
        AttackMechanics(baseDamage);
        
        
    }

    void HeavyAttack()
    {
        animator.SetTrigger("HeavyAttack");
        AttackMechanics(HeavyDamage);
    }

    public void MapControls()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { Attack();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.time >= nextAttackTime)
            {
                HeavyAttack();
                nextAttackTime = Time.time + 1f / HeavyAttackRate;
            }
        }
        
        
    }


    public void AttackMechanics(float damage)
    {
        Collider[] hitEnemies =Physics.OverlapSphere(attackPoint.position,attackRange,EnemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<HealthManager>().TakeDamage(damage);
            
        }
        
    }
}