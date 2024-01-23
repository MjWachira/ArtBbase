using System.ComponentModel.DataAnnotations;

namespace ArtBbaseFrontend.Models
{
    public class Art
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string Image { get; set; } = string.Empty;

        public int StartBid { get; set; }
    
        public int HigestBid { get; set; }
      
        public int MyBid { get; set; }
     
        public DateTime TimePosted { get; set; }
      
        public DateTime StopTime { get; set; }

    }
}
