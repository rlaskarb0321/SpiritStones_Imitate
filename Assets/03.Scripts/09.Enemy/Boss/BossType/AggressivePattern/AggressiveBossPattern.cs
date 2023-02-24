using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBossPattern : MonoBehaviour
{
    // 출시전에 가중치 랜덤을 적용하여 같은 가중치를갖는 다양한 패턴중 하나를 선택하게 할 예정
    public void ChooseAggressiveAttack(HeroTeamMgr heroTeam, EnemyBase enemy)
    {
        AttackTwice(enemy, heroTeam);
    }


    void AttackTwice(EnemyBase enemy, HeroTeamMgr heroTeam)
    {
        
        float newDmg = enemy._atkPower * 0.75f;
        for (int i = 0; i < 2; i++)
        {
            heroTeam.GetComponent<HeroTeamUI>().DecreaseHp(newDmg);
        }
    }
}