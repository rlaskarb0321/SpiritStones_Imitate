using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eHeroLife
{
    Alive,
    Dead,
    DeadSceneProgress,
}

public class GameOverScript : MonoBehaviour
{
    public eHeroLife _heroLifeState;
    public Image[] _soulessBlocks;
    public Image _gameOverPanel;
    public float _reviveHpPercentage;
    WaitUntil _wu;

    public Button _retryBtn;
    public Button _exitBtn;

    private void Awake()
    {
        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i] = _soulessBlocks[i].GetComponent<Image>();
        }

        _wu = new WaitUntil(() => _heroLifeState == eHeroLife.Dead);
        StartCoroutine(CheckGameOver());
    }

    IEnumerator CheckGameOver()
    {
        yield return _wu;
        DoGameOverAction();

        StartCoroutine(CheckGameOver());
    }

    void DoGameOverAction()
    {
        _heroLifeState = eHeroLife.DeadSceneProgress;

        // 게임오버시 가지고 있던 블럭들을 의미없는 돌맹이 처럼 보이기 위한 작업
        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i].enabled = true;
        }

        /* 게임오버시 블럭들에게 SendMessage함수를 이용해 알파값을 서서히 줄이는 함수를 호출
         * SendMessage를 쓴 이유
         * 1. 게임오버는 게임동안 1번 발생할 일이라 BlockBase함수에서 GameOver상황을 계속 체크하게 하지 않기위해
         * 2. 불필요한 변수를 만들지 않기위해
         * 3. 디버깅 어려울 수 있다는 문제 해결을 위해 해당함수가 존재하는 클래스 명으로 GameObject를 선언함
         */
        for (int i = 0; i < GameManager._instance._blockMgrList.Capacity; i++)
        {
            GameObject blockBase = GameManager._instance._blockMgrList[i];
            blockBase.SendMessage("ExtractBlockSoul");
        }
        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3.5f);
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void OnClickExitBtn()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void OnClickReviveBtn()
    {
        _gameOverPanel.gameObject.SetActive(false);

        HeroTeamUI heroTeam = GameManager._instance._heroTeamMgrObj.GetComponent<HeroTeamUI>();
        float amount = heroTeam._totalHp * _reviveHpPercentage;
        heroTeam.IncreaseHp(amount, true);
        _heroLifeState = eHeroLife.Alive;

        for (int i = 0; i < _soulessBlocks.Length; i++)
        {
            _soulessBlocks[i].enabled = false;
        }

        for (int i = 0; i < GameManager._instance._blockMgrList.Count; i++)
        {
            GameObject blockBase = GameManager._instance._blockMgrList[i];
            blockBase.SendMessage("RestoreBlockSoul");
        }
    }
}
