using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace CoreySutton.Xrm.Utilities
{
    public static partial class IOrganizationServiceExtensions
    {
        public static Tuple<int, List<OrganizationResponse>> ExecuteMultiple(
            this IOrganizationService service,
            List<OrganizationRequest> requests)
        {
            var totalRequestsExecuted = 0;
            var batchesExecuted = 0;
            var organizationResponses = new List<OrganizationResponse>();

            // Create an ExecuteMultipleRequest object.
            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                // Assign settings that define execution behavior: continue on error, return responses. 
                Settings = new ExecuteMultipleSettings
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            foreach (var request in requests)
            {
                // Add the request
                executeMultipleRequest.Requests.Add(request);

                // If the amount of requests is EXECUTE_MULTIPLE_BATCH_SIZE then execute the request
                if (executeMultipleRequest.Requests.Count == Constants.ExecuteMultipleBatchSize)
                {
                    totalRequestsExecuted += executeMultipleRequest.Requests.Count;
                    batchesExecuted++;

                    Console.WriteLine($"ExecuteMutliple batch {batchesExecuted} (count = {executeMultipleRequest.Requests.Count}, totalRequestsExecuted = {totalRequestsExecuted})");

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var executeMultipleResponse = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;
                    stopwatch.Stop();
                    Console.WriteLine($"Complete {stopwatch.ElapsedMilliseconds} ms");

                    var responses = executeMultipleResponse.Responses.Select(r => r.Response);
                    organizationResponses.AddRange(responses);

                    executeMultipleRequest.Requests.Clear();
                }
            }

            if (executeMultipleRequest.Requests.Count > 0)
            {                
                totalRequestsExecuted += executeMultipleRequest.Requests.Count;
                batchesExecuted++;

                Console.WriteLine($"ExecuteMutliple batch {batchesExecuted} (count = {executeMultipleRequest.Requests.Count}, totalRequestsExecuted = {totalRequestsExecuted})");

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var executeMultipleResponse = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;
                stopwatch.Stop();
                Console.WriteLine($"Complete {stopwatch.ElapsedMilliseconds} ms");

                var responses = executeMultipleResponse.Responses.Select(r => r.Response);
                organizationResponses.AddRange(responses);
            }

            Console.WriteLine($"ExecuteMutliple complete. (totalRequestsExecuted = {totalRequestsExecuted}, batchesExecuted = {batchesExecuted})");
            return new Tuple<int,List<OrganizationResponse>>(totalRequestsExecuted, organizationResponses);
        }
    }
}