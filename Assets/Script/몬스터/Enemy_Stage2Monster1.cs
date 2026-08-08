using UnityEngine;

public class Enemy_Stage2Monster1 : Enemy
{
    public override float EnemyMaxHp => 60 + (EnemyLevel * 10);

    public override float EnemyAttack => 11 + (EnemyLevel * 2);
    public override float EnemyDefense => 0f;
    public override float EnemySpeed => 11f;
    public override int EnemyEXP => 50;
}
