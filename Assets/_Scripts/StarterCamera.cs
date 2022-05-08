using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarterCamera : MonoBehaviour
{
    FollowCamera followCamera;

    private void Awake() 
    {
        followCamera = GetComponentInChildren<FollowCamera>();
    }

    public void Play()
    {
        transform.DORotate(new Vector3(0,-180,0) ,1.5f).Play().OnComplete(()=>
        {
            followCamera.enabled = true;
        });
    }
}
