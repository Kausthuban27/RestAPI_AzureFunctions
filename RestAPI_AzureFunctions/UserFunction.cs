using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RestAPI_AzureFunctions.Interface;
using RestAPI_AzureFunctions.Service;
using RestAPI_AzureFunctions.ViewModel;
using System.Net;

namespace RestAPI_AzureFunctions
{
    public class UserFunction
    {
        private readonly ILogger<UserFunction> _logger;
        private readonly IUser _userEntity;

        public UserFunction(IUser userEntity, ILogger<UserFunction> logger)
        {
            _userEntity = userEntity;
            _logger = logger;
        }

        [Function("GetUser")]
        [OpenApiOperation(operationId: "LoginUser", tags: new[] { "User Login" }, Summary = "Gets the User", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "username", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "password", In = ParameterLocation.Query, Required = true, Type = typeof(string), Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(bool), Description = "User Do Exist")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(bool), Description = "User Doesn't Exist")]

        public async Task<HttpResponseData> LoginUser([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string username = req.Query["username"]!;
            string password = req.Query["password"]!;

            var (statusCode, userExists) = await _userEntity.GetUser(username, password);

            var response = req.CreateResponse(statusCode);
            response.WriteString(userExists.ToString());
            return response;
        }

        [Function("AddUser")]
        [OpenApiOperation(operationId: "RegisterUser", tags: new[] { "User Registration" }, Summary = "Adds the User", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserData), Required = true, Description = "User data")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(bool), Description = "User Added Successfully")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(bool), Description = "Invalid Details")]

        public async Task<HttpResponseData> RegisterUser([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await req.ReadFromJsonAsync<UserData>();
            if (requestBody == null)
            {
                _logger.LogError("Request body is null");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var (statusCode, userAdded) = await _userEntity.AddUsers(requestBody);
            _userEntity.SaveChanges();

            var response = req.CreateResponse(statusCode);
            response.WriteString(userAdded.ToString());
            return response;
        }
    }
}
