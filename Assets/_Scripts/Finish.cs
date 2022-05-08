using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Finish : MonoBehaviour
{
    public GameObject finishTarget;
    public float targetToMoveDelay;

    private void OnTriggerEnter(Collider other)
    {
        //print("ontrigger");
        if (other.gameObject.tag == "Collected_Money" || other.gameObject.tag == "Collected_Gold" || other.gameObject.tag == "Collected_Diamond")
        {
            //print("triggerfinish");
            FinishCollecterMoney(other.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            //print("gamestate");
            StackSystem.instance.gameState = StackSystem.GameState.finished;
            StackSystem.instance.newMoneyValue = StackSystem.instance.moneyValue;
        }
    }

    public void FinishCollecterMoney(GameObject gameObject)
    {
        StackSystem.instance.moneys.Remove(gameObject);
        //print("finishcollecter");
        gameObject.tag = "Untagged";
        gameObject.transform.parent = null;
        //Destroy(gameObject.GetComponent<BoxCollider>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<Collision>());
        gameObject.transform.DOMoveZ(finishTarget.transform.position.z, targetToMoveDelay);
        gameObject.transform.DOMoveX(finishTarget.transform.position.x, targetToMoveDelay * 1.25f);
    }
}
