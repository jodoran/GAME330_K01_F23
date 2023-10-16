using UnityEngine;

public class Wall : MonoBehaviour
{
    [Tooltip("무적 시간")]
    public float NoDamageTime = 1.0f;

    [Tooltip("HP")]
    public float HP = 1000;

    [SerializeField] private AudioClip wallCrash;

    /// <summary>
    /// 마지막으로 데미지 받은 시간
    /// </summary>
    private float lastDamageTime;

    /// <summary>
    ///  데미지 받음
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
        // Wall 공격받을 때 VFX + SFX

        if (HP <= 0)
        {
            SoundManager.Instance.PlayEffectSound(wallCrash);
            CameraShakeDH.Instance.Shake();
            Destroy(gameObject);

            //Wall 무너질 때 VFX + SFX
        }
    }
}