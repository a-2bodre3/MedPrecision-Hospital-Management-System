using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Lookups.Queries.GetAppLookups
{
    public record LookupsQuery : IRequest<AppLookupsResponse>;
}
