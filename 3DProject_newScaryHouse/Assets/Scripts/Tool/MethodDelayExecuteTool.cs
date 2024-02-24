using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodDelayExecuteTool : MonoBehaviour
{
    public delegate void DelayedMethod();

    // ����@�ө����k
    public static void ExecuteDelayedMethod(float delay, DelayedMethod method)
    {
        Instance.StartCoroutine(DelayedMethodCoroutine(delay, method));
    }

    // ������
    private static MethodDelayExecuteTool instance;
    private static MethodDelayExecuteTool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("MethodDelay").AddComponent<MethodDelayExecuteTool>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    // �w�q�@�Ө�{�Ӱ��橵���k
    private static IEnumerator DelayedMethodCoroutine(float delay, DelayedMethod method)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
