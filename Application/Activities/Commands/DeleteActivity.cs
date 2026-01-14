using System;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    
    public class Command : IRequest
    {
        public string Id { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity =  appDbContext.Activities.Find(request.Id)?? throw new Exception("Activity not found!");
            appDbContext.Remove(activity);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
