﻿using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Account status info
    /// </summary>
    public class BinanceAccountStatus
    {
        /// <summary>
        /// The result message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }
        /// <summary>
        /// Success boolean
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Parameters
        /// </summary>
        [JsonProperty("objs")]
        public object[] Objects { get; set; }
    }
}
