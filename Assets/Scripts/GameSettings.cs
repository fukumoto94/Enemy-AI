using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float droneSpeed = 2f;
    public static float DroneSpeed => Instance.droneSpeed;

    [SerializeField] private float aggroRadius = 4f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackRange = 3f;
    public static float AttackRange => Instance.attackRange;

    [SerializeField] private GameObject droneProjectilePrefab;
    public static GameObject DroneProjectilePrefab => Instance.droneProjectilePrefab;

    public static GameSettings Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }
}
