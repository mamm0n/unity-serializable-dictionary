// Solution: serializable-dictionary
// Project: SerializableDictionary.Core
// File Name: KeyValuePair.cs
// Copyright (c) Ogulcan Topsakal 2022.
// 
// This source is subject to the Creative Commons Attribution 4.0.
// See https://creativecommons.org/licenses/by/4.0/.
// All other rights reserved.

using System;

namespace SerializableDictionary.Core
{
    #region STRUCTS

    [Serializable]
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    #endregion
}