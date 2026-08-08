using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("몬스터 능력치")]
    public int EnemyLevel = 1;

    // 최대 HP는 몬스터마다 공식이 다름
    public virtual float EnemyMaxHp { get; }

    // 현재 HP는 실제 값을 저장
    public float EnemyHp { get; protected set; }

    public virtual float EnemyAttack { get; }
    public virtual float EnemyDefense { get; }
    public virtual float EnemySpeed { get; }
    public virtual int EnemyEXP { get; }

    public bool IsDead;

    protected virtual void Awake()
    {
        // 전투 시작 시 현재 HP = 최대 HP
        EnemyHp = EnemyMaxHp;
    }

    private void OnMouseDown()
    {
        GameManager.Instance.BattleManager.SelectEnemy(this);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        EnemyHp -= damage;

        /*
         if (EnemyHp < 0f)
            EnemyHp = 0f;
        */

        Debug.Log(
            $"{name}이(가) {damage}의 피해를 받았습니다. " +
            $"남은 HP: {EnemyHp}/{EnemyMaxHp}"
        );

        if (EnemyHp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;

        Debug.Log($"{name} 사망");

        Destroy(gameObject);
    }
}