using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPresentSystem : MonoBehaviour
{
    /// <summary>
    /// This is use to place the present object system's UI object parent.
    /// </summary>
    private GameObject _presentPanelUIObject;

    /// <summary>
    /// Some UI object.
    /// </summary>
    private Image _presentItemImage;
    private TextMeshProUGUI _itemDescriptText;
    private Button exitBtn;

    /// <summary>
    /// Presented object put on this position,make the present camera see it.
    /// </summary>
    private Vector3 presentObjectWorldPos;
    public static void StartItemPresent(GameObject presentedGameObject,string itemDescript)
    {

    }
}
