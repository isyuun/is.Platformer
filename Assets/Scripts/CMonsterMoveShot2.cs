using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterMoveShot2 : CMonsterMoveShot
{
    // 몬스터 타겟 체크 컴포넌트 참조
    public CMonsterTargetChecker targetChecker;

    private void Start()
    {
        // 몬스터 공격 타겟 체크
        targetChecker.FrontTargetChecker(this);
    }
}
