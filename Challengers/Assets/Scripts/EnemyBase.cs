using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBase : MonoBehaviour
{
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public float damage = 100f;

    protected float playerRealizeRange = 10f;
    protected float attackRange = 5f;
    protected float attackCoolTime = 5f;
    protected float attackCollTimeCacl = 5f;
    protected bool canAtk = true;

    protected float moveSpeed = 2f;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;

    protected GameObject parentRoom;
}
