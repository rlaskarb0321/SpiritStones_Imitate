using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile_Refact : MonoBehaviour
{
    [SerializeField] private float _launchSpeed;
    [SerializeField] private GameObject _target;

    private BowItem_Refact _launcher;

    public void SetTargetBlock(GameObject Targetblock, BowItem_Refact launcher)
    {
        _target = Targetblock;
        _launcher = launcher;
        StartCoroutine(Launch());
    }

    private IEnumerator Launch()
    {
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, _target.transform.position, _launchSpeed);
            yield return null;
        }

        _launcher.TargetHitCount++;
        Destroy(gameObject);
    }
}
