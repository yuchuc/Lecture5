using System;

namespace Lecture.Web.Models
{
  public class FormModel
  {
    public int Integer { get; set; }
    public int? NullableInteger { get; set; }
    public string String { get; set; }
    public DateTime Date { get; set; }
    public double Double { get; set; }
    public decimal Decimal { get; set; }

    public SubFormModel SubForm { get; set; }
  }
}