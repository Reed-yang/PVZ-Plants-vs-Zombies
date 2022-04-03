using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState {
    Idel,
    Walk,
    Attack,
    Dead
}


public class Zombie : MonoBehaviour
{
    private ZombieState state;


    private Animator animator;
    private Grid currGrid;
    /// <summary>
    /// 速度，决定几秒走过一格 
    /// </summary>
    private float speed = 6;
    //攻击状态
    private bool isAttackState ;
    //攻击力
    private float attackValue = 100;
    //
    private string walkAnimationStr;
    /// <summary>
    /// 修改状态会直接改变动画
    /// </summary>
    public ZombieState State { get => state;
        set{
            state = value;
            switch (state)
            {
                case ZombieState.Idel:
                    //播放动画但停留在第一帧
                    animator.Play(walkAnimationStr, 0, 0);
                    animator.speed = 0;
                    break;
                case ZombieState.Walk:
                    animator.Play(walkAnimationStr);
                    animator.speed = 1;
                    break;
                case ZombieState.Attack:
                    animator.Play("Zombie_Attack");
                    animator.speed = 1;
                    break;
                case ZombieState.Dead:
                    break;
            }

        }
    }

    private void Awake()
    {
        int rangeWalk = Random.Range(1, 4);
        switch (rangeWalk)
        {
            case 1:
                walkAnimationStr = "Zombie_Walk1";
                break;
            case 2:
                walkAnimationStr = "Zombie_Walk2";
                break;
            case 3:
                walkAnimationStr = "Zombie_Walk3";
                break;

        }
        GetGridByVerticalNum(0);
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play(walkAnimationStr);
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }
    /// <summary>
    /// 状态检测
    /// </summary>
    private void FSM()
    {
        switch (state)
        {
            case ZombieState.Idel:
                State = ZombieState.Walk;
                break;
            case ZombieState.Walk:
                Move();
                break;
            case ZombieState.Attack:
                if (isAttackState) break;
                Attack(currGrid.CurrPlantBase);
                break;
            case ZombieState.Dead:
                break;
        }
    }

    /// <summary>
    /// 获取一个网格的纵坐标，决定出现的排
    /// </summary>
    /// <param name="verticalNum"></param>
    private void GetGridByVerticalNum(int verticalNum)
    {
        currGrid = GridManager.Instance.GetGridByVerticalNum(verticalNum);
        //Debug.Log(currGrid.Point);
        //Debug.Log(currGrid.Position);
        transform.position = new Vector3(transform.position.x, currGrid.Position.y);
    }

    private void Move()
    {
        if (currGrid == null) return;
        currGrid = GridManager.Instance.GetGridByWorldPos(transform.position);
        if (currGrid.HavePlant
            && currGrid.CurrPlantBase.transform.position.x < transform.position.x
            && transform.position.x - currGrid.CurrPlantBase.transform.position.x < 0.3f)
        {
            //攻击这个植物
            State = ZombieState.Attack;
            return;
        }
        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }


    private void Attack(PlantBase plant)
    {
        isAttackState = true;
        //植物的相关逻辑
        StartCoroutine(DoHurt(plant));
    }
    /// <summary>
    /// 附加伤害给植物
    /// </summary>
    /// <param name="plant"></param>
    /// <returns></returns>
    IEnumerator DoHurt(PlantBase plant)
    {
        //扣血
        while (plant.Hp > 0)
        {
            plant.Hurt(attackValue / 5);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState = false;
        State = ZombieState.Walk;
    }
}
