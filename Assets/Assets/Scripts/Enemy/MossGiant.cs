using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
 
    protected override void Start()
    {

        health = 200;
        moveSpeed = 1.2f;
        gems = 10;
        base.Start();
          
    }
    protected override void Attack()
    {
      
    }
    protected override void Update()
    {

        base.Update();
       

    }
    
   


}
