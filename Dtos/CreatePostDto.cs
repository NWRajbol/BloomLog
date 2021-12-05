using System.ComponentModel.DataAnnotations;

namespace BloomLog.Dtos
{
    public record CreatePostDto{

        [Required]
         public string Title{ get; init;}
         [Required]
        public string Content{get; init;}
    }
}