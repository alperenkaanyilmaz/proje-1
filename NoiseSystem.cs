
using UnityEngine;
using System.Collections.Generic;


public class NoiseSystem : MonoBehaviour
{
    public static NoiseSystem Instance;
    List<NoiseEvent> events = new List<NoiseEvent>();

    void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);
    }

    void Update()
    {
        
        for (int i = events.Count - 1; i >= 0; i--)
        {
            events[i].timeLeft -= Time.deltaTime;
            if (events[i].timeLeft <= 0f) events.RemoveAt(i);
        }
    }

    public void MakeNoise(Vector3 pos, float volume, float duration = 2f)
    {
        events.Add(new NoiseEvent { position = pos, volume = volume, timeLeft = duration });
    }

    public bool IsNoiseNear(Vector3 pos, float range)
    {
        foreach (var e in events)
        {
            if (Vector3.Distance(pos, e.position) <= range + e.volume) return true;
        }
        return false;
    }

    public Vector3 GetLoudestNoisePosition(Vector3 pos, float searchRange)
    {
        NoiseEvent best = null;
        float bestScore = -1f;
        foreach (var e in events)
        {
            float d = Vector3.Distance(pos, e.position);
            if (d <= searchRange + e.volume)
            {
                float score = e.volume - d;
                if (score > bestScore)
                {
                    bestScore = score;
                    best = e;
                }
            }
        }
        return best != null ? best.position : pos;
    }

    class NoiseEvent
    {
        public Vector3 position;
        public float volume;
        public float timeLeft;
    }
}
