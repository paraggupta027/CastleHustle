using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy,IDamageable
{
  
    protected override void Attack()
    {
        
    }

    // Start is called before the first frame update
    protected override void  Start()
    {
        health = 10;
        moveSpeed = 1.0f;
        gems = 2;
        Health = health;
        base.Start();
    }
    protected override void Update()
    {

        base.Update();
        

    }
    // Update is called once per frame

}
