using System;
using System.Collections;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationExceptions : Exception
    {
        public ValidationExceptions() : base("Se ha producido uno o mas errores de validacion")
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; }

        public ValidationExceptions(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }    
        } 
        
    }
}