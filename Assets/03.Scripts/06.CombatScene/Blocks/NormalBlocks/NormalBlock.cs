using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eNormalBlockType
{
    Warrior,
    Archer,
    Thief,
    Magician,
    None,
}

public class NormalBlock : MonoBehaviour
{
    [SerializeField] private BlockBase _blockBase;
    [SerializeField] private GameObject _spiritPrefabs;
    [SerializeField] private eNormalBlockType _normalBlockType;
    public eNormalBlockType NormalBlockType
    {
        get { return _normalBlockType; }
        set { _normalBlockType = value; }
    }

    private void Start()
    {
        _normalBlockType = _blockBase._normalType;
    }

    public void DoAction()
    {
        _blockBase.DoNormalBlockAction();
        GenerateSpirit(_spiritPrefabs);

        GameManager._instance._blockMgrList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void MoveBlock()
    {
        _blockBase.MoveBlock(this.gameObject);
    }

    void GenerateSpirit(GameObject spirit)
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
        Instantiate(spirit, transform.position, Quaternion.identity, spiritGroup);
    }
}
