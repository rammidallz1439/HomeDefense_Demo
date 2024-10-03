using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHandler : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag is "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
