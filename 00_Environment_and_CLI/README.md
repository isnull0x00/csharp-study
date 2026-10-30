# 00. Environment & CLI

## 학습 요약
* .NET SDK 기본 빌드/실행 파이프라인 이해
* C# 9.0+ 최상위 문(Top-level Statements) 동작 방식 확인
* CLI 기반 프로젝트 라이프사이클 명령어 정리

## 주요 CLI 명령어

| 명령어 | 설명 | Java/Gradle 대응 |
| :--- | :--- | :--- |
| `dotnet new console` | 콘솔 프로젝트 템플릿 생성 | Spring Initializr |
| `dotnet build` | 프로젝트 컴파일 (IL 바이트코드 생성) | `gradle build` |
| `dotnet run` | 빌드 및 즉시 실행 | `gradle bootRun` |
| `dotnet clean` | 빌드 결과물(`bin`, `obj`) 정리 | `gradle clean` |
| `dotnet add package <PackageName>` | NuGet 의존성 추가 | `dependencies { implementation ... }` |

## 실행
```powershell
dotnet run
dotnet run -- Ulsan Factory MES
```
