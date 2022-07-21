using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace ems_backend.Models
{
    public class WishListResponseModel : WishListRequestModel
    {
        public int WishListId { get; set; }
    }
}