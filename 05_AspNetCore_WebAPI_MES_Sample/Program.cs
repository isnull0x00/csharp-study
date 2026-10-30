// 05_AspNetCore_WebAPI_MES_Sample
// ASP.NET Core 기반 스마트팩토리 MES 작업 지시(Work Order) REST API

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. DI 컨테이너 서비스 등록 (Spring: @Service, @Component 대응)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMesWorkOrderService, MesWorkOrderService>();

var app = builder.Build();

// 2. HTTP 요청 파이프라인 (미들웨어)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MES WorkOrder API v1");
        c.RoutePrefix = string.Empty; // 루트 경로 진입 시 Swagger UI 서빙
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


// --- Controllers ---

[ApiController]
[Route("api/[controller]")] // Route: /api/workorders
public class WorkOrdersController : ControllerBase
{
    private readonly IMesWorkOrderService _service;

    // 생성자 주입
    public WorkOrdersController(IMesWorkOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<WorkOrderDto>> GetAll([FromQuery] string? status)
    {
        var result = _service.GetOrders(status);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<WorkOrderDto> GetById(string id)
    {
        var order = _service.GetOrderById(id);
        if (order == null)
        {
            return NotFound(new { message = $"Work order [{id}] not found." });
        }
        return Ok(order);
    }

    [HttpPost]
    public ActionResult<WorkOrderDto> Create([FromBody] CreateWorkOrderRequest request)
    {
        var created = _service.CreateOrder(request);
        return CreatedAtAction(nameof(GetById), new { id = created.OrderId }, created);
    }

    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(string id, [FromQuery] string newStatus)
    {
        bool success = _service.UpdateStatus(id, newStatus);
        if (!success)
        {
            return NotFound(new { message = $"Work order [{id}] not found." });
        }
        return NoContent();
    }
}


// --- Services & DTOs ---

public interface IMesWorkOrderService
{
    IEnumerable<WorkOrderDto> GetOrders(string? statusFilter);
    WorkOrderDto? GetOrderById(string id);
    WorkOrderDto CreateOrder(CreateWorkOrderRequest req);
    bool UpdateStatus(string id, string newStatus);
}

public class MesWorkOrderService : IMesWorkOrderService
{
    private readonly ConcurrentDictionary<string, WorkOrderDto> _store = new();

    public MesWorkOrderService()
    {
        _store.TryAdd("WO-001", new("WO-001", "Bumper Injection Molding", 300, "COMPLETED", DateTime.Now.AddHours(-3)));
        _store.TryAdd("WO-002", new("WO-002", "Door Trim Pressing", 500, "PROCESSING", DateTime.Now.AddHours(-1)));
        _store.TryAdd("WO-003", new("WO-003", "Headlamp Module Assembly", 200, "WAITING", DateTime.Now));
    }

    public IEnumerable<WorkOrderDto> GetOrders(string? statusFilter)
    {
        var query = _store.Values.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(statusFilter))
        {
            query = query.Where(o => o.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase));
        }
        return query.OrderByDescending(o => o.CreatedAt);
    }

    public WorkOrderDto? GetOrderById(string id) => _store.GetValueOrDefault(id);

    public WorkOrderDto CreateOrder(CreateWorkOrderRequest req)
    {
        string newId = $"WO-{DateTime.Now:yyyyMMdd}-{_store.Count + 1:D3}";
        var newOrder = new WorkOrderDto(newId, req.ItemName, req.Quantity, "WAITING", DateTime.Now);
        _store[newId] = newOrder;
        return newOrder;
    }

    public bool UpdateStatus(string id, string newStatus)
    {
        if (_store.TryGetValue(id, out var existing))
        {
            _store[id] = existing with { Status = newStatus.ToUpper() };
            return true;
        }
        return false;
    }
}

public record WorkOrderDto(string OrderId, string ItemName, int Quantity, string Status, DateTime CreatedAt);
public record CreateWorkOrderRequest(string ItemName, int Quantity);
