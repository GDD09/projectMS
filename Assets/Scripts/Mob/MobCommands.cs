using UnityEngine;
using System.Collections;
using System;

public enum MobCommandType
{
    Wait,
    MoveTo,
}

[Serializable]
public class MobCommand
{
    public virtual IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        yield break;
    }

    public virtual string GetSummary()
    {
#if UNITY_EDITOR
        return UnityEditor.ObjectNames.NicifyVariableName(GetType().Name);
#else
        return GetType().Name;
#endif
    }
}

[Serializable]
public class WaitForMobCommand : MobCommand
{
    public float seconds = 1f;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        yield return new WaitForSeconds(seconds);
    }

    public override string GetSummary()
    {
        return $"대기: {seconds} 초";
    }
}

[Serializable]
public class MoveForMobCommand : MobCommand
{
    public Vector2 movement = Vector2.zero;
    public float speed = 1f;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        var movement = this.movement;
        if (interpreter.flipMovementY)
        {
            movement.y = -movement.y;
        }

        var destination = mob.transform.GetPos() + movement;

        while (Vector2.Distance(mob.transform.position, destination) > 0.01f)
        {
            mob.transform.MoveTowards(destination, speed * Time.deltaTime);
            yield return null;
        }

        mob.transform.SetPos(destination);
    }

    public override string GetSummary()
    {
        return $"이동: {movement} 만큼 [{speed} u/s]";
    }
}

[Serializable]
public class FireGunMobCommand : MobCommand
{
    [Tooltip("null일 경우, 자신의 모든 자식들이 알아서 총을 발사한다.")]
    public GameObject gun = null;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        if (gun == null)
        {
            mob.BroadcastMessage("FireBullets");
        }
        else
        {
            gun.SendMessage("FireBullets");
        }
        yield return null;
    }

    public override string GetSummary()
    {
        // UnityObject가 가지는 특성 때문에, null 조건 연산자(gun?.name)는 제대로 작동하지 않는다.
        return $"총 발사: {(gun ? gun.name : "(모든 자식)")} 에게서";
    }
}

[Serializable]
public class DestroyMobCommand : MobCommand
{
    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        GameObject.Destroy(mob);
        yield return null;
    }

    public override string GetSummary()
    {
        return "파괴!";
    }
}

[Serializable]
public class GotoMobCommand : MobCommand
{
    [Tooltip("이동할 명령어의 인덱스")]
    public int index = 0;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        interpreter.state.Goto(index);
        if (interpreter.state.IsDead)
        {
            Debug.LogWarning("Goto 명령어로 인해 MobCommandInterpreter가 종료됨", interpreter);
        }

        yield return null;
    }

    public override string GetSummary()
    {
        return $"GOTO {index}";
    }
}

[Serializable]
public class EnableObjectMobCommand : MobCommand
{
    public GameObject target = null;
    public bool toEnable = true;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        if (target)
        {
            target.SetActive(toEnable);
        }
        else
        {
            Debug.LogWarning("EnableObjectMobCommand: target이 null입니다.", interpreter);
        }

        yield return null;
    }

    public override string GetSummary()
    {
        var action = toEnable ? "활성화" : "비활성화";
        var targetName = target ? target.name : "null?!!?!";

        return $"{action}: {targetName}";
    }
}

[Serializable]
public class EnableChildMobCommand : MobCommand
{
    [Min(0)]
    public int childIndex = 0;
    public bool toEnable = true;

    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        if (childIndex < mob.transform.childCount)
        {
            mob.transform.GetChild(childIndex).gameObject.SetActive(toEnable);
        }
        else
        {
            Debug.LogWarning("EnableChildMobCommand: index가 너무 큽니다.", interpreter);
        }
        yield return null;
    }

    public override string GetSummary()
    {
        var action = toEnable ? "활성화" : "비활성화";
        var targetName = $"{childIndex} 번째 자식";

        return $"{action}: {targetName}";
    }
}

[Serializable]
public class WaitUntilChildGoneMobCommand : MobCommand
{
    public override IEnumerator Execute(GameObject mob, MobCommandInterpreter interpreter)
    {
        bool everyChildGone = false;
        while (!everyChildGone)
        {
            everyChildGone = true;

            foreach (Transform child in mob.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    everyChildGone = false;
                }
            }

            yield return null;
        }
    }

    public override string GetSummary()
    {
        return "대기: 남은 자식이 더는 없을 때까지";
    }
}
