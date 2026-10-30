# 02. LINQ & Collections

## 학습 내용

Java Stream API와 C# LINQ 간의 함수형 컬렉션 처리 방식 비교 및 실습:

| Java Stream API | C# LINQ | 설명 |
| :--- | :--- | :--- |
| `.filter(...)` | `.Where(...)` | 조건 필터링 |
| `.map(...)` | `.Select(...)` | 프로젝션(변환) |
| `.sorted(...)` | `.OrderBy(...)` / `.OrderByDescending(...)` | 정렬 |
| `.findFirst().orElse(null)` | `.FirstOrDefault(...)` | 단일 요소 검색 |
| `.anyMatch(...)` | `.Any(...)` | 조건 일치 여부 확인 |
| `.allMatch(...)` | `.All(...)` | 전체 일치 여부 확인 |
| `.collect(Collectors.toList())` | `.ToList()` | 리스트 수집 (즉시 실행) |
| `.collect(Collectors.groupingBy(...))` | `.GroupBy(...)` | 그룹화 |

## 주의점
* LINQ 메서드(`Where`, `Select`)는 지연 실행(Deferred Execution)됩니다.
* 실제 데이터 순회나 `ToList()`, `Count()`, `First()` 등의 연산자가 호출될 때 쿼리가 평가됩니다.

## 실행
```powershell
dotnet run
```
