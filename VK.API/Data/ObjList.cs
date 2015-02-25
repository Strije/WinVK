using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace VK.API.Data
{
    public class ObjList<T>
    {
        [JsonProperty("count")]
        public int Count = 0;

        [JsonProperty("items")]
        public T[] Items = null;

        public List<T> ToList()
        {
            if (Items != null)
                return Items.ToList();

            return null;
        }
    }
}
