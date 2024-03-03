using System;
using Newtonsoft.Json;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Models
{
    [Serializable]
    public class DataModel
    {
        public int id;
        [JsonConverter(typeof(ColorHandler))] public Color color;
        public bool animationType;
        public string text;
        
        public DataModel Generate()
        {
            text = $"Created {DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            animationType = Random.Range(0, 1) == 0;
            color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            return this;
        }
    }
}