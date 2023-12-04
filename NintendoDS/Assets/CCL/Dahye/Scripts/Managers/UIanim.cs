using UnityEngine;

public class UIanim : MonoBehaviour
{
    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    private float speed;

    public void GameOverUI()
    {
        LeanTween.moveLocal(gameOver, new Vector3(0f, 0f, 0f), 0.4f).setEase(LeanTweenType.easeOutBounce);
    }

}
