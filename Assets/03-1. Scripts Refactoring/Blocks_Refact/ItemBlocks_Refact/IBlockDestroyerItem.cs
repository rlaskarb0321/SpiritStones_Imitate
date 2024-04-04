using System.Collections;
using System.Collections.Generic;

public interface IBlockDestroyerItem
{
    /// <summary>
    /// ����� �ı��ϴ� ������ ����� ���, �Ѻ� �׸��� ���ÿ� ����� ��ϵ��� �־��־���Ѵ�.
    /// </summary>
    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack, DamageLoadState damageLoader);
}

public interface IBlockNoneDestroyerItem
{
}