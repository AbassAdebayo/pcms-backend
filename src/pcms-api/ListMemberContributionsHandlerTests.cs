using Application.Queries.Contribution.ListMemberContributionsQuery;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class ListMemberContributionsHandlerTests
{
    private readonly Mock<IContributionRepository> _mockContributionRepository;
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly Mock<ILogger<ListMemberContributionsHandler>> _mockLogger;
    private readonly ListMemberContributionsHandler _handler;

    public ListMemberContributionsHandlerTests()
    {
        _mockContributionRepository = new Mock<IContributionRepository>();
        _mockMemberRepository = new Mock<IMemberRepository>();
        _mockLogger = new Mock<ILogger<ListMemberContributionsHandler>>();
        _handler = new ListMemberContributionsHandler(_mockContributionRepository.Object, _mockLogger.Object, _mockMemberRepository.Object);
    }

    [Fact]
    public async Task Handle_MemberNotFound_ReturnsFailResult()
    {
        // Arrange
        var request = new ListMemberContributionsQuery { memberId = Guid.NewGuid(), page = 1, pageSize = 10 };
        _mockMemberRepository.Setup(repo => repo.GetByIdAsync(request.memberId)).ReturnsAsync((Member)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains($"Member with ID {request.memberId} not found", result.Messages);
    }

    [Fact]
    public async Task Handle_ContributionsNotFound_ReturnsFailResult()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var request = new ListMemberContributionsQuery { memberId = memberId, page = 1, pageSize = 10 };
        var member = new Member { Id = memberId, Name = "Test Member" };
        _mockMemberRepository.Setup(repo => repo.GetByIdAsync(request.memberId)).ReturnsAsync(member);
        _mockContributionRepository.Setup(repo => repo.ListAsync(request.memberId, request.page, request.pageSize)).ReturnsAsync((PaginatedResult<Contribution>)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Contains($"Contributions for member with name {member.Name} not found", result.Messages);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var request = new ListMemberContributionsQuery { memberId = memberId, page = 1, pageSize = 10 };
        var member = new Member { Id = memberId, Name = "Test Member" };
        var contributions = new PaginatedResult<Contribution>(new List<Contribution>(), 0, 1, 10);
        _mockMemberRepository.Setup(repo => repo.GetByIdAsync(request.memberId)).ReturnsAsync(member);
        _mockContributionRepository.Setup(repo => repo.ListAsync(request.memberId, request.page, request.pageSize)).ReturnsAsync(contributions);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Succeeded);
        Assert.Equal(contributions, result.Data.Contributions);
        Assert.Contains($"Contributions for {member.Name} fetched", result.Messages);
    }
}
