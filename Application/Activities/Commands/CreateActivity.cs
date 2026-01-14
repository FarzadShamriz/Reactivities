using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class CreateActivity
{

    public class Command : IRequest<string>
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbContext appDbContext) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                await appDbContext.Activities.AddAsync(request.Activity);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Error: {ex.Message}");
            }

            return request.Activity.Id;
        }
    }

}
