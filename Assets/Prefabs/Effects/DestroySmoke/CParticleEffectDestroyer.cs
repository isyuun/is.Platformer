using UnityEngine;
using System.Collections;

// 파티클 이펙트 파괴자
public class CParticleEffectDestroyer : CDestroyer
{

    protected ParticleSystem _particleSystem;   // 파티클 시스템

    protected override void Awake()
    {
        base.Awake();

        // 파티클 시스템 컴포넌트 참조
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Use this for initialization
    protected override void Start()
    {

        // 파티클 지연 시간
        _destroyDelayTime = _particleSystem.main.duration;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
