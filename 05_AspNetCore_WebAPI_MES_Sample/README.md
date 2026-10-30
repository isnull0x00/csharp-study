# 05. ASP.NET Core Web API (MES WorkOrder Sample)

## 학습 내용

Spring Boot 구조와 ASP.NET Core REST API 구조 간의 매핑 및 실습:

| 역할 | Spring Boot | ASP.NET Core |
| :--- | :--- | :--- |
| **진입점** | `@SpringBootApplication` | `WebApplication.CreateBuilder(args)` |
| **DI 등록** | `@Service`, `@Component` | `builder.Services.AddScoped<I..., ...>()` |
| **컨트롤러** | `@RestController` | `[ApiController] public class ...Controller : ControllerBase` |
| **라우팅** | `@RequestMapping("/api/...")` | `[Route("api/[controller]")]` |
| **HTTP 메서드** | `@GetMapping`, `@PostMapping` | `[HttpGet]`, `[HttpPost]`, `[HttpPatch]` |
| **문서화** | `springdoc-openapi` | `Swashbuckle.AspNetCore` (Swagger) |

## 엔드포인트
* `GET /api/workorders` : 작업 지시 목록 조회 (Status 쿼리 필터 지원)
* `GET /api/workorders/{id}` : 작업 지시 상세 조회
* `POST /api/workorders` : 신규 작업 지시 등록
* `PATCH /api/workorders/{id}/status` : 작업 지시 상태 변경

## 실행
```powershell
dotnet run
```
실행 후 브라우저에서 `http://localhost:5000` 접속 시 Swagger UI를 통해 API 테스트 가능.
