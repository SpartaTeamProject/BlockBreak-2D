using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : Enemy
{
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    //���� �ൿ

    private void RandomAction()
    {
        //RandomSound(); //���� �ϻ���� ���

        int _random = Random.Range(0, 4); //���, �θ���, �ȱ� (����int �� �ִ밪 ���Ծ���)

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Peek();
        else if (_random == 2)
            TryWalk();
        else if (_random == 3 && !isAttacking)
            Attack();

    }


    //##�ൿ����##
    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("���");
    }
    private void Peek()
    {
        currentTime = waitTime;
        //anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }
}
