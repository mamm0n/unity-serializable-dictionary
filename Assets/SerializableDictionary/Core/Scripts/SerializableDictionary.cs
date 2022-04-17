// Solution: serializable-dictionary
// Project: SerializableDictionary.Core
// File Name: SerializableDictionary.cs
// Copyright (c) Ogulcan Topsakal 2022.
// 
// This source is subject to the Creative Commons Attribution 4.0.
// See https://creativecommons.org/licenses/by/4.0/.
// All other rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SerializableDictionary.Core
{
    #region CLASSES

    [Serializable]
    public class SerializableDictionary<TKey, TValue> :
        ICollection<KeyValuePair<TKey, TValue>>,
        ISerializationCallbackReceiver
    {
        #region EDITOR EXPOSED FIELDS

        [SerializeField]
        private List<KeyValuePair<TKey, TValue>> elements = new List<KeyValuePair<TKey, TValue>>();

        public SerializableDictionary()
        {
        }
        
        public SerializableDictionary(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
        }

        #endregion

        #region PROPERTIES

        public int Count => elements.Count;
        public bool IsReadOnly { get; }

        public KeyValuePair<TKey, TValue> First => elements.Count > 0 ? elements[0] : default;
        public KeyValuePair<TKey, TValue> Last => elements.Count > 0 ? elements[elements.Count - 1] : default;

        public List<TKey> Keys => GetKeys();
        public List<TValue> Values => GetValues();

        #endregion

        #region METHODS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>
            {
                Key = key,
                Value = value
            };

            for (int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].Key.Equals(key)) continue;
#if UNITY_EDITOR
                Debug.LogWarning(
                    $"The ({key}) you want to add already exists in the dictionary. Old key will overriden.");
#endif
                Remove(key);
            }

            elements.Add(keyValuePair);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(TKey key)
        {
            foreach (var keyValuePair in elements.Where(keyValuePair => keyValuePair.Key.Equals(key)))
            {
                elements.Remove(keyValuePair);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            foreach (var keyValuePair in elements.Where(keyValuePair => keyValuePair.Equals(item)))
            {
                elements.Remove(keyValuePair);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetValue(TKey key)
        {
            foreach (var keyValuePair in elements.Where(keyValuePair => keyValuePair.Key.Equals(key)))
            {
                return keyValuePair.Value;
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var keyValuePair in elements.Where(keyValuePair => keyValuePair.Key.Equals(key)))
            {
                value = keyValuePair.Value;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<TValue> GetValues()
        {
            return elements.Select(keyValuePair => keyValuePair.Value).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            return elements.Any(pair => pair.Value.Equals(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return elements.Any(pair => pair.Equals(item));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TKey GetKey(TValue value)
        {
            foreach (var keyValuePair in elements.Where(keyValuePair => keyValuePair.Value.Equals(value)))
            {
                return keyValuePair.Key;
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryGetKey(TValue value, out TKey key)
        {
            foreach (var pair in elements.Where(pair => pair.Value.Equals(value)))
            {
                key = pair.Key;
                return true;
            }

            key = default;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<TKey> GetKeys()
        {
            return elements.Select(keyValuePair => keyValuePair.Key).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return elements.Any(pair => pair.Key.Equals(key));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            elements.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool HasDuplicatedKeys()
        {
            List<TKey> keys = GetKeys();
            return keys.Count != keys.Distinct().Count();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        private void OnValidate()
        {
            if (HasDuplicatedKeys())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Dictionary has duplicated keys.");
#endif
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    #endregion
}