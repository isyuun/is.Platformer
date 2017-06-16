using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDamage : _MonoBehaviour
{
    CCharacterState _state;
    Animator _animator;

    void Awake()
    {
        _state = GetComponent<CCharacterState>();
        _animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        //Debug.Log(this.GetMethodName());
        //Debug.Log("Damage!!!");

        // 이미 데미지를 입은 상태면 패쓰~
        if (_state.state == CCharacterState.State.Damage
            || _animator.GetCurrentAnimatorStateInfo(1).IsName("Damage")
            ) return;

        // 피격 애니메이션을 실행함
        _animator.Play("Damage", 1);
    }
}
