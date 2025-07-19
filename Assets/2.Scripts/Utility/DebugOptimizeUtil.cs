using UnityEngine;

public static class DebugOptimizeUtil
{
    public static void Log(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }
}