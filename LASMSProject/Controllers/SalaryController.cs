//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using LASMSProject.Data;
//using LASMSProject.Models;

//namespace LASMSProject.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class SalaryController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SalaryController(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public IActionResult SetSalary()
//        {
//            ViewBag.Employees = new SelectList(_context.Users, "Id", "FullName");
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> SetSalary(SalaryStructure model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Check if salary already exists, update it
//                var existing = await _context.SalaryStructures
//                    .FirstOrDefaultAsync(s => s.EmployeeId == model.EmployeeId);

//                if (existing != null)
//                {
//                    existing.BasicSalary = model.BasicSalary;
//                    existing.HRA = model.HRA;
//                    existing.DA = model.DA;
//                }
//                else
//                {
//                    _context.SalaryStructures.Add(model);
//                }

//                await _context.SaveChangesAsync();
//                return RedirectToAction("Index", "Admin");
//            }
//            return View(model);
//        }

//        [HttpGet]  //GENERATE SALARY (The Core Logic)
//        public IActionResult ProcessSalary()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> ProcessSalary(int month, int year)
//        {
//            // 1. Get Employees jinki Salary Structure Set hai
//            var employees = await _context.SalaryStructures
//                                          .Include(s => s.Employee)
//                                          .ToListAsync();

//            if (!employees.Any())
//            {
//                ViewBag.Message = "Error : 'Set Salary'";
//                return View();
//            }

//            int processedCount = 0;
//            int skippedCount = 0;

//            foreach (var empSalary in employees)
//            {
//                // 2. Check agar Salary pehle se ban chuki hai
//                bool alreadyExists = await _context.MonthlySalaries.AnyAsync(m =>
//                    m.EmployeeId == empSalary.EmployeeId && m.Month == month && m.Year == year);

//                if (alreadyExists)
//                {
//                    skippedCount++;
//                    continue;
//                }

//                // 3. Unpaid Leaves Calculate  (Approved Only)
//                // Ensure LeaveType.IsPaid == false (Loss of Pay)
//                var unpaidLeaves = await _context.LeaveRequests
//                    .Include(l => l.LeaveType)
//                    .Where(l => l.EmployeeId == empSalary.EmployeeId
//                             && l.Status == "Approved"
//                             && l.LeaveType.IsPaid == false
//                             && l.StartDate.Month == month
//                             && l.StartDate.Year == year)
//                    .ToListAsync();

//                int unpaidDays = unpaidLeaves.Sum(l => l.TotalDays);

//                decimal perDaySalary = empSalary.BasicSalary / 30;
//                decimal deduction = perDaySalary * unpaidDays;
//                decimal netSalary = (empSalary.BasicSalary + empSalary.HRA + empSalary.DA) - deduction;

              
//                var salarySlip = new MonthlySalary
//                {
//                    EmployeeId = empSalary.EmployeeId,
//                    Month = month,
//                    Year = year,
//                    BasicSalary = empSalary.BasicSalary,
//                    TotalAllowances = empSalary.HRA + empSalary.DA,
//                    TotalUnpaidDays = unpaidDays,
//                    LOPDeduction = deduction,
//                    NetSalary = netSalary,
//                    ProcessedDate = DateTime.Now
//                };

//                _context.MonthlySalaries.Add(salarySlip);
//                processedCount++;
//            }

//            //  Save to Database
//            if (processedCount > 0)
//            {
//                await _context.SaveChangesAsync();
//                ViewBag.Message = $"Success! {processedCount} employees ki salary process ho gayi. ({skippedCount} already exist thi).";
//            }
//            else
//            {
//                ViewBag.Message = $"No changes. Sabhi {skippedCount} employees ki salary pehle se bani hui hai.";
//            }

//            return View();
//        }
//    }
//}
