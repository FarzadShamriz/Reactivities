using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = appDbContext.Activities.Find(request.Activity.Id)
            ?? throw new Exception("Cannot find activity!");
            mapper.Map(request.Activity,activity);
            appDbContext.Activities.Update(activity);
            await appDbContext.SaveChangesAsync();
        }
    }


}
