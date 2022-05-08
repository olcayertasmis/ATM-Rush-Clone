using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Money")
        {
            if (!StackSystem.instance.moneys.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Collected_Money";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                StackSystem.instance.StackMoney(other.gameObject, StackSystem.instance.moneys.Count - 1);
                StackSystem.instance.moneyValue++;
                StackSystem.instance.moneyValueText.text = StackSystem.instance.moneyValue.ToString();
            }
        }
        else if (other.gameObject.tag == "Gold")
        {
            if (!StackSystem.instance.moneys.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Collected_Gold";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                StackSystem.instance.StackMoney(other.gameObject, StackSystem.instance.moneys.Count - 1);
                StackSystem.instance.moneyValue += 2;
                StackSystem.instance.moneyValueText.text = StackSystem.instance.moneyValue.ToString();
            }
        }
        else if (other.gameObject.tag == "Diamond")
        {
            if (!StackSystem.instance.moneys.Contains(other.gameObject))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.tag = "Collected_Diamond";
                other.gameObject.AddComponent<Collision>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                StackSystem.instance.StackMoney(other.gameObject, StackSystem.instance.moneys.Count - 1);
                StackSystem.instance.moneyValue += 3;
                StackSystem.instance.moneyValueText.text = StackSystem.instance.moneyValue.ToString();
            }
        }
    }
}
