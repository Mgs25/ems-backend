using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace ems_backend.Models
{
    public class EnrollmentRequestModel
    {
        public int UserID { get; set; }
        public int EventID { get; set; }
    }
}