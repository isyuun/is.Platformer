using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMovement : _MonoBehaviour
{
    protected CCharacterState _characterState; // 캐릭터 상태

    protected virtual void Awake()
    {
        _characterState = GetComponent<CCharacterState>();
    }

    // 방향 전환
    public void Flip()
    {
        // 오브젝트 반전
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        // 시선 정보 변경
        _characterState._isRightDir = !_characterState._isRightDir;
    }
}