using System;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance { get; private set; }

    [SerializeField] ParticleExperience particleSystemPrefab;
    public ObjectPool<ParticleExperience> particleExperiencePool { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (particleSystemPrefab == null)
        {
            Debug.LogError($"{particleSystemPrefab.name} is not assigned!");
        }

        particleExperiencePool = new ObjectPool<ParticleExperience>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 50, 100);
    }

    private ParticleExperience CreatePooledItem()
    {
        var particleExp = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        return particleExp;
    }

    private void OnTakeFromPool(ParticleExperience system)
    {
        system.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(ParticleExperience system)
    {
        system.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(ParticleExperience system)
    {
        Destroy(system.gameObject);
    }
}
