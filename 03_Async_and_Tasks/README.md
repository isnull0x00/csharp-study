# 03. Async & Tasks

## 학습 내용

C#의 `async / await` 패턴 및 `Task` 기반 비동기 프로그래밍:

| Java | C# | 비고 |
| :--- | :--- | :--- |
| `CompletableFuture<T>` | `Task<T>` | 비동기 반환 객체 |
| `CompletableFuture<Void>` | `Task` | 결과가 없는 비동기 반환 객체 |
| `future.get()` / `join()` | `await task` | 논블로킹 대기 |
| `CompletableFuture.allOf(...)` | `Task.WhenAll(...)` | 다중 비동기 작업 병렬 대기 |

## 규칙 및 패턴
1. **`async void` 지양:** 이벤트 핸들러를 제외하고는 항상 `async Task` 또는 `async Task<T>` 반환
2. **동기 대기 지양:** `task.Result`나 `task.Wait()` 호출 대신 항상 `await` 사용 (데드락 방지)
3. **네이밍:** 비동기 메서드는 접미사로 `Async`를 권장 (예: `FetchDataAsync`)

## 실행
```powershell
dotnet run
```
