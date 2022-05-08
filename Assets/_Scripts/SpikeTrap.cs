using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collected_Money" || other.gameObject.tag == "Collected_Gold" || other.gameObject.tag == "Collected_Diamond")
        {
            StackSystem.instance.DestroyMoney(other.gameObject, this.gameObject);
        }
    }
}
