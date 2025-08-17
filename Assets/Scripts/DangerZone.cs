using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private float damage = 25f;

    public float GetDamage()
    {
        return damage;
    }
}
