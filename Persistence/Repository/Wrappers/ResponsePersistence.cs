using System.Collections.Generic;

namespace Application.Wrappers
{
    public class ResponsePersistence<T>
    {
        public ResponsePersistence()
        {
            
        }

        public ResponsePersistence(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponsePersistence(string message)
        {
            Succeeded = false;
            Message = message;

        }
        
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}