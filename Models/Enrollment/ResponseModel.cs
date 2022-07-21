using System.ComponentModel.DataAnnotations;

namespace ems_backend.Models
{
    public class EnrollmentResponseModel : EnrollmentRequestModel
    {
        public int EnrollmentId { get; set; }
    }
}