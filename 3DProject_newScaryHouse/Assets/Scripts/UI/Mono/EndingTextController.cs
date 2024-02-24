using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingTextController : MonoBehaviour
{
    private void Start()
    {
        RectTransform upPoint = MainGame.Instance.GetSceneAniEffectControlSystemUpPoint();
        transform.DOMove(upPoint.position,7f).OnComplete(() => Destroy(this.gameObject));
    }
}
