using DocumentService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Domain.Helper
{
    public class ErrorException : Exception
    {
        public ErrorResponse ErrorResponse { get; }

        public ErrorException(ErrorResponse error)
            : base(error?.Error) // Fix for CS0019 and CS8602
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error)); // Ensure error is not null
            }

            ErrorResponse = new ErrorResponse
            {
                Error = error.Error,
                Details = error.Details
            };
        }
    }

}
