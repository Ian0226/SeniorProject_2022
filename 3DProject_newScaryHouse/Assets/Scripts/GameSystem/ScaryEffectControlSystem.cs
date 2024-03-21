using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.CustomTool;

public class ScaryEffectControlSystem : IGameSystem
{
    //���ƭ���
    private AudioSource[] effectAudios;

    //��������(�ĥ|������)
    private GameObject[] enemyObjs;

    private Volume globalVolume;

    //Post Processing�w��
    private Vignette vignette;

    //�O���ĪG����
    private List<GameObject> lightsObjs = new List<GameObject>();
    public ScaryEffectControlSystem(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        if (GameObject.FindGameObjectWithTag("EffectAudio"))
        {
            GameObject[] audioContainers = GameObject.FindGameObjectsWithTag("EffectAudio");
            effectAudios = new AudioSource[audioContainers.Length];
            for(int i = 0; i < effectAudios.Length; i++)
            {
                effectAudios[i] = audioContainers[i].GetComponent<AudioSource>();
                //Debug.Log(effectAudios[i].gameObject.name);
            }
        }
        if (UnityTool.FindGameObject("EnemyGroup"))
        {
            GameObject enemyGroupObj = UnityTool.FindGameObject("EnemyGroup");
            enemyObjs = new GameObject[enemyGroupObj.transform.childCount];
            for (int i = 0; i < enemyGroupObj.transform.childCount; i++)
            {
                enemyObjs[i] = enemyGroupObj.transform.GetChild(i).gameObject;
            }
        }
        if (UnityTool.FindGameObject("GlobalVolume"))
        {
            globalVolume = UnityTool.FindGameObject("GlobalVolume").GetComponent<Volume>();
            globalVolume.profile.TryGet<Vignette>(out vignette);
        }
        if (UnityTool.FindGameObject("ScaryLightObjGroup"))
        {
            lightsObjs.Add(UnityTool.FindGameObject("SpotLight_LivingroomLamp_1"));
            lightsObjs.Add(UnityTool.FindGameObject("Bulb_LivingroomLamp_1"));

            foreach (GameObject obj in lightsObjs)
                obj.SetActive(false);
        }
    }

    public GameObject FindLightObjByName(string obj)
    {
        GameObject lightObj = null;
        for(int i = 0; i < lightsObjs.Count; i++)
        {
            if (lightsObjs[i].name == obj)
                lightObj = lightsObjs[i];
        }
        return lightObj;
    }
    public AudioSource GetEffectAudioSourceByInt(int num)
    {
        return effectAudios[num];
    }
    public AudioSource GetEffectAudioSourceByName(string name)
    {
        AudioSource audio = null;
        for(int i = 0; i < effectAudios.Length; i++)
        {
            if(effectAudios[i].gameObject.name == name)
            {
                audio = effectAudios[i];
            }
        }
        return audio;
    }

    public void EnemyLookTarget(Transform target)
    {
        foreach(GameObject obj in enemyObjs)
        {
            Debug.Log(obj);
            obj.transform.Find("Head1").DOLookAt(target.transform.position, 1);
        }
    }

    //�w��ĪG
    public void SetVignetteEffect()
    {
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.7f, 0.5f);
        MethodDelayExecuteTool.ExecuteDelayedMethod(25f, () => DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0, 0.5f));
    }

    //�O���}���ĪG
    public void LightOpenClose(GameObject bulbObj,GameObject lightObj,bool isOpen)
    {
        if(bulbObj != null)
            bulbObj.SetActive(isOpen);
        if(lightObj != null)
            lightObj.SetActive(isOpen);
    }
}
