using LASMSProject.Data;
using LASMSProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LASMSProject.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        public async Task<IActionResult> Index()
        {
            // Fetch ALL requests where Status is 'Pending'
            var pendingRequests = await _context.LeaveRequests
                .Include(r => r.Employee) // Join Employee table to get Name
                .Include(r => r.LeaveType) // Join LeaveType table
                .Where(r => r.Status == "Pending")
                .OrderBy(r => r.StartDate)
                .ToListAsync();

            return View(pendingRequests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.LeaveRequests.FindAsync(id);
            if (request == null) return NotFound();

            // Update Status
            request.Status = "Approved";
            request.ApprovedById = _userManager.GetUserId(User); 

            // We need to find the specific balance record for this employee + leave type + year
            var currentYear = request.StartDate.Year;
            var balance = await _context.LeaveBalances
                .FirstOrDefaultAsync(b => b.EmployeeId == request.EmployeeId
                                       && b.LeaveTypeId == request.LeaveTypeId
                                       && b.Year == currentYear);

            if (balance != null)
            {
                balance.Used += request.TotalDays;
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Leave Approved Successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.LeaveRequests.FindAsync(id);
            if (request == null) return NotFound();

            // Just update status, do not touch balance
            request.Status = "Rejected";
            request.ApprovedById = _userManager.GetUserId(User);

            await _context.SaveChangesAsync();
            TempData["Message"] = "Leave Rejected.";

            return RedirectToAction("Index");
        }
    }
}
