using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YpassDesktop.Service
{
    public class ParameterBuilder
    {
        private Dictionary<string, object?> parameters = new Dictionary<string, object?>();

        public void Add(string key, object ?value)
        {
            if (parameters.ContainsKey(key))
            {
                parameters[key] = value;
            }
            else
            {
                parameters.Add(key, value);
            }
        }

        public T? Get<T>(string key)
        {
            if (parameters.TryGetValue(key, out var value) && value is T result)
            {
                return result;
            }

            // Handle the case where the parameter doesn't exist or is of the wrong type
            return default(T);
        }

        // Add other methods for constructing parameters as needed
    }

}
