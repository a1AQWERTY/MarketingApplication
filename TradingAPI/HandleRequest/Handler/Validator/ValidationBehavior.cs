using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Trading.Exception;
using TradingAPI.Extensions;

namespace TradingAPI.HandleRequest.Handler.Validator
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            var response = new List<CustomExceptionModel>();

            if (failures?.Any() == true)
            {
                response = failures.Select(x => new CustomExceptionModel() { Message = x.ErrorMessage,Code=(int)HttpStatusCode.PreconditionFailed })?.ToList();
                throw new CustomException(response)
                {
                    statusCode = System.Net.HttpStatusCode.PreconditionFailed
                };

            }

            return await next();




        }


    }
}
