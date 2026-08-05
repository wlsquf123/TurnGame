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

    
}