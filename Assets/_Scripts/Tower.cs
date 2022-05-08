using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    public int sayac;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StackSystem.instance.newMoneyValue *= sayac;
            transform.DOLocalMoveZ(98.6f, 0.25f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StackSystem.instance.newMoneyValue /= sayac;
            transform.DOLocalMoveZ(100.6f, 0.25f);
        }
    }
}
