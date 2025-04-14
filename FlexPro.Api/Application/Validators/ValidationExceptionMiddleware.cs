﻿using System.Text.Json;
using FluentValidation;

namespace FlexPro.Api.Application.Validators
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(ValidationException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var errors = ex.Errors.Select(e => new { campo = e.PropertyName, erro = e.ErrorMessage });
                await context.Response.WriteAsync(JsonSerializer.Serialize(new {erros = errors}));
            }
        }
    }
}
