using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    protected static T Instance
    {
        get
        {
            if(instance) return instance;
            
            DebugOptimizeUtil.Log("Instance is null. : " + typeof(T));
            return null;
        }
    }
    
    protected virtual void Awake()
    {
        if(!instance)
            instance = this as T;
        else
            Destroy(gameObject);
    }
}
