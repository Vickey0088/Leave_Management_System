using LASMSProject.Data;
using LASMSProject.Models;
using LASMSProject.Models.ViewModels; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Important for Dropdown
using Microsoft.EntityFrameworkCore;

namespace LeaveSalaryMgmt.Controllers
{
    [Authorize(Roles = "Employee")] // Only Employees can access
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var currentYear = DateTime.Now.Year;

            // Fetch Balances for this year
            var balances = await _context.LeaveBalances
                                         .Include(b => b.LeaveType) // Join with LeaveType table
                                         .Where(b => b.EmployeeId == user.Id && b.Year == currentYear)
                                         .ToListAsync();

            // Fetch Recent Requests
            var requests = await _context.LeaveRequests
                                         .Include(r => r.LeaveType)
                                         .Where(r => r.EmployeeId == user.Id)
                                         .OrderByDescending(r => r.AppliedOn)
                                         .Take(5) // Show only last 5
                                         .ToListAsync();

            var viewModel = new EmployeeDashboardViewModel
            {
                Balances = balances,
                RecentRequests = requests
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Apply()
        {
            // Dropdown data for the view
            ViewBag.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Apply(LeaveRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var currentYear = DateTime.Now.Year;

                if (model.EndDate < model.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End Date cannot be earlier than Start Date.");
                }
                else
                {
                    // 1. Leave Type ki details nikalo (Pata karne ke liye ki ye LOP hai ya nahi)
                    var leaveType = await _context.LeaveTypes.FindAsync(model.LeaveTypeId);

                    // 2. User ka Balance nikalo
                    var balance = await _context.LeaveBalances
                        .FirstOrDefaultAsync(b => b.EmployeeId == user.Id
                                                && b.LeaveTypeId == model.LeaveTypeId
                                                && b.Year == currentYear);

                    // 3. Days Calculate karo
                    int daysRequested = (model.EndDate - model.StartDate).Days + 1;

                    // --- MAIN LOGIC FIX ---
                    // Agar leave "Loss of Pay" hai, to Balance check mat karo (Direct allow karo)
                    bool isLOP = leaveType.Name == "Loss of Pay" || !leaveType.IsPaid;

                    // Check: Agar Normal Leave hai AUR Balance kam hai -> Tabhi Error do
                    if (!isLOP && (balance == null || balance.Remaining < daysRequested))
                    {
                        ModelState.AddModelError("", $"Insufficient Balance. You have {balance?.Remaining ?? 0} days remaining.");
                    }
                    else
                    {
                        // C. Create the Request
                        var request = new LeaveRequest
                        {
                            EmployeeId = user.Id,
                            LeaveTypeId = model.LeaveTypeId,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            Reason = model.Reason,
                            Status = "Pending",
                            AppliedOn = DateTime.Now,
                            // Note: LOP ke liye TotalDays store karna zaroori hai
                           // TotalDays = daysRequested
                        };

                        _context.LeaveRequests.Add(request);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
            }

            // Agar koi error hai to Dropdown wapas load karo
            ViewBag.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name");
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Apply(LeaveRequestViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        var currentYear = DateTime.Now.Year;

        //        if (model.EndDate < model.StartDate)
        //        {
        //            ModelState.AddModelError("EndDate", "End Date cannot be earlier than Start Date.");
        //        }
        //        else
        //        {
        //            // B. Check Balance (Business Logic)
        //            var balance = await _context.LeaveBalances
        //                .FirstOrDefaultAsync(b => b.EmployeeId == user.Id
        //                                       && b.LeaveTypeId == model.LeaveTypeId
        //                                       && b.Year == currentYear);

        //            // Calculate days requested
        //            int daysRequested = (model.EndDate - model.StartDate).Days + 1;

        //            if (balance == null || balance.Remaining < daysRequested)
        //            {
        //                ModelState.AddModelError("", $"Insufficient Balance. You have {balance?.Remaining ?? 0} days remaining.");
        //            }
        //            else
        //            {
        //                // C. Create the Request
        //                var request = new LeaveRequest
        //                {
        //                    EmployeeId = user.Id,
        //                    LeaveTypeId = model.LeaveTypeId,
        //                    StartDate = model.StartDate,
        //                    EndDate = model.EndDate,
        //                    Reason = model.Reason,
        //                    Status = "Pending",
        //                    AppliedOn = DateTime.Now
        //                };

        //                _context.LeaveRequests.Add(request);
        //                await _context.SaveChangesAsync();

        //                return RedirectToAction("Index"); 
        //            }
        //        }
        //    }
        //    ViewBag.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name");
        //    return View(model);
        //}

        //  My Payslips
        //public async Task<IActionResult> MyPayslips()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var slips = await _context.MonthlySalaries
        //                              .Where(s => s.EmployeeId == user.Id)
        //                              .OrderByDescending(s => s.Year)
        //                              .ThenByDescending(s => s.Month)
        //                              .ToListAsync();

        //    return View(slips);
        //}
    }
}