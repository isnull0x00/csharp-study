// 04_Database_MSSQL_and_ORM
// Entity Framework Core (EF Core) 기반 데이터 액세스

using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== [04] Entity Framework Core & Data Access ===");

await using var db = new FactoryDbContext();
await db.Database.EnsureCreatedAsync(); // 로컬 SQLite 스키마 생성

// 1. 엔티티 등록 (INSERT)
Console.WriteLine("\n[1. Insert Initial Equipment Data]");
if (!await db.Equipments.AnyAsync())
{
    db.Equipments.AddRange(
        new Equipment { EquipmentCode = "EQ-001", Name = "Servo Press 500T", Line = "Line-1", Status = "RUNNING", OperatingHours = 1240 },
        new Equipment { EquipmentCode = "EQ-002", Name = "Welding Robot 6-Axis", Line = "Line-1", Status = "IDLE", OperatingHours = 890 },
        new Equipment { EquipmentCode = "EQ-003", Name = "Paint Booth Sprayer", Line = "Line-2", Status = "MAINTENANCE", OperatingHours = 2400 },
        new Equipment { EquipmentCode = "EQ-004", Name = "Vision Sorting System", Line = "Line-2", Status = "RUNNING", OperatingHours = 450 }
    );
    await db.SaveChangesAsync(); // Java JPA entityManager.flush() 대응
    Console.WriteLine("  Initial 4 equipment records inserted.");
}

// 2. LINQ to Entities 조회 (SELECT with WHERE & ORDER BY)
Console.WriteLine("\n[2. Query Running Equipments (LINQ to Entities)]");
var runningEquipments = await db.Equipments
    .Where(e => e.Status == "RUNNING")
    .OrderByDescending(e => e.OperatingHours)
    .ToListAsync();

foreach (var eq in runningEquipments)
{
    Console.WriteLine($"  - [{eq.EquipmentCode}] {eq.Name,-22} | Line: {eq.Line} | Hours: {eq.OperatingHours}h | Status: {eq.Status}");
}

// 3. 엔티티 수정 (UPDATE)
Console.WriteLine("\n[3. Update Equipment Status]");
var maintTarget = await db.Equipments.FirstOrDefaultAsync(e => e.EquipmentCode == "EQ-003");
if (maintTarget != null)
{
    Console.WriteLine($"  Before: {maintTarget.Name} -> {maintTarget.Status}");
    maintTarget.Status = "RUNNING"; // 상태 변경 (더티 체킹에 의해 자동 감지)
    await db.SaveChangesAsync();
    Console.WriteLine($"  After : {maintTarget.Name} -> {maintTarget.Status}");
}


// --- 엔티티 및 DbContext ---

public class Equipment
{
    public int Id { get; set; }
    public required string EquipmentCode { get; set; }
    public required string Name { get; set; }
    public required string Line { get; set; }
    public required string Status { get; set; } // RUNNING, IDLE, MAINTENANCE
    public int OperatingHours { get; set; }
}

public class FactoryDbContext : DbContext
{
    public DbSet<Equipment> Equipments => Set<Equipment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 로컬 실행용 SQLite
        optionsBuilder.UseSqlite("Data Source=factory.db");

        // MSSQL 설정 예시:
        // optionsBuilder.UseSqlServer("Server=localhost;Database=FactoryDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;");
    }
}
