using UnityEngine;

public class Enemy_Stage3Monster2 : Enemy
{
    public override float EnemyMaxHp => 60 + (EnemyLevel * 10);

    public override float EnemyAttack => 17 + (EnemyLevel * 2);
    public override float EnemyDefense => 10f;
    public override float EnemySpeed => 17f;
    public override int EnemyEXP => 90;
}
