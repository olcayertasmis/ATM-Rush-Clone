using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Card : MonoBehaviour
{

    private bool moveleft = true;
    public float duration, waitTime;
    private void Update()
    {
        StartCoroutine(MoveCard());
    }
    IEnumerator MoveCard()
    {
        Vector3 moveVectortoRight = new Vector3(4.5f, 0.7721382f, 0);
        Vector3 moveVectortoLeft = new Vector3(-4.5f, 0.7721382f, 0);
        if (moveleft)
        {
            transform.DOLocalMove(moveVectortoLeft, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                moveleft = false;
            });
        }
        else
        {
            transform.DOLocalMove(moveVectortoRight, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                moveleft = true;
            });
        }
        yield return new WaitForSeconds(waitTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collected_Money" || other.gameObject.tag == "Collected_Gold" || other.gameObject.tag == "Collected_Diamond")
        {
            for (int i = 1; i <= StackSystem.instance.moneys.Count; i++)
            {
                if (StackSystem.instance.moneys[i] == other.gameObject)
                {
                    StackSystem.instance.DistributeCollectibles(other.gameObject, i, this.gameObject);
                    break;
                }
            }
        }
    }
}

