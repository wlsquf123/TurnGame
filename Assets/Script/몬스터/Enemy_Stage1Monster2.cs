using UnityEngine;

public class Enemy_Stage1Monster2 : Enemy
{
    public override float EnemyMaxHp => 40 + (EnemyLevel * 10);

    public override float EnemyAttack => 10 + (EnemyLevel * 2);
    public override float EnemyDefense => 0f;
    public override float EnemySpeed => 11f;
    public override int EnemyEXP => 30;
}
