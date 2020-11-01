using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.User
{
    public class CurrentUser
    {
        public class Query : IRequest<User> { }

        public class Handler : IRequestHandler<Query, User>
        {

            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenetrator;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenetrator,
            IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _jwtGenetrator = jwtGenetrator;
                _userManager = userManager;

            }
            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            { 
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUserName());

                return new User
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Token = _jwtGenetrator.CreateToken(user),
                    Image = null
                };
            }
        }
    }
}