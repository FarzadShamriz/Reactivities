using System;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{

    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = false;
            var activity = appDbContext.Activities.Find(request.Id);
            if (activity is null) return Result<Unit>.Failure("Activity Not Found!", 404);
            try
            {
                appDbContext.Remove(activity);
                result = await appDbContext.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(ex.Message,500);
            }
            if(!result) return Result<Unit>.Failure("Failed To Delete Activity!", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
