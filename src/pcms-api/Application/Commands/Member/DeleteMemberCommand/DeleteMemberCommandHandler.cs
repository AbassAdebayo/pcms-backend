using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Member.DeleteMemberCommand
{
    public class DeleteMemberCommandHandler : ICommand<DeleteMemberCommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<DeleteMemberCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository, ILogger<DeleteMemberCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<DeleteMemberCommandResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return new DeleteMemberCommandResponse()
                {
                    Message = $"Member with Id {request.id} does not exist"
                };

            }

            member.IsDeleted = true;

            await _memberRepository.UpdateAsync(request.id, member);
            await _unitOfWork.SaveChangesAsync();

            return new DeleteMemberCommandResponse()
            {
                Message = $"Member with Id {request.id} deleted successfully"
            };
        }
    }
}
