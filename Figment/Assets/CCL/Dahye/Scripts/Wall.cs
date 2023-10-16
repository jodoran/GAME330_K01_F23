using UnityEngine;

public class Wall : MonoBehaviour
{
    [Tooltip("���� �ð�")]
    public float NoDamageTime = 1.0f;

    [Tooltip("HP")]
    public float HP = 1000;

    /// <summary>
    /// ���������� ������ ���� �ð�
    /// </summary>
    private float lastDamageTime;

    /// <summary>
    ///  ������ ����
    /// </summary>
    public void OnDamage(float damage)
    {
        var now = Time.time;
        if (now - lastDamageTime <= NoDamageTime)
        {
            return;
        }

        lastDamageTime = now;
        HP -= damage;
        Debug.Log("Wall HP : " + HP + " damage : " + damage);
        // Wall ���ݹ��� �� VFX + SFX

        if (HP <= 0)
        {
            Destroy(gameObject);
            //Wall ������ �� VFX + SFX
        }
    }
}