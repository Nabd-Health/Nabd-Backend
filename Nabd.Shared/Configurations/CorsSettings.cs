using System;

namespace Nabd.Shared.Configurations
{
  
    public class CorsSettings
    {
        
        public string PolicyName { get; set; } = "NabdCorsPolicy"; 

     
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

    
        public string[] AllowedMethods { get; set; } = Array.Empty<string>();

        
        public string[] AllowedHeaders { get; set; } = Array.Empty<string>();

        
        public bool AllowCredentials { get; set; } = true;
    }
}