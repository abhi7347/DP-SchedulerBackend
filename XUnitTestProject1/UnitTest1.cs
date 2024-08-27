using DPscheduler.Presentation.Controllers;
using DPScheduler.BAL.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        private readonly IEventOperationsService _service;
        private readonly EventOperationsController _testing;
        public UnitTest1(IEventOperationsService eventServiceOps)
        {
            _service = eventServiceOps;
            _testing = new EventOperationsController(_service);
        }

        [Fact]
        public void BookedAppointmentTesting()
        {
            // Arrange
            DateTime SelectedDate = DateTime.Today;
            IEnumerable<int> LocationIds = new List<int> { 1, 2};

            // Act
            var result = _testing.GetBookedAppointments(SelectedDate, LocationIds);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}