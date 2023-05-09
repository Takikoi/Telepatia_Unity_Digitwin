using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlacementGrid : MonoBehaviour
{
    public GameObject moveObj;
    public Vector3 startPosition;
    public Vector3 newPosition;
    public float moveDuration;

    private void Update() {
        // if ()
    }

    public void OnMouseHover()
    {
        moveObj.transform.DOLocalMove(newPosition, moveDuration).SetEase(Ease.InOutExpo);
    }

    public void OnMouseExit()
    {
        moveObj.transform.DOLocalMove(startPosition, moveDuration).SetEase(Ease.InOutCubic);
    }
}
