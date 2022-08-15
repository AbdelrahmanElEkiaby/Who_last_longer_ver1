using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_script : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        
         if(other.gameObject.tag == "fan")
         {
             Destroy(gameObject);
         }
    }
}
