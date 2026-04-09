using LASMSProject.Data;
using LASMSProject.Models;
using LASMSProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LASMSProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalEmployees = await _userManager.Users.CountAsync();
            ViewBag.TotalDepartments = await _context.Departments.CountAsync();
            ViewBag.PendingRequests = await _context.LeaveRequests.CountAsync(r => r.Status == "Pending");

            return View();
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Designations = new SelectList(_context.Designations, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmployeeCode = model.EmployeeCode,
                    JoiningDate = model.JoiningDate,
                    DepartmentId = model.DepartmentId,
                    DesignationId = model.DesignationId,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Assign Role
                    await _userManager.AddToRoleAsync(user, "Employee");

                    // AUTO-ASSIGN LEAVE BALANCE 
                    int currentYear = DateTime.Now.Year;
                    var leaveTypes = _context.LeaveTypes.ToList();

                    foreach (var type in leaveTypes)
                    {
                        if (type.IsPaid) // Give balance only for Paid leaves
                        {
                            //_context.LeaveBalances.Add(new LeaveBalance
                            //{
                            //    EmployeeId = user.Id,
                            //    LeaveTypeId = type.Id,
                            //    Year = currentYear,
                            //    TotalAllocated = type.DefaultDays,
                            //    Used = 0
                            //});

                            int daysToAssign = (type.Name == "Loss of Pay") ? 365 : type.DefaultDays;

                            _context.LeaveBalances.Add(new LeaveBalance
                            {
                                EmployeeId = user.Id,
                                LeaveTypeId = type.Id, // Yahan ensure karo 'Id' use ho raha hai
                                Year = currentYear,
                                TotalAllocated = daysToAssign,
                                Used = 0
                            });
                        }
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Designations = new SelectList(_context.Designations, "Id", "Name");
            return View(model);
        }
    }
}
