using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodDelayExecuteTool : MonoBehaviour
{
    public delegate void DelayedMethod();

    // 執行一個延遲方法
    public static void ExecuteDelayedMethod(float delay, DelayedMethod method)
    {
        Instance.StartCoroutine(DelayedMethodCoroutine(delay, method));
    }

    // 獲取單例
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

    // 定義一個協程來執行延遲方法
    private static IEnumerator DelayedMethodCoroutine(float delay, DelayedMethod method)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
