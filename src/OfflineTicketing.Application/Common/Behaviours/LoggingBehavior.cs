using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTicketing.Application.Common.Behaviours
{

 
         public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        {
            private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

            public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
            {
                _logger = logger;
            }

            public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogInformation("Handling {RequestName} with payload: {@Request}", requestName, request);

                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var response = await next();
                    stopwatch.Stop();

                    _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms with response: {@Response}", requestName, stopwatch.ElapsedMilliseconds, response);

                    return response;
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Error handling {RequestName} after {ElapsedMilliseconds}ms", requestName, stopwatch.ElapsedMilliseconds);
                    throw; // اجازه می‌دهیم Behavior بعدی یا Middleware خطا را مدیریت کند
                }
            }
        }
    }
    

