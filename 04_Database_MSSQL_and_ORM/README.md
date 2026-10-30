# 04. Database & ORM (MSSQL / EF Core / Dapper)

## 학습 내용

1. **JPA vs EF Core vs Dapper**
   - **Entity Framework Core:** Java Spring Data JPA(Hibernate)와 유사한 객체 중심 ORM. LINQ to Entities 지원.
   - **Dapper:** MyBatis와 유사한 초경량 Micro-ORM. 원시 SQL 및 Stored Procedure 매핑 속도가 빨라 대용량/제조업 환경에서 자주 사용.

2. **T-SQL (MSSQL) 문법 주요 차이**
   ```sql
   -- Top N
   SELECT TOP (5) * FROM Equipment;

   -- Paging (ORDER BY 필수)
   SELECT * FROM Equipment 
   ORDER BY Id 
   OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;

   -- Stored Procedure
   CREATE PROCEDURE dbo.sp_UpdateEquipmentStatus
       @EquipmentCode NVARCHAR(50),
       @NewStatus NVARCHAR(20)
   AS
   BEGIN
       SET NOCOUNT ON;
       UPDATE Equipment
       SET Status = @NewStatus
       WHERE EquipmentCode = @EquipmentCode;
   END
   GO
   ```

## 실행
```powershell
dotnet run
```
