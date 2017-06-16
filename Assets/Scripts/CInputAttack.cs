using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInputAttack : _MonoBehaviour
{
    Animator _animator;

    int _attackIndex = 1; // 스윙 카운트

    public Transform _attackPoint; // 공격 위치 포인트

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool IsAttack()
    {
        // 전사의 공격 상태는 Attack1 ~ Attack3 에니메이션 상태를 의미함
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
            _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            return true;
        }

        return false;
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        // 왼쪽의 알트키를 누르면
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftShift))
        {
            if (IsAttack()) return;

            // Attack1 -> Attack2 -> Attack3 이름을 가진 애니메이션을 실행함
            _animator.SetTrigger("Attack" + _attackIndex++);

            if (_attackIndex > 3) _attackIndex = 1;
        }
    }

    // 공격 애니메이션 타이밍 이벤트 메소드
    public void AttackAnimationEvent()
    {
        //Debug.Log(this.GetMethodName());

        // 공격 포인트에서 1반지름 안에 들어오는 몬스터들의 콜라이더를 검출함
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, 1f, 1 << LayerMask.NameToLayer("Monster"));

        //Debug.Log("hit monster count : " + colliders.Length);

        if (colliders.Length <= 0) return;

        //Debug.Log("hit first monster name : " + colliders[0].name);

        // 피격이 검출된 첫번째 몬스터에게 피격 데미지를 줌
        foreach (Collider2D collider in colliders)
        {
            //Debug.Log(this.GetMethodName() + ":" + collider + ":" + collider.tag);

            if (collider == null) continue;

            //collider.GetComponent<CMonsterDamage>().Damage(1f);
            collider.SendMessage("Damage", 1f);

            break;
        }

    }
}
