using UnityEngine;

public class UIanim : MonoBehaviour
{
    [SerializeField]
    GameObject gameOver;

    public void GameOverUI()
    {
        LeanTween.moveLocal(gameOver, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
    }

}
