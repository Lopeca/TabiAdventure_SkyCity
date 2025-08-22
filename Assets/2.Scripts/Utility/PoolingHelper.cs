using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public static class PoolingHelper
{
    // 프리팹을 키로 삼아 오브젝트 풀을 일괄 관리합니다
    static readonly Dictionary<string, ObjectPool<GameObject>> prefabToPool = new Dictionary<string, ObjectPool<GameObject>>();

    private static GameObject poolContainer;
    private static readonly Dictionary<string, GameObject> containerChild = new Dictionary<string, GameObject>();

    public static bool HasRegisteredPooling(this GameObject obj)
    {
        string prefabName = obj.GetPrefabName();
        return prefabToPool.ContainsKey(prefabName);
    }

    public static GameObject PoolingGet(this GameObject prefab)
    {
        if (!prefabToPool.ContainsKey(prefab.name))
        {
            // 풀 컨테이너 오브젝트가 null, 씬이 바뀐 경우 딕셔너리 내 원소들이 파괴되어 있지만 접근 가능할 수 있기에 청소를 한번 해줍니다
            if (!poolContainer)
            {
                poolContainer = new GameObject("PoolContainer");
                prefabToPool.Clear();
                containerChild.Clear();
            }

            if (!containerChild.ContainsKey(prefab.name))
            {
                GameObject childContainer = new GameObject(prefab.name);
                childContainer.transform.SetParent(poolContainer.transform);
                containerChild.Add(prefab.name, childContainer);
            }

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    GameObject go = Object.Instantiate(prefab, containerChild[prefab.name].transform, true);
                    return go;
                },
                actionOnGet: (go) => go.SetActive(true),
                actionOnRelease: (go) => go.SetActive(false),
                actionOnDestroy: Object.Destroy,
                collectionCheck: false,
                defaultCapacity: 1
            );
            
            prefabToPool.Add(prefab.name, pool);
        }
        
        GameObject go = prefabToPool[prefab.name].Get();
        return go;
    }

    public static void PoolingRelease(this GameObject go)
    {
        string prefabName = go.GetPrefabName();
        prefabToPool[prefabName].Release(go);
    }
    
    public static string GetPrefabName(this GameObject go)
    {
        if (!go.name.Contains("(")) return go.name;
        return go.name.Substring(0, go.name.IndexOf("(", StringComparison.Ordinal));
    }
}
