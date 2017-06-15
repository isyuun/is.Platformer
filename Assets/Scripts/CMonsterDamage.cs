using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterDamage : CDamage
{

    public Transform _damageEffectPos;                  // 타격 이펙트 효과 위치
    public GameObject _damageEffectPrefab;              // 타격 이펙트 프리팹
    protected CCharacterState _monsterState;            // 몬스터 상태
    Animator _animator;                                 // 애니메이터

    CDestroyer _destroyer;                              // 파괴자

    void Awake()
    {
        _monsterState = GetComponent<CCharacterState>();
        _animator = GetComponent<Animator>();
        _destroyer = GetComponent<CDestroyer>();

    }

    // 피격 처리
    public void Damage(float damage)
    {
        //Debug.Log(this.GetMethodName() + ":" + damage);

        // 몬스터의 체력을 감소함
        _monsterState._hp -= damage;

        if (_monsterState._hp <= 0)
        {
            Debug.LogWarning(this.GetMethodName() + ":" + damage);
            if (_destroyer != null && _destroyer.enabled)
            {
                _destroyer.Destroy(); // 몬스터를 파괴해라
            }
        }

        // 이펙트가 생성됩니다.
        // 피격 이펙트를 생성
        GameObject effect = Instantiate(_damageEffectPrefab,
            _damageEffectPos.position, Quaternion.identity);
        Destroy(effect, 0.25f);

        // 피격 애니메이션 재생
        _animator.Play("Damage", 1);
    }

}
