using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public Player player;

    private List<Enemy> Enemys = new List<Enemy>();

    private bool isSelectingTarget;
    private bool isPlayerTurn;
    private bool isPlayerActionFinished;

    private void Start()
    {
        Enemys.AddRange(FindObjectsByType<Enemy>(FindObjectsSortMode.None));

        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (true)
        {
            // 죽거나 삭제된 몬스터 제거
            Enemys.RemoveAll(enemy =>
                enemy == null || enemy.IsDead
            );

            // 모든 적 사망
            if (Enemys.Count == 0)
            {
                Debug.Log("전투 승리!");
                yield break;
            }

            // 플레이어 사망
            if (player.Hp <= 0)
            {
                Debug.Log("플레이어 사망!");
                yield break;
            }

            // 이번 라운드의 행동 순서 만들기
            List<MonoBehaviour> turnOrder = new List<MonoBehaviour>();

            turnOrder.Add(player);

            foreach (Enemy enemy in Enemys)
            {
                if (enemy != null && !enemy.IsDead)
                {
                    turnOrder.Add(enemy);
                }
            }

            // 속도가 높은 순서대로 정렬
            turnOrder.Sort(CompareSpeed);

            Debug.Log("===== 새로운 라운드 =====");

            // 한 명씩 차례대로 행동
            foreach (MonoBehaviour actor in turnOrder)
            {
                if (player.Hp <= 0)
                    yield break;

                // =========================
                // 플레이어 턴
                // =========================
                if (actor is Player)
                {
                    isPlayerTurn = true;
                    isPlayerActionFinished = false;
                    isSelectingTarget = false;

                    Debug.Log("플레이어의 턴입니다.");

                    // 플레이어가 공격할 때까지 기다림
                    yield return new WaitUntil(
                        () => isPlayerActionFinished
                    );

                    isPlayerTurn = false;
                }

                // =========================
                // 몬스터 턴
                // =========================
                else if (actor is Enemy enemy)
                {
                    if (enemy == null || enemy.IsDead)
                        continue;

                    yield return StartCoroutine(
                        EnemyTurnRoutine(enemy)
                    );
                }
            }
        }
    }

    // 공격 버튼에서 호출
    public void BeginBasicAttack()
    {
        if (!isPlayerTurn)
        {
            Debug.Log("지금은 플레이어 턴이 아닙니다.");
            return;
        }

        if (isSelectingTarget)
            return;

        isSelectingTarget = true;

        Debug.Log("공격할 몬스터를 클릭하세요.");
    }

    // Enemy를 클릭하면 호출
    public void SelectEnemy(Enemy selectedEnemy)
    {
        if (!isPlayerTurn)
            return;

        if (!isSelectingTarget)
            return;

        if (selectedEnemy == null)
            return;

        if (selectedEnemy.IsDead)
        {
            Debug.Log("이미 사망한 몬스터입니다.");
            return;
        }

        // 플레이어 공격
        selectedEnemy.TakeDamage(player.Attack);

        // 대상 선택 종료
        isSelectingTarget = false;

        // 플레이어가 행동 1회를 끝냈다고 알림
        isPlayerActionFinished = true;
    }

    private IEnumerator EnemyTurnRoutine(Enemy enemy)
    {
        Debug.Log($"{enemy.name}의 턴");

        yield return new WaitForSeconds(1.5f);

        if (enemy != null && !enemy.IsDead)
        {
            Debug.Log($"{enemy.name}이(가) 플레이어를 공격!");

            player.TakeDamage(enemy.EnemyAttack);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private int CompareSpeed(MonoBehaviour a, MonoBehaviour b)
    {
        float speedA = GetSpeed(a);
        float speedB = GetSpeed(b);

        // 속도가 같다면 플레이어 우선
        if (speedA == speedB)
        {
            if (a is Player && b is Enemy)
                return -1;

            if (a is Enemy && b is Player)
                return 1;

            return 0;
        }

        // 속도가 높은 쪽이 앞
        return speedB.CompareTo(speedA);
    }

    private float GetSpeed(MonoBehaviour actor)
    {
        if (actor is Player playerActor)
            return playerActor.Speed;

        if (actor is Enemy enemyActor)
            return enemyActor.EnemySpeed;

        return 0f;
    }
}