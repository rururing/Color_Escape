using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject HelpPanel;
    public GameObject SettingPanel;
    public GameObject CreditPanel;
   


    void Start()
    {
        Cursor.visible = true; // 커서를 보이게 설정
        Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        // 게임 시작 시 도움말 창을 비활성화
        HelpPanel.SetActive(false);
        SettingPanel.SetActive(false);
        
}

    public void StartGame()
    {
       
        SceneManager.LoadScene("Stage1");
    }

    public void OpenHelpPanel()
    {
        // 도움말 창을 열음
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        // 도움말 창을 닫음
        HelpPanel.SetActive(false);
    }
    public void OpenSettingPanel()
    {
        // 도움말 창을 닫음
        SettingPanel.SetActive(true);
    }
    public void CloseSettingPanel()
    {
        // 도움말 창을 닫음
        SettingPanel.SetActive(false);
    }

    public void OpenCreditPanel()
    {
        // 도움말 창을 열음
        CreditPanel.SetActive(true);
    }

    public void CloseCreditPanel()
    {
        // 도움말 창을 닫음
        CreditPanel.SetActive(false);
    }

    public void QuitGame()
    {
        // 게임 종료
        Application.Quit();
    }

}