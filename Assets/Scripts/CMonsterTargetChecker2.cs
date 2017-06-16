using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterTargetChecker2 : CMonsterTargetChecker
{
    Color color;

    public override void DebugDrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (this.color != null)
            color = this.color;
        base.DebugDrawLine(start, end, color);
    }
}
