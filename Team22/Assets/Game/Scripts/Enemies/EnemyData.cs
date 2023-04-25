using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    public float Speed;
    public float AttackSpeed;
    public float MovementAmount;
    public float DetectingPlayerDistance;
}
