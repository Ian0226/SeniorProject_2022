using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHintController : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;

    //1 is origin, 2 is hintMaterial
    [SerializeField]
    private Material[] materials;

    [SerializeField]
    private Material hintMaterial;

    private void Start()
    {
        meshRenderer = this.transform.GetChild(0).GetComponent<MeshRenderer>();
        materials = meshRenderer.materials;
    }

    public bool IsOnHint()
    {
        if(materials[1] == hintMaterial)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowHint()
    {
        materials[1] = hintMaterial;
        meshRenderer.materials = materials;
        Debug.Log("Material changed");
    }
    public void CloseHint()
    {
        materials[1] = null;
        meshRenderer.materials = materials;
    }
}
