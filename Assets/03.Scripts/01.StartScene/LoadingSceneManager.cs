using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static string _nextScene;
    public Image _progressBar;
    public Text _progressText;
    public Text _jorkText;
    public string[] _numOfJork;

    private AsyncOperation _op;

    void Start()
    {
        StartCoroutine(LoadSceneProcess());

        int randomIdx = Random.Range(0, _numOfJork.Length);
        _jorkText.text = _numOfJork[randomIdx];
    }

    void Update()
    {
        
    }

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneProcess()
    {
        yield return null;

        _op = SceneManager.LoadSceneAsync(_nextScene);
        _op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!_op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (_op.progress < 0.9f)
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _op.progress, timer);
                if (_progressBar.fillAmount >= _op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, 1f, timer);
                if (_progressBar.fillAmount == 1.0f)
                {
                    _op.allowSceneActivation = true;
                    yield break;
                }
            }
            _progressText.text = string.Format("·Îµù Áß : {0:F0} %", _progressBar.fillAmount * 100);
        }
    }
}
