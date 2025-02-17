namespace Application.Features.Entrances.Commands.RegisterEntrance;

using System.Text.Json;
using Abstractions.Messaging;
using AutoMapper;
using Contracts.Persistence;
using Domain.Abstractions;

public class RegisterEntranceCommandHandler : ICommandHandler<RegisterEntranceCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterEntranceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(RegisterEntranceCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var isDuplicate = await _unitOfWork.Repository<Entrance>().ExistsAsync(x => 
                x.CustomerId == request.CustomerId &&
                x.EntranceTime == request.EntranceTime &&
                x.IsActive);

            if (isDuplicate)
            {
                return Result.Failure<bool>(EntranceErrors.DuplicatedEntity);
            }
            
            var customer = await _unitOfWork.Repository<Customer>().GetEntityAsync(
                x => x.Id == request.CustomerId && x.IsActive,
                [p => p.Entrances, p => p.Exits]
            );

            if (customer == null)
            {
                return Result.Failure<bool>(CustomerErrors.NotFound);
            }

            // Check if the last customer record was an input without an output
            var lastExit = await _unitOfWork.Repository<Exit>().GetEntityAsync(x =>
                x.CustomerId == request.CustomerId &&
                x.IsLastStatus &&
                x.IsActive);

            if (customer.Entrances.Any() || customer.Exits.Any())
            {
                if (lastExit == null)
                {
                    return Result.Failure<bool>(EntranceErrors.MustBeHaveAnExit);
                }

                lastExit.IsLastStatus = false;
                await _unitOfWork.Repository<Exit>().UpdateAsync(lastExit);
            }
            
            var entranceEntity = _mapper.Map<Entrance>(request);
            entranceEntity.IsLastStatus = true;
            await _unitOfWork.Repository<Entrance>().AddAsync(entranceEntity);

            await _unitOfWork.CommitTransactionAsync(transaction);
            return true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(transaction);
            return Result.Failure<bool>(EntranceErrors.Exception(ex.Message));
        }
    }
}