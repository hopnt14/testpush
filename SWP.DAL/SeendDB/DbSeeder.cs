using Microsoft.Extensions.DependencyInjection;
using SWP.DAL.Models;

public static class DbSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<HrmSystemContext>();

        // ===============================
        // 1. DEPARTMENTS
        // ===============================
        if (!context.Departments.Any(d => d.DepartmentId == "HR"))
        {
            context.Departments.Add(new Department { DepartmentId = "HR", DepartmentName = "Human Resources" });
        }

        if (!context.Departments.Any(d => d.DepartmentId == "IT"))
        {
            context.Departments.Add(new Department { DepartmentId = "IT", DepartmentName = "IT Department" });
        }

        if (!context.Departments.Any(d => d.DepartmentId == "FIN"))
        {
            context.Departments.Add(new Department { DepartmentId = "FIN", DepartmentName = "Financial Department" });
        }

        context.SaveChanges();

        // ===============================
        // 2. HR 
        // ===============================
        if (!context.Users.Any(u => u.UserId == "HR01"))
        {
            context.Users.Add(new User
            {
                UserId = "HR01",
                Email = "hr@gmail.com",
                FullName = "HR Admin",
                Role = "HR",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "HR",
                Status = "Active",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(1990, 1, 1)
            });
        }

        // ===============================
        // 3. ACCOUNTANT
        // ===============================
        if (!context.Users.Any(u => u.UserId == "ACC01"))
        {
            context.Users.Add(new User
            {
                UserId = "ACC01",
                Email = "acc@gmail.com",
                FullName = "Accountant Main",
                Role = "Accountant",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "FIN",
                Status = "Active",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(1992, 2, 2)
            });
        }

        // ===============================
        // 4. DEPARTMENT MANAGER
        // ===============================
        if (!context.Users.Any(u => u.UserId == "MGR01"))
        {
            var manager = new User
            {
                UserId = "MGR01",
                Email = "manager@gmail.com",
                FullName = "IT Manager",
                Role = "Manager",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "IT",
                Status = "Active",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(1985, 5, 5)
            };
            context.Users.Add(manager);
            context.SaveChanges();

            // Gán manager cho phòng IT
            var itDept = context.Departments.FirstOrDefault(d => d.DepartmentId == "IT");
            if (itDept != null)
            {
                itDept.ManagerId = manager.UserId;
            }
        }

        // = :==============================
        // 5. EMPLOYEES (ACTIVE)
        // ===============================
        if (!context.Users.Any(u => u.UserId == "EMP01"))
        {
            context.Users.Add(new User
            {
                UserId = "EMP01",
                Email = "emp1@gmail.com",
                FullName = "Employee One",
                Role = "Employee",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "IT",
                Status = "Active",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(2000, 3, 3)
            });
        }

        if (!context.Users.Any(u => u.UserId == "EMP02"))
        {
            context.Users.Add(new User
            {
                UserId = "EMP02",
                Email = "emp2@gmail.com",
                FullName = "Employee Two",
                Role = "Employee",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "IT",
                Status = "Active",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(2001, 4, 4)
            });
        }

        // ===============================
        // 6. EMPLOYEE INACTIVE (TEST LOGIN)
        // ===============================
        if (!context.Users.Any(u => u.UserId == "EMP99"))
        {
            context.Users.Add(new User
            {
                UserId = "EMP99",
                Email = "inactive@gmail.com",
                FullName = "Inactive Employee",
                Role = "Employee",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DepartmentId = "IT",
                Status = "Inactive",
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = new DateOnly(2002, 6, 6)
            });
        }

        context.SaveChanges();
    }
}
