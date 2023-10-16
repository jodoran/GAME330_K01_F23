using UnityEngine;

public class textActive : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void Enable()
    {
        this.gameObject.SetActive(true);
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }

}
