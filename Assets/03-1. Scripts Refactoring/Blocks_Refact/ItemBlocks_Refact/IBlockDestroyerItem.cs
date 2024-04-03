using System.Collections;
using System.Collections.Generic;

public interface IBlockDestroyerItem
{
    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack);
}

public interface IBlockNoneDestroyerItem
{
}