using UnityEngine;

public class MagicBombScript : MonoBehaviour
{
    public Transform magicBombLocation;
    
   
    public AttackMech attackMechanics;
    
    public CombatScript combat;



    

    public float attackRange;

    public LayerMask EnemyLayers;

    
   

    //public GameObject _gameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MagicMechanics(combat.baseDamage);

    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

    }

    public void MagicMechanics(float damage)
    {
       attackMechanics.AttackMechanics(damage,magicBombLocation,attackRange,EnemyLayers);
        
        
        
    }

}
    
    


