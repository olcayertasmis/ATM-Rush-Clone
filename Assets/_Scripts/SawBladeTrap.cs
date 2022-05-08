using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawBladeTrap : MonoBehaviour
{
    public float angle = 50f;
    private bool moveleft = true;
    public float duration, waitTime;
    private void Update()
    {
        StartCoroutine(RotateSawBlade());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collected_Money" || other.gameObject.tag == "Collected_Gold" || other.gameObject.tag == "Collected_Diamond")
        {
            for (int i = 0; i < StackSystem.instance.moneys.Count - 1; i++)
            {
                if (StackSystem.instance.moneys[i] == other.gameObject)
                {
                    StackSystem.instance.DistributeCollectibles(other.gameObject, i, this.gameObject);
                    break;
                }
            }
        }
    }

    IEnumerator RotateSawBlade()
    {
        Vector3 rotateVector = new Vector3(0, 0, angle);
        if (moveleft)
        {

            transform.DORotate(-rotateVector, duration).OnComplete(() =>
            {
                moveleft = false;
            });
        }
        else
        {
            transform.DORotate(rotateVector, duration).OnComplete(() =>
            {
                moveleft = true;
            });
        }
        yield return new WaitForSeconds(waitTime);
    }
}
