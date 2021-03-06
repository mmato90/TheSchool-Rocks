﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImageSource.Concrete
{
    public class SerializerImp : ISerializer
    {
        public string ToJson<T>(T source)
        {
            return JsonConvert.SerializeObject(source, Formatting.Indented);
        }

        public T ToObject<T>(string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }
    }
}
