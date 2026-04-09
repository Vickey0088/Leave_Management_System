using LASMSProject.Models;

namespace LASMSProject.Models.ViewModels
{
    public class EmployeeDashboardViewModel
    {
        public List<LeaveBalance> Balances { get; set; }
        public List<LeaveRequest> RecentRequests { get; set; }
    }
}