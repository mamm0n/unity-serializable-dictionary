// Solution: serializable-dictionary
// Project: SerializableDictionary.Core
// File Name: Example.cs
// Copyright (c) Ogulcan Topsakal 2022.
// 
// This source is subject to the Creative Commons Attribution 4.0.
// See https://creativecommons.org/licenses/by/4.0/.
// All other rights reserved.

using UnityEngine;
using SerializableDictionary.Core;
using System.Collections.Generic;
using System.Linq;

namespace SerializableDictionary.Examples
{
    #region CLASSES

    public class Example : MonoBehaviour
    {
        #region EDITOR EXPOSED FIELDS

        [SerializeField]
        private SerializableDictionary<string, int> serializableDictionary = new SerializableDictionary<string, int>();
        
        [SerializeField]
        private Dictionary<string, int> normalDictionary = new Dictionary<string, int>();

        #endregion

        #region MONO

        private void SerializableDictionaryExample()
        {
            int count = serializableDictionary.Count;
            
            Core.KeyValuePair<string, int> firstPair = serializableDictionary.First;
            Core.KeyValuePair<string, int> lastPair = serializableDictionary.Last;
            
            List<string> keys = serializableDictionary.Keys.ToList();
            List<int> values = serializableDictionary.Values.ToList();

            foreach (var keyValuePair in serializableDictionary)
            {
                Debug.Log($"Key: {keyValuePair.Key} Value: {keyValuePair.Value}");
            }

            foreach (var key in serializableDictionary.Keys)
            {
                Debug.Log($"Key: {key}");
            }
            
            foreach (var value in serializableDictionary.Values)
            {
                Debug.Log($"Value {value}");
            }

            bool containsKey = serializableDictionary.ContainsKey("test");
            bool containsValue = serializableDictionary.ContainsValue(0);
            
            serializableDictionary.Add("test", 0);
            serializableDictionary.Add("test", 1);
            serializableDictionary.Remove("test");
            
            serializableDictionary.Clear();
        }

        private void NormalDictionaryExample()
        {
            int count = normalDictionary.Count;

            List<string> keys = normalDictionary.Keys.ToList();
            List<int> values = normalDictionary.Values.ToList();

            foreach (var keyValuePair in normalDictionary)
            {
                Debug.Log($"Key: {keyValuePair.Key} Value: {keyValuePair.Value}");
            }

            foreach (var key in normalDictionary.Keys)
            {
                Debug.Log($"Key: {key}");
            }
            
            foreach (var value in normalDictionary.Values)
            {
                Debug.Log($"Value {value}");
            }

            bool containsKey = normalDictionary.ContainsKey("test");
            bool containsValue = normalDictionary.ContainsValue(0);
            
            normalDictionary.Add("test", 0);
            normalDictionary.Add("test", 1);
            normalDictionary.Remove("test");
            
            normalDictionary.Clear();
        }

        #endregion
    }

    #endregion
   
}