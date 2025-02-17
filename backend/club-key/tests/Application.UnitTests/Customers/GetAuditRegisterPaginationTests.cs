namespace ClubKey.Application.UnitTests.Customers;

using global::Application.Contracts.Persistence;
using global::Application.Features.Customers.Queries.GetAuditRegister;
using global::Application.Features.Customers.Vms;
using global::Application.Specifications.Customers;

using AutoMapper;
using Domain.Abstractions;
using Moq;

public class GetAuditRegisterPaginationTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllCustomerPaginationQueryHandler _handler;

    public GetAuditRegisterPaginationTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllCustomerPaginationQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedCustomers_WhenCustomersExist()
    {
        // Arrange
        var query = new GetAllCustomerPaginationQuery
        {
            CustomerId = 1,
            PageIndex = 1,
            PageSize = 2,
            FromDate = DateTime.UtcNow.AddDays(-7),
            ToDate = DateTime.UtcNow,
        };

        var customers = new List<Customer> { new CustomerMock().Generate(), new CustomerMock().Generate() };
        var customerVms = new List<CustomerVm> { new(), new() };

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().GetAllWithSpec(It.IsAny<CustomerSpecification>()))
            .ReturnsAsync(customers);

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().CountAsync(It.IsAny<CustomerForCountingSpecification>()))
            .ReturnsAsync(5);

        _mapperMock.Setup(m => m.Map<List<CustomerVm>>(customers))
            .Returns(customerVms);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(5, result.Value.Count);
        Assert.Equal(3, result.Value.PageCount);
        Assert.Equal(1, result.Value.PageIndex);
        Assert.Equal(2, result.Value.PageSize);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyPagination_WhenNoCustomersExist()
    {
        // Arrange
        var query = new GetAllCustomerPaginationQuery
        {
            CustomerId = 1,
            PageIndex = 1,
            PageSize = 2
        };

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().GetAllWithSpec(It.IsAny<CustomerSpecification>()))
            .ReturnsAsync(new List<Customer>());

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().CountAsync(It.IsAny<CustomerForCountingSpecification>()))
            .ReturnsAsync(0);

        _mapperMock.Setup(m => m.Map<List<CustomerVm>>(It.IsAny<List<Customer>>()))
            .Returns([]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(0, result.Value.Count);
        Assert.Equal(0, result.Value.PageCount);
        Assert.Equal(0, result.Value.ResultByPage);
    }

    [Fact]
    public async Task Handle_ShouldCalculateTotalPagesCorrectly()
    {
        // Arrange
        var query = new GetAllCustomerPaginationQuery
        {
            CustomerId = 1,
            PageIndex = 1,
            PageSize = 3
        };

        var customers = new List<Customer> { new CustomerMock().Generate(), new CustomerMock().Generate(), new CustomerMock().Generate() };

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().GetAllWithSpec(It.IsAny<CustomerSpecification>()))
            .ReturnsAsync(customers);

        _unitOfWorkMock.Setup(u => u.Repository<Customer>().CountAsync(It.IsAny<CustomerForCountingSpecification>()))
            .ReturnsAsync(8);

        _mapperMock.Setup(m => m.Map<List<CustomerVm>>(customers))
            .Returns([new CustomerVm(), new CustomerVm(), new CustomerVm()]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(8, result.Value.Count);
        Assert.Equal(3, result.Value.PageSize);
        Assert.Equal(3, result.Value.PageCount);
    }
}