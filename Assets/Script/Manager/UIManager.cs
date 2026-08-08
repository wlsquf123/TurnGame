using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UIManager는 직접 전투 로직을 처리하지 않고, BattleManager에게 명령만 내립니다.

    // UI 공격 버튼의 OnClick 이벤트에 이 함수를 연결하세요.
    public void Attack()
    {
        // GameManager를 통해 BattleManager의 공격 준비 함수 호출
        GameManager.Instance.BattleManager.BeginBasicAttack();
    }
}