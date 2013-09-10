#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using Lecture.Web.Models;

#endregion

namespace Lecture.Web.Extensions
{
  public static class DataTablesGridExtensions
  {
    public static DataTablesGridData BuildGridData<T>(this IEnumerable<T> objects, IEnumerable<Func<T, object>> cellValueExpressions)
    {
      return new DataTablesGridData {aaData = BuildGridRows(objects, cellValueExpressions)};
    }

    private static IEnumerable<object> BuildGridRows<T>(IEnumerable<T> objects, IEnumerable<Func<T, object>> tableCellExpressions)
    {
      objects = objects.ToList();
      tableCellExpressions = (tableCellExpressions ?? Enumerable.Empty<Func<T, object>>()).ToList();
      if (!tableCellExpressions.Any())
      {
        var type = objects.Any() ? objects.First().GetType() : typeof (T);
        var properties = type.GetProperties();
        tableCellExpressions = properties.Select(prop => new Func<T, object>(obj => prop.GetValue(obj, null))).ToList();
      }

      return objects.Select(o => tableCellExpressions.Select(tce => tce(o)));
    }
  }
}