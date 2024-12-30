using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaaterCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tree"))
        {
            TreeFire treeFire = other.GetComponent<TreeFire>();
            if(treeFire != null)
            {
                treeFire.Extinguish();
            }
        }
    }
}
