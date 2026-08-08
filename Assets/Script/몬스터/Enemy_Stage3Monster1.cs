using UnityEngine;

public class Enemy_Stage3Monster1 : Enemy
{
    public override float EnemyMaxHp => 70 + (EnemyLevel * 10);

    public override float EnemyAttack => 13 + (EnemyLevel * 2);
    public override float EnemyDefense => 25f;
    public override float EnemySpeed => 13f;
    public override int EnemyEXP => 80;
}
