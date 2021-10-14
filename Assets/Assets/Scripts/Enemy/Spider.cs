using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    
  
    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
        health = Random.Range(50,75);
        moveSpeed = 3.0f;
        gems = Random.Range(5,10);
        
    }
    protected override void Attack() { }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
    }
  
}
