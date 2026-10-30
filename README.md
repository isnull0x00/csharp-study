# csharp-study

Java/Spring Boot 및 PostgreSQL/MariaDB 개발 환경 경험을 바탕으로, C# 12 / .NET 8 생태계 전환 및 백엔드 개발을 단계별로 학습하고 정리한 저장소입니다.

언어 기초부터 LINQ 컬렉션 연산, `async/await` 비동기 프로그래밍, Entity Framework Core 기반 데이터 액세스, 그리고 스마트팩토리 MES 도메인을 가정한 ASP.NET Core REST API 구축까지 포함되어 있습니다.

---

## 디렉토리 구조 및 단계별 학습 내용

| 디렉토리 | 주제 | 핵심 내용 |
| :--- | :--- | :--- |
| **00_Environment_and_CLI** | .NET CLI & 런타임 환경 | .NET 8 SDK 환경 확인, CLI 빌드/실행 명령어, 최상위 문(Top-level Statements) |
| **01_Java_to_CSharp_Basics** | Java vs C# 문법 차이점 | 프로퍼티(`get; set;`, `init`), Nullable 참조 형식, `record` 및 `with` 식, 패턴 매칭 |
| **02_LINQ_and_Collections** | LINQ & 컬렉션 연산 | Java Stream API 대응 LINQ(`Where`, `Select`, `OrderBy`, `GroupBy`), 제조 데이터 가공 |
| **03_Async_and_Tasks** | 비동기 프로그래밍 | `Task`, `async/await`, `Task.WhenAll`을 활용한 설비 센서 데이터 병렬 비동기 수집 |
| **04_Database_MSSQL_and_ORM** | MSSQL & EF Core | MSSQL T-SQL 문법, Stored Procedure 개념, EF Core ORM CRUD 및 Dapper 비교 |
| **05_AspNetCore_WebAPI_MES_Sample** | ASP.NET Core Web API | Spring Boot 구조 매핑, DI 컨테이너 구성, 작업 지시(Work Order) REST API & Swagger |

---

## Java/Spring Boot vs C#/.NET Core 개념 매핑

| 항목 | Java / Spring Boot | C# / .NET Core | 비고 |
| :--- | :--- | :--- | :--- |
| **가상 머신 / 런타임** | JVM (Java Virtual Machine) | CLR (Common Language Runtime) | GC 기반 메모리 관리 동일 |
| **프로젝트 / 빌드 설정** | `build.gradle`, `pom.xml` | `*.csproj` (MSBuild) | XML 기반 단일 프로젝트 파일 |
| **패키지 관리** | Gradle / Maven | NuGet (`dotnet add package`) | NuGet Gallery 기반 패키지 복원 |
| **Getter / Setter** | Lombok (`@Getter`, `@Setter`) | 프로퍼티 (`public int Id { get; set; }`) | 언어 레벨에서 프로퍼티 및 `init` 지원 |
| **불변 DTO** | Java 14+ `record` | C# 9+ `record` | C#은 `with` 비파괴 복사 표현식 지원 |
| **함수형 데이터 처리** | `Stream API` (`filter`, `map`, `toList`) | `LINQ` (`Where`, `Select`, `ToList`) | 확장 메서드 기반의 간결한 문법 |
| **비동기 처리** | `CompletableFuture<T>` | `Task<T>`, `async / await` | 언어 차원에서 논블로킹 비동기 흐름 제어 |
| **의존성 주입 (IoC/DI)** | Spring 컨테이너 (`@Autowired`) | `Microsoft.Extensions.DependencyInjection` | 프레임워크 코어에 DI 컨테이너 기본 내장 |
| **REST 컨트롤러** | `@RestController`, `@GetMapping` | `[ApiController]`, `[HttpGet]` | Java Annotation = C# Attribute |
| **ORM / 데이터베이스** | Spring Data JPA, MyBatis | Entity Framework Core, Dapper | 실무 대용량/성능 중심 환경에서 Dapper 다용 |

---

## PostgreSQL / MariaDB vs MSSQL (T-SQL) 주요 차이점

| 항목 | PostgreSQL / MariaDB | MSSQL (T-SQL) |
| :--- | :--- | :--- |
| **문자열 유니코드** | `VARCHAR`, `TEXT` | `NVARCHAR`, `NCHAR` (문자열 리터럴에 `N'...'` 접두사) |
| **조회 건수 제한** | `SELECT * FROM tbl LIMIT 10` | `SELECT TOP (10) * FROM tbl` |
| **페이징 처리** | `LIMIT 10 OFFSET 20` | `ORDER BY col OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY` |
| **Null 대체 함수** | `COALESCE(val, 0)`, `IFNULL(val, 0)` | `ISNULL(val, 0)`, `COALESCE(val, 0)` |
| **저장 프로시저** | `CREATE PROCEDURE ...` | `CREATE PROCEDURE [dbo].[sp_...]` (비즈니스 로직 분리 시 다용) |

---

## 실행 방법

### 요구 사항
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 이상
* Visual Studio Code (C# Dev Kit 권장) 또는 Visual Studio 2022

### 프로젝트 빌드 및 실행
```powershell
# 전체 솔루션 빌드
dotnet build workspace_csharp.sln

# 개별 실습 프로젝트 실행 예시 (01단계)
cd 01_Java_to_CSharp_Basics
dotnet run

# 05단계 Web API 실행
cd 05_AspNetCore_WebAPI_MES_Sample
dotnet run
# 실행 후 브라우저에서 http://localhost:5000 접속 시 Swagger UI 표시
```
