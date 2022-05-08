using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lens : MonoBehaviour
{
    public Mesh[] meshes;
    public Material[] Materials;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collected_Money")
        {
            other.gameObject.tag = "Collected_Gold";
            other.gameObject.GetComponent<MeshFilter>().mesh = meshes[0];
            //other.gameObject.GetComponent<MeshRenderer>().material = Materials[0];
            StackSystem.instance.moneyValue += 1;
            StackSystem.instance.moneyValueText.text = StackSystem.instance.moneyValue.ToString();
            // other.gameObject.transform.DOScale(new Vector3(2, 3, 3), 0.1f);
        }
        else if (other.gameObject.tag == "Collected_Gold")
        {
            other.gameObject.GetComponent<MeshFilter>().mesh = meshes[1];
            //other.gameObject.GetComponent<MeshRenderer>().material = Materials[0];
            other.gameObject.tag = "Collected_Diamond";
            StackSystem.instance.moneyValue += 2;
            StackSystem.instance.moneyValueText.text = StackSystem.instance.moneyValue.ToString();
            // other.gameObject.transform.DOScale(new Vector3(2, 2, 2), 0.1f);
        }
    }
}
