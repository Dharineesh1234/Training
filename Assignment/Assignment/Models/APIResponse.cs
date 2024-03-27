namespace Assignment.Models
{
    public class APIResponse
    {
        public APIResponse() 
        {
            ErrorsMessage=new List<string>();
        }
     public System.Net.HttpStatusCode statusCode {  get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessage { get; set; } 

        public object Result {  get; set; }
    }
}
