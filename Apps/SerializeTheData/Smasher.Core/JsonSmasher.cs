﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Smasher.Core {
  public static class JsonSmasher {

    public static string MakeJson<T>(T theClass) where T : class {
      Type type = typeof(T);
      
      var props = type.GetProperties(
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
      // does not include inherited properties


      var att = type.GetCustomAttributes(true);
      if (att.Any(x => x is SortAttribute))
      {
        props = props.OrderBy(x => x.Name).ToArray();
      }
      // 
      StringBuilder sb = new StringBuilder();
      sb.Append("{");
      foreach (var prop in props)
      {
        sb.AppendLine($"\t\"{prop.Name}\" : \"{prop.GetValue(theClass)}\",");

      }

      sb.Append("}");

      return sb.ToString();
    }
  }
}
