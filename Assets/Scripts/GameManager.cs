using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 게임 매니저에 대한 인스턴스에 접근하는 속성
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우 새로 생성
            if (_instance == null)
            {
                // 씬에서 GameManager를 찾음
                _instance = FindObjectOfType<GameManager>();

                // 씬에 GameManager가 없는 경우 새로운 게임 오브젝트에 추가
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

}