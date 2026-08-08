using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("플레이어 능력치")]
    public int Level = 1;
    public float Exp = 0f;
    public float Hp = 100f;
    public float Mp = 50f;
    public float Def = 10f;
    public float Attack = 20f;
    public float Speed = 10f; // 전투 턴 순서용
    public int Bag = 6;
    public int Crit = 10;
    public int Eva = 15;

    public bool IsDead => Hp <= 0f;

    // 플레이어가 데미지를 입는 함수
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        // 적의 데미지에서 내 방어력을 뺌 (최소 1의 피해는 받도록 Mathf.Max 사용)
        float actualDamage = Mathf.Max(damage - Def, 1f);

        Hp -= actualDamage;

        if (Hp < 0f) Hp = 0f;

        Debug.Log($"플레이어가 {actualDamage}의 피해를 받았습니다! 남은 HP: {Hp}");

        if (Hp <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망... 게임 오버!");
        // 차후 게임 오버 UI를 띄우는 로직을 여기에 추가
    }
}