using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Atm : MonoBehaviour
{
    public List<GameObject> atmMoney = new List<GameObject>();
    private int count = 0;
    public TextMeshProUGUI textMeshProUGUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collected_Money")
        {
            atmMoney.Add(other.gameObject);
            StackSystem.instance.moneys.Remove(other.gameObject);
            Destroy(other.gameObject);
            count++;
        }
        else if (other.gameObject.tag == "Collected_Gold")
        {
            atmMoney.Add(other.gameObject);
            count += 2;
            StackSystem.instance.moneys.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Collected_Diamond")
        {
            atmMoney.Add(other.gameObject);
            count += 3;
            StackSystem.instance.moneys.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        textMeshProUGUI.text = count.ToString();
    }


}
