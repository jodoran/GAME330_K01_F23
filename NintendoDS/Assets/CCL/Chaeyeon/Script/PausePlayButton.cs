using UnityEngine;
using UnityEngine.UI; 

public class PausePlayButton : MonoBehaviour
{
    public GameObject buttonObject; // 버튼 오브젝트
    public Texture pauseTexture;    // 일시 정지 이미지
    public Texture playTexture;     // 재생 이미지
    private bool isPaused = false;  // 게임의 일시 정지 상태

    // 버튼 클릭 시 호출될 함수
    public void TogglePausePlay()
    {
        isPaused = !isPaused; // 일시 정지 상태 토글
        Time.timeScale = isPaused ? 0 : 1; // 게임 일시 정지 또는 재개
        UpdateButtonTexture(); // 버튼 텍스쳐 업데이트
    }

    // 버튼 텍스쳐를 업데이트하는 함수
    private void UpdateButtonTexture()
    {
        var buttonTexture = isPaused ? playTexture : pauseTexture;
        buttonObject.GetComponent<Renderer>().material.mainTexture = buttonTexture;
    }

}
