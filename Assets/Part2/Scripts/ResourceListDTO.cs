using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace API_DND
{
    [System.Serializable]
    public class ResourceListDTO<T>
    {
        public int count;
        public T[] results;
    }
}
