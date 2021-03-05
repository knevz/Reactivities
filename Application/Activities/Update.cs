using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

namespace Application.Activities
{
    public class Update
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                
                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}