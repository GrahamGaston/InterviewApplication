using System;
using System.Collections.Generic;

namespace Mvvm.Communication
{
    /// <summary>
    /// Mediator implementation for cross-VM communication
    /// </summary>
    public static class Mediator
    {
        private static readonly IDictionary<string, List<Action<object>>> RegistrationsList = new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// Register to sign up for VM message notifications
        /// </summary>
        /// <param name="token">Identifier for VM</param>
        /// <param name="callback">Callback that will process messages</param>
        public static void Register(string token, Action<object> callback)
        {
            if (!RegistrationsList.ContainsKey(token))
            {
                var list = new List<Action<object>>();
                list.Add(callback);
                RegistrationsList.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in RegistrationsList[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    RegistrationsList[token].Add(callback);
            }
        }

        /// <summary>
        /// Stop notifications
        /// </summary>
        /// <param name="token">Identifier for VM</param>
        /// <param name="callback">Callback used to process messages</param>
        public static void Unregister(string token, Action<object> callback = null)
        {
            if (RegistrationsList.ContainsKey(token))
                RegistrationsList.Remove(token);

            // RegistrationsList[token].Remove(callback);
        }

        /// <summary>
        /// Broadcast a message to subscribers
        /// </summary>
        /// <param name="token">Identifier for VM</param>
        /// <param name="args">Argument that will be passed to the subscribers' registered callback.</param>
        public static void NotifyColleagues(string token, object args)
        {
            if (RegistrationsList.ContainsKey(token))
                foreach (var callback in RegistrationsList[token])
                    callback(args);
        }
    }
}
