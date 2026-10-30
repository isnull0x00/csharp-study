// 03_Async_and_Tasks
// C# Task 기반 비동기 프로그래밍 (async / await)

Console.WriteLine("=== [03] Asynchronous Programming (Task & async/await) ===");
Console.WriteLine($"[Main Thread ID]: {Environment.CurrentManagedThreadId}");

var stopwatch = System.Diagnostics.Stopwatch.StartNew();

// 1. 비동기 작업 병렬 실행 (Task.WhenAll)
Console.WriteLine("\n[1. Parallel Async Fetching with Task.WhenAll]");

Task<SensorReading> task1 = FetchPlcSensorDataAsync("PLC-PRESS-01", delayMs: 1200);
Task<SensorReading> task2 = FetchPlcSensorDataAsync("PLC-WELD-02",  delayMs: 800);
Task<SensorReading> task3 = FetchPlcSensorDataAsync("PLC-PAINT-03", delayMs: 1500);

SensorReading[] readings = await Task.WhenAll(task1, task2, task3);

stopwatch.Stop();
Console.WriteLine($"[Completed] Total Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");

foreach (var r in readings)
{
    Console.WriteLine($"  - [{r.MachineId}] Temp: {r.Temperature:F1} C | Pressure: {r.Pressure:F1} bar (Thread: {r.ThreadId})");
}

// 2. 비동기 예외 처리 (try-catch)
Console.WriteLine("\n[2. Async Exception Handling]");
try
{
    await SimulateFaultySensorAsync();
}
catch (TimeoutException ex)
{
    Console.WriteLine($"  [Exception Caught]: {ex.Message}");
}


// --- 비동기 메서드 ---

static async Task<SensorReading> FetchPlcSensorDataAsync(string machineId, int delayMs)
{
    // 스레드 블로킹(Thread.Sleep) 대신 논블로킹 Task.Delay 사용
    await Task.Delay(delayMs);

    var random = new Random();
    double temp = 40.0 + random.NextDouble() * 30.0;
    double pressure = 5.0 + random.NextDouble() * 2.0;

    return new SensorReading(
        machineId, 
        temp, 
        pressure, 
        Environment.CurrentManagedThreadId
    );
}

static async Task SimulateFaultySensorAsync()
{
    await Task.Delay(200);
    throw new TimeoutException("Sensor communication timeout (> 200ms)");
}

public record SensorReading(string MachineId, double Temperature, double Pressure, int ThreadId);
