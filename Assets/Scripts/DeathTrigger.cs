using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public Vector3 resetPoint;

    void Update()
    {
        Debug.Log("You Win!!!!");
        
    }

    //    private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.tag == "Player")
    //    //{
    //     Debug.Log("You Win!!!!");
    //    base.dead = true;

       
    //    base.Death();
    //    // transform.position = resetPoint;

     
      //  }        
    //}


}
