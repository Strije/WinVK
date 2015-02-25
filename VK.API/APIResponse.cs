using Newtonsoft.Json;

namespace VK.API
{
    public class APIResponse<T>
    {
        [JsonProperty("response")]
        public T Response = default(T);
    }
}
