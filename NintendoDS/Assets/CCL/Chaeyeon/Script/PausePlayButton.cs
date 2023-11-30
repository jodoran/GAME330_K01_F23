using UnityEngine;
using UnityEngine.UI; 

public class PausePlayButton : MonoBehaviour
{
    public GameObject buttonObject; // ��ư ������Ʈ
    public Texture pauseTexture;    // �Ͻ� ���� �̹���
    public Texture playTexture;     // ��� �̹���
    private bool isPaused = false;  // ������ �Ͻ� ���� ����

    // ��ư Ŭ�� �� ȣ��� �Լ�
    public void TogglePausePlay()
    {
        isPaused = !isPaused; // �Ͻ� ���� ���� ���
        Time.timeScale = isPaused ? 0 : 1; // ���� �Ͻ� ���� �Ǵ� �簳
        UpdateButtonTexture(); // ��ư �ؽ��� ������Ʈ
    }

    // ��ư �ؽ��ĸ� ������Ʈ�ϴ� �Լ�
    private void UpdateButtonTexture()
    {
        var buttonTexture = isPaused ? playTexture : pauseTexture;
        buttonObject.GetComponent<Renderer>().material.mainTexture = buttonTexture;
    }

}
