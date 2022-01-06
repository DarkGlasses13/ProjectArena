using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines m_instance;
    private static Coroutines _instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject coroutiner = new GameObject("[COROUTINER]");
                m_instance = coroutiner.AddComponent<Coroutines>();
                DontDestroyOnLoad(coroutiner);
            }

            return m_instance;
        }
    }

    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return _instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(IEnumerator enumerator)
    {
        _instance.StopCoroutine(enumerator);
    }
}
