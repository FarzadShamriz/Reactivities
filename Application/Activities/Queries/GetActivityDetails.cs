using System;
using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Result<Activity>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Query, Result<Activity>>
    {
        public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await appDbContext.Activities.FindAsync([request.Id],cancellationToken);

            if (activity == null)
                return Result<Activity>.Failure("Activity Not Found!",404);

            return Result<Activity>.Success(activity);
        }
    }

}
