using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem_Refact : ItemBlock_Refact
{
    [Header("=== Į ������ ===")]
    [SerializeField] private GameObject _swordSlice;
    [SerializeField] private bool _isInUpwardRightSliceZone;

    // �Ѻױ׸���� ���õǾ�����, ���õ� �� ��ҵǾ����� ���� UI On/Off
    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

    // ������ ����� ��� ����ϸ� ��ȭ���·� ���
    public override void DoBreakAction()
    {
        IgniteItemBlock();
        StartCoroutine(DoItemAction());
    }

    // ������ ��� ��ȭ
    protected override void IgniteItemBlock()
    {
        Image maskImage = transform.GetComponentInChildren<Image>();
        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
    }

    // Į �������� ��ġ�Ѱ������� �׾����� ������ �ٸ���
    protected override IEnumerator DoItemAction()
    {
        yield return new WaitUntil(() => GameFlowMgr_Refact._instance.DockedCount == 63);
        yield return new WaitForSeconds(_actionDelay);

        if (_isSpecialItem)
        {
            for (int i = 0; i < 2; i++)
                Instantiate(_swordSlice, transform.position, Quaternion.Euler(0.0f, 0.0f, (-1 + (2 * i)) * 27.425f));
        }

        if (_isInUpwardRightSliceZone)
        {
            Instantiate(_swordSlice, transform.position, Quaternion.Euler(0, 0, 27.425f));
        }
        else
        {
            Instantiate(_swordSlice, transform.position, Quaternion.Euler(0, 0, -27.425f));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        print("Change Slice Upward Right Dir");
        _isInUpwardRightSliceZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        print("Change Slice Downward Right Dir");
        _isInUpwardRightSliceZone = false;
    }
}
