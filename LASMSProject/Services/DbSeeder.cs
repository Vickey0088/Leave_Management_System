using LASMSProject.Data;
using LASMSProject.Models;
using LASMSProject.Data;
using LASMSProject.Models;
using Microsoft.AspNetCore.Identity;

namespace LeaveSalaryMgmt.Services
{
    public static class DbSeeder
    {
      
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Get necessary services from the Dependency Injection 
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            // Create Roles 
            string[] roleNames = { "Admin", "Manager", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //  Create the Default Admin User 
            var adminEmail = "admin@lms.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Vickey",
                    LastName = "Admin",
                    EmployeeCode = "ADMIN001",
                    EmailConfirmed = true,
                    JoiningDate = DateTime.Now
                };

                
                var result = await userManager.CreateAsync(newAdmin, "Admin@123");

                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }

        // 2. Seed Default Leave Types
        public static async Task SeedLeaveTypesAsync(ApplicationDbContext context)
        {
            // Check if LeaveTypes table is empty
            if (!context.LeaveTypes.Any())
            {
                context.LeaveTypes.AddRange(
                    new LeaveType { Name = "Casual Leave", DefaultDays = 12, IsPaid = true },
                    new LeaveType { Name = "Sick Leave", DefaultDays = 12, IsPaid = true },
                    new LeaveType { Name = "Earned Leave", DefaultDays = 15, IsPaid = true },
                    new LeaveType { Name = "Loss of Pay", DefaultDays = 365, IsPaid = false } // Unpaid leave
                );
                await context.SaveChangesAsync();
            }
        }
    }
}