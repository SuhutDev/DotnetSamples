using System;

var dt = new DateTime(2023, 4, 1, 8, 7, 0);
var now = DateTime.Now;

System.Console.WriteLine(DateTime.MinValue.Ticks);
System.Console.WriteLine(DateTime.MinValue.AddSeconds(1).Ticks.ToString("#,##0"));

var jakartaOffset = TimeSpan.FromHours(7);
var dto = new DateTimeOffset(dt, jakartaOffset);

System.Console.WriteLine($"Offset Time : {dto} ");
System.Console.WriteLine($"UTC Time : {dto.UtcDateTime} ");