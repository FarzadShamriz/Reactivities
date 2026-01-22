using System;
using System.Reflection.Metadata.Ecma335;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = appDbContext.Activities.Find(request.Activity.Id);
            if (activity == null) return Result<Unit>.Failure("Activity Not Found!", 404);

            mapper.Map(request.Activity, activity);
            appDbContext.Activities.Update(activity);
            var result = await appDbContext.SaveChangesAsync() > 0;

            if(!result) return Result<Unit>.Failure("Failed To Edit Activity!", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }


}
