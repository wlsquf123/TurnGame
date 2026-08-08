using UnityEngine;

public class Enemy_Stage2Monster2 : Enemy
{
    public override float EnemyMaxHp => 50 + (EnemyLevel * 10);

    public override float EnemyAttack => 14 + (EnemyLevel * 3);
    public override float EnemyDefense => 0f;
    public override float EnemySpeed => 13f;
    public override int EnemyEXP => 60;
}