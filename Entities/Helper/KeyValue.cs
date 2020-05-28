using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class KeyValue<K,V>
    {
        
        public KeyValue(K key,V value)
        {
            this.Key = key;
            this.Value = value;   
        }
        public KeyValue()
        {
           
        }
        public K Key { get; set; }
        public V Value { get; set; }
    }
}
