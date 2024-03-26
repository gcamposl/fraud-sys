using System.Net;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Services
{
    public class ResultService
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ICollection<ErrorValidator> Errors { get; set; }

        public static ResultService Ok(string message)
            => new ResultService { IsSuccess = true, Message = message };
        public static ResultService<T> Ok<T>(T data)
            => new ResultService<T> { IsSuccess = true, Data = data };
        public static ResultService Fail(string message)
            => new ResultService { IsSuccess = false, Message = message };
        public static ResultService<T> Fail<T>(string message)
            => new ResultService<T> { IsSuccess = false, Message = message };
        public static ResultService RequestError(string message, ValidationResult validationResult)
            => new ResultService
            {
                IsSuccess = false,
                Message = message,
                Errors = validationResult.Errors
                    .Select(x => new ErrorValidator
                    {
                        Field = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                    .ToList()
            };
        public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
            => new ResultService<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = validationResult.Errors
                    .Select(x => new ErrorValidator
                    {
                        Field = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                    .ToList()
            };
    }

    public class ResultService<T> : ResultService
    {
        public T Data { get; set; }
    }
}