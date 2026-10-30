// 01_Java_to_CSharp_Basics
// Java 개발자를 위한 C# 핵심 문법 차이점 정리

Console.WriteLine("=== [01] Java vs C# Core Syntax Differences ===");

// 1. 프로퍼티 (Properties) & init 접근자
// Getter/Setter 보일러플레이트 제거 및 생성 시점 불변성 지정
Console.WriteLine("\n[1. Properties & Object Initializer]");
var line = new ProductionLine
{
    LineCode = "LINE-A01",
    LineName = "Assembly Line 1",
    TargetQuantity = 500
};
// line.LineCode = "LINE-B01"; // Error: init-only property can only be set during initialization
line.TargetQuantity = 520;     // OK: mutable property

Console.WriteLine($"Line: {line.LineCode}, Target Qty: {line.TargetQuantity}");


// 2. Nullable 참조 형식 (C# 8.0+) & 널 연산자
Console.WriteLine("\n[2. Nullable Reference Types & Operators]");
string? operatorName = null; // ? 붙은 경우 null 허용

// (1) 널 병합 연산자 (??) : null일 경우 우측 기본값 적용
string displayName = operatorName ?? "Unassigned Operator";
Console.WriteLine($"Operator: {displayName}");

// (2) 널 조건부 연산자 (?.) : NPE 방지
int? nameLength = operatorName?.Length;
Console.WriteLine($"Operator Name Length: {nameLength?.ToString() ?? "null"}");


// 3. record 와 with 비파괴 복사식
// Java 14+ Record와 유사하며, with 키워드를 통한 불변 객체 부분 복사 생성 지원
Console.WriteLine("\n[3. record & Non-destructive Mutation (with)]");
var originalOrder = new WorkOrder("WO-20261030-001", "Cylinder Block", 100, "WAITING");
Console.WriteLine($"Original: {originalOrder}");

var processingOrder = originalOrder with { Status = "PROCESSING" };
Console.WriteLine($"Updated : {processingOrder}");
Console.WriteLine($"Reference Equals: {ReferenceEquals(originalOrder, processingOrder)}");


// 4. 패턴 매칭 switch 식 (Pattern Matching Switch Expression)
Console.WriteLine("\n[4. Switch Expression & Pattern Matching]");
int machineTemperature = 85;

string alertMessage = machineTemperature switch
{
    < 40 => "Normal (Cooling)",
    >= 40 and <= 75 => "Normal (Operating Range)",
    > 75 and <= 90 => "Warning: Overheating Check Required",
    _ => "Critical: Emergency Shutdown Required"
};
Console.WriteLine($"Temp: {machineTemperature}C -> Result: {alertMessage}");


// 5. out 매개변수 (TryParse 패턴)
Console.WriteLine("\n[5. out Parameter (TryParse Pattern)]");
string sensorRawInput = "1024";

if (int.TryParse(sensorRawInput, out int parsedValue))
{
    Console.WriteLine($"Parsed Success: {parsedValue} (Type: {parsedValue.GetType().Name})");
}
else
{
    Console.WriteLine("Parse Failed: Invalid format");
}


// --- 모델 정의 ---

public class ProductionLine
{
    public required string LineCode { get; init; } 
    public string LineName { get; set; } = string.Empty;
    public int TargetQuantity { get; set; }
}

public record WorkOrder(string OrderId, string ProductName, int Quantity, string Status);
