using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NguyenVietTuanAnh0204068De1.Exceptions;

namespace NguyenVietTuanAnh0204068De1.Middleware
{
    public class GlobalExceptionMiddleware0204068De1
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware0204068De1(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserFriendlyException0204068De1 ex)
            {
                await WriteErrorResponseAsync(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[ERROR] {ex}");
                await WriteErrorResponseAsync(context, (int)HttpStatusCode.InternalServerError, "Da xay ra loi he thong.");
            }
        }

        private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";

            var errorResponse = new
            {
                statusCode = statusCode,
                message = message,
                success = false
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            var json = JsonSerializer.Serialize(errorResponse, jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
}