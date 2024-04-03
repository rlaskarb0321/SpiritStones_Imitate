using System.Collections.Generic;

public interface IBlockDestroyerItem
{
    public void FillDestroyStack(Stack<BlockBase_Refact> stack);
}

public interface IBlockNoneDestroyerItem
{
}