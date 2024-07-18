using AutoMapper;
using ChatApplication.Services.Chat.Queries.GetByIdChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.BusinessLayer.IntegrationTests.User.Queries.GetByUserIdChatsId
{
    public class GetChatsIdByUserIdQueryResponse
    {
        public uint Id { get; init; }

        private GetChatsIdByUserIdQueryResponse()
        {
            Id = 0;
        }

        private class Mapping : Profile
        {
            public Mapping() => CreateMap<Database.Data.Models.Chat, GetByIdChatQueryResponse>();
        }
    }
}
