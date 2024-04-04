using System.Collections;
using System.Collections.Generic;

public interface IBlockDestroyerItem
{
    /// <summary>
    /// 블록을 파괴하는 아이템 블록의 경우, 한붓 그리기 스택에 검출된 블록들을 넣어주어야한다.
    /// </summary>
    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack, DamageLoadState damageLoader);
}

public interface IBlockNoneDestroyerItem
{
}