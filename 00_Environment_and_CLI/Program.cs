// 00_Environment_and_CLI
// .NET 런타임 환경 진단 및 최상위 문(Top-level Statements) 테스트

Console.WriteLine("=== [00] .NET Runtime & Environment Diagnostics ===");

// 1. 문자열 보간 (String Interpolation: $ 접두사)
string targetStack = "C# / .NET 8 Backend";
Console.WriteLine($"Target Stack: {targetStack}");

// 2. 런타임 및 OS 환경 정보
Console.WriteLine("\n[Environment Information]");
Console.WriteLine($"- .NET Version     : {Environment.Version}");
Console.WriteLine($"- OS Platform      : {Environment.OSVersion}");
Console.WriteLine($"- 64-bit Process   : {Environment.Is64BitProcess}");
Console.WriteLine($"- Processor Count  : {Environment.ProcessorCount}");
Console.WriteLine($"- Current Directory: {Environment.CurrentDirectory}");

// 3. 커맨드라인 인자 (args) 처리
// 최상위 문 환경에서도 컴파일러가 암묵적으로 `args` 배열을 지원함
Console.WriteLine($"\n[Arguments Count]: {args.Length}");
if (args.Length > 0)
{
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"  args[{i}] = {args[i]}");
    }
}
else
{
    Console.WriteLine("  No arguments passed. (Run via 'dotnet run -- arg1 arg2')");
}
