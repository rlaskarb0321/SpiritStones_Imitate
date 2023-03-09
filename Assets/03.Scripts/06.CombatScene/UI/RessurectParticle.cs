using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessurectParticle : MonoBehaviour
{
    public GameObject[] _ressurectPS;
    private WaitForSeconds _ws;

    private void Awake()
    {
        _ws = new WaitForSeconds(_ressurectPS[_ressurectPS.Length - 1].GetComponent<ParticleSystem>().main.duration);
    }

    public void SetRevive()
    {
        for (int i = 0; i < _ressurectPS.Length; i++)
        {
            if (!_ressurectPS[i].activeSelf)
                _ressurectPS[i].gameObject.SetActive(true); 
        }

        StartCoroutine(SetActiveFalseParticle());
    }

    IEnumerator SetActiveFalseParticle()
    {
        yield return _ws;
        for (int i = 0; i < _ressurectPS.Length; i++)
        {
            if (_ressurectPS[i].activeSelf)
                _ressurectPS[i].gameObject.SetActive(false);
        }
    }
}
