# 01. Java to C# Core Syntax

## 학습 내용

1. **자동 구현 프로퍼티 (Properties) & `init`**
   - Java의 `getter`/`setter` 및 Lombok 어노테이션 대체
   - `init` 접근자를 통한 객체 생성 시점 불변 프로퍼티 설정

2. **Nullable 참조 형식 (Nullable Reference Types)**
   - `string?` vs `string` (컴파일 시점 null 안정성 검사)
   - 널 병합 연산자 (`??`), 널 조건부 연산자 (`?.`)

3. **`record` 및 `with` 식**
   - 불변 DTO 정의 및 값 동등성(Value Equality) 기본 제공
   - `with` 식을 통한 비파괴적 필드 복사 및 새 인스턴스 생성

4. **패턴 매칭 `switch` 식 (Switch Expressions)**
   - 문(Statement)이 아닌 식(Expression) 형태의 패턴 분기
   - 관계 연산자(`<`, `>=`), 논리 연산자(`and`, `or`) 조합 지원

5. **`out` 매개변수와 `TryParse`**
   - 예외를 발생시키지 않는 안전한 파싱 패턴

## 실행
```powershell
dotnet run
```
