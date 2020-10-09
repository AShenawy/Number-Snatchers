using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ArrayFuncs
    {
        public static void FillArray<T>(T[] cardsArray, T value, int startIndex, int count)
        {
            for (int i = startIndex; i < startIndex + count; i++)
                cardsArray[i] = value;
        }
    }
}

