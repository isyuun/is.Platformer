using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParabolaBullet : CBullet
{
    public float _damageRange;      // 공격 범위
    public float _shotForce;        // 발포 힘

    // 직선 총알 초기화(방향)
    public override void Init(bool isRightDir)
    {
        //Debug.Log(this.GetMethodName() + ":" + isRightDir);
        // 직선 총알 초기화
        base.Init(isRightDir);
        Move(); // 총알 이동
    }

    // 포탄 이동을 처리함
    public override void Move()
    {
        //Debug.Log(this.GetMethodName());
        // 지정한 방향과 속도로 총알이 이동함
        _rigidbody2d.AddForce(new Vector2(1f * _dirValue, 1f) * _shotForce);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // 스플래시 데미지를 처리함
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _damageRange, 1 << LayerMask.NameToLayer("Player"));

        ////Player
        //colliders = Physics2D.OverlapCircleAll(transform.position, _damageRange, 1 << LayerMask.NameToLayer("Player"));
        //foreach (Collider2D collider in colliders)
        //{
        //    //Debug.Log(this.GetMethodName() + ":" + collider + ":" + collider.tag);
        //    if (collider.tag == "Player")
        //    {
        //        collider.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        //    }
        //}
        //All
        colliders = Physics2D.OverlapCircleAll(transform.position, _damageRange);
        foreach (Collider2D collider in colliders)
        {
            Debug.LogWarning(this.GetMethodName() + ":" + collider + ":" + collider.tag);
            SendMessageOptions options = SendMessageOptions.DontRequireReceiver;
            collider.SendMessage("Damage", 1f, options);
        }

        // 충돌 이펙트 처리함
        base.OnCollisionEnter2D(collision);
    }
}
