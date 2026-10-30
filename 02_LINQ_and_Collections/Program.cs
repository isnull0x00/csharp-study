// 02_LINQ_and_Collections
// Java Stream API 대응 LINQ(Language Integrated Query) 연산 실습

Console.WriteLine("=== [02] LINQ & Collections ===");

// 1. 부품 검사 샘플 데이터
var inspectionItems = new List<PartInspection>
{
    new("PART-001", "Cylinder Block", Line: "Line-A", DefectRate: 1.2, Passed: true),
    new("PART-002", "Gear Shaft",     Line: "Line-B", DefectRate: 4.8, Passed: false),
    new("PART-003", "Chassis Frame",  Line: "Line-A", DefectRate: 0.5, Passed: true),
    new("PART-004", "Brake Rotor",    Line: "Line-C", DefectRate: 5.2, Passed: false),
    new("PART-005", "Control Arm",    Line: "Line-B", DefectRate: 2.1, Passed: true),
    new("PART-006", "Worm Gear",      Line: "Line-A", DefectRate: 3.4, Passed: true)
};

// 2. Where & OrderByDescending (Java: filter -> sorted)
Console.WriteLine("\n[1. High Defect Rate Parts (>= 3.0%, Descending)]");
var highRiskParts = inspectionItems
    .Where(p => p.DefectRate >= 3.0)
    .OrderByDescending(p => p.DefectRate)
    .ToList();

foreach (var item in highRiskParts)
{
    Console.WriteLine($"  [{item.PartCode}] {item.PartName,-15} | Line: {item.Line} | Defect: {item.DefectRate:F1}% | Passed: {item.Passed}");
}

// 3. FirstOrDefault & Any (Java: findFirst -> orElse, anyMatch)
Console.WriteLine("\n[2. Search & Existence Check]");
var lineCPart = inspectionItems.FirstOrDefault(p => p.Line == "Line-C");
Console.WriteLine($"  First item on Line-C: {lineCPart?.PartName ?? "None"}");

bool hasCriticalDefect = inspectionItems.Any(p => p.DefectRate > 5.0);
Console.WriteLine($"  Has critical defect (> 5.0%): {hasCriticalDefect}");

// 4. GroupBy & 집계 (Java: Collectors.groupingBy)
Console.WriteLine("\n[3. Aggregation by Line]");
var lineStatistics = inspectionItems
    .GroupBy(p => p.Line)
    .Select(g => new
    {
        Line = g.Key,
        TotalCount = g.Count(),
        PassCount = g.Count(p => p.Passed),
        AverageDefectRate = g.Average(p => p.DefectRate)
    })
    .OrderBy(stat => stat.Line);

foreach (var stat in lineStatistics)
{
    Console.WriteLine($"  [Line {stat.Line}] Total: {stat.TotalCount} | Passed: {stat.PassCount} | Avg Defect: {stat.AverageDefectRate:F2}%");
}


// --- 모델 정의 ---
public record PartInspection(
    string PartCode, 
    string PartName, 
    string Line, 
    double DefectRate, 
    bool Passed
);
