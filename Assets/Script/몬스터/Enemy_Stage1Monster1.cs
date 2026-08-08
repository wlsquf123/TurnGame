using UnityEngine;

public class Enemy_Stage1Monster1 : Enemy
{
    public override float EnemyMaxHp => 50 + (EnemyLevel * 10);
    public override float EnemyAttack => 8 + (EnemyLevel * 2);
    public override float EnemyDefense => 0f;
    public override float EnemySpeed => 8f;
    public override int EnemyEXP =>  20;
}