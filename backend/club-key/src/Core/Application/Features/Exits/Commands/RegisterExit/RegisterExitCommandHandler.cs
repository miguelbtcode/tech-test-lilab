namespace Application.Features.Exits.Commands.RegisterExit;

using Abstractions.Messaging;
using AutoMapper;
using Contracts.Persistence;
using Domain.Abstractions;

public class RegisterExitCommandHandler : ICommandHandler<RegisterExitCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterExitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(RegisterExitCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var isDuplicate = await _unitOfWork.Repository<Exit>().ExistsAsync(x => 
                x.CustomerId == request.CustomerId &&
                x.ExitTime == request.ExitTime &&
                x.IsActive);

            if (isDuplicate)
            {
                return Result.Failure<bool>(EntranceErrors.DuplicatedEntity);
            }
            
            var customer = await _unitOfWork.Repository<Customer>().GetEntityAsync(x => 
                x.Id == request.CustomerId && x.IsActive,
                [p => p.Entrances, p => p.Exits]);

            if (customer == null)
            {
                return Result.Failure<bool>(CustomerErrors.NotFound);
            }
            
            if (!customer.Entrances.Any(x => x.IsActive))
            {
                return Result.Failure<bool>(EntranceErrors.NotExistsEntrance);
            }
            
            // Check if the last customer record was an output without an input
            var lastEntrance = await _unitOfWork.Repository<Entrance>().GetEntityAsync(x =>
                x.CustomerId == request.CustomerId &&
                x.IsLastStatus &&
                x.IsActive);
                
            if (lastEntrance == null)
            {
                return Result.Failure<bool>(ExitErrors.MustBeHaveAnEntrance);
            }

            lastEntrance.IsLastStatus = false;
            await _unitOfWork.Repository<Entrance>().UpdateAsync(lastEntrance);
            
            var exitEntity = _mapper.Map<Exit>(request);
            exitEntity.IsLastStatus = true;
            await _unitOfWork.Repository<Exit>().AddAsync(exitEntity);

            await _unitOfWork.CommitTransactionAsync(transaction);
            return true;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(transaction);
            return Result.Failure<bool>(ExitErrors.Exception(ex.Message));
        }
    }
}