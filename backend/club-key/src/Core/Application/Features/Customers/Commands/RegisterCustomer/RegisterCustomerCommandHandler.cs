namespace Application.Features.Customers.Commands.RegisterCustomer;

using Abstractions.Messaging;
using AutoMapper;
using Contracts.Persistence;
using Domain.Abstractions;
using Vms;

public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, CustomerVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CustomerVm>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validateDup = await _unitOfWork.Repository<Customer>()
                .ExistsAsync(x => x.DocumentNumber == request.DocumentNumber && x.IsActive);

            if (validateDup)
            {
                return Result.Failure<CustomerVm>(CustomerErrors.DuplicatedEntity);
            }

            var customerEntity = _mapper.Map<Customer>(request);
            var result = await _unitOfWork.Repository<Customer>().AddAsync(customerEntity);

            return _mapper.Map<CustomerVm>(result);
        }
        catch (Exception e)
        {
            throw new ApplicationException("Failed to register Customer", e);
        }
    }
}