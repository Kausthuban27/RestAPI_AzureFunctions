using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Todo_CRUD.Interface;
using Todo_CRUD.Model;
using Todo_CRUD.Service;
using Todo_CRUD.ViewModel;

namespace Todo_CRUD
{
    public class TodoFunction
    {
        private readonly ILogger<TodoFunction> _logger;
        private readonly ITodo _todoEntity;
        public TodoFunction(ILogger<TodoFunction> logger, ITodo todo)
        {
            _todoEntity = todo;
            _logger = logger;
        }

        [Function("GetUserTasks")]
        [OpenApiOperation(operationId: "UserTasks", tags: new[] { "Get tasks" }, Summary = "Gets the user tasks", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Todo), Description = "Tasks Successfully Fetched")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(StatusCodes), Description = "Tasks Not successfull")]
        public async Task<IEnumerable<Todo>> GetUserTasks([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string username = req.Query["username"]!;
            if (string.IsNullOrEmpty(username))
            {
                return [];
            }

            var todoTaskNames = await _todoEntity.GetTodoTasks(username);
            if (todoTaskNames == null)
            {
                return [];
            }

            return todoTaskNames;
        }

        [Function("AddTasks")]
        [OpenApiOperation(operationId: "AddTodoTasks", tags: new[] {"Todo Tasks"}, Summary = "Add New Tasks", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(TodoData), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Task Added Successfully")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Invalid Details")]
        public async Task<HttpResponseData> AddTodoTasks([HttpTrigger(AuthorizationLevel.Anonymous, "post")]  HttpRequestData req)
        {
            var requestBody = await req.ReadFromJsonAsync<TodoData>();
            if (requestBody == null)
            {
                _logger.LogError("Request body is null");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var (statusCode, taskAdded) = await _todoEntity.AddTodoTasks(requestBody);
            _todoEntity.SaveChanges();

            var response = req.CreateResponse(statusCode);
            response.WriteString(taskAdded.ToString());
            return response;
        }

        [Function("UpdateTasks")]
        [OpenApiOperation(operationId: "UpdateTasks", tags: new[] {"UpdateTodoTasks"}, Summary = "Update the user tasks", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType:typeof(Todo), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Task Updated Successfully")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Invalid Details")]
        public async Task<HttpResponseData> UpdateTodoTasks([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req)
        {
            var requestBody = await req.ReadFromJsonAsync<Todo>();
            if(requestBody == null)
            {
                _logger.LogError("Request Body is null");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var (statusCode, todoTask, isUpdated) = await _todoEntity.UpdateTodoTasks(requestBody);
            _todoEntity.SaveChanges();

            var response = req.CreateResponse(statusCode);
            response.WriteString(isUpdated);
            return response;
        }

        [Function("DeleteTask")]
        [OpenApiOperation(operationId: "DeleteTask", tags: new[] {"DeleteTodos"}, Summary = "Delete the Todo Tasks", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Task Deleted Successfully")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Invalid Details")]
        public async Task<HttpResponseData> DeleteTodoTasks([HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequestData req)
        {
            string username = req.Query["username"]!;
            if(username == null)
            {
                _logger.LogError("Username is null");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var (statusCode, isDeleted) = await _todoEntity.DeleteTodoTasks(username);
            _todoEntity.SaveChanges();

            var response = req.CreateResponse(statusCode);
            response.WriteString(isDeleted);
            return response;

        }
    }
}
