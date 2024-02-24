using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MouseClickEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;

    public Sprite changesImage;
    public bool buttonstate;
    private Sprite originImage;
    [SerializeField]
    private RectTransform StartButtonImage;

    [SerializeField]
    private Material materialOrigin;
    [SerializeField]
    private Material materialOnEnter;

    private void Start()
    {
        originImage = GetComponent<Image>().sprite;
        image = GetComponent<Image>();
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //originImage = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = changesImage;
        if (buttonstate) 
        {
            //Tween myTween = StartButtonImage.DOScale(new Vector2(1.2f, 1.2f), 0.5f);
            image.material = materialOnEnter;
        }
    }
    
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = originImage;
        if (buttonstate)
        {
            //Tween myTween = StartButtonImage.DOScale(new Vector2(1f, 1f), 0.5f);
            image.material = materialOrigin;
        }
    }

    public void ResetImage()
    {
        GetComponent<Image>().sprite = originImage;
    }
}
