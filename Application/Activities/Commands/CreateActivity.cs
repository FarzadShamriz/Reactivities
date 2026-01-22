using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class CreateActivity
{

    public class Command : IRequest<Result<string>>
    {
        public required CreateActivityDto ActivityDto { get; set; }
    }

    public class Handler(AppDbContext appDbContext, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = mapper.Map<Activity>(request.ActivityDto);
            try
            {
                await appDbContext.Activities.AddAsync(activity);
                var result = await appDbContext.SaveChangesAsync() > 0;
                if(!result) return Result<string>.Failure("Failed To Create Activity!", 400);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure("Faild To Create Activity!", 500);
            }

            return Result<string>.Success(activity.Id);
        }
    }

}
