using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterDestroyer : CDestroyer
{
    // 파괴 이펙트
    public GameObject _destroyEffectPrefab;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    public override void Destroy()
    {
        Debug.Log(this.GetMethodName() + ":" + _destroyEffectPrefab);
        if (_destroyEffectPrefab != null)
        {
            // 오브젝트 파괴 이펙트 생성
            Instantiate(_destroyEffectPrefab,
                transform.position, Quaternion.identity);
        }

        base.Destroy();

    }
}
