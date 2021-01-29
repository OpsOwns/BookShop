﻿using Shop.Auth.API.Contracts.V1.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Shop.Auth.API.Contracts.V1.Sample
{
    public class TokenSample : IExamplesProvider<RefreshTokenRequest>
    {
        public RefreshTokenRequest GetExamples() => new RefreshTokenRequest("qIOkUvBA2N6FkVATtoWmRxlwDozpB8dbGjYrjqwcGxU=", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKc29uIiwianRpIjoiZGRkZmFhYzgtYTVjNS00ZjVmLWIxOTgtZTNjZDc1NGRkNDM2IiwiaWF0IjoxNjExNDMxNjQ2LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2NvdW50cnkiOiJQb2xza2EiLCJuYmYiOjE2MTE0MzE2NDUsImV4cCI6MTYxMTQzODg0NSwiaXNzIjoid2ViQXBpIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyJ9.1j-iFXr-WKnFsSWSJP7xd5Ft_iyV1CR37keUOgugcAM");
    }
}
