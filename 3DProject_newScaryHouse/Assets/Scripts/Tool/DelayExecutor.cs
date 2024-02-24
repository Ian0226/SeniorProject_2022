using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DelayExecutor
{
    private Timer timer;
    private Action method;

    public DelayExecutor(float delay, Action method)
    {
        this.method = method;
        timer = new Timer(ExecuteMethod, null, (int)(delay * 1000), Timeout.Infinite);
    }

    private void ExecuteMethod(object state)
    {
        method();
    }
}
