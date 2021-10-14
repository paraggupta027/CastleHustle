using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Player player = GetComponentInParent<Player>();
        
      
        IDamageable hit = other.GetComponentInParent<IDamageable>();
        if (hit != null) {
            Debug.Log("Damaged");
            hit.Damage(100);
           
        }
        Debug.Log("Hitted: " + other.name);
    }
}

