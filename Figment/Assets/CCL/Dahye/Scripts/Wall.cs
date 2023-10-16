using UnityEngine;

public class Wall : MonoBehaviour
{
    [Tooltip("���� �ð�")]
    public float NoDamageTime = 1.0f;

    [Tooltip("HP")]
    public float HP = 1000;

    [SerializeField] private AudioClip wallCrash;

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
        // Wall ���ݹ��� �� VFX + SFX

        if (HP <= 0)
        {
            SoundManager.Instance.PlayEffectSound(wallCrash);
            CameraShakeDH.Instance.Shake();
            Destroy(gameObject);

            //Wall ������ �� VFX + SFX
        }
    }
}